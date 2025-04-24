namespace TsunagiModule.Goap
{
    public struct GoapAction
    {
        public string name { get; private set; }
        public ConditionInterface condition { get; private set; }
        public StateDiffSet stateDiffSet { get; private set; }
        public double cost { get; private set; }

        public GoapAction(
            string name,
            ConditionInterface condition,
            StateDiffSet stateDiffSet,
            double cost
        )
        {
            this.name = name;
            this.condition = condition;
            this.stateDiffSet = stateDiffSet;
            this.cost = cost;
        }

        public bool Available(GoapState state)
        {
            return condition.IsSatisfied(state);
        }

        public GoapState Simulate(GoapState state, bool overwrite = true)
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

            // apply action
            stateTarget = stateDiffSet.Apply(stateTarget, overwrite: true);

            return stateTarget;
        }
    }
}
