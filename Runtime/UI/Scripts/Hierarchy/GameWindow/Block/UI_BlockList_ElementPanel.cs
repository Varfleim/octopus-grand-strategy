
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Leopotam.EcsProto;

namespace GS.UI
{
    public class UI_BlockList_ElementPanel : MonoBehaviour
    {
        internal static List<UI_BlockList_ElementPanel> cachedPanels = new();

        public TextMeshProUGUI elementName;
        public VerticalLayoutGroup layoutGroup;
        public List<UI_BlockList_ElementValuePanel> elementValues = new();

        public ProtoEntity elementEntity;
    }
}
