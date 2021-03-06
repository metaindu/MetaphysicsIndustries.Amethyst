using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;
using System.Drawing;
using MetaphysicsIndustries.Crystalline;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class GaussianNoiseMatrixFilterElement : MatrixFilterElement
    {
        public GaussianNoiseMatrixFilterElement()
            : base(new GaussianNoiseMatrixFilterNode(), new SizeV(120, 80))
        {
        }

        public new GaussianNoiseMatrixFilterNode Node
        {
            get { return (GaussianNoiseMatrixFilterNode)base.Node; }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Input].Position = Height / 2;
            TerminalsByConnection[Node.NoiseVariance].Side = BoxOrientation.Up;
            TerminalsByConnection[Node.NoiseVariance].DisplayText = "w";
            TerminalsByConnection[Node.NoiseVariance].Position = Width / 2;
        }

        [Serializable]
        public class GaussianNoiseMatrixFilterNode : MatrixFilterNode
        {
            public GaussianNoiseMatrixFilterNode()
                : base("Gaussian Noise")
            {
            }

            private InputConnection<float> _noiseVariance = new InputConnection<float>("Noise Variance");
            public InputConnection<float> NoiseVariance
            {
                get { return _noiseVariance; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                InputConnectionBases.Add(NoiseVariance);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                float variance = (float)inputs[NoiseVariance];
                Filter = new GaussianNoiseMatrixFilter(variance);

                base.Execute(inputs, outputs);
            }
        }
    }
}
