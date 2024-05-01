using AForge.Math;
using BasicLoader.Implementation.Model;
using BasicLoader.Interface;
using STPLoader.Interface;

namespace STPLoader.Implementation.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class StpFile : IStpModel
	{
        readonly IList<IPart> _parts;

        /// <summary>
        /// 
        /// </summary>
	    public StpHeader Header { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StpData Data { get; set; }

        public override string ToString()
        {
            return $"<StpFile({Header}, {Data})>";
        }

        public IList<T> All<T>() where T : Entity.Entity
        {
            return Data.All<T>();
        }

        public IDictionary<long, Entity.Entity> All()
        {
            return Data.All();
        }

        public Entity.Entity Get(long id)
        {
            return Data.Get(id);
        }

        public T Get<T>(long id) where T : Entity.Entity
        {
            return Data.Get<T>(id);
        }
        
        public IList<IPart> Parts => _parts;

        public IConstraint GetConstraint(IModel a, IModel b)
        {
            throw new NotImplementedException();
        }

        public IList<Facet> Facets { get; }

        public IList<Vector3> Vertices { get; }

        public IList<int> Triangles { get; }

        public string Name => Header.Name.Name;
    }

}

