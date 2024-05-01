using STPLoader.Implementation.Parser;

namespace STPLoader.Implementation.Model.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Circle : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Info;
        /// <summary>
        /// 
        /// </summary>
        public long PointId;

        public double Radius;

        public override void Init()
        {
            Info = ParseHelper.Parse<string>(Data[0]);
            PointId = ParseHelper.ParseId(Data[1]);
            Radius = ParseHelper.Parse<double>(Data[2]);
        }

        public override string ToString()
        {
            return $"<Circle({Info}, {PointId}, {Radius})";
        }
    }

}
