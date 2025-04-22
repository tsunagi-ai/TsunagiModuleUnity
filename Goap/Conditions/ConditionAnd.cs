namespace TsunagiModule.Goap
{
    public class ConditionAnd : ConditionInterface
    {
        public Condition[] conditions { get; private set; }

        public ConditionAnd(Condition[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(State state)
        {
            foreach (var condition in conditions)
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
