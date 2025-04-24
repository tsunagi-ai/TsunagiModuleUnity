using System;
using System.Collections.Generic;
using System.Linq;

namespace TsunagiModule.Goap
{
    public struct GoapState : IEquatable<GoapState>
    {
        /// <summary>
        /// Main body of state vector
        /// </summary>
        private Dictionary<string, GoapValueInterface> values { get; set; }

        public string[] indices => values.Keys.ToArray();

        public GoapState(Dictionary<string, GoapValueInterface> values = null)
        {
            if (values == null)
            {
                this.values = new Dictionary<string, GoapValueInterface>();
            }
            else
            {
                this.values = values;
            }
        }

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

        public void SetValue<T>(string stateIndex, T value)
            where T : struct, IEquatable<T>
        {
            // wrapping
            SetValue(stateIndex, new GoapValue<T>(value));
        }

        public bool Equals(GoapState other)
        {
            // HACK: there could be a better data structure
            // since length of dictionary tends not to be too large,
            // this is fast enough.
            return values.SequenceEqual(other.values);
        }

        public override int GetHashCode()
        {
            // HACK: there could be a better data structure
            // since length of dictionary tends not to be too large,
            // hash collision is not likely to happen.

            int hash = 17;
            foreach (var pair in values)
            {
                hash = hash * 31 + pair.Key.GetHashCode();
                hash = hash * 31 + pair.Value.GetHashCode();
            }
            return hash;
        }

        public GoapState Clone()
        {
            // HACK: there could be a better data structure
            return new GoapState { values = new Dictionary<string, GoapValueInterface>(values) };
        }
    }
}
