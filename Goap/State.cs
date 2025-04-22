using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct State
    {
        /// <summary>
        /// Main body of state vector
        /// </summary>
        private Dictionary<string, GoapValueInterface> values { get; set; }

        public GoapValueInterface GetValue(string stateIndex)
        {
            if (values.TryGetValue(stateIndex, out GoapValueInterface value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException($"State index '{stateIndex}' not found.");
            }
        }

        public void SetValue(string stateIndex, GoapValueInterface value)
        {
            values[stateIndex] = value;
        }

        public State Copy()
        {
            return new State { values = new Dictionary<string, GoapValueInterface>(values) };
        }
    }
}
