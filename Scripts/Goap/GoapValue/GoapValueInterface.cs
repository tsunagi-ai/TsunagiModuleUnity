using System;

namespace TsunagiModule.Goap
{
    /// <summary>
    /// Defines an interface for GOAP value types.
    /// </summary>
    public interface GoapValueInterface
    {
        /// <summary>
        /// Gets the type of the GOAP value.
        /// </summary>
        public Type type { get; }
    }
}
