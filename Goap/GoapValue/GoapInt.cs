using System;

namespace TsunagiModule.Goap
{
    public struct GoapInt : GoapValueInterface
    {
        public int value { get; private set; }

        public GoapInt(int value)
        {
            this.value = value;
        }

        public ValueType type
        {
            get { return ValueType.Int; }
        }

        public int GetAsInt()
        {
            return value;
        }
    }
}
