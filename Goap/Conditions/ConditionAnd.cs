using System;
using System.Collections.Generic;

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

        public double EstimateCost(State state, Dictionary<string, double> costPerDiffes = null)
        {
            // square root of sum of squares
            double sum = 0;
            foreach (ConditionInterface condition in conditions)
            {
                double cost = condition.EstimateCost(state, costPerDiffes: costPerDiffes);
                sum += cost * cost;
            }
            return Math.Sqrt(sum);
        }
    }
}
