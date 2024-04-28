using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity
{
    public class PlaneConvertable : IConvertable
    {
        readonly Plane _surface;
        readonly IStpModel _model;

        public PlaneConvertable(Surface surface, IStpModel model)
        {
            _surface = (Plane)surface;
            _model = model;
            Init();
        }

        void Init()
        {
            //var planeAxis = _model.Get<Axis2Placement3D>(_surface.AxisId);
            
            Points = new List<Vector3>();
            Indices = new List<int>();
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }
}
