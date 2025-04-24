using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public partial class GoapSolver
    {
        /// <summary>
        /// Pool of actions
        /// </summary>
        /// <remarks>
        /// action name -> Action
        /// </remarks>
        private Dictionary<string, GoapAction> actionPool = new Dictionary<string, GoapAction>();

        public void AddAction(GoapAction action)
        {
            actionPool.Add(action.name, action);
        }

        public void RemoveAction(string name)
        {
            actionPool.Remove(name);
        }

        public void ReplaceAction(string name, GoapAction action)
        {
            actionPool[name] = action;
        }
    }
}
