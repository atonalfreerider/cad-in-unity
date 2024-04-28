namespace BasicLoader.Interface
{
    public enum CADType
    {
        STL,
        STP,
        ThreeDXML
    }

    public static class CADTypeUtils
    {
        static readonly IDictionary<string, CADType> map = new Dictionary<string, CADType>
        {
            {"stl", CADType.STL},
            {"stp", CADType.STP},
            {"3dxml", CADType.ThreeDXML}
        };

        public static CADType FromFileExtension(string fileName)
        {
            string extension = fileName.Substring((fileName.LastIndexOf('.')+1));
            return map[extension.ToLower()];
        }
    }
}
