using AForge.Math;
using BasicLoader.Implementation.Model;

namespace BasicLoader.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// 
        /// </summary>
        IList<IPart> Parts { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        IConstraint GetConstraint(IModel a, IModel b);
        /// <summary>
        /// 
        /// </summary>
        IList<Facet> Facets { get; }
        /// <summary>
        /// 
        /// </summary>
        IList<Vector3> Vertices { get; }
        /// <summary>
        /// 
        /// </summary>
        IList<int> Triangles { get; }
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
    }
}