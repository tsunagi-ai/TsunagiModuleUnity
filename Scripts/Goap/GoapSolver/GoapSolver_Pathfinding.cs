using System;
using System.Collections.Generic;
using TsunagiModule.Goap.Utils;

namespace TsunagiModule.Goap
{
    //https://medium.com/@hanxuyang0826/mastering-pathfinding-with-a-star-a-practical-guide-and-c-implementation-f76f1643d8c3
    /// <summary>
    /// Implements the A* pathfinding algorithm for the GOAP solver.
    /// </summary>
    public partial class GoapSolver
    {
        /// <summary>
        /// Represents a node in the A* search queue.
        /// </summary>
        private class AstarQueue : IComparable<AstarQueue>
        {
            /// <summary>
            /// The current state of the node.
            /// </summary>
            public GoapState state;

            /// <summary>
            /// The parent node in the search path.
            /// </summary>
            public AstarQueue parent;

            /// <summary>
            /// The action that led to this state.
            /// </summary>
            public GoapAction? action;

            /// <summary>
            /// The cost incurred to reach this state.
            /// </summary>
            public double currentCost;

            /// <summary>
            /// The estimated cost to reach the goal from this state.
            /// </summary>
            public double heuristicCost;

            /// <summary>
            /// Gets the total cost (current cost + heuristic cost) for this node.
            /// </summary>
            public double totalCost => currentCost + heuristicCost;

            /// <summary>
            /// Gets the depth of this node in the search tree.
            /// </summary>
            public int depth => parent == null ? 0 : parent.depth + 1;

            /// <summary>
            /// Initializes a new instance of the <see cref="AstarQueue"/> class.
            /// </summary>
            /// <param name="state">The current state of the node.</param>
            /// <param name="parent">The parent node in the search path.</param>
            /// <param name="currentCost">The cost incurred to reach this state.</param>
            /// <param name="heuristicCost">The estimated cost to reach the goal from this state.</param>
            /// <param name="action">The action that led to this state.</param>
            public AstarQueue(
                GoapState state,
                AstarQueue parent,
                double currentCost,
                double heuristicCost,
                GoapAction? action
            )
            {
                this.state = state;
                this.parent = parent;
                this.currentCost = currentCost;
                this.heuristicCost = heuristicCost;
                this.action = action;
            }

            /// <summary>
            /// Compares this node with another node based on their total costs.
            /// </summary>
            /// <param name="other">The other node to compare with.</param>
            /// <returns>A negative value if this node has a lower total cost, a positive value if it has a higher total cost, or zero if the costs are equal.</returns>
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
        /// Cost estimation weights for each state index.
        /// </summary>
        private Dictionary<string, double> costPerDiffes = new Dictionary<string, double>();

        /// <summary>
        /// Solves the GOAP problem using the A* algorithm.
        /// </summary>
        /// <param name="stateCurrent">The current state.</param>
        /// <param name="goal">The goal condition to satisfy.</param>
        /// <param name="maxLength">The maximum depth of the search tree.</param>
        /// <returns>The result of the GOAP problem-solving process.</returns>
        public GoapResult Solve(GoapState stateCurrent, ConditionInterface goal, int maxLength = 10)
        {
            // compute cost weights
            costPerDiffes = ComputeCostWeights(stateCurrent);

            return SolveAstar(stateCurrent, goal, maxLength);
        }

        /// <summary>
        /// Computes the cost weights for each state index based on the current state.
        /// </summary>
        /// <param name="stateCurrent">The current state.</param>
        /// <returns>A dictionary mapping state indices to their cost weights.</returns>
        private Dictionary<string, double> ComputeCostWeights(GoapState stateCurrent)
        {
            // state index -> cost per diff
            Dictionary<string, double> largestCostPerDiff = new Dictionary<string, double>();
            foreach (string stateIndex in stateCurrent.indices)
            {
                largestCostPerDiff[stateIndex] = 0.0;
            }

            foreach (GoapAction action in actionPool.Values)
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

        /// <summary>
        /// Performs the A* search to solve the GOAP problem.
        /// </summary>
        /// <param name="stateCurrent">The current state.</param>
        /// <param name="goal">The goal condition to satisfy.</param>
        /// <param name="maxDepth">The maximum depth of the search tree.</param>
        /// <returns>The result of the A* search process.</returns>
        private GoapResult SolveAstar(GoapState stateCurrent, ConditionInterface goal, int maxDepth)
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

                    // action list
                    List<GoapAction> actions = new List<GoapAction>();
                    while (current.action != null)
                    {
                        actions.Add(current.action ?? throw new InvalidOperationException());
                        current = current.parent;
                    }
                    actions.Reverse();

                    // create GoapResult
                    GoapResult result = new GoapResult(
                        actions.ToArray(),
                        current.currentCost,
                        true
                    );

                    return result;
                }

                // close the current node
                closedSet.Add(current.state);

                // if not arrived to the max depth...
                if (current.depth < maxDepth)
                {
                    // ...find next nodes
                    foreach (GoapAction action in actionPool.Values)
                    {
                        // if the action is available...
                        if (action.condition.IsSatisfied(current.state))
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

            return new GoapResult(null, -1, false);
        }

        /// <summary>
        /// Estimates the cost to satisfy the goal condition from the given state.
        /// </summary>
        /// <param name="state">The current state.</param>
        /// <param name="goal">The goal condition to satisfy.</param>
        /// <returns>The estimated cost to satisfy the goal condition.</returns>
        private double EstimateCost(GoapState state, ConditionInterface goal)
        {
            return goal.EstimateCost(state, costPerDiffes: costPerDiffes);
        }
    }
}
