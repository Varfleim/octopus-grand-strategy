
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
            set
            {
                selfType = value;
            }
        }
        string selfType;

        public TabGroup tabGroup;

        public Dictionary<string, UIA_ObjectSubpanelTab> subpanelTabs = new();
        public UIA_ObjectSubpanelTab activeTab;

        public void ActiveTab_Hide()
        {
            //Скрываем активную вкладку
            activeTab.gameObject.SetActive(false);

            //Очищаем сущность активного объекта
            activeTab.objectPE = new();

            //Указываем, что активной вкладки нет
            activeTab = null;
        }
    }
}
