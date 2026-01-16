
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectMainOverviewPanel_Show
    {
        public R_ObjectMainOverviewPanel_Show(
            int overviewSubpanelType, int overviewSubpanelTabType, 
            int objectMainOverviewPanelType, EcsPackedEntity objectPE)
        {
            this.overviewSubpanelType = overviewSubpanelType;
            this.overviewSubpanelTabType = overviewSubpanelTabType;

            this.objectMainOverviewPanelType = objectMainOverviewPanelType;
            this.objectPE = objectPE;
        }

        public readonly int overviewSubpanelType;
        public readonly int overviewSubpanelTabType;

        public readonly int objectMainOverviewPanelType;
        public readonly EcsPackedEntity objectPE;
    }
}
