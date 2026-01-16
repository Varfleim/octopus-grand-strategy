
using System.Collections.Generic;

using Leopotam.EcsLite;

namespace GS.UI
{
    public struct C_ObjectDisplayedMapPanels
    {
        public C_ObjectDisplayedMapPanels(
            int a)
        {
            objectPanels = new();

            parentProvincePE = new();
        }

        public Dictionary<int, UIA_ObjectMapPanel> objectPanels;

        public EcsPackedEntity parentProvincePE;
    }
}
