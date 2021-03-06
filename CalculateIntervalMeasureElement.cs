using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;
using System.Drawing;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Collections;
using MetaphysicsIndustries.Acuity;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class CalculateIntervalMeasureElement : AmethystElement
    {
        public CalculateIntervalMeasureElement()
            : base(new CalculateIntervalMeasureNode(), new SizeV(80,60))
        {
        }

        public new CalculateIntervalMeasureNode Node
        {
            get { return (CalculateIntervalMeasureNode)base.Node; }
        }

        [Serializable]
        public class CalculateIntervalMeasureNode : Node
        {
            public CalculateIntervalMeasureNode()
                : base("Calc Interval")
            {
            }

            private InputConnection<Matrix> _input = new InputConnection<Matrix>("Input");
            public InputConnection<Matrix> Input
            {
                get { return _input; }
            }

            private OutputConnection<float> _min = new OutputConnection<float>("Min");
            public OutputConnection<float> Min
            {
                get { return _min; }
            }

            private OutputConnection<float> _max = new OutputConnection<float>("Max");
            public OutputConnection<float> Max
            {
                get { return _max; }
            }

            protected override void InitConnections()
            {
                InputConnectionBases.Add(Input);
                OutputConnectionBases.AddRange(Min, Max);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                Matrix input = (Matrix)inputs[Input];

                Pair<float> ret = IntervalFitMatrixFilter.CalcInterval(input);

                outputs[Min] = ret.First;
                outputs[Max] = ret.Second;
            }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Min].DisplayText = "min";
            TerminalsByConnection[Node.Max].DisplayText = "max";
        }
    }
}
