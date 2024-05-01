using STPLoader.Implementation.Parser;

namespace STPLoader.Implementation.Model.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Axis2Placement3D : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Info;
        /// <summary>
        /// 
        /// </summary>
        public long[] PointIds;

        public override void Init()
        {
            Info = ParseHelper.ParseString(Data[0]);
            PointIds = Data.Skip(1).Select(ParseHelper.ParseId).ToArray();
        }

        public override string ToString()
        {
            return $"<Axis2Placement3D({Info}, {PointIds})";
        }
    }

}
