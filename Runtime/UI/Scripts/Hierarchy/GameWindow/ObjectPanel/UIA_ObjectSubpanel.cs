
using System.Collections.Generic;

using UnityEngine;

namespace GS.UI
{
    public abstract class UIA_ObjectSubpanel : MonoBehaviour
    {
        public string SelfType
        {
            get
            {
                return selfType;
            }
        }
        string selfType;

        public Dictionary<string, UIA_ObjectSubpanelTab> subpanelTabs = new();

        public TabGroup tabGroup;

        public UIA_ObjectSubpanelTab activeTab;

        public void SetSelfType(
            string selfType)
        {
            this.selfType = selfType;
        }

        public void HideActiveTab()
        {
            //�������� �������� �������
            activeTab.gameObject.SetActive(false);

            //������� �������� ��������� �������
            activeTab.objectPE = new();

            //���������, ��� �������� ������� ���
            activeTab = null;
        }
    }
}
