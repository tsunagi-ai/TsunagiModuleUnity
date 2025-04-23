using System;

namespace TsunagiModule.Goap
{
    public struct StateDiffAddition<T> : StateDiffInterface
        where T : struct, IConvertible, IComparable, IComparable<T>, IEquatable<T>
    {
        public string stateIndex { get; private set; }
        public T additionValue { get; set; }

        public StateDiffAddition(string stateIndex, T additionValue)
        {
            this.stateIndex = stateIndex;
            this.additionValue = additionValue;
        }

        public State Operate(State state, bool overwrite = true)
        {
            // cloning or not
            State stateTarget;
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
                stateTarget.SetNumericValue(stateIndex, newValue); // Updated to remove unnecessary new()
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
