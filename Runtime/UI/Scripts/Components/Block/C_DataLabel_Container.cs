
using System.Collections.Generic;

using Leopotam.EcsProto;

namespace GS.UI
{
    internal struct C_DataLabel_Container
    {
        public C_DataLabel_Container(
            int a)
        {
            dataLabelEntities = new();
        }

        public Dictionary<int, ProtoEntity> dataLabelEntities;
    }
}
