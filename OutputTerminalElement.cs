using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Crystalline;
using MetaphysicsIndustries.Epiphany;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class OutputTerminalElement : TerminalElement
    {
        public OutputTerminalElement(OutputTerminal terminal)
            : base(terminal)
        {
        }

        protected override void InitTerminals2()
        {
            throw new NotImplementedException();
        }

        public OutputTerminal outputTerminal
        {
            get { return (OutputTerminal)Terminal; }
        }

        protected override PointF[] GetPolygon()
        {
            PointF[] pt = new PointF[3];

            if (Terminal == null)
            {
                //pt = new PointF[1];
            }
            else if (Terminal.Side == BoxOrientation.Up)
            {
                pt[0].X = -1;
                pt[0].Y = 1.732f;
                pt[2].X = 1;
                pt[2].Y = 1.732f;
            }
            else if (Terminal.Side == BoxOrientation.Right)
            {
                pt[0].X = -1.732f;
                pt[0].Y = -1;
                pt[2].X = -1.732f;
                pt[2].Y = 1;
            }
            else if (Terminal.Side == BoxOrientation.Down)
            {
                pt[0].X = -1;
                pt[0].Y = -1.732f;
                pt[2].X = 1;
                pt[2].Y = -1.732f;
            }
            else //if (Terminal.Side == BoxOrientation.Left)
            {
                pt[0].X = 1.732f;
                pt[0].Y = -1;
                pt[2].X = 1.732f;
                pt[2].Y = 1;
            }

            return pt;
        }

        protected override Terminal CreateTerminal(Type type, string name)
        {
            return new OutputTerminal(OutputConnectionBase.ConstructOutputConnection(type, name));
        }
    }
}
