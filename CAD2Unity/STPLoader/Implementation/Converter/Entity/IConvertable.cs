using AForge.Math;

namespace STPLoader.Implementation.Converter.Entity
{
    public interface IConvertable
    {
        IList<Vector3> Points { get; }
        IList<int> Indices { get; }
    }
}
