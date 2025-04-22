using System.Collections.Generic;

namespace TsunagiModule.Goap
{
    public struct StateDiffSet
    {
        public List<StateDiff> diffs;

        public StateDiffSet(List<StateDiff> diffs)
        {
            this.diffs = diffs;
        }
    }
}
