
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_OutlinerPanel_Control : IEcsRunSystem
    {
        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем вкладку планировщика
            OutlinerPTs_Show();

            //Скрываем панель планировщика
            OutlinerPs_Hide();
        }

        readonly EcsFilterInject<Inc<R_OutlinerPanelTab_Show>> outlinerPT_Show_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanelTab_Show> outlinerPT_Show_R_P = default;
        readonly EcsPoolInject<R_OutlinerPanelTab_Update> outlinerPT_Update_R_P = default;
        void OutlinerPTs_Show()
        {
            //Для каждого запроса отображения вкладки планировщика
            foreach(int rEntity in outlinerPT_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OutlinerPanelTab_Show rComp = ref outlinerPT_Show_R_P.Value.Get(rEntity);

                //Отображаем вкладку
                OutlinerPT_Show(
                    ref rComp,
                    out bool isSameTab);

                //Запрос передаётся дальше, в модуль игры, где могут быть особые функции отображения

                //Запрашиваем обновление данных во вкладке
                UI_Data.OutlinerPT_Update_R(
                    outlinerPT_Update_R_P.Value,
                    rEntity,
                    isSameTab);
            }
        }

        void OutlinerPT_Show(
            ref R_OutlinerPanelTab_Show rComp,
            out bool isSameTab)
        {
            //Берём панель планировщика
            UI_OutlinerPanel outlinerPanel = uI_Core.Value.gameWindow.outlinerPanel;

            //Берём запрошенную вкладку
            UIA_OutlinerPanelTab requestedTab = outlinerPanel.tabs[rComp.outlinerPanelTabType];

            //Значения по умолчанию отрицательны
            isSameTab = false;

            //Если активна необходимая вкладка
            if(outlinerPanel.activeTab == requestedTab)
            {
                //Сообщаем, что была активна та же вкладка
                isSameTab = true;
            }

            //Если была активна та же вкладка
            if(isSameTab)
            {
                UnityEngine.Debug.LogWarning("Same outliner tab!");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Not same outliner tab!");

                //Активируем запрошенную вкладку
                outlinerPanel.tabGroup.OnTabSelected(requestedTab.selfTabButton);

                //Указываем её как активную вкладку
                outlinerPanel.activeTab = requestedTab;
            }
        }

        readonly EcsFilterInject<Inc<R_OutlinerPanel_Hide>> outlinerP_Hide_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanel_Hide> outlinerP_Hide_R_P = default;
        void OutlinerPs_Hide()
        {
            //Для каждого запроса сокрытия панели планировщика
            foreach(int rEntity in outlinerP_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OutlinerPanel_Hide rComp = ref outlinerP_Hide_R_P.Value.Get(rEntity);

                //Скрываем панель
                OutlinerP_Hide();

                //Запрос передаётся дальше, в модуль игры, где могут быть особые функции сокрытия
            }
        }

        void OutlinerP_Hide()
        {
            //Берём панель планировщика
            UI_OutlinerPanel outlinerPanel = uI_Core.Value.gameWindow.outlinerPanel;

            //Скрываем активную вкладку
            OutlinerP_ActiveTHide(outlinerPanel);

            //Скрываем панель планировщика
            uI_Core.Value.gameWindow.outlinerPanel.gameObject.SetActive(false);
        }

        void OutlinerP_ActiveTHide(
            UI_OutlinerPanel outlinerPanel)
        {
            //Скрываем активную вкладку планировщика
            outlinerPanel.activeTab.gameObject.SetActive(false);

            //УКазываем, что активной вкладки нет
            outlinerPanel.activeTab = null;
        }
    }
}
