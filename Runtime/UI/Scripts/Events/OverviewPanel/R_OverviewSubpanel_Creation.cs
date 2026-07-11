
namespace GS.UI
{
    public readonly struct R_OverviewSubpanel_Creation
    {
        public R_OverviewSubpanel_Creation(
            string parentPanelCode, 
            string subpanelCode,
            bool hasButton)
        {
            this.parentPanelCode = parentPanelCode;
            this.subpanelCode = subpanelCode;

            this.hasButton = hasButton;
        }

        public readonly string parentPanelCode;

        public readonly string subpanelCode;

        public readonly bool hasButton;
    }
}
