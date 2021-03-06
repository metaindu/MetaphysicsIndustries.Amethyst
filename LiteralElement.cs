using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MetaphysicsIndustries.Epiphany;
using System.Runtime.Serialization;
using MetaphysicsIndustries.Utilities;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Amethyst
{
    //public interface INodeValueChangedReceiver
    //{
    //    void OnValueChanged(object sender, EventArgs e);
    //}

    [Serializable]
    public abstract class LiteralElement<T> : AmethystElement, IDeserializationCallback//, INodeValueChangedReceiver
        where T : IEquatable<T>
    {
        public LiteralElement(LiteralNode node, SizeV size)
            : base(node, size)
        {
            node.ValueChanged += new EventHandler(node_ValueChanged);
            //node.NodeValueChangeReceiver = this;
        }

        [Serializable]
        public class LiteralNode : Node
        {
            //protected LiteralNode() //required for serialization
            //{
            //}

            public LiteralNode(string name)
                : base(name)
            {
            }

            protected override void InitConnections()
            {
                OutputConnectionBases.Add(Output);
            }

            protected override void InitDependencies()
            {
            }

            private T _value;
            public T Value
            {
                get { return _value; }
                set
                {
                    if ((_value == null && value != null) ||
                        !_value.Equals(value))
                    {
                        _value = value;

                        OnValueChanged(new EventArgs());
                    }
                }
            }

            //private INodeValueChangedReceiver _nodeValueChangeReceiver;

            //public INodeValueChangedReceiver NodeValueChangeReceiver
            //{
            //    get { return _nodeValueChangeReceiver; }
            //    set { _nodeValueChangeReceiver = value; }
            //}

            protected void OnValueChanged(EventArgs e)
            {
                if (ValueChanged != null)
                {
                    ValueChanged(this, e);
                }
                //if (NodeValueChangeReceiver != null)
                //{
                //    NodeValueChangeReceiver.OnValueChanged(this, e);
                //}
            }

            [field: NonSerialized]
            public event EventHandler ValueChanged;
            //

            private OutputConnection<T> _output = new OutputConnection<T>("Output");
            public OutputConnection<T> Output
            {
                get { return _output; }
                set { _output = value; }
            }

            public override void Execute(Dictionary<InputConnectionBase, object> inputs, Dictionary<OutputConnectionBase, object> outputs)
            {
                outputs[Output] = Value;
            }
        }

        public virtual LiteralNode Node2
        {
            get { return (LiteralNode)base.Node; }
        }

        public T Value
        {
            get { return ((LiteralNode)Node).Value; }
            set { ((LiteralNode)Node).Value = value; }
        }

        public override string Text
        {
            get
            {
                return Value.ToString();
            }
        }

        protected abstract T ConvertString(string value);
        protected abstract bool IsConvertable(string value);

        public override bool ShallProcessDoubleClick
        {
            get
            {
                return true;
            }
        }

        public override void ProcessDoubleClick(AmethystControl control)
        {
            StringEditorForm form = new StringEditorForm();
            form.Value = Value.ToString();
            if (typeof(T) != typeof(string))
            {
                form.Multiline = false;
            }
            if (form.ShowDialog(control) == DialogResult.OK && IsConvertable(form.Value))
            {
                Value = ConvertString(form.Value);
            }
        }

        void node_ValueChanged(object sender, EventArgs e)
        {
            if (ParentAmethystControl != null)
            {
                foreach (OutputTerminal terminal in Collection.Extract<Terminal, OutputTerminal>(Terminals))
                {
                    ParentAmethystControl.RemoveFromValueCache(terminal);
                }
            }
        }

        //public void OnValueChanged(object sender, EventArgs e)
        //{
        //    node_ValueChanged(sender, e);
        //}

        #region IDeserializationCallback Members

        public void OnDeserialization(object sender)
        {
            Node2.ValueChanged += new EventHandler(node_ValueChanged);
        }

        #endregion
    }
}
