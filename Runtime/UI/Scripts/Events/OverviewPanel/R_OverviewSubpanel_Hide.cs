
namespace GS.UI
{
    public readonly struct R_OverviewSubpanel_Hide    
    {
        public R_OverviewSubpanel_Hide(
            int panelType, 
            int subpanelType)
        {
            this.panelType = panelType;
            this.subpanelType = subpanelType;
        }

        public readonly int panelType;
        public readonly int subpanelType;
    }
}
