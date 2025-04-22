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
            this.value = ValueConverter.ToFloat(value);
            type = ValueType.Int;
        }

        public GoapValue(bool value)
        {
            this.value = ValueConverter.ToFloat(value);
            type = ValueType.Bool;
        }

        public bool GetAsBool()
        {
            return ValueConverter.ToBool(value);
        }

        public int GetAsInt()
        {
            return ValueConverter.ToInt(value);
        }

        public float GetAsFloat()
        {
            return value;
        }
    }
}
