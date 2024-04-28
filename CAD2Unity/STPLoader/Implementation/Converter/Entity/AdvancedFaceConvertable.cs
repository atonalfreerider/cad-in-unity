using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvancedFaceConvertable : IConvertable
    {
        readonly AdvancedFace _face;
        readonly IStpModel _model;
        
        public SurfaceConvertable SurfaceConvertable { get; private set; }
        public List<BoundConvertable> BoundConvertables { get; private set; }

        public AdvancedFaceConvertable(AdvancedFace face, IStpModel model)
        {
            _face = face;
            _model = model;
            Init();
        }

        void Init()
        {
            IEnumerable<Bound> bounds = _face.BoundIds.Select(_model.Get<Bound>);
            Surface surface = _model.Get<Surface>(_face.SurfaceId);
            SurfaceConvertable = new SurfaceConvertable(surface, _model);
            // create convertable for all faces and merge points and indices
            BoundConvertables = bounds.Select(bound => new BoundConvertable(bound, _model)).ToList();
            
            List<Tuple<IList<Vector3>, IList<int>>> convertables =BoundConvertables.Select(c => Tuple.New(c.Points, c.Indices)).ToList();
            convertables.Add(Tuple.New(SurfaceConvertable.Points, SurfaceConvertable.Indices));

            Points = convertables.Select(c => c.First).SelectMany(p => p).ToList();
            Indices = convertables.Aggregate(Tuple.New(0, new List<int>()), Tuple.AggregateIndices).Second;
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }
}
