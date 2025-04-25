using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Basic conditioning in the GOAP system.
    /// Compares a state value with a target value using a specified operator.
    /// </summary>
    /// <remarks>
    /// Available conditioning method is listed in the <see cref="ConditionOperator"/> enum.
    /// </remarks>
    /// <example>
    /// This means (> 5)
    /// <code>
    /// new Condition("correspondingindex", ConditionOperator.Greater, 5)
    /// </code>
    /// </example>
    /// <typeparam name="T">This must be the same type as the value in the State.</typeparam>
    public struct Condition<T> : ConditionInterface
        where T : struct, IEquatable<T>
    {
        /// <summary>
        /// The index of the state value to compare.
        /// </summary>
        public string stateIndex { get; private set; }

        /// <summary>
        /// The value to compare against.
        /// </summary>
        public T valueComparing { get; set; }

        /// <summary>
        /// Conditioning method
        /// </summary>
        public ConditionOperator conditionOperator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Condition{T}"/> struct.
        /// </summary>
        /// <param name="stateIndex">The index of the state value to compare.</param>
        /// <param name="conditionOperator">Conditioning method</param>
        /// <param name="valueComparing">Value to compare against.</param>
        public Condition(string stateIndex, ConditionOperator conditionOperator, T valueComparing)
        {
            this.stateIndex = stateIndex;
            this.valueComparing = valueComparing;
            this.conditionOperator = conditionOperator;
        }

        /// <summary>
        /// Determines whether the condition is satisfied given the current state.
        /// </summary>
        /// <param name="state">The current GOAP state. This class will read the state value associated with the stateIndex.</param>
        /// <returns>True if the condition is satisfied; otherwise, false.</returns>
        /// <exception cref="InvalidCastException">Thrown when the state value type does not match the expected type.</exception>
        public bool IsSatisfied(GoapState state)
        {
            GoapValueInterface valueGivenInterface = state.GetValue(stateIndex);

            // if type check passed...
            if (valueGivenInterface is GoapValue<T> valueGiven)
            {
                // ...compare value
                return Compare(valueGiven.value, valueComparing);
            }
            // if different type...
            else
            {
                // ...panic
                throw new InvalidCastException(
                    $"State index '{stateIndex}' is not of type '{typeof(T)}'."
                );
            }
        }

        /// <summary>
        /// Estimates the cost of satisfying the condition given the current state.
        /// </summary>
        /// <remarks>
        /// If boolean, the diff is set as 1.0.
        /// If numerical (<see cref="IConvertible"/>), the diff is the absolute difference of them.
        /// If others, the diff assumed to be 1.0 if not equal.
        /// </remarks>
        /// <param name="state">The current GOAP state. This class will read the state value associated with the stateIndex.</param>
        /// <param name="costPerDiffes">Optional dictionary of costs per state difference. If null, this will assume as 1.0</param>
        /// <returns>The estimated cost of satisfying the condition.</returns>
        /// <exception cref="InvalidCastException">Thrown when the state value type does not match the expected type.</exception>
        public double EstimateCost(GoapState state, Dictionary<string, double> costPerDiffes = null)
        {
            // if already satisfied...
            if (IsSatisfied(state))
            {
                // ... early return
                return 0.0;
            }

            // not satisfied!

            // get weight
            double costPerDiff = 1.0;
            // if corresponding weight given...
            if (
                (costPerDiffes != null)
                && costPerDiffes.TryGetValue(stateIndex, out double weightFound)
            )
            {
                // ...use it
                costPerDiff = weightFound;
            }

            // compute distance
            double distance = 0.0;
            GoapValueInterface valueGivenInterface = state.GetValue(stateIndex);
            // if type check passed...
            if (valueGivenInterface is GoapValue<T> valueGiven)
            {
                // bool
                if (valueGiven.value is bool)
                {
                    distance = 1.0;
                }
                // numeric
                else if (valueGiven.value is IConvertible)
                {
                    // convert to double
                    double valueGivenDouble = Convert.ToDouble(valueGiven.value);
                    double valueComparingDouble = Convert.ToDouble(valueComparing);

                    // compute distance
                    distance = Math.Abs(valueGivenDouble - valueComparingDouble);
                }
                // unknown
                else
                {
                    distance = 1.0;
                }
            }
            // if different type...
            else
            {
                // ...panic
                throw new InvalidCastException(
                    $"State index '{stateIndex}' is not of type '{typeof(T)}'."
                );
            }

            return distance * costPerDiff;
        }

        /// <summary>
        /// Compares the given value with the target value using the specified operator.
        /// </summary>
        /// <remarks>
        /// This function routes to corresponding comparison method.
        /// </remarks>
        /// <param name="valueGiven">The value to compare.</param>
        /// <param name="valueComparing">The target value to compare against.</param>
        /// <returns>True if the comparison is successful; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the operator is not supported for the value type.</exception>
        /// <exception cref="NotImplementedException">Thrown when the operator is not implemented.</exception>
        private bool Compare(T valueGiven, T valueComparing)
        {
            // delegate to corresponding comparison method
            switch (conditionOperator)
            {
                case ConditionOperator.Equal:
                case ConditionOperator.NotEqual:
                    // if operation available...
                    if (valueGiven is IEquatable<T> valueGivenEquatable)
                    {
                        // ...compare value
                        return CompareEquatable(valueGivenEquatable, valueComparing);
                    }
                    else
                    {
                        // ...panic
                        throw new InvalidOperationException(
                            $"Condition operator '{conditionOperator}' is not supported for type '{typeof(T)}'."
                        );
                    }
                case ConditionOperator.Greater:
                case ConditionOperator.GreaterOrEqual:
                case ConditionOperator.Less:
                case ConditionOperator.LessOrEqual:
                    // if operation available...
                    if (valueGiven is IComparable<T> valueGivenComparable)
                    {
                        // ...compare value
                        return CompareComparable(valueGivenComparable, valueComparing);
                    }
                    else
                    {
                        // ...panic
                        throw new InvalidOperationException(
                            $"Condition operator '{conditionOperator}' is not supported for type '{typeof(T)}'."
                        );
                    }

                // unknown
                default:
                    // panic
                    throw new NotImplementedException(
                        $"Condition operator '{conditionOperator}' not implemented yet."
                    );
            }
        }

        /// <summary>
        /// Compares two values using equatable operators.
        /// </summary>
        /// <param name="valueGivenEquatable">The value to compare.</param>
        /// <param name="valueComparing">The target value to compare against.</param>
        /// <returns>True if the comparison is successful; otherwise, false.</returns>
        /// <exception cref="NotImplementedException">Thrown when the operator is not implemented.</exception>
        private bool CompareEquatable(IEquatable<T> valueGivenEquatable, T valueComparing)
        {
            switch (conditionOperator)
            {
                case ConditionOperator.Equal:
                    return valueGivenEquatable.Equals(valueComparing);
                case ConditionOperator.NotEqual:
                    return !valueGivenEquatable.Equals(valueComparing);
                default:
                    throw new NotImplementedException(
                        $"Condition operator '{conditionOperator}' not implemented yet."
                    );
            }
        }

        /// <summary>
        /// Compares two values using comparable operators.
        /// </summary>
        /// <param name="valueGivenComparable">The value to compare.</param>
        /// <param name="valueComparing">The target value to compare against.</param>
        /// <returns>True if the comparison is successful; otherwise, false.</returns>
        /// <exception cref="NotImplementedException">Thrown when the operator is not implemented.</exception>
        private bool CompareComparable(IComparable<T> valueGivenComparable, T valueComparing)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-9.0
            // A.CompareTo(B) < 0 means A < B
            // A.CompareTo(B) == 0 means A == B
            // A.CompareTo(B) > 0 means A > B

            switch (conditionOperator)
            {
                case ConditionOperator.Greater:
                    return valueGivenComparable.CompareTo(valueComparing) > 0;
                case ConditionOperator.GreaterOrEqual:
                    return valueGivenComparable.CompareTo(valueComparing) >= 0;
                case ConditionOperator.Less:
                    return valueGivenComparable.CompareTo(valueComparing) < 0;
                case ConditionOperator.LessOrEqual:
                    return valueGivenComparable.CompareTo(valueComparing) <= 0;
                default:
                    throw new NotImplementedException(
                        $"Condition operator '{conditionOperator}' not implemented yet."
                    );
            }
        }
    }
}
