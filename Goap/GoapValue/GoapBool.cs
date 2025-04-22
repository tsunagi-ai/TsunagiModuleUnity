using System;

namespace TsunagiModule.Goap
{
    public struct GoapBool : GoapValue
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

        public GoapValue Operate(GoapValue other, Func<GoapValue, GoapValue> operationHandler)
        {
            // type check
            if (other.type != ValueType.Bool)
            {
                throw new InvalidOperationException(
                    "GoapBool can only operate with another GoapBool."
                );
            }

            return operationHandler(other);
        }
    }
}
