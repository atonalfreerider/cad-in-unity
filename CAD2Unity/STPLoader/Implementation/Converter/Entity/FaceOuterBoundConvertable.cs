using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity;

public class FaceOuterBoundConvertable : IConvertable
{
    public IList<Vector3> Points { get; }
    public IList<int> Indices { get; }
    
    public EdgeLoopConvertable EdgeLoopConvertable { get; private set; }
    
    public FaceOuterBoundConvertable(FaceOuterBound faceOuterBound, IStpModel model)
    {
        Points = new List<Vector3>();
        Indices = new List<int>();
        
        EdgeLoop edgeLoop = model.Get<EdgeLoop>(faceOuterBound.EdgeLoopId);
        EdgeLoopConvertable = new EdgeLoopConvertable(edgeLoop, model);
    }
}