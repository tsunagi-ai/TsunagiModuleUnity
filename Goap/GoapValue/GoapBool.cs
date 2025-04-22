using System;

namespace TsunagiModule.Goap
{
    public struct GoapBool : GoapValueInterface
    {
        public bool value { get; private set; }

        public GoapBool(bool value)
        {
            this.value = value;
        }

        public ValueType type
        {
            get { return ValueType.Bool; }
        }

        public bool GetAsBool()
        {
            return value;
        }
    }
}
