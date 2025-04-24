using System;
using System.Collections.Generic;
using TsunagiModule.Goap.Utils;

namespace TsunagiModule.Goap
{
    //https://medium.com/@hanxuyang0826/mastering-pathfinding-with-a-star-a-practical-guide-and-c-implementation-f76f1643d8c3
    public partial class GoapSolver
    {
        private class AstarQueue : IComparable<AstarQueue>
        {
            public GoapState state;
            public AstarQueue parent;
            public Action? action;
            public double currentCost;
            public double heuristicCost;
            public double totalCost => currentCost + heuristicCost;
            public int depth => parent == null ? 0 : parent.depth + 1;

            public AstarQueue(
                GoapState state,
                AstarQueue parent,
                double currentCost,
                double heuristicCost,
                Action? action
            )
            {
                this.state = state;
                this.parent = parent;
                this.currentCost = currentCost;
                this.heuristicCost = heuristicCost;
                this.action = action;
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
        private Dictionary<string, double> costPerDiffes = new Dictionary<string, double>();

        public Action[] Solve(GoapState stateCurrent, ConditionInterface goal, int maxLength = 10)
        {
            // compute cost weights
            costPerDiffes = ComputeCostWeights(stateCurrent);

            return SolveAstar(stateCurrent, goal, maxLength);
        }

        private Dictionary<string, double> ComputeCostWeights(GoapState stateCurrent)
        {
            // state index -> cost per diff
            Dictionary<string, double> largestCostPerDiff = new Dictionary<string, double>();
            foreach (string stateIndex in stateCurrent.indices)
            {
                largestCostPerDiff[stateIndex] = 0.0;
            }

            foreach (Action action in actionPool.Values)
            {
                foreach (StateDiffInterface stateDiff in action.stateDiffSet.stateDiffes)
                {
                    // get cost per diff
                    double costPerDiff = Math.Abs(action.cost / stateDiff.diff);
                    if (largestCostPerDiff[stateDiff.stateIndex] < costPerDiff)
                    {
                        largestCostPerDiff[stateDiff.stateIndex] = costPerDiff;
                    }
                }
            }
            return largestCostPerDiff;
        }

        private Action[] SolveAstar(GoapState stateCurrent, ConditionInterface goal, int maxDepth)
        {
            PriorityQueue<AstarQueue> queue = new PriorityQueue<AstarQueue>();
            HashSet<GoapState> closedSet = new HashSet<GoapState>();

            // starting point
            queue.Enqueue(
                new AstarQueue(stateCurrent, null, 0, EstimateCost(stateCurrent, goal), null)
            );

            while (queue.Count > 0)
            {
                // to next node
                AstarQueue current = queue.Dequeue();

                // if already closed...
                if (closedSet.Contains(current.state))
                {
                    // ...skip
                    continue;
                }

                // if arrived at goal...
                if (goal.IsSatisfied(current.state))
                {
                    // ...return the Action path
                    List<Action> actions = new List<Action>();
                    while (current.action != null)
                    {
                        actions.Add(current.action ?? throw new InvalidOperationException());
                        current = current.parent;
                    }
                    actions.Reverse();
                    return actions.ToArray();
                }

                // close the current node
                closedSet.Add(current.state);

                // if not arrived to the max depth...
                if (current.depth < maxDepth)
                {
                    // ...find next nodes
                    foreach (Action action in actionPool.Values)
                    {
                        // if the action is available...
                        if (action.condition.IsSatisfied(stateCurrent))
                        {
                            // ...go to this state
                            GoapState nextState = action.Simulate(current.state, overwrite: false);
                            double costCurrent = current.currentCost + action.cost;
                            double costEstimated = EstimateCost(nextState, goal);
                            queue.Enqueue(
                                new AstarQueue(
                                    nextState,
                                    current,
                                    costCurrent,
                                    costEstimated,
                                    action
                                )
                            );
                        }
                    }
                }
            }

            // Return empty array if no solution is found
            return new Action[0];
        }

        private double EstimateCost(GoapState state, ConditionInterface goal)
        {
            return goal.EstimateCost(state, costPerDiffes: costPerDiffes);
        }
    }
}
