
using UnityEngine;

using Leopotam.EcsProto;

namespace GS.UI
{
    public readonly struct R_BlockList_Action
    {
        public R_BlockList_Action(
            ProtoEntity blockEntity,
            GameObject actionObject)
        {
            this.blockEntity = blockEntity;
            
            this.actionObject = actionObject;
        }

        public readonly ProtoEntity blockEntity;

        public readonly GameObject actionObject;
    }
}
