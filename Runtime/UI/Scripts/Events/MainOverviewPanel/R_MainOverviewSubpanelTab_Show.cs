
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_MainOverviewSubpanelTab_Show
    {
        public R_MainOverviewSubpanelTab_Show(
            int overviewSubpanelType, int overviewSubpanelTabType,
            EcsPackedEntity objectPE)
        {
            this.overviewSubpanelType = overviewSubpanelType;
            this.overviewSubpanelTabType = overviewSubpanelTabType;


            this.objectPE = objectPE;
        }

        public readonly int overviewSubpanelType;
        public readonly int overviewSubpanelTabType;

        public readonly EcsPackedEntity objectPE;
    }
}
