using System;

namespace TsunagiModule.Goap
{
    public struct GoapInt : GoapValue
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

        public GoapValue Operate(GoapValue other, Func<GoapValue, GoapValue> operationHandler)
        {
            // type check
            if (other.type != ValueType.Int)
            {
                throw new InvalidOperationException(
                    "GoapInt can only operate with another GoapInt."
                );
            }

            return operationHandler(other);
        }

        public int GetAsInt()
        {
            return value;
        }
    }
}
