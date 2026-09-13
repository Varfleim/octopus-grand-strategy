
using System.Collections.Generic;

using Leopotam.EcsProto;

namespace GS.UI
{
    internal struct C_Block
    {
        public C_Block(
            int selfType)
        {
            this.selfType = selfType;
            selfPanel = null;

            sortingDataType = 0;
            descendingSort = false;
            panelEntities = new();
        }

        public readonly int selfType;
        public UI_Block selfPanel;

        public int sortingDataType;
        public bool descendingSort;
        public List<ProtoEntity> panelEntities;
    }
}
