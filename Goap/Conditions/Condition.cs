using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct Condition<T> : ConditionInterface
        where T : struct, IEquatable<T>
    {
        public enum ConditionOperator
        {
            Larger,
            LargerOrEqual,
            Smaller,
            SmallerOrEqual,
            Equal,
            NotEqual,
        }

        public string stateIndex { get; private set; }
        public T valueComparing { get; set; }
        public ConditionOperator conditionOperator { get; set; }

        public Condition(string stateIndex, T valueComparing, ConditionOperator conditionOperator)
        {
            this.stateIndex = stateIndex;
            this.valueComparing = valueComparing;
            this.conditionOperator = conditionOperator;
        }

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
                else
                {
                    throw new NotImplementedException(
                        $"Condition operator '{conditionOperator}' not implemented yet."
                    );
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
                case ConditionOperator.Larger:
                case ConditionOperator.LargerOrEqual:
                case ConditionOperator.Smaller:
                case ConditionOperator.SmallerOrEqual:
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
                default:
                    // ...panic
                    throw new NotImplementedException(
                        $"Condition operator '{conditionOperator}' not implemented yet."
                    );
            }
        }

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

        private bool CompareComparable(IComparable<T> valueGivenComparable, T valueComparing)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-9.0
            // A.CompareTo(B) < 0 means A < B
            // A.CompareTo(B) == 0 means A == B
            // A.CompareTo(B) > 0 means A > B

            switch (conditionOperator)
            {
                case ConditionOperator.Larger:
                    return valueGivenComparable.CompareTo(valueComparing) > 0;
                case ConditionOperator.LargerOrEqual:
                    return valueGivenComparable.CompareTo(valueComparing) >= 0;
                case ConditionOperator.Smaller:
                    return valueGivenComparable.CompareTo(valueComparing) < 0;
                case ConditionOperator.SmallerOrEqual:
                    return valueGivenComparable.CompareTo(valueComparing) <= 0;
                default:
                    throw new NotImplementedException(
                        $"Condition operator '{conditionOperator}' not implemented yet."
                    );
            }
        }
    }
}
