﻿using STPLoader.Implementation.Parser;

namespace STPLoader.Implementation.Model.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class BSplineCurveWithKnots : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Info;

        public int Number;
        /// <summary>
        /// 
        /// </summary>
        public IList<long> PointIds;

        public bool Boo1;

        public bool Boo2;

        public bool Boo3;

        /// <summary>
        /// 
        /// </summary>
        public IList<double> List1;
        /// <summary>
        /// 
        /// </summary>
        public IList<double> List2;

        public bool Boo4;
        
        public override void Init()
        {
            Info = ParseHelper.Parse<string>(Data[0]);
            Number = ParseHelper.Parse<int>(Data[1]);
            PointIds = ParseHelper.ParseList<string>(Data[2]).Select(ParseHelper.ParseId).ToList();
            Boo1 = ParseHelper.Parse<bool>(Data[3]);
            Boo2 = ParseHelper.Parse<bool>(Data[4]);
            Boo3 = ParseHelper.Parse<bool>(Data[5]);
            List1 = ParseHelper.ParseList<double>(Data[6]);
            List2 = ParseHelper.ParseList<double>(Data[7]);
            Boo4 = ParseHelper.Parse<bool>(Data[8]);
        }

        public override string ToString()
        {
            return
                $"<BSplineCurveWithKnots({Info}, {Number}, {PointIds}, {Boo1}, {Boo2}, {Boo3}, {List1}, {List2}, {Boo4})";
        }
    }

}
