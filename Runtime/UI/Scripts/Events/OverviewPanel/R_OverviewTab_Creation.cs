
namespace GS.UI
{
    public readonly struct R_OverviewTab_Creation
    {
        public R_OverviewTab_Creation(
            string parentPanelCode, 
            string parentSubpanelCode, 
            string tabCode)
        {
            this.parentPanelCode = parentPanelCode;
            this.parentSubpanelCode = parentSubpanelCode;
            this.tabCode = tabCode;
        }

        public readonly string parentPanelCode;

        public readonly string parentSubpanelCode;

        public readonly string tabCode;
    }
}
