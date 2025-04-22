using System;

namespace TsunagiModule.Goap
{
    public struct GoapFloat : GoapValueInterface
    {
        public float value { get; private set; }

        public GoapFloat(float value)
        {
            this.value = value;
        }

        public ValueType type
        {
            get { return ValueType.Float; }
        }

        public float GetAsFloat()
        {
            return value;
        }
    }
}
