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
        private Dictionary<string, Action> actionPool = new Dictionary<string, Action>();

        public void AddAction(Action action)
        {
            actionPool.Add(action.name, action);
        }

        public void RemoveAction(string name)
        {
            actionPool.Remove(name);
        }

        public void ReplaceAction(string name, Action action)
        {
            actionPool[name] = action;
        }
    }
}
