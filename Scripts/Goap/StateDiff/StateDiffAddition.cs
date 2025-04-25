using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Represents an addition operation for state differences in the GOAP system.
    /// </summary>
    /// <example>
    /// This is example of adding 3 at index0
    /// <code>
    /// new StateDiffAddition("index0", 1)
    /// </code>
    /// </example>
    /// <typeparam name="T">The type of the state values. Must be a struct and implement <see cref="IConvertible"/>, <see cref="IComparable"/>, and <see cref="IEquatable{T}"/>.</typeparam>
    public struct StateDiffAddition<T> : StateDiffInterface
        where T : struct, IConvertible, IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// The index of the state to which this addition applies.
        /// </summary>
        public string stateIndex { get; private set; }

        /// <summary>
        /// The value to be added to the state.
        /// </summary>
        public T additionValue { get; set; }

        /// <summary>
        /// Gets the difference value associated with this addition operation.
        /// </summary>
        public double diff => Convert.ToDouble(additionValue);

        /// <summary>
        /// Initializes a new instance of the <see cref="StateDiffAddition{T}"/> struct.
        /// </summary>
        /// <param name="stateIndex">The index of the state to which this addition applies.</param>
        /// <param name="additionValue">The value to be added to the state.</param>
        public StateDiffAddition(string stateIndex, T additionValue)
        {
            this.stateIndex = stateIndex;
            this.additionValue = additionValue;
        }

        /// <summary>
        /// Applies the addition operation to the given GOAP state.
        /// </summary>
        /// <param name="state">The GOAP state to apply the addition to.</param>
        /// <param name="overwrite">Whether to overwrite the current state or clone it.</param>
        /// <returns>The resulting state after applying the addition.</returns>
        /// <exception cref="ArgumentException">Thrown when there is a type mismatch between the addition operation and the state value type.</exception>
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

                // compute value
                double newValueDouble =
                    Convert.ToDouble(targetValue.value) + Convert.ToDouble(additionValue);

                // type converting
                T newValue;
                if (typeof(T) == typeof(int))
                {
                    // int support
                    newValue = (T)Convert.ChangeType(Math.Round(newValueDouble), typeof(T));
                }
                else
                {
                    newValue = (T)Convert.ChangeType(newValueDouble, typeof(T));
                }

                // update state
                stateTarget.SetRawValue(stateIndex, newValue);
                return stateTarget;
            }
            else
            {
                // ...panic
                throw new ArgumentException(
                    "StateDiffAddition: Type mismatch in addition operation and State value type."
                );
            }
        }
    }
}
