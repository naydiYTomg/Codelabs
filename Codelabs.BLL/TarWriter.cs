using System.Text;
using ICSharpCode.SharpZipLib.Tar;

namespace Codelabs.BLL;
public class TarWriter(Stream stream) : IDisposable
{
    private readonly TarOutputStream _tarStream = new(stream, Encoding.UTF8)
    {
        IsStreamOwner = false
    };

    public void AddFile(string filename, byte[] content)
    {
        var entry = TarEntry.CreateTarEntry(filename.Replace('\\', '/'));
        entry.Size = content.Length;
        entry.TarHeader.Mode = 493;
        _tarStream.PutNextEntry(entry);
        _tarStream.Write(content, 0, content.Length);
        _tarStream.CloseEntry();
    }

    public void Finish()
    {
        _tarStream.Finish();
    }

    public void Dispose() => _tarStream.Dispose();
}