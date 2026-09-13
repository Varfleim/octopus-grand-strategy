
using Leopotam.EcsProto;

namespace GS.UI
{
    internal struct C_BlockEntityPanel
    {
        public C_BlockEntityPanel(
            int a)
        {
            selfPanel = null;

            displayedEntity = new();

            dLPanels = new UI_DataLabelPanel[0];
        }

        public UI_BlockEntityPanel selfPanel;

        public ProtoEntity DisplayedEntity
        {
            get
            {
                return displayedEntity;
            }
            internal set
            {
                displayedEntity = value;
            }
        }
        ProtoEntity displayedEntity;

        public UI_DataLabelPanel[] dLPanels;
    }
}
