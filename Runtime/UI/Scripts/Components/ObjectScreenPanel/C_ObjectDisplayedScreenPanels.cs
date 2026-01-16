
using System.Collections.Generic;

namespace GS.UI
{
    public struct C_ObjectDisplayedScreenPanels
    {
        public C_ObjectDisplayedScreenPanels(
            int a)
        {
            objectPanels = new();
        }

        public Dictionary<int, UIA_ObjectScreenPanel> objectPanels;
    }
}
