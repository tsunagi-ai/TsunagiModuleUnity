using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public partial class GoapSolver
    {
        /// <summary>
        /// Cost estimation weights for each state index
        /// </summary>
        private Dictionary<string, float> stateIndexCostWeights = new Dictionary<string, float>();

        public Action[] Solve(State stateCurrent, Condition goal)
        {
            // compute cost weights
            stateIndexCostWeights = ComputeCostWeights(stateCurrent);

            throw new NotImplementedException("Solve method not implemented yet.");
        }

        private Dictionary<string, float> ComputeCostWeights(State stateCurrent)
        {
            // state index -> cost per diff
            Dictionary<string, float> largestCostPerDiff = new Dictionary<string, float>();
            foreach (string stateIndex in stateCurrent.indices)
            {
                largestCostPerDiff[stateIndex] = 0f;
            }

            foreach (Action action in actionPool.Values)
            {
                foreach (StateDiffInterface stateDiff in action.stateDiffSet.stateDiffes)
                {
                    // get cost per diff
                    float costPerDiff = Math.Abs(action.cost / stateDiff.diff);
                    if (largestCostPerDiff[stateDiff.stateIndex] < costPerDiff)
                    {
                        largestCostPerDiff[stateDiff.stateIndex] = costPerDiff;
                    }
                }
            }
            return largestCostPerDiff;
        }
    }
}
