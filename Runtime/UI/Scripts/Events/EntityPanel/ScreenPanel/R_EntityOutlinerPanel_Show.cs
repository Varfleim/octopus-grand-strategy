
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_EntityOutlinerPanel_Show
    {
        public R_EntityOutlinerPanel_Show(
            int outlinerPanelTabType, 
            int entOutlinerPanelType, EcsPackedEntity entPE)
        {
            this.outlinerPanelTabType = outlinerPanelTabType;
            
            this.entOutlinerPanelType = entOutlinerPanelType;
            this.entPE = entPE;
        }

        public readonly int outlinerPanelTabType;

        public readonly int entOutlinerPanelType;
        public readonly EcsPackedEntity entPE;
    }
}
