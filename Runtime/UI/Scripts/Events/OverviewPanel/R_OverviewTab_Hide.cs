
namespace GS.UI
{
    public readonly struct R_OverviewTab_Hide
    {
        public R_OverviewTab_Hide(
            int panelType, int subpanelType, int tabType)
        {
            this.panelType = panelType;
            this.subpanelType = subpanelType;
            this.tabType = tabType;
        }

        public readonly int panelType;
        public readonly int subpanelType;
        public readonly int tabType;
    }
}
