using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity;

public class EdgeCurveConvertable : IConvertable
{
    public IList<Vector3> Points { get; }
    public IList<int> Indices { get; }
    
    public EdgeCurveConvertable(EdgeCurve edgeCurve, IStpModel model)
    {
        Points = new List<Vector3>();
        Indices = new List<int>();
        
        // TODO
    }
}