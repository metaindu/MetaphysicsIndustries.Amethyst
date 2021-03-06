using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;
using MetaphysicsIndustries.Crystalline;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class AlphaTrimmedDualBellMatrixFilterElement : MatrixFilterElement
    {
        public AlphaTrimmedDualBellMatrixFilterElement()
            : base(new AlphaTrimmedDualBellFilterNode(), new SizeV(100,80))
        {
        }

        public new AlphaTrimmedDualBellFilterNode Node
        {
            get { return (AlphaTrimmedDualBellFilterNode)base.Node; }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            //TerminalsByConnection[Node.WindowSize].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.WindowSize].DisplayText = "ws";
            //TerminalsByConnection[Node.Alpha].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.Alpha].DisplayText = "α";
        }

        [Serializable]
        public class AlphaTrimmedDualBellFilterNode : MatrixFilterNode
        {
            public AlphaTrimmedDualBellFilterNode()
                : base("AT Dual Bell Filter")
            {
            }

            private InputConnection<int> _windowSize = new InputConnection<int>("Window Size");
            public InputConnection<int> WindowSize
            {
                get { return _windowSize; }
            }
            private InputConnection<float> _alpha = new InputConnection<float>("Alpha");
            public InputConnection<float> Alpha
            {
                get { return _alpha; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                InputConnectionBases.AddRange(WindowSize, Alpha);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                float alpha = (float)inputs[Alpha];
                int windowSize = (int)inputs[WindowSize];
                Filter = new AlphaTrimmedDualBellEdgeDetectorMatrixFilter(alpha, windowSize);

                base.Execute(inputs, outputs);
            }
        }
    }
}
