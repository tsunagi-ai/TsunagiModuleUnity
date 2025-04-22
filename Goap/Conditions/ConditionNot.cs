namespace TsunagiModule.Goap
{
    public class ConditionNot : ConditionInterface
    {
        public Condition condition { get; private set; }

        public ConditionNot(Condition condition)
        {
            this.condition = condition;
        }

        public bool IsSatisfied(State state)
        {
            return !condition.IsSatisfied(state);
        }
    }
}
