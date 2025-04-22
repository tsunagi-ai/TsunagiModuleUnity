namespace TsunagiModule.Goap
{
    public struct StateDiffSet
    {
        public StateDiffInterface[] stateDiffes;

        public StateDiffSet(StateDiffInterface[] stateDiffes)
        {
            this.stateDiffes = stateDiffes;
        }

        public State Apply(State state, bool overwrite = true)
        {
            // cloning or not
            State stateTarget;
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
