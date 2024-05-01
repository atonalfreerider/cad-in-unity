using System.Globalization;
using AForge.Math;

namespace STPLoader.Interface
{
    public class MeshModel
    {
        public string Meta;
        readonly IList<Vector3> _points;
        readonly IList<int> _triangles;

        public MeshModel(IList<Vector3> points, IList<int> triangles)
        {
            _points = points;
            _triangles = triangles;
        }

        public IList<Vector3> Points => _points;

        public IList<int> Triangles => _triangles;

        public override string ToString()
        {
            return string.Format("<MeshModel({0}, {1})>",
                string.Join("|", Points.Select(x => x.ToString()).ToArray()),
                string.Join("|", Triangles.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()));
        }
    }
}
