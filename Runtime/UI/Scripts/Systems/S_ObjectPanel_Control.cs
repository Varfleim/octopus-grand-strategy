
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_ObjectPanel_Control : IEcsRunSystem
    {
        readonly EcsCustomInject<UI_Core> uICore = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем вкладку подпанели объекта
            OSbpTs_Show();

            //Скрываем панель объекта
            OPs_Hide();
        }

        readonly EcsFilterInject<Inc<R_ObjectSubpanelTab_Show>> oSbpT_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectSubpanelTab_Show> oSbpT_Show_R_P = default;
        readonly EcsPoolInject<R_ObjectSubpanelTab_Update> oSbpT_Update_R_P = default;
        void OSbpTs_Show()
        {
            //Для каждого запроса отображения вкладки подпанели объекта
            foreach(int requestEntity in oSbpT_Show_R_F.Value)
            {
                //Берём запрос
                ref R_ObjectSubpanelTab_Show requestComp = ref oSbpT_Show_R_P.Value.Get(requestEntity);

                //Отображаем вкладку
                OSbpT_Show(
                    ref requestComp,
                    out bool isSamePanel,
                    out bool isSameSubpanel,
                    out bool isSameTab,
                    out bool isSameObject);

                //Запрашиваем обновление данных во вкладке
                UI_Data.OSbpT_Update_R(
                    oSbpT_Update_R_P.Value,
                    requestEntity,
                    isSamePanel,
                    isSameSubpanel,
                    isSameTab,
                    isSameObject);
            }
        }

        /// <summary>
        /// Функция, активирующая запрошенную вкладку и проверяющая, была ли она уже активна и был ли активен запрошенный объект
        /// </summary>
        /// <param name="requestComp"></param>
        /// <param name="isSamePanel"></param>
        /// <param name="isSameSubpanel"></param>
        /// <param name="isSameTab"></param>
        /// <param name="isSameObject"></param>
        void OSbpT_Show(
            ref R_ObjectSubpanelTab_Show requestComp,
            out bool isSamePanel,
            out bool isSameSubpanel,
            out bool isSameTab,
            out bool isSameObject)
        {
            //Берём запрошенную подпанель объекта
            UIA_ObjectSubpanel objectSubpanel = uICore.Value.gameWindow.objectPanel.objectSubpanels[requestComp.objectSubpanelType];

            //Активируем подпанель, если необходимо
            OSbp_Show(
                objectSubpanel,
                out isSamePanel,
                out isSameSubpanel);


            //Берём запрошенную вкладку
            UIA_ObjectSubpanelTab requestedTab = objectSubpanel.subpanelTabs[requestComp.objectSubpanelTabType];

            //Значения по умолчанию отрицательны
            isSameTab = false;
            isSameObject = false;

            //Если активна необходимая вкладка
            if (objectSubpanel.activeTab == requestedTab)
            {
                //Если была активна та же подпанель
                if(isSameSubpanel == true)
                {
                    //Сообщаем, что была активна та же вкладка
                    isSameTab = true;

                    //Если вкладка была активна для того же объекта
                    if(objectSubpanel.activeTab.objectPE.EqualsTo(requestComp.objectPE) == true)
                    {
                        //Сообщаем, что был активен тот же объект
                        isSameObject = true;
                    }
                }
            }

            //Если была активна та же панель
            if (isSamePanel == true)
            {
                UnityEngine.Debug.LogWarning("Same panel!");

            }
            //Иначе
            else
            {
                UnityEngine.Debug.LogWarning("Not same panel!");

            }

            //Если была активна та же подпанель 
            if (isSameSubpanel == true)
            {
                UnityEngine.Debug.LogWarning("Same subpanel!");

            }
            //Иначе
            else
            {
                UnityEngine.Debug.LogWarning("Not same subpanel!");

            }

            //Если была активна та же вкладка
            if (isSameTab == true)
            {
                UnityEngine.Debug.LogWarning("Same tab!");

            }
            //Иначе
            else
            {
                UnityEngine.Debug.LogWarning("Not same tab!");

                //Активируем запрошенную вкладку
                objectSubpanel.tabGroup.OnTabSelected(requestedTab.selfTabButton);

                //Указываем её как активную вкладку
                objectSubpanel.activeTab = requestedTab;
            }

            //Если был активен тот же объект
            if (isSameObject == true)
            {
                UnityEngine.Debug.LogWarning("Same object!");
            }
            //Иначе
            else
            {
                UnityEngine.Debug.LogWarning("Not same object!");

                //Указываем его как активный объект
                objectSubpanel.activeTab.objectPE = requestComp.objectPE;
            }
        }

        /// <summary>
        /// Функция, активирующая запрошенную подпанель и проверяющая, была ли она уже активирована
        /// </summary>
        /// <param name="requestedSubpanel"></param>
        /// <param name="isSamePanel"></param>
        /// <param name="isSameSubpanel"></param>
        void OSbp_Show(
            UIA_ObjectSubpanel requestedSubpanel,
            out bool isSamePanel,
            out bool isSameSubpanel)
        {
            //Берём панель объекта
            UI_ObjectPanel objectPanel = uICore.Value.gameWindow.objectPanel;

            //Активируем панель объекта, если необходимо
            uICore.Value.gameWindow.MainPanel_Activation(
                objectPanel.gameObject,
                out isSamePanel);


            //Значение по умолчанию отрицательно
            isSameSubpanel = false;

            //Если активна необходимая подпанель
            if (objectPanel.activeSubpanel == requestedSubpanel)
            {
                //Если была активна та же панель
                if (isSamePanel == true)
                {
                    //Сообщаем, что активна та же подпанель
                    isSameSubpanel = true;
                }
            }

            //Если была активна та же панель
            if (isSamePanel == true)
            {

            }
            //Иначе
            else
            {

            }

            //Если была активна та же подпанель 
            if (isSameSubpanel == true)
            {

            }
            //Иначе
            else
            {
                //Активируем запрошенную подпанель

                //Если какая-либо подпанель была активна, скрываем её
                if (objectPanel.activeSubpanel != null)
                {
                    objectPanel.activeSubpanel.gameObject.SetActive(false);
                }

                //Делаем запрошенную подпанель активной
                requestedSubpanel.gameObject.SetActive(true);

                //Указываем её как активную подпанель
                objectPanel.activeSubpanel = requestedSubpanel;
            }
        }

        readonly EcsFilterInject<Inc<R_ObjectPanel_Hide>> oP_Hide_R_F = default;
        readonly EcsPoolInject<R_ObjectPanel_Hide> oP_Hide_R_P = default;
        void OPs_Hide()
        {
            //Для каждого запроса сокрытия панели объекта
            foreach(int requestEntity in oP_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_ObjectPanel_Hide requestComp = ref oP_Hide_R_P.Value.Get(requestEntity);

                //Скрываем панель
                OP_Hide();

                //Удаляем запрос
                oP_Hide_R_P.Value.Del(requestEntity);
            }
        }

        void OP_Hide()
        {
            //Берём панель объекта
            UI_ObjectPanel objectPanel = uICore.Value.gameWindow.objectPanel;

            //Скрываем активную вкладку активной подпанели
            objectPanel.activeSubpanel.ActiveTab_Hide();

            //Скрываем активную подпанель
            objectPanel.ActiveSubpanel_Hide();

            //Скрываем активную главную панель, то есть панель объекта
            uICore.Value.gameWindow.MainPanel_Hide();
        }
    }
}
