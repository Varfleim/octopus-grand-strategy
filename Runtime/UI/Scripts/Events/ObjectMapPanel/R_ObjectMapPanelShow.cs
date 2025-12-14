
using Leopotam.EcsLite;

namespace GS.UI
{
    public readonly struct R_ObjectMapPanelShow
    {
        public R_ObjectMapPanelShow(
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
