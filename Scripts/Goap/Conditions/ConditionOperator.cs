namespace TsunagiModule.Goap
{
    /// <summary>
    /// Defines the operators used in GOAP conditions for comparison.
    /// </summary>
    public enum ConditionOperator
    {
        /// <summary>
        /// Represents a comparison where the left-hand side is larger than the right-hand side.
        /// </summary>
        Greater,

        /// <summary>
        /// Represents a comparison where the left-hand side is larger than or equal to the right-hand side.
        /// </summary>
        GreaterOrEqual,

        /// <summary>
        /// Represents a comparison where the left-hand side is smaller than the right-hand side.
        /// </summary>
        Less,

        /// <summary>
        /// Represents a comparison where the left-hand side is smaller than or equal to the right-hand side.
        /// </summary>
        LessOrEqual,

        /// <summary>
        /// Represents a comparison where the left-hand side is equal to the right-hand side.
        /// </summary>
        Equal,

        /// <summary>
        /// Represents a comparison where the left-hand side is not equal to the right-hand side.
        /// </summary>
        NotEqual,
    }
}
