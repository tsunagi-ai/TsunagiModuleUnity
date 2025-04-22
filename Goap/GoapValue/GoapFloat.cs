using System;

namespace TsunagiModule.Goap
{
    public struct GoapFloat : GoapValue
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

        public GoapValue Operate(GoapValue other, Func<GoapValue, GoapValue> operationHandler)
        {
            // type check
            if (other.type != ValueType.Float)
            {
                throw new InvalidOperationException(
                    "GoapFloat can only operate with another GoapFloat."
                );
            }

            return operationHandler(other);
        }
    }
}
