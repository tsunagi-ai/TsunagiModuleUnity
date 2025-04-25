namespace TsunagiModule.Goap
{
    public struct GoapResult
    {
        public GoapAction[] actions;
        public double cost;
        public int length => actions.Length;
        public bool success;

        public GoapResult(GoapAction[] actions, double cost, bool success)
        {
            this.actions = actions;
            this.cost = cost;
            this.success = success;
        }
    }
}
