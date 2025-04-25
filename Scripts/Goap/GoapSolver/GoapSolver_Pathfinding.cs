using System;
using System.Collections.Generic;
using TsunagiModule.Goap.Utils;

namespace TsunagiModule.Goap
{
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
            /// simulated State
            /// </summary>
            public GoapState state;

            /// <summary>
            /// previous queue
            /// </summary>
            public AstarQueue parent;
            public GoapAction? action;
            public double currentCost;
            public double estimatedCost;
            public double totalCost => currentCost + estimatedCost;
            public int depth => parent == null ? 0 : parent.depth + 1;

            public AstarQueue(
                GoapState state,
                AstarQueue parent,
                double currentCost,
                double estimatedCost,
                GoapAction? action
            )
            {
                this.state = state;
                this.parent = parent;
                this.currentCost = currentCost;
                this.estimatedCost = estimatedCost;
                this.action = action;
            }

            // for sorting
            public int CompareTo(AstarQueue other)
            {
                if (totalCost < other.totalCost)
                {
                    return -1;
                }

                if (totalCost > other.totalCost)
                {
                    return 1;
                }

                return 0;
            }
        }

        /// <summary>
        /// Cost estimation weights for each state index.
        /// This is simply multipied with the diff from the goal of State
        /// </summary>
        private Dictionary<string, double> costPerDiffes = new Dictionary<string, double>();

        /// <summary>
        /// Solves the GOAP problem using the A* algorithm.
        /// </summary>
        /// <param name="stateCurrent">The current state.</param>
        /// <param name="goal">The goal condition to satisfy.</param>
        /// <param name="maxLength">The maximum depth of the search tree.
        /// If the search exceeds this depth, it will terminate early.</param>
        /// <returns>The result of the GOAP problem-solving process.</returns>
        public GoapResult Solve(GoapState stateCurrent, ConditionInterface goal, int maxLength)
        {
            // compute cost weights
            costPerDiffes = ComputeCostWeights(stateCurrent);

            return SolveAstar(stateCurrent, goal, maxLength);
        }

        /// <summary>
        /// Computes the cost weights for each state index based on the current state.
        /// </summary>
        /// <remarks>
        /// This assume that the weight should be the largest costPerDiff about each State values
        /// </remarks>
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
        /// <seealso href="https://medium.com/@hanxuyang0826/mastering-pathfinding-with-a-star-a-practical-guide-and-c-implementation-f76f1643d8c3">
        /// Implementation of A* Pathfinding
        /// </seeslso>
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
                    // ...early return result
                    return CreateSuccessResult(current);
                }

                // close the current node
                closedSet.Add(current.state);

                // if not arrived to the max depth...
                if (current.depth < maxDepth)
                {
                    // ...enqueue next nodes
                    EnqueueNextActions(queue, current, goal);
                }
            }

            return CreateFailureResult();
        }

        private void EnqueueNextActions(
            PriorityQueue<AstarQueue> queue,
            AstarQueue current,
            ConditionInterface goal
        )
        {
            // ...find next nodes
            foreach (GoapAction action in actionPool.Values)
            {
                // if the action is available...
                if (action.IsAvailable(current.state))
                {
                    // ...go to this state
                    GoapState nextState = action.Simulate(current.state, false);
                    double costCurrent = current.currentCost + action.cost;
                    double costEstimated = EstimateCost(nextState, goal);
                    queue.Enqueue(
                        new AstarQueue(nextState, current, costCurrent, costEstimated, action)
                    );
                }
            }
        }

        private GoapResult CreateSuccessResult(AstarQueue latestQueue)
        {
            // action list
            List<GoapAction> actions = new List<GoapAction>();
            while (latestQueue.action != null)
            {
                actions.Add(latestQueue.action ?? throw new InvalidOperationException());
                latestQueue = latestQueue.parent;
            }
            actions.Reverse();

            // create GoapResult
            return new GoapResult(actions.ToArray(), latestQueue.currentCost, true);
        }

        private GoapResult CreateFailureResult()
        {
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
