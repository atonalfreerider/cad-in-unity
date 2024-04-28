using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity;

public class OrientedEdgeConvertable : IConvertable
{
    public IList<Vector3> Points { get; }
    public IList<int> Indices { get; }
    
    public EdgeCurveConvertable EdgeCurveConvertable { get; private set; }
    
    public OrientedEdgeConvertable(OrientedEdge orientedEdge, IStpModel model)
    {
        EdgeCurve edgeCurve = model.Get<EdgeCurve>(orientedEdge.PointIds[2]);
        EdgeCurveConvertable = new EdgeCurveConvertable(edgeCurve, model);
        Points = new List<Vector3>();
        Indices = new List<int>();
    }
}