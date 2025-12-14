
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectSubpanelTabShow
    {
        public R_ObjectSubpanelTabShow(
            string objectSubpanelType, string objectSubpanelTabType,
            EcsPackedEntity objectPE)
        {
            this.objectSubpanelType = objectSubpanelType;
            this.objectSubpanelTabType = objectSubpanelTabType;


            this.objectPE = objectPE;
        }

        public readonly string objectSubpanelType;
        public readonly string objectSubpanelTabType;

        public readonly EcsPackedEntity objectPE;
    }
}
