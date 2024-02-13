namespace Shell;
internal static class FileUtility
{
    public static List<string> ListCurrentDirectory()
    {
        var items = new List<string>();

        var files = ListFiles(Environment.CurrentDirectory);

        var folders = ListFolders(Environment.CurrentDirectory);

        foreach (var folder in folders)
        {
            items.Add(folder);
        }

        foreach (var file in files)
        {
            items.Add(file);
        }

        return items;
    }

    public static List<DirectoryDetail> ListCurrentDirectoryWithDetails()
    {
        var directoryDetails = new List<DirectoryDetail>();

        var files = ListFiles(Environment.CurrentDirectory);

        var folders = ListFolders(Environment.CurrentDirectory);

        foreach (var folder in folders)
        {
            var lastWrite = Directory.GetLastWriteTime(folder);
            directoryDetails.Add(
                new DirectoryDetail
                {
                    Name = folder,
                    LastWrite = lastWrite.ToString(),
                    Type = DirectoryType.Folder
                });
        }

        foreach (var file in files)
        {
            var lastWrite = Directory.GetLastWriteTime(file);
            directoryDetails.Add(
                new DirectoryDetail
                {
                    Name = file,
                    LastWrite = lastWrite.ToString(),
                    Type = DirectoryType.File
                });
        }

        return directoryDetails;
    }

    private static string[] ListFolders(string path)
    {
        var directories = Directory
            .GetDirectories(path)
            .Where(dir => (File.GetAttributes(dir) & FileAttributes.Hidden) == 0)
            .Where(dir => (File.GetAttributes(dir) & FileAttributes.System) == 0)
            .Select(dir => dir.Split('\\').Last())
            .ToArray();

        return directories;
    }

    private static string[] ListFiles(string path)
    {
        var files = Directory
            .GetFiles(path)
            .Where(dir => (File.GetAttributes(dir) & FileAttributes.Hidden) == 0)
            .Where(dir => (File.GetAttributes(dir) & FileAttributes.System) == 0)
            .Select(dir => dir.Split('\\').Last())
            .ToArray();

        return files;
    }
}

internal readonly struct DirectoryDetail
{
    public string Name { get; init; }

    public string LastWrite { get; init; }

    public DirectoryType Type { get; init; }

}

internal enum DirectoryType
{
    File,
    Folder
}