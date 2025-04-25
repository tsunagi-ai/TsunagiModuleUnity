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
        Larger,

        /// <summary>
        /// Represents a comparison where the left-hand side is larger than or equal to the right-hand side.
        /// </summary>
        LargerOrEqual,

        /// <summary>
        /// Represents a comparison where the left-hand side is smaller than the right-hand side.
        /// </summary>
        Smaller,

        /// <summary>
        /// Represents a comparison where the left-hand side is smaller than or equal to the right-hand side.
        /// </summary>
        SmallerOrEqual,

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
