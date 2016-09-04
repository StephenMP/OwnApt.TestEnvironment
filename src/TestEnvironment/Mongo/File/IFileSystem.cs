namespace OwnApt.TestEnvironment.Mongo.File
{
    internal interface IFileSystem
    {
        #region Public Methods

        string CreateTempFolder();

        void DeleteDirectory(string path, bool recursive);

        bool DirectoryExists(string path);

        bool FileExists(string path);

        #endregion Public Methods
    }
}
