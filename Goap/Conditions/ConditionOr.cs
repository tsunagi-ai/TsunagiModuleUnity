using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct ConditionOr : ConditionInterface
    {
        public ConditionInterface[] conditions { get; private set; }

        public ConditionOr(ConditionInterface[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(GoapState state)
        {
            foreach (ConditionInterface condition in conditions)
            {
                if (condition.IsSatisfied(state))
                {
                    return true;
                }
            }
            return false;
        }

        public double EstimateCost(GoapState state, Dictionary<string, double> costPerDiffes = null)
        {
            // shortest distance
            double min = double.MaxValue;
            foreach (ConditionInterface condition in conditions)
            {
                double cost = condition.EstimateCost(state, costPerDiffes: costPerDiffes);
                if (cost < min)
                {
                    min = cost;
                }
            }
            return min;
        }
    }
}
