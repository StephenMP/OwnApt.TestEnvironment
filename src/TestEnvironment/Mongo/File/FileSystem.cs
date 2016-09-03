using System;
using System.IO;

namespace OwnApt.TestEnvironment.Mongo.File
{
    internal class FileSystem : IFileSystem
    {
        public string CreateTempFolder()
        {
            var tempFolderPath = Path.Combine(Path.GetTempPath(), "OwnApt\\Mongo");
            var dbPath = Path.Combine(tempFolderPath, Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(dbPath);

            return dbPath;
        }

        public bool FileExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }
    }
}