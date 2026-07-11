
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIA_OverviewSubpanel : MonoBehaviour
    {
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

        [SerializeField]
        internal GameObject buttonsGroup;
        [SerializeField]
        internal GameObject contentPanel;

        internal UIA_OverviewSubpanelButton selfButton;
        protected bool isFiltersAndPoolsFilled;

        internal Dictionary<string, int> tabTypes = new();
        internal Dictionary<int, UIA_OverviewTab> tabs = new();
        public UIA_OverviewTab activeTab;

        public virtual void RenderShow(EcsWorld world)
        {
            //Активируем подпанель и панель кнопок
            gameObject.SetActive(true);
            buttonsGroup.SetActive(true);

            //Активируем панель содержимого
            Content_RenderShow(world);

            //Если у подпанели есть кнопка, то отмечаем в ней, что подпанель активна
            if(selfButton != null)
            {
                selfButton.isSubpanelActive = true;
            }
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
            //Деактивируем подпанель и панель кнопок
            gameObject.SetActive(false);
            buttonsGroup.SetActive(false);

            //Деактивируем панель содержимого
            Content_RenderHide(world);

            //Если у подпанели есть кнопка, то отмечаем в ней, что подпанель неактивна
            if (selfButton != null)
            {
                selfButton.isSubpanelActive = false;
            }
        }
        public virtual void Content_RenderHide(EcsWorld world)
        {
            //Деактивируем панель содержимого
            contentPanel.SetActive(false);
        }

        public virtual void FiltersAndPools_CheckFilled(EcsWorld world)
        {

        }

        public virtual void AddTab(
            UIA_OverviewTab overviewTab,
            int tabIndex)
        {
            //Заносим вкладку в словарь и назначаем индекс
            tabs.Add(tabIndex, overviewTab);
            overviewTab.SelfType = tabIndex;
        }
    }
}
