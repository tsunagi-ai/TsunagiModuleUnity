namespace TsunagiModule.Goap
{
    public interface StateDiffInterface
    {
        public string stateIndex { get; }
        public void Operate(State state);
    }
}
