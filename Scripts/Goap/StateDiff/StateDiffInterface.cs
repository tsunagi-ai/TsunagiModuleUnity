namespace TsunagiModule.Goap
{
    /// <summary>
    /// Defines an interface for state difference operations in the GOAP system.
    /// </summary>
    public interface StateDiffInterface
    {
        /// <summary>
        /// Gets the index of the state to which this difference applies.
        /// </summary>
        public string stateIndex { get; }

        /// <summary>
        /// Applies the state difference operation to the given GOAP state.
        /// </summary>
        /// <param name="state">The GOAP state to apply the difference to.</param>
        /// <param name="overwrite">Whether to overwrite the current state or clone it.</param>
        /// <returns>The resulting state after applying the difference.</returns>
        public GoapState Operate(GoapState state, bool overwrite = true);

        /// <summary>
        /// Gets the difference value associated with this operation.
        /// </summary>
        /// <remarks>
        /// This is used to estimate cost weight by <see cref="GoapSolver"/>
        /// </remarks>
        public double diff { get; }
    }
}
