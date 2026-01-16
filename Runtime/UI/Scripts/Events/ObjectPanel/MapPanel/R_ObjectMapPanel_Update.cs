
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectMapPanel_Update
    {
        public R_ObjectMapPanel_Update(
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
