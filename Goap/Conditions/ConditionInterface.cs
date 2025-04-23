namespace TsunagiModule.Goap
{
    public interface ConditionInterface
    {
        public bool IsSatisfied(State state);
        public double EstimateDistance(State state);
    }
}
