namespace TsunagiModule.Goap
{
    public struct ConditionNot : ConditionInterface
    {
        public ConditionInterface condition { get; private set; }

        public ConditionNot(ConditionInterface condition)
        {
            this.condition = condition;
        }

        public bool IsSatisfied(State state)
        {
            return !condition.IsSatisfied(state);
        }
    }
}
