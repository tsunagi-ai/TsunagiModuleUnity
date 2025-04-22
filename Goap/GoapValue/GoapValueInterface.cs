using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Value used in Goap system.
    /// </summary>
    /// <remarks>
    /// This is implemented for support integrated control of State Control, being independent of value type.
    /// </remarks>
    public interface GoapValueInterface
    {
        public ValueType type { get; }
        public float GetAsFloat()
        {
            throw new NotImplementedException(
                "GetAsFloat is not implemented for this GoapValue type."
            );
        }
        public int GetAsInt()
        {
            throw new NotImplementedException(
                "GetAsInt is not implemented for this GoapValue type."
            );
        }
        public bool GetAsBool()
        {
            throw new NotImplementedException(
                "GetAsBool is not implemented for this GoapValue type."
            );
        }
    }
}
