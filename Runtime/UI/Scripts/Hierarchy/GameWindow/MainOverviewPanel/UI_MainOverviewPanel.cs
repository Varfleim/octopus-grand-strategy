
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace GS.UI
{
    public class UI_MainOverviewPanel : MonoBehaviour
    {
        internal Dictionary<int, UIA_MainOverviewSubpanel> subpanels = new();

        public TextMeshProUGUI subpanelName;
        public UIA_MainOverviewSubpanel activeSubpanel;
    }
}
