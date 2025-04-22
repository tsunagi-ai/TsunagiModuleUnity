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

        public State Operate(State state)
        {
            // compute value
            int valueState = state.GetValue(stateIndex).GetAsInt();
            int newValue = operationHandler(valueState);

            // update state
            state.SetValue(stateIndex, new GoapInt(newValue));

            return state;
        }
    }
}
