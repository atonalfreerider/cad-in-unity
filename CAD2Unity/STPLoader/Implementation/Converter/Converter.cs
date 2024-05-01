using AForge.Math;
using STPLoader.Implementation.Converter.Entity;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Converter
{
    class Converter : IConverter
    {

        public MeshModel Convert(IStpModel model)
        {
            List<Vector3> vectors = new List<Vector3>();
            List<int> indices = new List<int>();

            GetValue<ClosedShell>(model, indices, vectors);
            MeshModel mesh = new MeshModel(vectors, indices);

            return mesh;
        }

        void GetValue<T>(IStpModel model, List<int> indices, List<Vector3> vectors) where T : Model.Entity.Entity
        {
            foreach (T element in model.All<T>())
            {
                int offset = vectors.Count;
                IConvertable convertable = CreateConvertable(element, model);
                IList<Vector3> circleVectors = convertable.Points;
                IList<int> circleIndices = convertable.Indices;
                vectors.AddRange(circleVectors);
                indices.AddRange(circleIndices.Select(x => x + offset));
            }
        }

        static IConvertable CreateConvertable<T>(T element, IStpModel model) where T : Model.Entity.Entity
        {
            Type type = typeof (T);
            if (type == typeof(Circle))
            {
                return new CircleConvertable(element as Circle, model);
            }
            if (type == typeof(Line))
            {
                return new LineConvertable(element as Line, model);
            }
            if (type == typeof(AdvancedFace))
            {
                return new AdvancedFaceConvertable(element as AdvancedFace, model);
            }
            if (type == typeof(ClosedShell))
            {
                return new ClosedShellConveratable(element as ClosedShell, model);
            }
            throw new Exception("Not supported");
        }

    }
}
