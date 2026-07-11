
namespace GS.UI
{
    public readonly struct SR_BlockList_Creation
    {
        public SR_BlockList_Creation(
            int parentPanelType, 
            int parentSubpanelType, 
            int parentTabType)
        {
            this.parentPanelType = parentPanelType;

            this.parentSubpanelType = parentSubpanelType;

            this.parentTabType = parentTabType;
        }

        public readonly int parentPanelType;

        public readonly int parentSubpanelType;

        public readonly int parentTabType;
    }
}
