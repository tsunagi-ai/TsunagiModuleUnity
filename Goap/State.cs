using System;
using System.Collections.Generic;
using System.Linq;

namespace TsunagiModule.Goap
{
    public struct State
    {
        /// <summary>
        /// Main body of state vector
        /// </summary>
        private Dictionary<string, GoapValueInterface> values { get; set; }

        public string[] indices => values.Keys.ToArray();

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

        public void SetNumericValue<T>(string stateIndex, T value)
            where T : struct, IConvertible, IComparable, IComparable<T>, IEquatable<T>
        {
            SetValue(stateIndex, new GoapValueNumeric<T>(value));
        }

        public State Clone()
        {
            return new State { values = new Dictionary<string, GoapValueInterface>(values) };
        }
    }
}
