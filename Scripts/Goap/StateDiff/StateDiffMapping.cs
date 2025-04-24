using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct StateDiffMapping<T> : StateDiffInterface
        where T : struct, IEquatable<T>
    {
        public string stateIndex { get; private set; }
        public Dictionary<T, T> mapping { get; set; }
        public float diff => 1f;

        public StateDiffMapping(string stateIndex, Dictionary<T, T> mapping)
        {
            this.stateIndex = stateIndex;
            this.mapping = mapping;
        }

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
                    stateTarget.SetValue(stateIndex, value);
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
