
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_EntityScreenPanel_Show
    {
        public R_EntityScreenPanel_Show(
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
