
namespace GS.UI
{
    public readonly struct R_OverviewPanel_Update
    {
        public R_OverviewPanel_Update(
            int panelType, 
            bool isPanelAlreadyActive, bool isPanelContentAlreadyActive,
            bool isSubpanelAlreadyActive,
            bool isTabAlreadyActive)
        {
            this.panelType = panelType;
            this.isPanelAlreadyActive = isPanelAlreadyActive;
            this.isPanelContentAlreadyActive = isPanelContentAlreadyActive;

            this.isSubpanelAlreadyActive = isSubpanelAlreadyActive;

            this.isTabAlreadyActive = isTabAlreadyActive;
        }

        public readonly int panelType;
        public readonly bool isPanelAlreadyActive;
        public readonly bool isPanelContentAlreadyActive;

        public readonly bool isSubpanelAlreadyActive;

        public readonly bool isTabAlreadyActive;
    }
}
