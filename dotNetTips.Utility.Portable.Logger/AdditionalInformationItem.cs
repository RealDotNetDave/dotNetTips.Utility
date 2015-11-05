using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.CompilerServices;

namespace dotNetTips.Utility.Portable.Logger
{
    public class AdditionalInformationItem
    {
        private object _lock = RuntimeHelpers.GetObjectValue(new object());
        private string _property;
        private string _text;

        public AdditionalInformationItem(string property, string textValue)
        {
            object expression = this._lock;
            ObjectFlowControl.CheckForSyncLockOnValueType(expression);
            lock (expression)
            {
                this._property = property;
                this._text = textValue;
            }
        }

        public override bool Equals(object obj)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public string Property
        {
            get
            {
                return this._property;
            }
            set
            {
                this._property = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }
}

