
using UnityEngine;

namespace GS.UI
{
    public readonly struct R_BlockList_Action
    {
        public R_BlockList_Action(
            int blockEntity,
            GameObject actionObject)
        {
            this.blockEntity = blockEntity;
            
            this.actionObject = actionObject;
        }

        public readonly int blockEntity;

        public readonly GameObject actionObject;
    }
}
