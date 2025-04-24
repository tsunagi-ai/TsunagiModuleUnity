using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Value used in Goap system.
    /// </summary>
    /// <remarks>
    /// This is implemented for support integrated control of State Control, being independent of value type.
    /// </remarks>
    public struct GoapValue<T> : GoapValueInterface, IEquatable<GoapValue<T>>
        where T : struct, IEquatable<T>
    {
        public Type type => typeof(T); // getter for type
        public T value { get; set; } // getter and setter for value

        public GoapValue(T value)
        {
            this.value = value;
        }

        public bool Equals(GoapValue<T> other)
        {
            return value.Equals(other.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
