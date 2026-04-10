
using System.Collections.Generic;

namespace GS.UI
{
    public struct C_EntityDisplayedScreenPanels
    {
        public C_EntityDisplayedScreenPanels(
            int a)
        {
            entPanels = new();
        }

        public Dictionary<int, UIA_EntityScreenPanel> entPanels;
    }
}
