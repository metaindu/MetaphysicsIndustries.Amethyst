using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;
using System.Drawing;
using MetaphysicsIndustries.Epiphany;

namespace MetaphysicsIndustries.Amethyst
{
    public class IntervalFit2FilterElement : MatrixFilterElement
    {
        public IntervalFit2FilterElement()
            : base(new IntervalFit2FilterNode(), new SizeF(100, 80))
        {
        }

        public new IntervalFit2FilterNode Node
        {
            get { return (IntervalFit2FilterNode)base.Node; }
        }

        protected override void InitTerminals()
        {
            base.InitTerminals();

            TerminalsByConnection[Node.Min].DisplayText = "min";
            TerminalsByConnection[Node.Max].DisplayText = "max";
        }

        public class IntervalFit2FilterNode : MatrixFilterNode
        {
            public IntervalFit2FilterNode()
                : base("Interval Fit 2")
            {
            }

            private InputConnection<double> _min = new InputConnection<double>("Min");
            public InputConnection<double> Min
            {
                get { return _min; }
            }
            private InputConnection<double> _max = new InputConnection<double>("Max");
            public InputConnection<double> Max
            {
                get { return _max; }
            }

            protected override void InitConnections()
            {
                base.InitConnections();

                //sort of the reverse of accumulate and fire
                InputConnectionBases.AddRange(Min, Max);
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                double max = (double)inputs[Max];
                double min = (double)inputs[Min];
                Filter = new IntervalFitBaseMatrixFilter(min, max);

                //accumulate & fire
                base.Execute(inputs, outputs);
            }
        }
    }
}