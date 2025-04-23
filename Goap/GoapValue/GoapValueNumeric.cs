using System;

namespace TsunagiModule.Goap
{
    public struct GoapValueNumeric<T> : GoapValueInterface<T>, IFormattable
        where T : struct, IConvertible, IComparable, IComparable<T>, IEquatable<T>
    {
        public Type type => typeof(T);
        public T value { get; set; } // getter and setter for value

        public GoapValueNumeric(T value)
        {
            this.value = value;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            double numericValue = Convert.ToDouble(value, formatProvider);
            return numericValue.ToString(format, formatProvider);
        }
    }
}
