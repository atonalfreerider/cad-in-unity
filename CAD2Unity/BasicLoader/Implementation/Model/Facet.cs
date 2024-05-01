using AForge.Math;

namespace BasicLoader.Implementation.Model
{
    public class Facet
    {

        public Facet()
        {
            Verticies = new List<Vector3>();
        }

        public Vector3 Normal { get; set; }
        public IList<Vector3> Verticies { get; private set; }
    }
}