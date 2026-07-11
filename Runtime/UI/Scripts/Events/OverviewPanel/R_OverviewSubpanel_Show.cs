
namespace GS.UI
{
    public readonly struct R_OverviewSubpanel_Show
    {
        public R_OverviewSubpanel_Show(
            int panelType,
            int subpanelType, bool activateDefaultTab)
        {
            this.panelType = panelType;

            this.subpanelType = subpanelType;

            this.activateDefaultTab = activateDefaultTab;
        }

        public readonly int panelType;

        public readonly int subpanelType;
        public readonly bool activateDefaultTab;
    }
}
