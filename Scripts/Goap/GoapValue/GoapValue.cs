using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Value used in Goap system.
    /// </summary>
    /// <remarks>
    /// This is implemented for State supporting as much type as possible.<br/>
    /// You can use:<br/>
    /// - <see cref="int"/>
    /// - <see cref="double"/>
    /// - <see cref="bool"/>
    /// - <see cref="float"/>
    /// <br/> <see cref="double"/> is recommended instead of <see cref="float"/> for accurate pathfinding.
    /// </remarks>
    /// <typeparam name="T">The type of the value. Must be a struct and implement <see cref="IEquatable{T}"/>.
    /// </typeparam>
    public struct GoapValue<T> : GoapValueInterface, IEquatable<GoapValue<T>>
        where T : struct, IEquatable<T>
    {
        /// <summary>
        /// Gets the type of the GOAP value.
        /// </summary>
        public Type type => typeof(T);

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoapValue{T}"/> struct.
        /// </summary>
        /// <param name="value">The value to initialize with.</param>
        public GoapValue(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Determines whether the current value is equal to another value of the same type.
        /// </summary>
        /// <param name="other">The other value to compare with.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public bool Equals(GoapValue<T> other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Returns the hash code for the current value.
        /// </summary>
        /// <remarks>
        /// the GetHashCode() of the original value should be implemented
        /// </remarks>
        /// <returns>The hash code for the current value.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
