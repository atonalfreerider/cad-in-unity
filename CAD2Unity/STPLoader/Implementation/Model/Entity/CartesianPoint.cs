﻿using AForge.Math;
using STPLoader.Implementation.Parser;

namespace STPLoader.Implementation.Model.Entity
{
    public class CartesianPoint : Entity
    {
        public Vector3 Vector;
        public string Info;

        public override void Init()
        {
            Info = ParseHelper.ParseString(Data[0]);
            IList<float> coord = ParseHelper.ParseList<float>(Data[1]);
            Vector = new Vector3(coord[0], coord[1], coord[2]);
        }

        public override string ToString()
        {
            return $"<CartesianPoint({Info}, {Vector})";
        }
    }
}
