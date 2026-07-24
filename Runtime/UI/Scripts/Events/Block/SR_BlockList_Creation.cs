
namespace GS.UI
{
    public readonly struct SR_BlockList_Creation
    {
        public SR_BlockList_Creation(
            string parentPanelCode, string parentSubpanelCode, string parentTabCode)
        {
            this.parentPanelCode = parentPanelCode;
            this.parentSubpanelCode = parentSubpanelCode;
            this.parentTabCode = parentTabCode;
        }

        public readonly string parentPanelCode;
        public readonly string parentSubpanelCode;
        public readonly string parentTabCode;
    }
}
