namespace TsunagiModule.Goap
{
    public class ConditionOr : ConditionInterface
    {
        public Condition[] conditions { get; private set; }

        public ConditionOr(Condition[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(State state)
        {
            foreach (var condition in conditions)
            {
                if (condition.IsSatisfied(state))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
