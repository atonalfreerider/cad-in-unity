using STPLoader.Implementation.Parser;

namespace STPLoader.Implementation.Model.Entity
{
    public class AdvancedFace : Entity
    {
        public string Info;
        public IList<long> BoundIds;
        public long SurfaceId;
        public bool Boo;

        public override void Init()
        {
            Info = ParseHelper.Parse<string>(Data[0]);
            BoundIds = ParseHelper.ParseList<string>(Data[1]).Select(ParseHelper.ParseId).ToList();
            SurfaceId = ParseHelper.ParseId(Data[2]);
            Boo = ParseHelper.Parse<bool>(Data[3]);
        }

        public override string ToString()
        {
            return $"<AdvancedFace({Info}, {BoundIds}, {SurfaceId}, {Boo})";
        }
    }

}
