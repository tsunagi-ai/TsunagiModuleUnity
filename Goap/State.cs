using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public class State
    {
        /// <summary>
        /// Main body of state vector
        /// </summary>
        private Dictionary<string, GoapValue> values { get; set; }

        public GoapValue GetValue(string stateIndex)
        {
            if (values.TryGetValue(stateIndex, out GoapValue value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException($"State index '{stateIndex}' not found.");
            }
        }
    }
}
