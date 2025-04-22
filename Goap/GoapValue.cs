using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Value used in Goap system.
    /// </summary>
    /// <remarks>
    /// This is implemented for support integrated control of State Control, being independent of value type.
    /// </remarks>
    public struct GoapValue
    {
        public enum ValueType
        {
            Int,
            Float,
            Bool
        }

        public float value { get; private set; }
        public ValueType type { get; private set; }

        public GoapValue(float value)
        {
            this.value = value;
            type = ValueType.Float;
        }

        public GoapValue(int value)
        {
            this.value = value;
            type = ValueType.Int;
        }

        public GoapValue(bool value)
        {
            this.value = value ? 1 : 0;
            type = ValueType.Bool;
        }

        public bool GetAsBool()
        {
            return value > 0.5f;
        }

        public int GetAsInt()
        {
            return (int)Math.Round(value);
        }

        public float GetAsFloat()
        {
            if (type == ValueType.Float)
                return value;
            else if (type == ValueType.Int)
                return (int)value;
            else if (type == ValueType.Bool)
                return value > 0 ? 1 : 0;
            else
                throw new NotImplementedException("Unknown GoapValue type.");
        }
    }
}
