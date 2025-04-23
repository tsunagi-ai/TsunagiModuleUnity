namespace TsunagiModule.Goap
{
    public interface StateDiffInterface
    {
        public string stateIndex { get; }
        public State Operate(State state, bool overwrite = true);
        public float diff { get; }
    }
}
