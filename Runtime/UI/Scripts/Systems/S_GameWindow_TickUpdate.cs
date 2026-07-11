
using System.Collections.Generic;

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

            //Для каждой обзорной панели
            foreach (KeyValuePair<int, UIA_OverviewPanel> kVP_OP in gameWindow.overviewPanels)
            {
                //Обновляем панель, если необходимо
                OP_TickUpdate(kVP_OP.Value);
            }
        }

        readonly EcsPoolInject<R_OverviewPanel_Update> oP_Update_R_P = default;
        void OP_TickUpdate(
            UIA_OverviewPanel overviewPanel)
        {
            //Если панель активна
            if(overviewPanel.gameObject.activeInHierarchy)
            {
                //Поскольку панель уже открыта, то производим везде неполное обновление
                UI_Data.OverviewP_Update_R(
                    world.Value,
                    oP_Update_R_P.Value,
                    overviewPanel.SelfType,
                    true, true,
                    true,
                    true);
            }
        }
    }
}
