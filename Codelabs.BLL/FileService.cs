namespace Codelabs.BLL
{
    public static class FileService
    {
        public static async Task WriteToFile(string filePath, string text)
        {
            string dirPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(Path.GetFullPath(dirPath)))
            {
                Directory.CreateDirectory(Path.GetFullPath(dirPath));
            }

            using var writer = new StreamWriter(Path.GetFullPath(filePath), false);
            await writer.WriteAsync(text);
        }

        public static async Task<string?> ReadFile(string filePath)
        {
            if (Path.Exists(Path.GetFullPath(filePath)))
            {
                using var reader = new StreamReader(Path.GetFullPath(filePath), false);
                return await reader.ReadToEndAsync();
            }
            return null;
        }
    }
}
