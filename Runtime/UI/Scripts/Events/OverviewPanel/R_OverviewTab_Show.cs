
namespace GS.UI
{
    public readonly struct R_OverviewTab_Show
    {
        public R_OverviewTab_Show(
            int panelType,
            int subpanelType,
            int tabType)
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
