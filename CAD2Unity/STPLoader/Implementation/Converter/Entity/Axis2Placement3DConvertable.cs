using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity;

public class Axis2Placement3DConvertable : IConvertable
{
    public IList<Vector3> Points { get; }
    public IList<int> Indices { get; }
    
    public Vector3 Location { get; private set; }
    
    public Axis2Placement3DConvertable(Axis2Placement3D axis2Placement3D, IStpModel model)
    {
        CartesianPoint axisPtX = model.Get<CartesianPoint>(axis2Placement3D.PointIds[0]);
        CartesianPoint axisPtY = model.Get<CartesianPoint>(axis2Placement3D.PointIds[1]);
        CartesianPoint axisPtZ = model.Get<CartesianPoint>(axis2Placement3D.PointIds[2]);
        Location = new Vector3(axisPtX.Vector.X, axisPtX.Vector.Y, axisPtX.Vector.Z);
        
        Points = new List<Vector3> { axisPtX.Vector, axisPtY.Vector, axisPtZ.Vector };
        Indices = new List<int>();
    }
}