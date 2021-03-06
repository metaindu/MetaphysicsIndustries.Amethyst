using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Crystalline;
using System.Drawing;
using System.Drawing.Drawing2D;
using MetaphysicsIndustries.Epiphany;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class MmseAmethystElement : AmethystElement
    {
        public MmseAmethystElement()
            : this(new MmseNode())
        {
        }

        public MmseAmethystElement(MmseNode node)
            : base(node, new SizeV(180, 80))
        {
        }

        [Serializable]
        public class MmseNode : Node
        {
            public MmseNode()
                : base("MMSE")
            {
            }

            protected override void InitConnections()
            {
                InputConnectionBases.Add(_input);
                InputConnectionBases.Add(_noiseVariance);
                InputConnectionBases.Add(_windowSize);
                InputConnectionBases.Add(_row);
                InputConnectionBases.Add(_column);
                InputConnectionBases.Add(_signalMeanInput);
                InputConnectionBases.Add(_signalVariance);

                OutputConnectionBases.Add(_output);
                OutputConnectionBases.Add(_signalMeanOutput);
                OutputConnectionBases.Add(_window);
            }

            private InputConnectionBase _input = new InputConnection<Matrix>("Input");
            public InputConnectionBase Input
            {
                get { return _input; }
            }

            private InputConnectionBase _noiseVariance = new InputConnection<float>("NoiseVariance");
            public InputConnectionBase NoiseVariance
            {
                get { return _noiseVariance; }
            }

            private InputConnectionBase _windowSize = new InputConnection<int>("WindowSize");
            public InputConnectionBase WindowSize
            {
                get { return _windowSize; }
            }

            private InputConnectionBase _row = new InputConnection<int>("Row");
            public InputConnectionBase Row
            {
                get { return _row; }
            }

            private InputConnectionBase _column = new InputConnection<int>("Column");
            public InputConnectionBase Column
            {
                get { return _column; }
            }

            private InputConnectionBase _signalMeanInput = new InputConnection<float>("SignalMean");
            public InputConnectionBase SignalMeanInput
            {
                get { return _signalMeanInput; }
            }

            private InputConnectionBase _signalVariance = new InputConnection<float>("SignalVariance");
            public InputConnectionBase SignalVariance
            {
                get { return _signalVariance; }
            }

            private OutputConnectionBase _output = new OutputConnection<float>("Output");
            public OutputConnectionBase Output
            {
                get { return _output; }
            }
            private OutputConnectionBase _window = new OutputConnection<Matrix>("Window");
            public OutputConnectionBase Window
            {
                get { return _window; }
            }
            private OutputConnectionBase _signalMeanOutput = new OutputConnection<float>("SignalMean");
            public OutputConnectionBase SignalMeanOutput
            {
                get { return _signalMeanOutput; }
            }


            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        protected override void InitTerminals()
        {
            InputTerminal it;

            it = new InputTerminal(((MmseNode)Node).Input);
            it.Side = BoxOrientation.Left;
            it.Position = Height / 2;
            it.Size *= 2;
            Terminals.Add(it);

            it = new InputTerminal(((MmseNode)Node).NoiseVariance);
            it.Side = BoxOrientation.Down;
            it.Position = 25;
            it.DisplayText = "σn²";
            Terminals.Add(it);

            it = new InputTerminal(((MmseNode)Node).WindowSize);
            it.Side = BoxOrientation.Down;
            it.Position = 40;
            Terminals.Add(it);
            it.DisplayText = "ws";

            it = new InputTerminal(((MmseNode)Node).Row);
            it.Side = BoxOrientation.Down;
            it.Position = 55;
            it.DisplayText = "r";
            Terminals.Add(it);

            it = new InputTerminal(((MmseNode)Node).Column);
            it.Side = BoxOrientation.Down;
            it.Position = 70;
            it.DisplayText = "c";
            Terminals.Add(it);

            it = new InputTerminal(((MmseNode)Node).SignalMeanInput);
            it.Side = BoxOrientation.Up;
            it.Position = Width / 2 - 5;
            it.DisplayText = "μx";
            Terminals.Add(it);

            it = new InputTerminal(((MmseNode)Node).SignalVariance);
            it.Side = BoxOrientation.Up;
            it.Position = Width - 30;
            it.DisplayText = "σx²";
            Terminals.Add(it);

            OutputTerminal ot;

            ot = new OutputTerminal(((MmseNode)Node).Output);
            ot.Side = BoxOrientation.Right;
            ot.Position = Height / 2;
            ot.Size *= 2;
            Terminals.Add(ot);

            ot = new OutputTerminal(((MmseNode)Node).Window);
            ot.Side = BoxOrientation.Up;
            ot.Position = 25;
            ot.DisplayText = "w";
            Terminals.Add(ot);

            ot = new OutputTerminal(((MmseNode)Node).SignalMeanOutput);
            ot.Side = BoxOrientation.Up;
            ot.Position = Width / 2 + 5;
            Terminals.Add(ot);
        }

        //σn²

        //private InputTerminal _input = 
    }
}
