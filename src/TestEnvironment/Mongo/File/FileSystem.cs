using System;
using System.IO;

namespace OwnApt.TestEnvironment.Mongo.File
{
    internal class FileSystem : IFileSystem
    {
        #region Public Methods

        public string CreateTempFolder()
        {
            var tempFolderPath = Path.Combine(Path.GetTempPath(), "OwnApt\\Mongo");
            var dbPath = Path.Combine(tempFolderPath, Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(dbPath);

            return dbPath;
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        #endregion Public Methods
    }
}
