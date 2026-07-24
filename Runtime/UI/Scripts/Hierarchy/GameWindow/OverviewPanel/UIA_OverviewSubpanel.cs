
using UnityEngine;

namespace GS.UI
{
    public abstract class UIA_OverviewSubpanel : MonoBehaviour
    {
        public int SelfType
        {
            get
            {
                return selfIndex;
            }
            internal set
            {
                selfIndex = value;
            }
        }
        private int selfIndex;

        [SerializeField]
        internal GameObject buttonsGroup;
        [SerializeField]
        internal GameObject contentPanel;

        internal UIA_OverviewSubpanelButton selfButton;
        protected bool isFiltersAndPoolsFilled;

        public UIA_OverviewTab activeTab;

        public virtual void RenderShow()
        {
            //Активируем подпанель и панель кнопок
            gameObject.SetActive(true);
            buttonsGroup.SetActive(true);

            //Активируем панель содержимого
            Content_RenderShow();

            //Если у подпанели есть кнопка, то отмечаем в ней, что подпанель активна
            if(selfButton != null)
            {
                selfButton.isSubpanelActive = true;
            }
        }
        public virtual void Content_RenderShow()
        {
            //Активируем панель содержимого
            contentPanel.SetActive(true);
        }

        public virtual void RenderUpdate()
        {

        }
        public virtual void Buttons_RenderUpdate()
        {

        }
        public virtual void Content_RenderUpdate()
        {

        }

        public virtual void TickUpdate()
        {

        }
        public virtual void Buttons_TickUpdate()
        {

        }
        public virtual void Content_TickUpdate()
        {

        }

        public virtual void RenderHide()
        {
            //Деактивируем подпанель и панель кнопок
            gameObject.SetActive(false);
            buttonsGroup.SetActive(false);

            //Деактивируем панель содержимого
            Content_RenderHide();

            //Если у подпанели есть кнопка, то отмечаем в ней, что подпанель неактивна
            if (selfButton != null)
            {
                selfButton.isSubpanelActive = false;
            }
        }
        public virtual void Content_RenderHide()
        {
            //Деактивируем панель содержимого
            contentPanel.SetActive(false);
        }

        public virtual void FiltersAndPools_CheckFilled()
        {

        }
    }
}
