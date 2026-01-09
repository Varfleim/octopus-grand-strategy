
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_GameWindow_TickUpdate : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Если активно окно игры
            if (uI_Core.Value.activeWindow == uI_Core.Value.gameWindow.gameObject)
            {
                //Проверяем, не требуется ли обновление в окне игры
                GameWindow_TickUpdate();
            }
        }

        void GameWindow_TickUpdate()
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Если активна панель объекта
            if (gameWindow.activeMainPanel == gameWindow.objectPanel.gameObject)
            {
                //Проверяем, требуется ли обновление в панели объекта
                OP_TickUpdate();
            }
        }

        readonly EcsPoolInject<R_ObjectSubpanelTab_Show> OSbpT_Show_R_P = default;
        void OP_TickUpdate()
        {
            //Берём панель объекта
            UI_ObjectPanel objectPanel = uI_Core.Value.gameWindow.objectPanel;

            //Берём активную подпанель
            UIA_ObjectSubpanel activeSubpanel = objectPanel.activeSubpanel;

            //Берём активную вкладку
            UIA_ObjectSubpanelTab activeSubpanelTab = activeSubpanel.activeTab;

            //Запрашиваем отображение этой подпанели
            UI_Data.ObjectSubpanelTab_Show_R(
                world.Value,
                OSbpT_Show_R_P.Value,
                activeSubpanel.SelfType, activeSubpanelTab.SelfType,
                activeSubpanelTab.objectPE);
        }
    }
}
