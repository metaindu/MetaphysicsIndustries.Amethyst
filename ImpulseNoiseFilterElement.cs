using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;
using MetaphysicsIndustries.Crystalline;
using System.Drawing;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class ImpulseNoiseFilterElement : MatrixFilterElement
    {
        public ImpulseNoiseFilterElement()
            : base(new ImpulseNoiseFilterNode(), new SizeV(100, 80))
        {
        }

        public new ImpulseNoiseFilterNode Node
        {
            get { return (ImpulseNoiseFilterNode)base.Node; }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Input].Position = Height / 2;
            TerminalsByConnection[Node.ImpulseProbability].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.ImpulseProbability].DisplayText = "p";
            TerminalsByConnection[Node.ImpulseProbability].Position = Width / 2;
        }

        [Serializable]
        public class ImpulseNoiseFilterNode : MatrixFilterNode
        {
            public ImpulseNoiseFilterNode()
                : base("Impulse Noise")
            {
            }

            private InputConnection<float> _impulseProbability = new InputConnection<float>("Impulse Probability");
            public InputConnection<float> ImpulseProbability
            {
                get { return _impulseProbability; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                InputConnectionBases.Add(ImpulseProbability);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                float p = (float)inputs[ImpulseProbability];
                Filter = new ImpulseNoiseMatrixFilter(p);

                base.Execute(inputs, outputs);
            }
        }
    }
}
