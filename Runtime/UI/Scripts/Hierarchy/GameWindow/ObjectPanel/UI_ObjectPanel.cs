
using UnityEngine;

using TMPro;

namespace GS.UI
{
    public class UI_ObjectPanel : MonoBehaviour
    {
        public TextMeshProUGUI objectName;

        public UIA_ObjectSubpanel activeSubpanel;

        public void HideActiveSubpanel()
        {
            //—крываем активную подпанель
            activeSubpanel.gameObject.SetActive(false);

            //”казываем, что активной подпанели нет
            activeSubpanel = null;
        }
    }
}
