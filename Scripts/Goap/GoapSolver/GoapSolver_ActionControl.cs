using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public partial class GoapSolver
    {
        /// <summary>
        /// Pool of actions.
        /// </summary>
        /// <remarks>
        /// Maps action names to their corresponding <see cref="GoapAction"/> instances.
        /// </remarks>
        private readonly Dictionary<string, GoapAction> actionPool =
            new Dictionary<string, GoapAction>();

        /// <summary>
        /// Adds a new action to the action pool.
        /// </summary>
        /// <param name="action">The action to add.</param>
        public void AddAction(GoapAction action)
        {
            actionPool.Add(action.name, action);
        }

        /// <summary>
        /// Removes an action from the action pool by its name.
        /// </summary>
        /// <param name="name">The name of the action to remove.</param>
        public void RemoveAction(string name)
        {
            actionPool.Remove(name);
        }

        /// <summary>
        /// Replaces an existing action in the action pool with a new one.
        /// </summary>
        /// <param name="name">The name of the action to replace.</param>
        /// <param name="action">The new action to replace the existing one.</param>
        public void ReplaceAction(string name, GoapAction action)
        {
            actionPool[name] = action;
        }

        /// <summary>
        /// Clear all actions in the action pool.
        /// </summary>
        public void ClearActionPool()
        {
            actionPool.Clear();
        }
    }
}
