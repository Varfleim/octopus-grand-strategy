
using System.Collections.Generic;

namespace GS.UI
{
    public struct C_ObjectDisplayedMapPanels
    {
        public C_ObjectDisplayedMapPanels(
            int a)
        {
            objectMapPanels = new();
        }

        public Dictionary<string, UIA_ObjectMapPanel> objectMapPanels;
    }
}
