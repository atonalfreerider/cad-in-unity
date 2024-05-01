using BasicLoader.Interface;
using STPLoader.Implementation.Model.Entity;

namespace STPLoader.Interface
{
    /// <summary>
    /// 
    /// </summary>
	public interface IStpModel : IModel
    {
        IList<T> All<T>() where T : Entity;
        IDictionary<long, Entity> All();
        Entity Get(long id);
        T Get<T>(long id) where T : Entity;
    }
}

