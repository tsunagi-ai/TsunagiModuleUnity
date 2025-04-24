namespace TsunagiModule.Goap
{
    public interface StateDiffInterface
    {
        public string stateIndex { get; }
        public GoapState Operate(GoapState state, bool overwrite = true);
        public float diff { get; }
    }
}
