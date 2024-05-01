using BasicLoader.Interface;
using STPLoader.Implementation.Parser;

namespace STPLoader.Interface
{
    public static class ParserFactory
    {
        public static IParser Create()
        {
            return new StpParser();
        }
    }
}
