namespace TsunagiModule.Goap
{
    public struct ConditionOr : ConditionInterface
    {
        public ConditionInterface[] conditions { get; private set; }

        public ConditionOr(ConditionInterface[] conditions)
        {
            this.conditions = conditions;
        }

        public bool IsSatisfied(State state)
        {
            foreach (ConditionInterface condition in conditions)
            {
                if (condition.IsSatisfied(state))
                {
                    return true;
                }
            }
            return false;
        }

        public float EstimateDistance(State state)
        {
            // shortest distance
            float min = float.MaxValue;
            foreach (ConditionInterface condition in conditions)
            {
                float distance = condition.EstimateDistance(state);
                if (distance < min)
                {
                    min = distance;
                }
            }
            return min;
        }
    }
}
