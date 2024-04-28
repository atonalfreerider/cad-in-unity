using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity
{
    public class BoundConvertable : IConvertable
    {
        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }

        readonly Bound _bound;
        readonly IStpModel _model;

        public BoundConvertable(Bound bound, IStpModel model)
        {
            _bound = bound;
            _model = model;
            Init();
        }

        void Init()
        {
            //var loop = _model.Get<EdgeLoop>(_bound.EdgeLoopId);

            Points = new List<Vector3>();
            Indices = new List<int>();   
        }
    }
}
