
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_MainOverviewPanel_Control : IEcsRunSystem
    {
        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем вкладку главной обзорной панели
            MOSbpTs_Show();

            //Скрываем главную обзорную панель
            MOPs_Hide();
        }

        readonly EcsFilterInject<Inc<R_MainOverviewSubpanelTab_Show>> mOSbpT_Show_R_F = default;
        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Show> mOSbpT_Show_R_P = default;
        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Update> mOSbpT_Update_R_P = default;
        void MOSbpTs_Show()
        {
            //Для каждого запроса отображения вкладки главной обзорной панели
            foreach(int rEntity in mOSbpT_Show_R_F.Value)
            {
                //Берём запрос
                ref R_MainOverviewSubpanelTab_Show rComp = ref mOSbpT_Show_R_P.Value.Get(rEntity);

                //Отображаем вкладку
                MOSbpT_Show(
                    ref rComp,
                    out bool isSamePanel,
                    out bool isSameSubpanel,
                    out bool isSameTab,
                    out bool isSameObject);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции отображения

                //Запрашиваем обновление данных во вкладке
                UI_Data.MOSbpT_Update_R(
                    mOSbpT_Update_R_P.Value,
                    rEntity,
                    isSamePanel,
                    isSameSubpanel,
                    isSameTab,
                    isSameObject);
            }
        }

        /// <summary>
        /// Функция, активирующая запрошенную вкладку и проверяющая, была ли она уже активна и был ли активен запрошенный объект
        /// </summary>
        /// <param name="rComp"></param>
        /// <param name="isSamePanel"></param>
        /// <param name="isSameSubpanel"></param>
        /// <param name="isSameTab"></param>
        /// <param name="isSameObject"></param>
        void MOSbpT_Show(
            ref R_MainOverviewSubpanelTab_Show rComp,
            out bool isSamePanel,
            out bool isSameSubpanel,
            out bool isSameTab,
            out bool isSameObject)
        {
            //Берём запрошенную главную обзорную подпанель
            UIA_MainOverviewSubpanel mOSubpanel = uI_Core.Value.gameWindow.mainOverviewPanel.subpanels[rComp.overviewSubpanelType];

            //Активируем подпанель, если необходимо
            MOSbp_Show(
                mOSubpanel,
                out isSamePanel,
                out isSameSubpanel);

            //Берём запрошенную вкладку
            UIA_MainOverviewSubpanelTab requestedTab = mOSubpanel.tabs[rComp.overviewSubpanelTabType];

            //Значения по умолчанию отрицательны
            isSameTab = false;
            isSameObject = false;

            //Если активна необходимая вкладка
            if (mOSubpanel.activeSubpanelTab == requestedTab)
            {
                //Если была активна та же подпанель
                if(isSameSubpanel)
                {
                    //Сообщаем, что была активна та же вкладка
                    isSameTab = true;

                    //Если вкладка была активна для того же объекта
                    if(mOSubpanel.activeSubpanelTab.objectPE.EqualsTo(rComp.objectPE))
                    {
                        //Сообщаем, что был активен тот же объект
                        isSameObject = true;
                    }
                }
            }

            //Если была активна та же панель
            if (isSamePanel)
            {
                UnityEngine.Debug.LogWarning("Same panel!");

            }
            else
            {
                UnityEngine.Debug.LogWarning("Not same panel!");

            }

            //Если была активна та же подпанель 
            if (isSameSubpanel)
            {
                UnityEngine.Debug.LogWarning("Same subpanel!");

            }
            else
            {
                UnityEngine.Debug.LogWarning("Not same subpanel!");

            }

            //Если была активна та же вкладка
            if (isSameTab)
            {
                UnityEngine.Debug.LogWarning("Same tab!");

            }
            else
            {
                UnityEngine.Debug.LogWarning("Not same tab!");

                //Активируем запрошенную вкладку
                mOSubpanel.tabGroup.OnTabSelected(requestedTab.selfTabButton);

                //Указываем её как активную вкладку
                mOSubpanel.activeSubpanelTab = requestedTab;
            }

            //Если был активен тот же объект
            if (isSameObject)
            {
                UnityEngine.Debug.LogWarning("Same object!");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Not same object!");

                //Указываем его как активный объект
                mOSubpanel.activeSubpanelTab.objectPE = rComp.objectPE;
            }
        }

        /// <summary>
        /// Функция, активирующая запрошенную подпанель и проверяющая, была ли она уже активирована
        /// </summary>
        /// <param name="requestedSubpanel"></param>
        /// <param name="isSamePanel"></param>
        /// <param name="isSameSubpanel"></param>
        void MOSbp_Show(
            UIA_MainOverviewSubpanel requestedSubpanel,
            out bool isSamePanel,
            out bool isSameSubpanel)
        {
            //Берём главную обзорную панель
            UI_MainOverviewPanel mOPanel = uI_Core.Value.gameWindow.mainOverviewPanel;

            //Активируем её, если необходимо
            uI_Core.Value.gameWindow.MainPanel_Activation(
                mOPanel.gameObject,
                out isSamePanel);


            //Значение по умолчанию отрицательно
            isSameSubpanel = false;

            //Если активна необходимая подпанель
            if (mOPanel.activeSubpanel == requestedSubpanel)
            {
                //Если была активна та же панель
                if (isSamePanel)
                {
                    //Сообщаем, что активна та же подпанель
                    isSameSubpanel = true;
                }
            }

            //Если была активна та же панель
            if (isSamePanel)
            {

            }
            else
            {

            }

            //Если была активна та же подпанель 
            if (isSameSubpanel)
            {

            }
            else
            {
                //Активируем запрошенную подпанель

                //Если какая-либо подпанель была активна, скрываем её
                if (mOPanel.activeSubpanel != null)
                {
                    mOPanel.activeSubpanel.gameObject.SetActive(false);
                }

                //Делаем запрошенную подпанель активной
                requestedSubpanel.gameObject.SetActive(true);

                //Указываем её как активную подпанель
                mOPanel.activeSubpanel = requestedSubpanel;
            }
        }

        readonly EcsFilterInject<Inc<R_MainOverviewPanel_Hide>> mOPanel_Hide_R_F = default;
        readonly EcsPoolInject<R_MainOverviewPanel_Hide> mOPanel_Hide_R_P = default;
        void MOPs_Hide()
        {
            //Для каждого запроса сокрытия главной обзорной панели
            foreach(int rEntity in mOPanel_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_MainOverviewPanel_Hide rComp = ref mOPanel_Hide_R_P.Value.Get(rEntity);

                //Скрываем панель
                MOP_Hide();

                //Удаляем запрос
                mOPanel_Hide_R_P.Value.Del(rEntity);
            }
        }

        void MOP_Hide()
        {
            //Берём главную обзорную панель
            UI_MainOverviewPanel mOPanel = uI_Core.Value.gameWindow.mainOverviewPanel;

            //Скрываем активную вкладку активной подпанели
            MOP_ActiveSbpTHide(mOPanel.activeSubpanel);

            //Скрываем активную подпанель
            MOP_ActiveSbpHide(mOPanel);

            //Скрываем активную главную панель, то есть главную обзорную панель
            uI_Core.Value.gameWindow.MainPanel_Hide();
        }

        void MOP_ActiveSbpHide(
            UI_MainOverviewPanel mOPanel)
        {
            //Скрываем активную подпанель
            mOPanel.activeSubpanel.gameObject.SetActive(false);

            //Указываем, что активной подпанели нет
            mOPanel.activeSubpanel = null;
        }

        void MOP_ActiveSbpTHide(
            UIA_MainOverviewSubpanel overviewSubpanel)
        {
            //Скрываем активную вкладку
            overviewSubpanel.activeSubpanelTab.gameObject.SetActive(false);

            //Очищаем сущность активного объекта
            overviewSubpanel.activeSubpanelTab.objectPE = new();

            //Указываем, что активной вкладки нет
            overviewSubpanel.activeSubpanelTab = null;
        }
    }
}
