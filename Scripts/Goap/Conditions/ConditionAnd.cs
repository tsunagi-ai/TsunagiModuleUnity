using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents a logical AND condition composed of multiple sub-conditions.
    /// </summary>
    public struct ConditionAnd : ConditionInterface
    {
        /// <summary>
        /// The array of sub-conditions that make up this AND condition.
        /// </summary>
        public ConditionInterface[] conditions { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionAnd"/> struct.
        /// </summary>
        /// <param name="conditions">The array of sub-conditions.</param>
        public ConditionAnd(ConditionInterface[] conditions)
        {
            this.conditions = conditions;
        }

        /// <summary>
        /// Determines whether all sub-conditions are satisfied given the current state.
        /// </summary>
        /// <param name="state">The current GOAP state.</param>
        /// <returns>True if all sub-conditions are satisfied; otherwise, false.</returns>
        public bool IsSatisfied(GoapState state)
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

        /// <summary>
        /// Estimates the cost of satisfying this AND condition given the current state.
        /// </summary>
        /// <remarks>
        /// This method uses the square root of the sum of squares to estimate the cost of the sub-conditions
        /// </remarks>
        /// <param name="state">The current GOAP state.</param>
        /// <param name="costPerDiffes">Optional dictionary of costs per state difference.</param>
        /// <returns>The estimated cost of satisfying the condition.</returns>
        public double EstimateCost(GoapState state, Dictionary<string, double> costPerDiffes = null)
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
