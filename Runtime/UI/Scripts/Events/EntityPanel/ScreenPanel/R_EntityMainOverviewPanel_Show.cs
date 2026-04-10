
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_EntityMainOverviewPanel_Show
    {
        public R_EntityMainOverviewPanel_Show(
            int overviewSubpanelType, int overviewSubpanelTabType, 
            int entMainOverviewPanelType, EcsPackedEntity entPE)
        {
            this.overviewSubpanelType = overviewSubpanelType;
            this.overviewSubpanelTabType = overviewSubpanelTabType;

            this.entMainOverviewPanelType = entMainOverviewPanelType;
            this.entPE = entPE;
        }

        public readonly int overviewSubpanelType;
        public readonly int overviewSubpanelTabType;

        public readonly int entMainOverviewPanelType;
        public readonly EcsPackedEntity entPE;
    }
}
