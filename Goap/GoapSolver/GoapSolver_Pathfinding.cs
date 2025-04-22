using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public partial class GoapSolver
    {
        /// <summary>
        /// Cost estimation weights for each state index
        /// </summary>
        private Dictionary<string, float> stateIndexCostWeights = new Dictionary<string, float>();

        public Action[] Solve(State stateCurrent, Condition goal)
        {
            throw new NotImplementedException("Solve method not implemented yet.");
        }

        private Dictionary<string, float> ComputeCostWeights() { }
    }
}
