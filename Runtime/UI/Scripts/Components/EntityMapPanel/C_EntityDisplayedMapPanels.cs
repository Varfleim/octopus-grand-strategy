
using System.Collections.Generic;

using Leopotam.EcsLite;

namespace GS.UI
{
    public struct C_EntityDisplayedMapPanels
    {
        public C_EntityDisplayedMapPanels(
            int a)
        {
            entPanels = new();

            parentProvincePE = new();
        }

        public Dictionary<int, UIA_EntityMapPanel> entPanels;

        public EcsPackedEntity parentProvincePE;
    }
}
