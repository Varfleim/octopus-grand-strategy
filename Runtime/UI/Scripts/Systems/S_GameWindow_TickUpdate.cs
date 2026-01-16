
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

            //Обновляем отображаемые панели объектов
            OPs_TickUpdate();
        }

        readonly EcsPoolInject<R_OutlinerPanelTab_Show> outlinerPT_Show_R_P = default;
        void OutlinerPT_TickUpdate()
        {
            //Берём панель планировщика
            UI_OutlinerPanel outlinerPanel = uI_Core.Value.gameWindow.outlinerPanel;

            //Берём активную вкладку
            UIA_OutlinerPanelTab activeOutlinerPT = outlinerPanel.activeTab;

            //Запрашиваем отображение этой вкладки
            UI_Data.OutlinerPT_Show_R(
                world.Value,
                outlinerPT_Show_R_P.Value,
                activeOutlinerPT.SelfType);
        }

        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Show> mOSbpT_Show_R_P = default;
        void MOPanel_TickUpdate()
        {
            //Берём главную обзорную панель
            UI_MainOverviewPanel mOPanel = uI_Core.Value.gameWindow.mainOverviewPanel;

            //Берём активную подпанель
            UIA_MainOverviewSubpanel activeSubpanel = mOPanel.activeSubpanel;

            //Берём активную вкладку
            UIA_MainOverviewSubpanelTab activeSubpanelTab = activeSubpanel.activeSubpanelTab;

            //Запрашиваем отображение этой вкладки
            UI_Data.MOSbpT_Show_R(
                world.Value,
                mOSbpT_Show_R_P.Value,
                activeSubpanel.SelfType, activeSubpanelTab.SelfType,
                activeSubpanelTab.objectPE);
        }

        readonly EcsFilterInject<Inc<C_ObjectDisplayedScreenPanels>> oDSPs_F = default;
        readonly EcsPoolInject<C_ObjectDisplayedScreenPanels> oDSPs_P = default;
        readonly EcsFilterInject<Inc<C_ObjectDisplayedMapPanels>> oDMPs_F = default;
        readonly EcsPoolInject<C_ObjectDisplayedMapPanels> oDMPs_P = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Update> oSP_Update_R_P = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Update> oMP_Update_R_P = default;
        void OPs_TickUpdate()
        {
            //Для каждого компонента экранных панелей объекта
            foreach(int objectEntity in oDSPs_F.Value)
            {
                //Берём компонент и упаковываем сущность объекта
                ref C_ObjectDisplayedScreenPanels oDSPs = ref oDSPs_P.Value.Get(objectEntity);
                EcsPackedEntity objectPE = world.Value.PackEntity(objectEntity);

                //Для каждой панели
                foreach(KeyValuePair<int, UIA_ObjectScreenPanel> kVP_OSP in oDSPs.objectPanels)
                {
                    //Запрашиваем обновление панели
                    UI_Data.OSP_Update_R(
                        world.Value,
                        oSP_Update_R_P.Value,
                        kVP_OSP.Key,
                        objectPE);
                }
            }

            //Для каждого компонента панелей карты объекта
            foreach (int objectEntity in oDMPs_F.Value)
            {
                //Берём компонент и упаковываем сущность объекта
                ref C_ObjectDisplayedMapPanels oDSPs = ref oDMPs_P.Value.Get(objectEntity);
                EcsPackedEntity objectPE = world.Value.PackEntity(objectEntity);

                //Для каждой панели
                foreach (KeyValuePair<int, UIA_ObjectMapPanel> kVP_OMP in oDSPs.objectPanels)
                {
                    //Запрашиваем обновление панели
                    UI_Data.OMP_Update_R(
                        world.Value,
                        oMP_Update_R_P.Value,
                        kVP_OMP.Key,
                        objectPE);
                }
            }
        }
    }
}
