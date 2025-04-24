using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct NoCondition : ConditionInterface
    {
        public bool IsSatisfied(GoapState state)
        {
            return true;
        }

        public double EstimateCost(GoapState state, Dictionary<string, double> costPerDiffes = null)
        {
            return 0;
        }
    }
}
