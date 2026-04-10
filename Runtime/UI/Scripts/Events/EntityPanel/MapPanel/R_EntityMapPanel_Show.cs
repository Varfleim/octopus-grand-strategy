
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_EntityMapPanel_Show
    {
        public R_EntityMapPanel_Show(
            int panelType,
            EcsPackedEntity entPE)
        {
            this.panelType = panelType;
            
            this.entPE = entPE;
        }

        public readonly int panelType;

        public readonly EcsPackedEntity entPE;
    }
}
