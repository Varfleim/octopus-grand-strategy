
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectOutlinerPanel_Show
    {
        public R_ObjectOutlinerPanel_Show(
            int outlinerPanelTabType, 
            int objectOutlinerPanelType, EcsPackedEntity objectPE)
        {
            this.outlinerPanelTabType = outlinerPanelTabType;
            
            this.objectOutlinerPanelType = objectOutlinerPanelType;
            this.objectPE = objectPE;
        }

        public readonly int outlinerPanelTabType;

        public readonly int objectOutlinerPanelType;
        public readonly EcsPackedEntity objectPE;
    }
}
