using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Solus;
using MetaphysicsIndustries.Crystalline;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class ThresholdMatrixFilterElement : MatrixFilterElement
    {
        public ThresholdMatrixFilterElement()
            : base(new ThresholdMatrixFilterNode(), new SizeV(100, 80))
        {
        }

        public new ThresholdMatrixFilterNode Node
        {
            get { return (ThresholdMatrixFilterNode)base.Node; }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Input].Position = Height / 2;
            TerminalsByConnection[Node.Threshold].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.Threshold].DisplayText = "t";
            TerminalsByConnection[Node.Threshold].Position = Width / 2;
        }

        [Serializable]
        public class ThresholdMatrixFilterNode : MatrixFilterNode
        {
            public ThresholdMatrixFilterNode()
                : base("Threshold")
            {
            }

            private InputConnection<float> _threshold = new InputConnection<float>("Threshold");
            public InputConnection<float> Threshold
            {
                get { return _threshold; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                InputConnectionBases.Add(Threshold);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                float t = (float)inputs[Threshold];
                Filter = new ThresholdMatrixFilter(t);

                base.Execute(inputs, outputs);
            }

        }
    }
}
