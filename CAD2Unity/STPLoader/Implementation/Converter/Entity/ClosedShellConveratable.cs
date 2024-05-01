using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class ClosedShellConveratable : IConvertable
    {
        readonly ClosedShell _closedShell;
        readonly IStpModel _model;

        public ClosedShellConveratable(ClosedShell closedShell, IStpModel model)
        {
            _closedShell = closedShell;
            _model = model;
            Init();
        }

        void Init()
        {
            IEnumerable<AdvancedFace> faces = _closedShell.PointIds.Select(_model.Get<AdvancedFace>);
            // create convertable for all faces and merge points and indices
            IEnumerable<Tuple<IList<Vector3>, IList<int>>> convertables = faces.Select(face => new AdvancedFaceConvertable(face, _model)).Select(c => Tuple.New(c.Points, c.Indices));

            Points = convertables.Select(c => c.First).SelectMany(p => p).ToList();
            Indices = convertables.Aggregate(Tuple.New(0, new List<int>()), Tuple.AggregateIndices).Second;
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }
}
