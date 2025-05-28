using System.Text;
using Codelabs.BLL.Types;
using Codelabs.Core;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Codelabs.BLL;
public class DockerService
{
    private readonly DockerClient _client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();

    public async Task<(string output, bool success)> RunCodeAsync(ILanguage lang, string code, string? input,
        string desiredOutput, Action<ExecutionStatus> callback)
    {
        callback(ExecutionStatus.Queued);
        var create = await _client.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = lang.ImageName,
            Name = Guid.NewGuid().ToString(),
            WorkingDir = "/usr/src/app",
            Cmd = new[] {"tail", "-f", "/dev/null"},
            Tty = false,
            OpenStdin = true,
            StdinOnce = true,
            HostConfig = new HostConfig
            {
                AutoRemove = true,
                Memory = 100 * 1024 * 1024,
                CPUQuota = 50000,
                NetworkMode = "none"
            }
        });

        var containerID = create.ID;
        await _client.Containers.StartContainerAsync(containerID, new ContainerStartParameters());
        callback(ExecutionStatus.Compiling);

        using var tarStream = new MemoryStream();
        using (var tw = new TarWriter(tarStream))
        {
            tw.AddFile(lang.Filename, Encoding.UTF8.GetBytes(code));
            tw.Finish();
        }
        
        tarStream.Position = 0;
        await _client.Containers.ExtractArchiveToContainerAsync(containerID, new ContainerPathStatParameters
        {
            Path = "/usr/src/app",
            AllowOverwriteDirWithFile = true,
        }, tarStream);
        if (lang.IsCompilable)
        {
            var compileCommand = lang.CompileCommand;
            var execCompile = await _client.Exec.ExecCreateContainerAsync(containerID, new ContainerExecCreateParameters
            {
                Cmd = ["bash", "-c", compileCommand],
                AttachStdout = true,
                AttachStderr = true
            });
            using var compileStream = await _client.Exec.StartAndAttachContainerExecAsync(execCompile.ID, false);
            var buffer = new byte[4096];
            int read;
            while ((read = (await compileStream.ReadOutputAsync(buffer, 0, buffer.Length, CancellationToken.None))
                       .Count) > 0)
            {
                Console.Write(Encoding.UTF8.GetString(buffer, 0, read));
            }
        }

        var runCommand = lang.RunCommand;
        var cmd = new[] { "bash", "-c", $"echo {input ?? ""} | {runCommand}" };
        var execRun = await _client.Exec.ExecCreateContainerAsync(containerID, new ContainerExecCreateParameters
        {
            AttachStdin = true,
            AttachStdout = true,
            AttachStderr = true,
            Tty = false,
            WorkingDir = "/usr/src/app",
            Cmd = cmd
        });
        callback(ExecutionStatus.Executing);
        using var stream = await _client.Exec.StartAndAttachContainerExecAsync(execRun.ID, false);

        using var outMS = new MemoryStream();
        using var errMS = new MemoryStream();
        await stream.CopyOutputToAsync(Stream.Null, outMS, errMS, CancellationToken.None);

        var stdout = Encoding.UTF8.GetString(outMS.ToArray()).Trim();
        var stderr = Encoding.UTF8.GetString(errMS.ToArray()).Trim();

        await _client.Containers.KillContainerAsync(containerID, new ContainerKillParameters());
        var success = stdout == desiredOutput.Trim();

        if (!success && stderr.Length > 0)
        {
            callback(ExecutionStatus.HasErrors);
            return (stderr, false);
        }
        callback(success ? ExecutionStatus.Solved : ExecutionStatus.NotSolved);
        return (stdout, success);
    }
}