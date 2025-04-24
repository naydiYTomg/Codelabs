using System.Text;
using Codelabs.Core;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Codelabs.BLL;

public class DockerService
{
    private readonly DockerClient _client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();

    public async Task<(string output, bool success)> RunCodeAsync(string lang, string code, string? input,
        string desiredOutput, Action<ExecutionStatus> callback)
    {
        callback(ExecutionStatus.Queued);
        var create = await _client.Containers.CreateContainerAsync(new CreateContainerParameters()
        {
            Image = GetImageName(lang),
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
            tw.AddFile(GetFileName(lang), Encoding.UTF8.GetBytes(code));
            tw.Finish();
        }
        
        tarStream.Position = 0;
        await _client.Containers.ExtractArchiveToContainerAsync(containerID, new ContainerPathStatParameters
        {
            Path = "/usr/src/app",
            AllowOverwriteDirWithFile = true,
        }, tarStream);

        var exec = await _client.Exec.ExecCreateContainerAsync(containerID, new ContainerExecCreateParameters
        {
            AttachStdin = true,
            AttachStdout = true,
            AttachStderr = true,
            Tty = false,
            WorkingDir = "/usr/src/app",
            Cmd = new[] {"sh", "-c", $"{GetRunCommand(lang)}"}
        });
        callback(ExecutionStatus.Executing);
        using var stream = await _client.Exec.StartAndAttachContainerExecAsync(exec.ID, false);

        if (!string.IsNullOrEmpty(input))
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            Console.WriteLine($"Writing bytes {bytes[0]}, {bytes[1]}, {bytes[2]}...");
            await stream.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None);
        }
        
        stream.CloseWrite();

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

        // callback.Invoke(ExecutionStatus.Queued);
        // var imageName = GetImageName(lang);
        // var containerName = Guid.NewGuid().ToString();
        // var filename = GetFileName(lang);
        //
        //
        // var createResponse = await _client.Containers.CreateContainerAsync(new CreateContainerParameters
        // {
        //     Image = imageName,
        //     Name = containerName,
        //     Cmd = ["tail", "-f", "/dev/null"],
        //     OpenStdin = true,
        //     StdinOnce = true,
        //     Tty = false,
        //     HostConfig = new HostConfig
        //     {
        //         AutoRemove = true,
        //         Memory = 100 * 1024 * 1024,
        //         CPUQuota = 50000,
        //         NetworkMode = "none"
        //     }
        // });
        //
        // var started = await _client.Containers.StartContainerAsync(createResponse.ID, new ContainerStartParameters());
        // // ContainerInspectResponse inspect;
        // // do
        // // {
        // //     await Task.Delay(200);
        // //     inspect = await _client.Containers.InspectContainerAsync(createResponse.ID);
        // // } while (!inspect.State.Running);
        //
        // callback.Invoke(ExecutionStatus.Compiling);
        // var codeBytes = Encoding.UTF8.GetBytes(code);
        // var tarStream = new MemoryStream();
        // using (var tarWriter = new TarWriter(tarStream))
        // {
        //     tarWriter.AddFile(filename, codeBytes);
        // }
        // // tarStream.Flush();
        // tarStream.Position = 0;
        // await _client.Containers.ExtractArchiveToContainerAsync(createResponse.ID, new ContainerPathStatParameters
        // {
        //     Path = "/usr/src/app",
        //     AllowOverwriteDirWithFile = true,
        // }, tarStream);
        //
        // var command = GetRunCommand(lang);
        //
        // callback.Invoke(ExecutionStatus.Executing);
        // var execCreateResponse = await _client.Exec.ExecCreateContainerAsync(createResponse.ID,
        //     new ContainerExecCreateParameters
        //     {
        //         AttachStdin = true,
        //         AttachStdout = true,
        //         AttachStderr = true,
        //         Tty = false,
        //         WorkingDir = "/usr/src/app",
        //         Cmd = ["sh", "-c", command]
        //     });
        // var stream = await _client.Exec.StartAndAttachContainerExecAsync(execCreateResponse.ID, false);
        // if (input is not null)
        // {
        //     var inputBytes = Encoding.UTF8.GetBytes(input+"\n");
        //     await stream.WriteAsync(inputBytes, 0, inputBytes.Length, default);
        //     stream.CloseWrite();
        // }
        // var (stdout, stderr) = await stream.ReadOutputToEndAsync(default);
        // var success = stdout.Trim() == desiredOutput.Trim();
        // if (!string.IsNullOrEmpty(stderr) && string.IsNullOrEmpty(stdout))
        // {
        //     callback.Invoke(ExecutionStatus.HasErrors);
        //     return (stderr, success);
        // }
        //
        // callback.Invoke(success ? ExecutionStatus.Solved : ExecutionStatus.NotSolved);
        // await _client.Containers.KillContainerAsync(createResponse.ID, new ContainerKillParameters());
        // return (stdout, success);

    }

    private string GetImageName(string language) => language.ToLower() switch
    {
        "c++" => "cpp-image",
        "rust" => "rust-image",
        "python" => "py-image",
        _ => throw new ArgumentException($"Unexpected language {language}")
    };

    private string GetFileName(string language) => language.ToLower() switch
    {
        "c++" => "main.cpp",
        "rust" => "main.rs",
        "python" => "main.py",
        _ => throw new ArgumentException($"Unexpected language {language}")
    };

    private string GetRunCommand(string language) => language.ToLower() switch
    {
        "c++" => "g++ -o main main.cpp && chmod +x main && ./main",
        "rust" => "rustc main.rs && chmod +x main && ./main",
        "python" => "python main.py",
        _ => throw new ArgumentException($"Unexpected language {language}")
    };
}