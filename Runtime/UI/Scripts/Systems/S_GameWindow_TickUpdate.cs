
using System.Collections.Generic;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_GameWindow_TickUpdate : GBB.VFSystem, IProtoRunSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;
        [DI] UI_Core uI_Core;

        public void Run()
        {
            //Если активно окно игры
            if (uI_Core.activeWindow == uI_Core.gameWindow.gameObject)
            {
                //Проверяем, не требуется ли обновление в окне игры
                GameWindow_TickUpdate();
            }
        }

        void GameWindow_TickUpdate()
        {
            //Для каждой обзорной панели
            foreach (KeyValuePair<int, UIA_OverviewPanel> kVP_OP in uI_Data.oPsIndexToObjectDict)
            {
                //Обновляем панель, если необходимо
                OP_TickUpdate(kVP_OP.Value);
            }
        }

        void OP_TickUpdate(
            UIA_OverviewPanel overviewPanel)
        {
            //Если панель активна
            if(overviewPanel.gameObject.activeInHierarchy)
            {
                //Поскольку панель уже открыта, то производим везде неполное обновление
                uI_A.OverviewP_Update_R(
                    overviewPanel.SelfType,
                    true, true,
                    true,
                    true);
            }
        }
    }
}
