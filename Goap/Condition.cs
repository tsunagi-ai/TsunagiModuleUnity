using System;
using NUnit.Framework;

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
                    Assert.IsTrue(false, "Unknown condition operator.");
                    return false;
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
