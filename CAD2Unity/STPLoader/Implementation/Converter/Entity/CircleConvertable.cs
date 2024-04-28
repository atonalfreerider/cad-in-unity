using AForge.Math;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter.Entity
{
    public class CircleConvertable : IConvertable
    {
        readonly Circle _circle;
        readonly IStpModel _model;
        const int Sides = 64;

        public Axis2Placement3DConvertable Axis2Placement3DConvertable { get; private set; }
        public float Radius { get; private set; }
        public float Yaw { get; private set; }
        public float Pitch { get; private set; }

        public CircleConvertable(Circle circle, IStpModel model)
        {
            _circle = circle;
            _model = model;
            Init();
        }

        void Init()
        {
            Radius = (float)_circle.Radius;
            Axis2Placement3D placement = _model.Get<Axis2Placement3D>(_circle.PointId);
            Axis2Placement3DConvertable = new Axis2Placement3DConvertable(placement, _model);
            CartesianPoint cartesianPoint = _model.Get<CartesianPoint>(placement.PointIds[0]);
            DirectionPoint direction = _model.Get<DirectionPoint>(placement.PointIds[1]);

            Points = new List<Vector3> { cartesianPoint.Vector };
            Vector3 x = new Vector3(1, 0, 0);
            Vector3 y = new Vector3(0, 1, 0);
            double ax = Math.Acos(Vector3.Dot(direction.Vector, x) / (direction.Vector.Norm * x.Norm));
            double ay = Math.Acos(Vector3.Dot(direction.Vector, y) / (direction.Vector.Norm * y.Norm));

            Yaw = (float)(Math.PI / 2 - ax);
            Pitch = (float)(Math.PI / 2 - ay);
            Matrix3x3 rotationMatrix = Matrix3x3.CreateFromYawPitchRoll(Yaw, Pitch, 0);

            for (int i = 0; i < Sides; i++)
            {
                double angle = 360 - (i * 360d / Sides);
                angle = angle * 2 * Math.PI / 360d;
                // calculate point on unit circle and multiply by radius
                Vector3 vector = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0) * (float)_circle.Radius;
                // change normal vector to direction vector
                vector = rotationMatrix * vector;
                // add midpoint position vector
                vector = vector + cartesianPoint.Vector;
                Points.Add(vector);
            }

            Indices = Enumerable.Range(1, Sides).Select(i => new[] { 0, i, (i % Sides) + 1 }).SelectMany(d => d)
                .ToList();
        }

        public IList<Vector3> Points { get; private set; }
        public IList<int> Indices { get; private set; }
    }
}