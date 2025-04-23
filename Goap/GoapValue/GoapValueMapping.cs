using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct StateDiffMapping<T> : StateDiffInterface
        where T : struct, IEquatable<T>
    {
        public string stateIndex { get; private set; }
        public Dictionary<T, T> mapping { get; set; }

        public StateDiffMapping(string stateIndex, Dictionary<T, T> mapping)
        {
            this.stateIndex = stateIndex;
            this.mapping = mapping;
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
            if (targetValueInterface is GoapValueInterface<T> targetValue)
            {
                // ...operate mapping

                // if the current valus is in the map...
                if (mapping.TryGetValue(targetValue.value, out T newValue))
                {
                    // ...update state
                    GoapValueInterface<T> newGoapValue = new GoapValueEquitable<T>(newValue);
                    stateTarget.SetValue(stateIndex, newGoapValue);
                    return stateTarget;
                }
                else
                {
                    // ...do nothing
                    return stateTarget;
                }
            }
            else
            {
                // ...panic
                throw new ArgumentException(
                    $"State index '{stateIndex}' is not of type {typeof(T)}."
                );
            }
        }
    }
}
