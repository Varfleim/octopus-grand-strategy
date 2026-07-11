
using System.Collections.Generic;

using UnityEngine;

namespace GS.UI
{
    public class UI_GameWindow : MonoBehaviour
    {
        internal Dictionary<string, int> overviewPanelsTypes = new();
        internal Dictionary<int, UIA_OverviewPanel> overviewPanels = new();
    }
}
