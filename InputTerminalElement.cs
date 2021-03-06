using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Crystalline;
using MetaphysicsIndustries.Epiphany;
using System.Windows.Forms;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class InputTerminalElement : TerminalElement
    {
        public InputTerminalElement(InputTerminal terminal)
            :base(terminal)
        {
        }

        protected override void InitTerminals2()
        {
            System.Type conType = Terminal.ConnectionBase.TypeForConnection;
            OutputConnectionBase con = OutputConnectionBase.ConstructOutputConnection(conType,"");
            Node.OutputConnectionBases.Add(con);
            OutputTerminal term = new OutputTerminal(con);
            term.Side = BoxOrientation.Right;
            term.Position = Height / 2;
            Terminals.Add(term);

            base.InitTerminals();
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

        public InputTerminal InputTerminal
        {
            get { return (InputTerminal)Terminal; }
        }

        protected override Terminal CreateTerminal(Type type, string name)
        {
            return new InputTerminal(InputConnectionBase.ConstructInputConnection(type, name));
        }
    }
}
