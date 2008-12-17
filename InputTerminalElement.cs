using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Crystalline;

namespace MetaphysicsIndustries.Epiphany.Amethyst
{
    public class InputTerminalElement : TerminalElement
    {
        public InputTerminalElement()
        {
        }

        protected override PointF[] GetPolygon()
        {
            PointF[] pt = new PointF[3];

            if (Terminal == null)
            {
                pt = new PointF[1];
            }
            else if (Terminal.Side == BoxOrientation.Up)
            {
                pt[0].X = -1;
                pt[1].Y = 1.732f;
                pt[2].X = 1;
            }
            else if (Terminal.Side == BoxOrientation.Right)
            {
                pt[0].Y = -1;
                pt[1].X = -1.732f;
                pt[2].Y = 1;
            }
            else if (Terminal.Side == BoxOrientation.Down)
            {
                pt[0].X = -1;
                pt[1].Y = -1.732f;
                pt[2].X = 1;
            }
            else //if (Terminal.Side == BoxOrientation.Left)
            {
                pt[0].Y = -1;
                pt[1].X = 1.732f;
                pt[2].Y = 1;
            }

            return pt;
        }

        private InputTerminal _inputTerminal;
        public InputTerminal InputTerminal
        {
            get { return _inputTerminal; }
            set { _inputTerminal = value; }
        }

        public override Terminal Terminal
        {
            get { return _inputTerminal; }
        }


    }
}
