namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents the result of solving a GOAP (Goal-Oriented Action Planning) problem.
    /// </summary>
    public struct GoapResult
    {
        /// <summary>
        /// The sequence of actions that lead to the goal.
        /// </summary>
        public GoapAction[] actions;

        /// <summary>
        /// The total cost of the actions.
        /// </summary>
        public double cost;

        /// <summary>
        /// Gets the number of actions in the result.
        /// </summary>
        public int length => actions.Length;

        /// <summary>
        /// Indicates whether the goal was successfully achieved.
        /// </summary>
        public bool success;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoapResult"/> struct.
        /// </summary>
        /// <param name="actions">The sequence of actions that lead to the goal.</param>
        /// <param name="cost">The total cost of the actions.</param>
        /// <param name="success">Indicates whether the goal was successfully achieved.</param>
        public GoapResult(GoapAction[] actions, double cost, bool success)
        {
            this.actions = actions;
            this.cost = cost;
            this.success = success;
        }
    }
}
