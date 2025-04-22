using System;

namespace TsunagiModule.Goap
{
    public struct StateDiffInt : StateDiffInterface
    {
        public string stateIndex { get; private set; }
        public Func<int, int> operationHandler { get; private set; }

        public StateDiffInt(string stateIndex, Func<int, int> operationHandler)
        {
            this.stateIndex = stateIndex;
            this.operationHandler = operationHandler;
        }

        public State Operate(State state, bool overwrite = true)
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

            // compute value
            int valueState = stateTarget.GetValue(stateIndex).GetAsInt();
            int newValue = operationHandler(valueState);

            // update state
            stateTarget.SetValue(stateIndex, new GoapInt(newValue));

            return stateTarget;
        }
    }
}
