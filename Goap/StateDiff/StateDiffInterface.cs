namespace TsunagiModule.Goap
{
    public interface StateDiffInterface
    {
        public string stateIndex { get; }
        public State Operate(State state);
    }
}
