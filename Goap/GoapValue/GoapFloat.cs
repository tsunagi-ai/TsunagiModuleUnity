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

        public GoapValueInterface Operate(
            GoapValueInterface other,
            Func<GoapValueInterface, GoapValueInterface, GoapValueInterface> operationHandler
        )
        {
            // type check
            if (other.type != ValueType.Float)
            {
                throw new InvalidOperationException(
                    "GoapFloat can only operate with another GoapFloat."
                );
            }

            return operationHandler(this, other);
        }

        public float GetAsFloat()
        {
            return value;
        }
    }
}
