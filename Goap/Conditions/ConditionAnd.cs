namespace TsunagiModule.Goap
{
    public struct ConditionAnd : ConditionInterface
    {
        public ConditionInterface[] conditions { get; private set; }

        public ConditionAnd(ConditionInterface[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(State state)
        {
            foreach (ConditionInterface condition in conditions)
            {
                if (!condition.IsSatisfied(state))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
