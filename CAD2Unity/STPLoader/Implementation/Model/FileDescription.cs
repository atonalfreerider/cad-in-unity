namespace STPLoader.Implementation.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class FileDescription
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="implementationLevel"></param>
        public FileDescription(IList<string> description, string implementationLevel)
        {
            Description = description;
            ImplementationLevel = implementationLevel;
        }
        /// <summary>
        /// 
        /// </summary>
        public IList<string> Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ImplementationLevel { get; set; }

        public override string ToString()
        {
            return $"<FileDescription({Description}, {ImplementationLevel})>";
        }
    }
}
