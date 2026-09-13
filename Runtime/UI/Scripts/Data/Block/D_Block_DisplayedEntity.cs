
using System;

using Leopotam.EcsProto;

namespace GS.UI
{
    public struct D_Block_DisplayedEntity<TSortType>
        where TSortType : notnull, IComparable<TSortType>
    {
        public D_Block_DisplayedEntity(
            ProtoEntity entity)
        {
            this.entity = entity;
            sortValue = default;
        }

        public ProtoEntity entity;
        internal TSortType sortValue;
    }
}
