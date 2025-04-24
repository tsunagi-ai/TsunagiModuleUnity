using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public interface ConditionInterface
    {
        public bool IsSatisfied(State state);
        public double EstimateCost(State state, Dictionary<string, double> costPerDiffes = null);
    }
}
