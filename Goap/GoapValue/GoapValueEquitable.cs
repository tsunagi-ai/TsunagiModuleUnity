using System;

namespace TsunagiModule.Goap
{
    public struct GoapValueEquitable<T> : GoapValueInterface<T>
        where T : struct, IEquatable<T>
    {
        public Type type => typeof(T);
        public T value { get; set; } // getter and setter for value

        public GoapValueEquitable(T value)
        {
            this.value = value;
        }
    }
}
