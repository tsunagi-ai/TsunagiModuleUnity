using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents a condition that is always satisfied
    /// </summary>
    public struct NoCondition : ConditionInterface
    {
        /// <summary>
        /// Determines whether the condition is satisfied.
        /// </summary>
        /// <param name="state">The current GOAP state.</param>
        /// <returns>Always returns true.</returns>
        public bool IsSatisfied(GoapState state)
        {
            return true;
        }

        /// <summary>
        /// Estimates the cost of satisfying the condition.
        /// </summary>
        /// <remarks>
        /// this is always 0 because the condition is always satisfied.
        /// </remarks>
        /// <param name="state">The current GOAP state.</param>
        /// <param name="costPerDiffes">Optional dictionary of costs per state difference.</param>
        /// <returns>Always returns 0.</returns>
        public double EstimateCost(GoapState state, Dictionary<string, double> costPerDiffes)
        {
            return 0;
        }
    }
}
