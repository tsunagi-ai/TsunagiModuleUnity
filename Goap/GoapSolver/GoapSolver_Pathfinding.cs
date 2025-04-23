using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    //https://medium.com/@hanxuyang0826/mastering-pathfinding-with-a-star-a-practical-guide-and-c-implementation-f76f1643d8c3
    public partial class GoapSolver
    {
        private class AstarQueue : IComparable<AstarQueue>
        {
            public State state;
            public AstarQueue parent;
            public float currentCost;
            public float heuristicCost;
            public float totalCost => currentCost + heuristicCost;

            public AstarQueue(
                State state,
                AstarQueue parent,
                float currentCost,
                float heuristicCost
            )
            {
                this.state = state;
                this.parent = parent;
                this.currentCost = currentCost;
                this.heuristicCost = heuristicCost;
            }

            public int CompareTo(AstarQueue other)
            {
                if (totalCost < other.totalCost)
                    return -1;
                if (totalCost > other.totalCost)
                    return 1;
                return 0;
            }
        }

        /// <summary>
        /// Cost estimation weights for each state index
        /// </summary>
        private Dictionary<string, float> stateIndexCostWeights = new Dictionary<string, float>();

        public Action[] Solve(State stateCurrent, ConditionInterface goal)
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
