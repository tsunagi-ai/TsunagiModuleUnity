namespace TsunagiModule.Goap
{
    public interface ConditionInterface
    {
        public bool IsSatisfied(State state);
        public float EstimateDistance(State state);
    }
}
