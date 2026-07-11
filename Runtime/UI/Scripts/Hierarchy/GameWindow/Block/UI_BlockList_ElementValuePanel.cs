
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace GS.UI
{
    public class UI_BlockList_ElementValuePanel : MonoBehaviour
    {
        internal static List<UI_BlockList_ElementValuePanel> cachedPanels = new();

        public TextMeshProUGUI valueName;
        public TextMeshProUGUI value;
    }
}
