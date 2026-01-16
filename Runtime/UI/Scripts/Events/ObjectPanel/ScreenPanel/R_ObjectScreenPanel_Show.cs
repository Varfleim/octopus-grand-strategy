
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectScreenPanel_Show
    {
        public R_ObjectScreenPanel_Show(
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
