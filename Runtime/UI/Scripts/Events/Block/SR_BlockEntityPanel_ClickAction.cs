
using UnityEngine.EventSystems;

using Leopotam.EcsProto;

namespace GS.UI
{
    public readonly struct SR_BlockEntityPanel_ClickAction
    {
        public SR_BlockEntityPanel_ClickAction(
            ProtoEntity blockEntity,
            PointerEventData.InputButton inputButton)
        {
            this.blockEntity = blockEntity;

            this.inputButton = inputButton;
        }

        public readonly ProtoEntity blockEntity;

        public readonly PointerEventData.InputButton inputButton;
    }
}
