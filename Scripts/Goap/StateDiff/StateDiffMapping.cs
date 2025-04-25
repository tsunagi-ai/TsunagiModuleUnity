using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents a mapping operation for state differences in the GOAP system.
    /// </summary>
    /// <remarks>
    /// the State value is replaced according to the given mapping dictionary.
    /// The unwritten state value in the dictionary will not be changed.
    /// </remarks>
    /// <typeparam name="T">The type of the state values. Must be a struct and implement <see cref="IEquatable{T}"/>.</typeparam>
    public struct StateDiffMapping<T> : StateDiffInterface
        where T : struct, IEquatable<T>
    {
        /// <summary>
        /// The index of the state to which this mapping applies.
        /// </summary>
        public string stateIndex { get; private set; }

        /// <summary>
        /// The dictionary defining the mapping of state values.
        /// </summary>
        public Dictionary<T, T> mapping { get; set; }

        /// <summary>
        /// Gets the difference value for this mapping operation.
        /// </summary>
        public double diff => 1.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateDiffMapping{T}"/> struct.
        /// </summary>
        /// <param name="stateIndex">The index of the state to which this mapping applies.</param>
        /// <param name="mapping">The dictionary defining the mapping of state values.</param>
        public StateDiffMapping(string stateIndex, Dictionary<T, T> mapping)
        {
            this.stateIndex = stateIndex;
            this.mapping = mapping;
        }

        /// <summary>
        /// Applies the mapping operation to the given GOAP state.
        /// </summary>
        /// <param name="state">The GOAP state to apply the mapping to.</param>
        /// <param name="overwrite">Whether to overwrite the current state or clone it.</param>
        /// <returns>The resulting state after applying the mapping.</returns>
        /// <exception cref="ArgumentException">Thrown when there is a type mismatch between the mapping operation and the state value type.</exception>
        public GoapState Operate(GoapState state, bool overwrite = true)
        {
            // cloning or not
            GoapState stateTarget;
            if (overwrite)
            {
                stateTarget = state;
            }
            else
            {
                stateTarget = state.Clone();
            }

            GoapValueInterface targetValueInterface = stateTarget.GetValue(stateIndex);

            // if both are same type...
            if (targetValueInterface is GoapValue<T> targetValue)
            {
                // ...operate addtion

                T currentValue = targetValue.value;

                // if the current value is in the map...
                if (mapping.TryGetValue(currentValue, out T value))
                {
                    // ...update the state with the mapped value
                    stateTarget.SetRawValue(stateIndex, value);
                }
                // if there isn't...
                else
                {
                    // ...do nothing
                }

                return stateTarget;
            }
            else
            {
                // ...panic
                throw new ArgumentException(
                    "StateDiffMapping: Type mismatch in mapping operation and State value type."
                );
            }
        }
    }
}
