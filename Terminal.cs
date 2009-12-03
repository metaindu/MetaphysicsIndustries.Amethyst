using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Crystalline;
using System.Drawing;
using MetaphysicsIndustries.Epiphany;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public abstract class Terminal : Entity//, IConnectee<Terminal, Terminal>, IConnector<Terminal, Terminal>
    {
        private BoxOrientation _side = BoxOrientation.Left;
        public BoxOrientation Side
        {
            get { return _side; }
            set { _side = value; }
        }

        private float _position = 0;
        public float Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private float _size = 7;
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public abstract Connection ConnectionBase { get; }
        

        public PointF GetLocationInElementSpace()
        {
            if (Side == BoxOrientation.Up)
            {
                return new PointF(Position, 0);
            }
            else if (Side == BoxOrientation.Right)
            {
                return new PointF(ParentAmethystElement.Width, Position);
            }
            else if (Side == BoxOrientation.Down)
            {
                return new PointF(Position, ParentAmethystElement.Height);
            }
            else if (Side == BoxOrientation.Left)
            {
                return new PointF(0, Position);
            }
            else
            {
                throw new Exception("Invalid Side in Terminal");
            }
        }

        public PointF GetLocationInDocumentSpace()
        {
            if (ParentAmethystElement != null)
            {
                return ParentAmethystElement.Location + new SizeF(GetLocationInElementSpace());
            }

            return new PointF();
        }

        private AmethystElement _parentAmethystElement;
        public AmethystElement ParentAmethystElement
        {
            get { return _parentAmethystElement; }
            set
            {

                if (_parentAmethystElement != value)
                {
                    if (_parentAmethystElement != null)
                    {
                        _parentAmethystElement.Terminals.Remove(this);
                    }

                    SetParentAmethystElement(value);

                    if (_parentAmethystElement != null)
                    {
                        _parentAmethystElement.Terminals.Add(this);
                    }
                }
            }
        }

        protected void SetParentAmethystElement(AmethystElement value)
        {
            _parentAmethystElement = value;
        }

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
            RenderPolygon(g, pen, brush, font);

            RenderDisplayText(g, pen, brush, font);
        }

        private void RenderDisplayText(Graphics g, Pen pen, Brush brush, Font font)
        {
            string displayText = DisplayText;
            if (!string.IsNullOrEmpty(displayText))
            {
                PointF displayTextLocation = GetLocationInElementSpace();
                float offset = 2.5f * Size;

                switch (Side)
                {
                    case BoxOrientation.Down:
                        displayTextLocation.Y -= offset;
                        break;
                    case BoxOrientation.Left:
                        displayTextLocation.X += offset;
                        break;
                    case BoxOrientation.Right:
                        displayTextLocation.X -= offset;
                        break;
                    case BoxOrientation.Up:
                        displayTextLocation.Y += offset;
                        break;
                    default:
                        break;
                }

                Font font2 = new Font(font.FontFamily, font.Size - 2);

                SizeF displayTextSize = g.MeasureString(DisplayText, font2);

                displayTextLocation.X -= displayTextSize.Width / 2;
                displayTextLocation.Y -= displayTextSize.Height / 2;

                g.DrawString(displayText, font2, pen.Brush, displayTextLocation);
            }
        }

        private string _displayText;
        public string DisplayText
        {
            get { return _displayText; }
            set { _displayText = value; }
        }

        protected virtual void RenderPolygon(Graphics g, Pen pen, Brush brush, Font font)
        {
            PointF[] pt = GetPolygon();
            int i;

            SizeF position = new SizeF(GetLocationInElementSpace());

            for (i = 0; i < pt.Length; i++)
            {
                pt[i].X *= Size;
                pt[i].Y *= Size;
                pt[i] += position;
            }

            g.FillPolygon(brush, pt);
            g.DrawPolygon(pen, pt);
        }

        protected abstract PointF[] GetPolygon();

        //private TerminalState _state = TerminalState.NeedsExecute;
        //public TerminalState State
        //{
        //    get { return _state; }
        //    set { _state = value; }
        //}

        //public abstract void UpdateTerminalState();

        public override RectangleF GetBoundingBox()
        {
            RectangleF rect = new RectangleF(GetLocationInDocumentSpace(), new SizeF());
            rect.Inflate(Size*2, Size*2);
            return rect;
        }

        public override CrystallineControl ParentCrystallineControl
        {
            get
            {
                return ParentAmethystElement != null ? ParentAmethystElement.ParentAmethystControl : null;
            }
            set
            {
                //base.ParentCrystallineControl = value;
            }
        }
    }
}
