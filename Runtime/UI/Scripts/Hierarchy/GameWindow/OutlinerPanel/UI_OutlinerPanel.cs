using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace GS.UI
{
    public class UI_OutlinerPanel : MonoBehaviour
    {
        internal Dictionary<int, UIA_OutlinerPanelTab> tabs = new();
        public TabGroup tabGroup;

        public TextMeshProUGUI tabName;
        public UIA_OutlinerPanelTab activeTab;
    }
}
