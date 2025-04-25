namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents an action in the GOAP system.
    /// </summary>
    /// <remarks>
    /// This contains the condition and effect of the action.
    /// The <see cref="GoapSolver"/> will conduct a pathfinding according to these conditions and effects.
    /// </remarks>
    public struct GoapAction
    {
        /// <summary>
        /// The name of the action.
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// The condition that must be satisfied for the action to be available.
        /// </summary>
        public ConditionInterface condition { get; private set; }

        /// <summary>
        /// The set of state differences that this action applies.
        /// </summary>
        public StateDiffSet stateDiffSet { get; private set; }

        /// <summary>
        /// The cost of performing this action.
        /// </summary>
        public double cost { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoapAction"/> struct.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="condition">The condition for the action.</param>
        /// <param name="stateDiffSet">The state differences applied by the action.</param>
        /// <param name="cost">The cost of the action.</param>
        public GoapAction(
            string name,
            ConditionInterface condition,
            StateDiffSet stateDiffSet,
            double cost
        )
        {
            this.name = name;
            this.condition = condition;
            this.stateDiffSet = stateDiffSet;
            this.cost = cost;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoapAction"/> struct with an array of state differences.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="condition">The condition for the action.</param>
        /// <param name="stateDiffSet">The array of state differences applied by the action.</param>
        /// <param name="cost">The cost of the action.</param>
        public GoapAction(
            string name,
            ConditionInterface condition,
            StateDiffInterface[] stateDiffSet,
            double cost
        )
        {
            this.name = name;
            this.condition = condition;
            this.stateDiffSet = new StateDiffSet(stateDiffSet);
            this.cost = cost;
        }

        /// <summary>
        /// Determines whether the action is available given the current state.
        /// </summary>
        /// <param name="state">The current state.</param>
        /// <returns>True if the action is available; otherwise, false.</returns>
        public bool IsAvailable(GoapState state)
        {
            return condition.IsSatisfied(state);
        }

        /// <summary>
        /// Simulates the application of the action on the given state.
        /// </summary>
        /// <param name="state">The current state.</param>
        /// <param name="overwrite">Whether to overwrite the current state or clone it.</param>
        /// <returns>The resulting state after applying the action.</returns>
        public GoapState Simulate(GoapState state, bool overwrite)
        {
            GoapState stateTarget;
            if (overwrite)
            {
                stateTarget = state;
            }
            else
            {
                stateTarget = state.Clone();
            }

            stateTarget = stateDiffSet.Apply(stateTarget, overwrite);

            return stateTarget;
        }
    }
}
