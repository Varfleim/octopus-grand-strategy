
namespace GS.UI
{
    public readonly struct R_OverviewPanel_Show
    {
        public R_OverviewPanel_Show(
            int panelType, bool activateContent)
        {
            this.panelType = panelType;
            this.activateContent = activateContent;
        }

        public readonly int panelType;
        public readonly bool activateContent;
    }
}
