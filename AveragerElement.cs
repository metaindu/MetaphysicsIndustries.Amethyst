using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    public class AveragerElement : AmethystElement
    {
        public AveragerElement()
            : base(new AveragerNode(), new SizeV(60, 60))
        {
        }

        [Serializable]
        public class AveragerNode : Node
        {
            public AveragerNode()
                : base("μ")
            {
            }

            protected override void InitConnections()
            {
                InputConnectionBases.Add(Input);
                OutputConnectionBases.Add(Output);
            }

            private InputConnectionBase _input = new InputConnection<IEnumerable<float>>("Input");
            public InputConnectionBase Input
            {
                get { return _input; }
            }
            private OutputConnectionBase _output = new OutputConnection<float>("Output");
            public OutputConnectionBase Output
            {
                get { return _output; }
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                IEnumerable<float> input = (IEnumerable<float>)inputs[Input];

                float sum = 0;
                int count = 0;
                foreach (float value in input)
                {
                    sum += value;
                    count++;
                }

                outputs[Output] = sum / count;
            }
        }
    }
}
