namespace STPLoader.Implementation.Model.Entity
{
    public class DirectionPoint : CartesianPoint
    {

        public override string ToString()
        {
            return $"<DirectionPoint({Info}, {Vector})";
        }
    }
}
