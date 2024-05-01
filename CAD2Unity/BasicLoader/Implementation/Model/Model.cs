using AForge.Math;
using BasicLoader.Implementation.Model.Constraint;
using BasicLoader.Interface;

namespace BasicLoader.Implementation.Model
{
    public class Model : IModel
    {
        IList<Facet> _facets;
        string _name;
        IList<IPart> _parts;
        
        public IList<IPart> Parts {
            get => _parts;
            set => _parts = value;
        }

        public Model()
        {
            _parts = new List<IPart>();
        }


        public IConstraint GetConstraint(IModel a, IModel b)
        {
            return new EmptyConstraint();
        }

        public IList<Facet> Facets
        {
            get => _facets;

            set => _facets = value;
        }

        public IList<Vector3> Vertices
        {
            get { return Facets.SelectMany(x => x.Verticies.ToArray()).ToList(); }
        }

        public IList<int> Triangles => Enumerable.Range(0, Facets.Count()*3).ToList();

        public override string ToString()
        {
            return @"Model with {_facets.Count} facets";
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Author { get; set; }
    }
}