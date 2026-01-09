
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace GS.UI
{
    public class UI_ObjectPanel : MonoBehaviour
    {
        public TextMeshProUGUI objectName;

        public Dictionary<string, UIA_ObjectSubpanel> objectSubpanels = new();
        public UIA_ObjectSubpanel activeSubpanel;

        public void ActiveSubpanel_Hide()
        {
            //—крываем активную подпанель
            activeSubpanel.gameObject.SetActive(false);

            //”казываем, что активной подпанели нет
            activeSubpanel = null;
        }
    }
}
