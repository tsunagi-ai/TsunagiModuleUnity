using System;

namespace TsunagiModule.Goap
{
    public struct ConditionAnd : ConditionInterface
    {
        public ConditionInterface[] conditions { get; private set; }

        public ConditionAnd(ConditionInterface[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(State state)
        {
            foreach (ConditionInterface condition in conditions)
            {
                if (!condition.IsSatisfied(state))
                {
                    return false;
                }
            }
            return true;
        }

        public float EstimateDistance(State state)
        {
            // square root of sum of squares
            float sum = 0;
            foreach (ConditionInterface condition in conditions)
            {
                float distance = condition.EstimateDistance(state);
                sum += distance * distance;
            }
            return (float)Math.Sqrt(sum);
        }
    }
}
