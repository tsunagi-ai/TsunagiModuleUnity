namespace TsunagiModule.Goap
{
    public struct Action
    {
        public string name { get; private set; }
        public ConditionInterface condition { get; private set; }
        public StateDiffSet stateDiffSet { get; private set; }
        public double cost { get; private set; }

        public Action(
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

        public bool Available(State state)
        {
            return condition.IsSatisfied(state);
        }

        public State Simulate(State state, bool overwrite = true)
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

            // apply action
            stateTarget = stateDiffSet.Apply(stateTarget, overwrite: true);

            return stateTarget;
        }
    }
}
