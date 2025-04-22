using System;

namespace TsunagiModule.Goap
{
    public abstract class Condition
    {
        public enum ConditionOperator
        {
            Larger,
            LargerOrEqual,
            Smaller,
            SmallerOrEqual,
            Equal,
        }

        /// <summary>
        /// error acceptance value for float comparison.
        /// </summary>
        private const float EPSILON = 0.0001f;

        public string name { get; private set; }
        public GoapValue value { get; private set; }
        public ConditionOperator conditionOperator { get; private set; }

        public bool IsSatisfied(float valueComparing)
        {
            switch (value.type)
            {
                case GoapValue.ValueType.Float:
                    return IsSatisfiedFloat(valueComparing);
                case GoapValue.ValueType.Int:
                    return IsSatisfiedInt(ValueConverter.ToInt(valueComparing));
                case GoapValue.ValueType.Bool:
                    return IsSatisfiedBool(ValueConverter.ToBool(valueComparing));
                default:
                    throw new NotImplementedException("Unknown GoapValue.ValueType");
            }
        }

        private bool IsSatisfiedFloat(float valueComparing)
        {
            switch (conditionOperator)
            {
                case ConditionOperator.Larger:
                    return value.GetAsFloat() < valueComparing;
                case ConditionOperator.LargerOrEqual:
                    return value.GetAsFloat() <= valueComparing;
                case ConditionOperator.Smaller:
                    return value.GetAsFloat() > valueComparing;
                case ConditionOperator.SmallerOrEqual:
                    return value.GetAsFloat() >= valueComparing;
                case ConditionOperator.Equal:
                    return IsEqualApproximately(value.GetAsFloat(), valueComparing);
                default:
                    throw new NotImplementedException("Unknown condition operator.");
            }
        }

        private bool IsSatisfiedInt(int valueComparing)
        {
            switch (conditionOperator)
            {
                case ConditionOperator.Larger:
                    return value.GetAsInt() < valueComparing;
                case ConditionOperator.LargerOrEqual:
                    return value.GetAsInt() <= valueComparing;
                case ConditionOperator.Smaller:
                    return value.GetAsInt() > valueComparing;
                case ConditionOperator.SmallerOrEqual:
                    return value.GetAsInt() >= valueComparing;
                case ConditionOperator.Equal:
                    return value.GetAsInt() == valueComparing;
                default:
                    throw new NotImplementedException("Unknown condition operator.");
            }
        }

        private bool IsSatisfiedBool(bool valueComparing)
        {
            switch (conditionOperator)
            {
                case ConditionOperator.Equal:
                    return value.GetAsBool() == valueComparing;
                case ConditionOperator.Larger:
                case ConditionOperator.LargerOrEqual:
                case ConditionOperator.Smaller:
                case ConditionOperator.SmallerOrEqual:
                    throw new NotImplementedException(
                        "Condition operator is not supported for bool type."
                    );
                default:
                    throw new NotImplementedException("Unknown condition operator.");
            }
        }

        /// <summary>
        /// Equality check for float values.
        /// </summary>
        private bool IsEqualApproximately(float a, float b)
        {
            return Math.Abs(a - b) < EPSILON;
        }
    }
}
