
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_EntityMapPanel_Update
    {
        public R_EntityMapPanel_Update(
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
