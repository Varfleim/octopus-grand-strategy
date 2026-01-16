
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectScreenPanel_Update
    {
        public R_ObjectScreenPanel_Update(
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
