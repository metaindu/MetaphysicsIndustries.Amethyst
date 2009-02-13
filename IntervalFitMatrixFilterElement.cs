using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;

namespace MetaphysicsIndustries.Amethyst
{
    public class IntervalFitMatrixFilterElement : MatrixFilterElement
    {
        public IntervalFitMatrixFilterElement()
            : base(new IntervalFitMatrixFilter(), "Interval Fit")
        {
        }
    }
}