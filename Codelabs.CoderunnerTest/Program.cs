using System.Text;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Codelabs.CoderunnerTest;

class Program
{
    static void PrintLn(object data)
    {
        Console.WriteLine(data);
    }

    static void EPrintLn(object data)
    {
        Console.Error.WriteLine(data);
    }

    static string? ReadLn() => Console.ReadLine();
    static async Task Main(string[] args)
    {
        PrintLn("Write language: ");
        var lang = ReadLn()!;
        PrintLn("Write desired output: ");
        var desired = ReadLn()!;
        PrintLn("need stdin?");
        var needStdin = bool.Parse(ReadLn()!);
        List<string> stdin = [];
        if (needStdin)
        {
            string inputted;
            while ((inputted = ReadLn()!).Trim() != "\\q")
            {
                stdin.Add(inputted.Trim());
            }
        }
        const string code = """
                   use std::io::{self, Write};
                   fn main() {
                        let stdin = io::stdin();
                        let mut buffer = String::new();
                        stdin.read_line(&mut buffer).unwrap();
                        let nums: Vec<i32> = buffer.trim().split_whitespace().map(|x| x.parse().unwrap()).collect();
                        let (a, b): (i32, i32) = (nums[0], nums[1]);
                        println!("{}", a + b)
                   }
                   """;
        var image = GetImage(lang);
        if (image is null)
        {
            EPrintLn("Unsupported language");
            return;
        }

        using var client = new DockerClientConfiguration().CreateClient();
        
        var createResponse = await client.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = image,
            Env = [$"CODE={code}"],
            OpenStdin = needStdin,
            AttachStdin = needStdin,
            AttachStdout = true,
            AttachStderr = true,
            Tty = false
        });

        await client.Containers.StartContainerAsync(createResponse.ID, null);
        using var stream = await client.Containers.AttachContainerAsync(createResponse.ID, false,
            new ContainerAttachParameters
            {
                Stream = true,
                Stdin = needStdin,
                Stdout = true,
                Stderr = true
            });

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        var stdoutBuilder = new StringBuilder();
        var stderrBuilder = new StringBuilder();
        var readTask = Task.Run(async () =>
        {
            var buffer = new byte[1024];
            while (!cts.Token.IsCancellationRequested)
            {
                var result = await stream.ReadOutputAsync(buffer, 0, buffer.Length, cts.Token);
                if (result.Count > 0)
                {
                    var output = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (result.Target == MultiplexedStream.TargetStream.StandardOut)
                    {
                        stdoutBuilder.Append(output);
                    }
                    else if (result.Target == MultiplexedStream.TargetStream.StandardError)
                    {
                        stderrBuilder.Append(output);
                    }
                }
                else
                {
                    break;
                }
            }
        }, cts.Token);

        if (needStdin)
        {
            foreach (var v in stdin)
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes($"{v}\n"), 0, v.Length + 1, cts.Token);
            }
        }

        await readTask;
        stream.CloseWrite();
        await client.Containers.RemoveContainerAsync(createResponse.ID, new ContainerRemoveParameters(), cts.Token);
        var stderr = stderrBuilder.ToString();
        var stdout = stdoutBuilder.ToString();
        if (!string.IsNullOrEmpty(stderr))
        {
            PrintLn($"Stderr:\n{stderr}\n");
        }
        PrintLn(stdout.Trim() == desired.Trim() ? "Correct!" : $"Incorrect! Desired: [{desired}]; Got: [{stdout}]");
    }

    static string? GetImage(string lang)
    {
        return lang.ToLower() switch
        {
            "rust" => "rust-env",
            "python" or "py" => "py-env",
            "cpp" or "c++" => "cpp-env",
            _ => null
        };
    }
}