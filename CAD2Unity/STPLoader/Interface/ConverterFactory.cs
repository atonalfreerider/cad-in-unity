using STPLoader.Implementation.Converter;

namespace STPLoader.Interface
{
    public static class ConverterFactory
    {
        public static IConverter Create()
        {
            return new Converter();
        }
    }
}
