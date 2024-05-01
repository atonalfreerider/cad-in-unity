namespace STPLoader.Implementation.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class StpData
    {
        readonly IDictionary<long, Entity.Entity> _entities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public StpData(IDictionary<long, Entity.Entity> entities)
        {
            _entities = entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> All<T>() where T : Entity.Entity
        {
            return _entities.Values.OfType<T>().ToList();
        }

        public IDictionary<long, Entity.Entity> All()
        {
            return _entities;
        }

        public Entity.Entity Get(long id)
        {
            return _entities[id];
        }

        public T Get<T>(long id) where T : Entity.Entity
        {
            Entity.Entity entity = _entities[id];
            return entity as T;
        }

        public override string ToString()
        {
            return
                $"<StpData({string.Join(",\n", _entities.Select(pair => $"{pair.Key} => {pair.Value}").ToArray())})>";
        }
    }
}
