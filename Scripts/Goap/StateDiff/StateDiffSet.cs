namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents a collection of state differences and provides functionality to apply them to a GOAP state.
    /// </summary>
    public struct StateDiffSet
    {
        /// <summary>
        /// The array of state differences in this set.
        /// </summary>
        public StateDiffInterface[] stateDiffes;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateDiffSet"/> struct.
        /// </summary>
        /// <param name="stateDiffes">The array of state differences.</param>
        public StateDiffSet(StateDiffInterface[] stateDiffes)
        {
            this.stateDiffes = stateDiffes;
        }

        /// <summary>
        /// Applies all state differences in this set to the given GOAP state.
        /// </summary>
        /// <param name="state">The GOAP state to apply the differences to.</param>
        /// <param name="overwrite">Whether to overwrite the current state or clone it.</param>
        /// <returns>The resulting state after applying the differences.</returns>
        public GoapState Apply(GoapState state, bool overwrite = true)
        {
            // cloning or not
            GoapState stateTarget;
            if (overwrite)
            {
                stateTarget = state;
            }
            else
            {
                stateTarget = state.Clone();
            }

            // apply all operations
            foreach (StateDiffInterface stateDiff in stateDiffes)
            {
                stateDiff.Operate(stateTarget, overwrite: true);
            }

            return stateTarget;
        }
    }
}
