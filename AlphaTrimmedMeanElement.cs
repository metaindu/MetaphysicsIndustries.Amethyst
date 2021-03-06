using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Utilities;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Crystalline;

namespace MetaphysicsIndustries.Amethyst
{
    public class AlphaTrimmedMeanElement : MatrixFilterElement
    {
        public AlphaTrimmedMeanElement()
            : base(new AlphaTrimmedMeanNode(), new SizeV(120,80))
        {
        }

        public new AlphaTrimmedMeanNode Node
        {
            get { return (base.Node as AlphaTrimmedMeanNode); }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Input].Position = Height / 2;

            TerminalsByConnection[Node.Width].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.Width].DisplayText = "w";
            TerminalsByConnection[Node.Width].Position = Width / 3;

            TerminalsByConnection[Node.Alpha].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.Alpha].DisplayText = "α";
            TerminalsByConnection[Node.Alpha].Position = 2 * Width / 3;
        }

        [Serializable]
        public class AlphaTrimmedMeanNode : MatrixFilterNode
        {
            public AlphaTrimmedMeanNode()
                : base("Alpha Trimmed Mean")
            {
            }

            private InputConnection<int> _width = new InputConnection<int>("Window Size");
            public InputConnection<int> Width
            {
                get { return _width; }
            }

            private InputConnection<float> _alpha = new InputConnection<float>("Alpha");
            public InputConnection<float> Alpha
            {
                get { return _alpha; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                InputConnectionBases.Add(Width);
                InputConnectionBases.Add(Alpha);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                int width = (int)inputs[Width];
                float alpha = (float)inputs[Alpha];
                Filter = new AlphaTrimmedMeanMatrixFilter(width, alpha);

                base.Execute(inputs, outputs);
            }
        }
    }
}
