namespace TsunagiModule.Goap
{
    public struct Action
    {
        public string name { get; private set; }
        public ConditionInterface condition { get; private set; }
        public StateDiffSet stateDiffSet { get; private set; }
        public float cost { get; private set; }

        public Action(
            string name,
            ConditionInterface condition,
            StateDiffSet stateDiffSet,
            float cost
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
    }
}
