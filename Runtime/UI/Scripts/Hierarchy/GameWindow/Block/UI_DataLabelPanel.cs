
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace GS.UI
{
    internal class UI_DataLabelPanel : MonoBehaviour
    {
        internal static List<UI_DataLabelPanel> cachedPanels = new();

        public TextMeshProUGUI dataName;
        public TextMeshProUGUI dataValue;
    }
}
