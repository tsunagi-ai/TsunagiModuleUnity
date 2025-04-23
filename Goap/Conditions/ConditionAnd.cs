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

        public double EstimateDistance(State state)
        {
            // square root of sum of squares
            double sum = 0;
            foreach (ConditionInterface condition in conditions)
            {
                double distance = condition.EstimateDistance(state);
                sum += distance * distance;
            }
            return Math.Sqrt(sum);
        }
    }
}
