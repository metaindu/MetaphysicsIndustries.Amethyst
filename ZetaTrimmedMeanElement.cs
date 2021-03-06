using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Utilities;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Crystalline;

namespace MetaphysicsIndustries.Amethyst
{
    public class ZetaTrimmedMeanElement : MatrixFilterElement
    {
        public ZetaTrimmedMeanElement()
            : base(new ZetaTrimmedMeanNode(), new SizeV(120, 80))
        {
        }

        public new ZetaTrimmedMeanNode Node
        {
            get { return (base.Node as ZetaTrimmedMeanNode); }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Input].Position = Height / 2;

            TerminalsByConnection[Node.Width].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.Width].DisplayText = "w";
            TerminalsByConnection[Node.Width].Position = Width / 3;

            TerminalsByConnection[Node.Zeta].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.Zeta].DisplayText = "ζ";
            TerminalsByConnection[Node.Zeta].Position = 2 * Width / 3;
        }

        [Serializable]
        public class ZetaTrimmedMeanNode : MatrixFilterNode
        {
            public ZetaTrimmedMeanNode()
                : base("Zeta Trimmed Mean")
            {
            }

            private InputConnection<int> _width = new InputConnection<int>("Window Size");
            public InputConnection<int> Width
            {
                get { return _width; }
            }

            private InputConnection<float> _alpha = new InputConnection<float>("Zeta");
            public InputConnection<float> Zeta
            {
                get { return _alpha; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                InputConnectionBases.Add(Width);
                InputConnectionBases.Add(Zeta);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                int width = (int)inputs[Width];
                float alpha = (float)inputs[Zeta];
                Filter = new ZetaTrimmedMeanMatrixFilter(width, alpha);

                Execute(inputs, outputs, Filter);
            }
        }
    }
}
