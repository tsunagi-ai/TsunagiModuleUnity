using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Value used in Goap system.
    /// </summary>
    /// <remarks>
    /// This is implemented for support integrated control of State Control, being independent of value type.
    /// </remarks>
    public interface GoapValue
    {
        public ValueType type { get; }
        public GoapValue Operate(GoapValue other, Func<GoapValue, GoapValue> operationHandler);
    }
}
