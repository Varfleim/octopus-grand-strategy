
using UnityEngine;

using TMPro;

namespace GS.UI
{
    public class UIA_OverviewPanel : MonoBehaviour
    {
        public string selfCode;
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

        public UIA_OverviewSubpanel activeSubpanel;

        public virtual void RenderShow()
        {
            //Активируем панель и панель кнопок
            gameObject.SetActive(true);
            buttonsGroup.SetActive(true);
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
            //Дективируем всю панель и панель кнопок
            gameObject.SetActive(false);
            buttonsGroup.SetActive(false);

            //Деактивируем панель содержимого
            Content_RenderHide();
        }
        public virtual void Content_RenderHide()
        {
            //Деактивируем панель содержимого
            contentPanel.SetActive(false);
        }
    }
}
