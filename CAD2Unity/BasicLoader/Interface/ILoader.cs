namespace BasicLoader.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Load this instance.
        /// </summary>
        /// <returns></returns>
        Stream Load();
        /// <summary>
        /// 
        /// </summary>
        void Close();
    }
}

