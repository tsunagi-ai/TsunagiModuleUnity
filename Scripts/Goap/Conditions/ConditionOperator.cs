namespace TsunagiModule.Goap
{
    /// <summary>
    /// Defines the comparing method in <see cref="Condition"/>
    /// </summary>
    public enum ConditionOperator
    {
        /// <summary>
        /// StateValue > ConditionValue
        /// </summary>
        Greater,

        /// <summary>
        /// StateValue >= ConditionValue
        /// </summary>
        GreaterOrEqual,

        /// <summary>
        /// StateValue < ConditionValue
        /// </summary>
        Less,

        /// <summary>
        /// StateValue <= ConditionValue
        /// </summary>
        LessOrEqual,

        /// <summary>
        /// StateValue == ConditionValue
        /// </summary>
        Equal,

        /// <summary>
        /// StateValue != ConditionValue
        /// </summary>
        NotEqual,
    }
}
