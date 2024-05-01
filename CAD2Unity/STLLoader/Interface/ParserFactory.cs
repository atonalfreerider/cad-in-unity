using BasicLoader;

namespace STLLoader
{
    public static class ParserFactory
    {
        public static IParser Create()
        {
            return new STLParser();
        }
    }
}
