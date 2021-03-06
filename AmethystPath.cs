using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Crystalline;

namespace MetaphysicsIndustries.Amethyst
{
    [Serializable]
    public class AmethystPath : Path
    {
        private OutputTerminal _fromTerminal;
        public OutputTerminal FromTerminal
        {
            get { return _fromTerminal; }
            set
            {
                if (_fromTerminal != value)
                {
                    if (_fromTerminal != null)
                    {
                        _fromTerminal.AmethystPaths.Remove(this);
                    }

                    From = null;
                    _fromTerminal = value;
                    if (_fromTerminal != null)
                    {
                        From = _fromTerminal.ParentAmethystElement;
                    }
                    //else
                    //{
                    //    ToTerminal = null;
                    //    ParentCrystallineControl.DisconnectAndRemoveEntity(this);
                    //}

                    if (_fromTerminal != null)
                    {
                        _fromTerminal.AmethystPaths.Add(this);
                    }
                }
            }
        }

        private InputTerminal _toTerminal;
        public InputTerminal ToTerminal
        {
            get { return _toTerminal; }
            set
            {
                if (_toTerminal != value)
                {
                    InputTerminal tempTerminal = _toTerminal;

                    To = null;
                    _toTerminal = value;
                    if (_toTerminal != null)
                    {
                        To = _toTerminal.ParentAmethystElement;
                    }
                    //else
                    //{
                    //    FromTerminal = null;
                    //    ParentCrystallineControl.DisconnectAndRemoveEntity(this);
                    //}

                    if (tempTerminal != null)
                    {
                        tempTerminal.Path = null;
                    }

                    if (_toTerminal != null)
                    {
                        _toTerminal.Path = this;
                    }
                }
            }
        }

        public override void Disconnect(out Entity[] entitiesToRemove)
        {
            if (ToTerminal != null)
            {
                ParentCrystallineControl.InvalidateRectFromEntity(ToTerminal);
            }
            if (FromTerminal != null)
            {
                ParentCrystallineControl.InvalidateRectFromEntity(FromTerminal);
            }

            ToTerminal = null;
            FromTerminal = null;

            base.Disconnect(out entitiesToRemove);
        }
    }
}
