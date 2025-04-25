using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents a logical OR condition composed of multiple sub-conditions.
    /// </summary>
    public struct ConditionOr : ConditionInterface
    {
        /// <summary>
        /// The array of sub-conditions that make up this OR condition.
        /// </summary>
        public ConditionInterface[] conditions { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionOr"/> struct.
        /// </summary>
        /// <param name="conditions">The array of sub-conditions.</param>
        public ConditionOr(ConditionInterface[] conditions)
        {
            this.conditions = conditions;
        }

        /// <summary>
        /// Determines whether any of the sub-conditions are satisfied given the current state.
        /// </summary>
        /// <param name="state">The current state.</param>
        /// <returns>True if any sub-condition is satisfied; otherwise, false.</returns>
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

        /// <summary>
        /// Estimates the cost of satisfying this OR condition given the current state.
        /// </summary>
        /// <param name="state">The current state.</param>
        /// <param name="costPerDiffes">Optional dictionary of costs per state difference.</param>
        /// <returns>The estimated cost of satisfying the condition.</returns>
        public double EstimateCost(GoapState state, Dictionary<string, double> costPerDiffes = null)
        {
            // shortest distance
            double min = // shortest distance
            double.MaxValue;
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
