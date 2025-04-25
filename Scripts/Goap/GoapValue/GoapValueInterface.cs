using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Defines an interface for GOAP value types.
    /// </summary>
    /// <remarks>
    /// You may want to use <see cref="GoapValue"/> for real instance.
    /// This is implemented for State supporting as much type as possible.
    /// </remarks>
    public interface GoapValueInterface
    {
        /// <summary>
        /// Gets the type of the GOAP value.
        /// </summary>
        public Type type { get; }
    }
}
