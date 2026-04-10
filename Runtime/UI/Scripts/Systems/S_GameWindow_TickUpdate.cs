
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

            //Если панель планировщика активна и есть активная вкладка
            if(gameWindow.outlinerPanel.isActiveAndEnabled 
                && gameWindow.outlinerPanel.activeTab != null)
            {
                //Проверяем, требуется ли обновление в нём
                OutlinerPT_TickUpdate();
            }

            //Если активна главная обзорная панель и есть активная подпанель
            if (gameWindow.activeMainPanel == gameWindow.mainOverviewPanel.gameObject
                && gameWindow.mainOverviewPanel.activeSubpanel != null)
            {
                //Проверяем, требуется ли обновление в ней
                MOPanel_TickUpdate();
            }

            //Обновляем отображаемые панели сущностей
            OPs_TickUpdate();
        }

        readonly EcsPoolInject<R_OutlinerPanelTab_Update> outlinerPT_Update_R_P = default;
        void OutlinerPT_TickUpdate()
        {
            //Берём панель планировщика
            //UI_OutlinerPanel outlinerPanel = uI_Core.Value.gameWindow.outlinerPanel;

            //Берём активную вкладку
            //UIA_OutlinerPanelTab activeOutlinerPT = outlinerPanel.activeTab;

            //Запрашиваем обновление этой вкладки
            UI_Data.OutlinerPT_Update_R(
                world.Value,
                outlinerPT_Update_R_P.Value,
                true);
        }

        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Update> mOSbpT_Update_R_P = default;
        void MOPanel_TickUpdate()
        {
            //Берём главную обзорную панель
            //UI_MainOverviewPanel mOPanel = uI_Core.Value.gameWindow.mainOverviewPanel;

            //Берём активную подпанель
            //UIA_MainOverviewSubpanel activeSubpanel = mOPanel.activeSubpanel;

            //Берём активную вкладку
            //UIA_MainOverviewSubpanelTab activeSubpanelTab = activeSubpanel.activeSubpanelTab;

            //Запрашиваем отображение этой вкладки
            UI_Data.MOSbpT_Update_R(
                world.Value,
                mOSbpT_Update_R_P.Value,
                true,
                true,
                true,
                true);
        }

        readonly EcsFilterInject<Inc<C_EntityDisplayedScreenPanels>> eDSPs_F = default;
        readonly EcsPoolInject<C_EntityDisplayedScreenPanels> eDSPs_P = default;
        readonly EcsFilterInject<Inc<C_EntityDisplayedMapPanels>> eDMPs_F = default;
        readonly EcsPoolInject<C_EntityDisplayedMapPanels> eDMPs_P = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Update> eSP_Update_R_P = default;
        readonly EcsPoolInject<R_EntityMapPanel_Update> eMP_Update_R_P = default;
        void OPs_TickUpdate()
        {
            //Для каждого компонента экранных панелей сущности
            foreach (int entEntity in eDSPs_F.Value)
            {
                //Берём компонент и упаковываем сущность
                ref C_EntityDisplayedScreenPanels oDSPs = ref eDSPs_P.Value.Get(entEntity);
                EcsPackedEntity entPE = world.Value.PackEntity(entEntity);

                //Для каждой панели
                foreach(KeyValuePair<int, UIA_EntityScreenPanel> kVP_OSP in oDSPs.entPanels)
                {
                    //Запрашиваем обновление панели
                    UI_Data.ESP_Update_R(
                        world.Value,
                        eSP_Update_R_P.Value,
                        kVP_OSP.Key,
                        entPE);
                }
            }

            //Для каждого компонента панелей карты сущности
            foreach (int entEntity in eDMPs_F.Value)
            {
                //Берём компонент и упаковываем сущность
                ref C_EntityDisplayedMapPanels oDSPs = ref eDMPs_P.Value.Get(entEntity);
                EcsPackedEntity entPE = world.Value.PackEntity(entEntity);

                //Для каждой панели
                foreach (KeyValuePair<int, UIA_EntityMapPanel> kVP_OMP in oDSPs.entPanels)
                {
                    //Запрашиваем обновление панели
                    UI_Data.EMP_Update_R(
                        world.Value,
                        eMP_Update_R_P.Value,
                        kVP_OMP.Key,
                        entPE);
                }
            }
        }
    }
}
