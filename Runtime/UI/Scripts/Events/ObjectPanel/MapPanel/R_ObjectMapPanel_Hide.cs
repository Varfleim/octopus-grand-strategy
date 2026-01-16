
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectMapPanel_Hide
    {
        public R_ObjectMapPanel_Hide(
            int panelType, 
            EcsPackedEntity objectPE)
        {
            this.panelType = panelType;
            
            this.objectPE = objectPE;
        }

        public readonly int panelType;

        public readonly EcsPackedEntity objectPE;
    }
}
