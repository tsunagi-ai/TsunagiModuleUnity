using System;

namespace TsunagiModule.Goap
{
    public interface GoapValueInterface
    {
        public Type type { get; }
    }

    /// <summary>
    /// Value used in Goap system.
    /// </summary>
    /// <remarks>
    /// This is implemented for support integrated control of State Control, being independent of value type.
    /// </remarks>
    public interface GoapValueInterface<T> : GoapValueInterface
    {
        public T value { get; set; } // getter and setter for value
    }
}
