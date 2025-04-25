using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// interface for conditions in the GOAP system.
    /// </summary>
    /// <remarks>
    /// If you want basical conditioning, see <see cref="Condition"/>.
    /// If you DONT want to use the default condition, see <see cref="NoCondition"/>.
    /// </remarks>
    public interface ConditionInterface
    {
        /// <summary>
        /// Determines whether the condition is satisfied given the current state.
        /// </summary>
        /// <param name="state">The current GOAP state.</param>
        /// <returns>True if the condition is satisfied; otherwise, false.</returns>
        public bool IsSatisfied(GoapState state);

        /// <summary>
        /// Estimates the cost of satisfying the condition given the current state.
        /// </summary>
        /// <param name="state">The current GOAP state.</param>
        /// <param name="costPerDiffes">Optional dictionary of costs per state difference.</param>
        /// <returns>The estimated cost of satisfying the condition.</returns>
        public double EstimateCost(
            GoapState state,
            Dictionary<string, double> costPerDiffes = null
        );
    }
}
