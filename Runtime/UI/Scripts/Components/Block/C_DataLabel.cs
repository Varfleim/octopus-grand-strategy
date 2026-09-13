
using Leopotam.EcsProto;

namespace GS.UI
{
    public readonly struct C_DataLabel
    {
        public C_DataLabel(
            ProtoEntity displayedEntity)
        {
            this.displayedEntity = displayedEntity;
        }

        public readonly ProtoEntity displayedEntity;
    }
}
