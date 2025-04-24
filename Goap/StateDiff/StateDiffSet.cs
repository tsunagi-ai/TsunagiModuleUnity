namespace TsunagiModule.Goap
{
    public struct StateDiffSet
    {
        public StateDiffInterface[] stateDiffes;

        public StateDiffSet(StateDiffInterface[] stateDiffes)
        {
            this.stateDiffes = stateDiffes;
        }

        public GoapState Apply(GoapState state, bool overwrite = true)
        {
            // cloning or not
            GoapState stateTarget;
            if (overwrite)
            {
                stateTarget = state;
            }
            else
            {
                stateTarget = state.Clone();
            }

            // apply all operations
            foreach (StateDiffInterface stateDiff in stateDiffes)
            {
                stateDiff.Operate(stateTarget, overwrite: true);
            }

            return stateTarget;
        }
    }
}
