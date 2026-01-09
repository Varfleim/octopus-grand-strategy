
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectMapPanel_Show
    {
        public R_ObjectMapPanel_Show(
            string objectMapPanelType, 
            EcsPackedEntity objectPE)
        {
            this.objectMapPanelType = objectMapPanelType;
            
            this.objectPE = objectPE;
        }

        public readonly string objectMapPanelType;

        public readonly EcsPackedEntity objectPE;
    }
}
