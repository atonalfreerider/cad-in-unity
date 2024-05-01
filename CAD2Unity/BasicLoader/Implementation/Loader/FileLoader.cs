using BasicLoader.Interface;

namespace BasicLoader.Implementation.Loader
{
    /// <summary>
    /// File loader.
    /// </summary>
    public class FileLoader : ILoader
    {
        readonly FileStream _fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoader"/> class.
        /// </summary>
        /// <param name="stream">Stream.</param>
        public FileLoader(FileStream stream)
        {
            _fileStream = stream;
        }

        #region ILoader implementation
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Stream Load()
        {
            return _fileStream;
        }

        public void Close()
        {
            _fileStream.Close();
        }

        #endregion
    }
}

