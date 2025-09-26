
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
            //�������� �������� ���������
            activeSubpanel.gameObject.SetActive(false);

            //���������, ��� �������� ��������� ���
            activeSubpanel = null;
        }
    }
}
