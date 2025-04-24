using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public interface ConditionInterface
    {
        public bool IsSatisfied(GoapState state);
        public double EstimateCost(
            GoapState state,
            Dictionary<string, double> costPerDiffes = null
        );
    }
}
