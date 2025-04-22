namespace TsunagiModule.Goap
{
    public struct ConditionAnd : ConditionInterface
    {
        public Condition[] conditions { get; private set; }

        public ConditionAnd(Condition[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(State state)
        {
            foreach (Condition condition in conditions)
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
