
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UIA_OverviewPanel : MonoBehaviour
    {
        public string selfCode;
        public int SelfType
        {
            get
            {
                return selfType;
            }
            internal set
            {
                selfType = value;
            }
        }
        private int selfType;

        public UIA_OverviewSubpanel subpanelPrefab;
        public UIA_OverviewSubpanelButton subpanelButtonPrefab;
        public UIA_OverviewTab tabPrefab;
        public UIA_OverviewTabButton tabButtonPrefab;

        [SerializeField]
        internal GameObject buttonsGroup;
        [SerializeField]
        internal GameObject contentPanel;
        [SerializeField]
        internal GameObject subpanelsGroup;

        public TextMeshProUGUI panelNameText;
        protected bool isFiltersAndPoolsFilled;

        internal Dictionary<string, int> subpanelTypes = new();
        internal Dictionary<int, UIA_OverviewSubpanel> subpanels = new();
        public UIA_OverviewSubpanel activeSubpanel;

        public virtual void RenderShow(EcsWorld world)
        {
            //Активируем панель и панель кнопок
            gameObject.SetActive(true);
            buttonsGroup.SetActive(true);
        }
        public virtual void Content_RenderShow(EcsWorld world)
        {
            //Активируем панель содержимого
            contentPanel.SetActive(true);
        }

        public virtual void RenderUpdate(EcsWorld world)
        {

        }
        public virtual void Buttons_RenderUpdate(EcsWorld world)
        {

        }
        public virtual void Content_RenderUpdate(EcsWorld world)
        {

        }

        public virtual void TickUpdate(EcsWorld world)
        {

        }
        public virtual void Buttons_TickUpdate(EcsWorld world)
        {

        }
        public virtual void Content_TickUpdate(EcsWorld world)
        {

        }

        public virtual void RenderHide(EcsWorld world)
        {
            //Дективируем всю панель и панель кнопок
            gameObject.SetActive(false);
            buttonsGroup.SetActive(false);

            //Деактивируем панель содержимого
            Content_RenderHide(world);
        }
        public virtual void Content_RenderHide(EcsWorld world)
        {
            //Деактивируем панель содержимого
            contentPanel.SetActive(false);
        }

        public virtual void FiltersAndPools_CheckFilled(EcsWorld world)
        {

        }

        public virtual void AddSubpanel(
            UIA_OverviewSubpanel overviewSubpanel,
            int subpanelIndex)
        {
            //Заносим подпанель в словарь и назначаем индекс
            subpanels.Add(subpanelIndex, overviewSubpanel);
            overviewSubpanel.SelfType = subpanelIndex;
        }
    }
}
