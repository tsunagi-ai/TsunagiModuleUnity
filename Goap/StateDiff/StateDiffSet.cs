namespace TsunagiModule.Goap
{
    public struct StateDiffSet
    {
        public StateDiffInterface[] stateDiffes;

        public StateDiffSet(StateDiffInterface[] stateDiffes)
        {
            this.stateDiffes = stateDiffes;
        }

        public void Apply(State state)
        {
            foreach (StateDiffInterface stateDiff in stateDiffes)
            {
                stateDiff.Operate(state);
            }
        }
    }
}
