﻿using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class FloatLiteralElement : LiteralElement<float>
    {
        public FloatLiteralElement()
            : base(new LiteralNode("Float Literal"), new SizeV(80, 40))
        {
        }

        protected override float ConvertString(string value)
        {
            return float.Parse(value);
        }

        protected override bool IsConvertable(string value)
        {
            float v;
            return float.TryParse(value, out v);
        }

        public override string Text
        {
            get { return Value.ToString("G5"); }
        }
    }
}
