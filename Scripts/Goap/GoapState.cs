using System;
using System.Collections.Generic;
using System.Linq;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents the state in the GOAP (Goal-Oriented Action Planning) system.
    /// </summary>
    /// <remarks>
    /// You need to write what is happening in the game world into this <see cref="GoapState"/>.
    /// </remarks>
    public struct GoapState : IEquatable<GoapState>
    {
        /// <summary>
        /// The main body of the state vector, storing state values by their indices.
        /// </summary>
        private Dictionary<string, GoapValueInterface> values { get; set; }

        /// <summary>
        /// Gets the indices of all state values.
        /// </summary>
        public string[] indices => values.Keys.ToArray();

        /// <summary>
        /// Initializes a new instance of the <see cref="GoapState"/> struct.
        /// </summary>
        /// <param name="values">The dictionary of state values.</param>
        public GoapState(Dictionary<string, GoapValueInterface> values)
        {
            this.values = values;
        }

        /// <summary>
        /// Retrieves the value associated with the specified state index.
        /// </summary>
        /// <param name="stateIndex">The index of the state value to retrieve.</param>
        /// <returns>The value associated with the specified state index.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the specified state index is not found.</exception>
        public GoapValueInterface GetValue(string stateIndex)
        {
            GuardValueNull();

            if (values.TryGetValue(stateIndex, out GoapValueInterface value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException($"State index '{stateIndex}' not found.");
            }
        }

        /// <summary>
        /// Sets the value for the specified state index.
        /// </summary>
        /// <param name="stateIndex">The index of the state value to set.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(string stateIndex, GoapValueInterface value)
        {
            GuardValueNull();

            values[stateIndex] = value;
        }

        /// <summary>
        /// Sets the raw value for the specified state index.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="stateIndex">The index of the state value to set.</param>
        /// <param name="value">The raw value to set.</param>
        public void SetRawValue<T>(string stateIndex, T value)
            where T : struct, IEquatable<T>
        {
            // wrapping
            SetValue(stateIndex, new GoapValue<T>(value));
        }

        /// <summary>
        /// Determines whether the current state is equal to another state.
        /// </summary>
        /// <param name="other">The other state to compare with.</param>
        /// <returns>True if the states are equal; otherwise, false.</returns>
        public bool Equals(GoapState other)
        {
            GuardValueNull();

            // HACK: there could be a better data structure
            // since length of dictionary tends not to be too large,
            // this is fast enough.
            return values.SequenceEqual(other.values);
        }

        /// <summary>
        /// Returns the hash code for the current state.
        /// </summary>
        /// <returns>The hash code for the current state.</returns>
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

        /// <summary>
        /// Creates a clone of the current state.
        /// </summary>
        /// <returns>A new instance of <see cref="GoapState"/> that is a clone of the current state.</returns>
        public GoapState Clone()
        {
            // HACK: there could be a better data structure
            return new GoapState { values = new Dictionary<string, GoapValueInterface>(values) };
        }

        /// <summary>
        /// Ensures that the state values dictionary is not null.
        /// </summary>
        private void GuardValueNull()
        {
            if (values == null)
            {
                values = new Dictionary<string, GoapValueInterface>();
            }
        }
    }
}
