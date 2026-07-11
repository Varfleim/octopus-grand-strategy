
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_OverviewPanel_Initialization : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;


        //readonly EcsPoolInject<R_OverviewPanel_Show> oP_Show_R_P = default;


        readonly EcsCustomInject<UI_Data> uI_Data = default;

        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Init(IEcsSystems systems)
        {
            //Заносим обзорные панели в словарь
            OPs_Initialization();

            //ТЕСТ            
            //Создаём запрос
            //UI_Data.OverviewP_Show_R(
            //    world.Value,
            //    oP_Show_R_P.Value,
            //    uI_Data.Value.mainOverviewPanel.SelfType);

            //UI_Data.OverviewP_Show_R(
            //    world.Value,
            //    oP_Show_R_P.Value,
            //    uI_Data.Value.outlinerPanel.SelfType);
            //UI_Data.OverviewP_Show_R(
            //    world.Value,
            //    oP_Show_R_P.Value,
            //    uI_Data.Value.outlinerPanel.SelfType);

            //UI_Data.OverviewP_Show_R(
            //    world.Value,
            //    oP_Show_R_P.Value,
            //    uI_Data.Value.lensPanel.SelfType);
            //ТЕСТ

            //Создаём обзорные подпанели
            OSbps_Creation();

            //Создаём обзорные вкладки
            OTs_Creation();
        }

        void OPs_Initialization()
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Для каждой обзорной панели в списке
            for(int a = 0; a < uI_Data.Value.overviewPanelsList.Count; a++)
            {
                //Берём панель
                UIA_OverviewPanel overviewPanel = uI_Data.Value.overviewPanelsList[a];

                //Преобразуем код панели в тип
                overviewPanel.SelfType = a;
                gameWindow.overviewPanelsTypes.Add(overviewPanel.selfCode, overviewPanel.SelfType);
                gameWindow.overviewPanels.Add(overviewPanel.SelfType, overviewPanel);
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewSubpanel_Creation>> oSbp_Creation_R_F = default;
        void OSbps_Creation()
        {
            //Для каждого запроса создания подпанели
            foreach(int rEntity in oSbp_Creation_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Creation rComp = ref oSbp_Creation_R_F.Pools.Inc1.Get(rEntity);

                //Создаём подпанель
                OSbp_Creation(ref rComp);

                //Удаляем запрос
                oSbp_Creation_R_F.Pools.Inc1.Del(rEntity);
            }
        }

        void OSbp_Creation(
            ref R_OverviewSubpanel_Creation rComp)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Получаем тип родительской панели
            int parentPanelType = -1;
            foreach(KeyValuePair<string, int> kVP_PanelType in gameWindow.overviewPanelsTypes)
            {
                //Если код соответствует
                if(kVP_PanelType.Key == rComp.parentPanelCode)
                {
                    parentPanelType = kVP_PanelType.Value;

                    break;
                }
            }

            //Если родительская панель была найдена
            if(parentPanelType != -1)
            {
                //Берём её
                UIA_OverviewPanel overviewPanel = gameWindow.overviewPanels[parentPanelType];

                //Инстанциируем префаб подпанели и сразу прикрепляем к панели
                UIA_OverviewSubpanel overviewSubpanel = GameObject.Instantiate(overviewPanel.subpanelPrefab, overviewPanel.subpanelsGroup.transform);

                //Преобразуем код подпанели в тип и сохраняем
                overviewSubpanel.SelfType = overviewPanel.subpanels.Count;
                overviewPanel.subpanelTypes.Add(rComp.subpanelCode, overviewSubpanel.SelfType);
                overviewPanel.subpanels.Add(overviewSubpanel.SelfType, overviewSubpanel);

                //Деактивируем подпанель после создания
                overviewSubpanel.RenderHide(world.Value);

                //Если подпанель должна иметь кнопку
                if(rComp.hasButton)
                {
                    //Инстанциируем префаб кнопки и сразу прикрепляем к группе кнопок
                    UIA_OverviewSubpanelButton subpanelButton = GameObject.Instantiate(overviewPanel.subpanelButtonPrefab, overviewPanel.buttonsGroup.transform);
                    overviewSubpanel.selfButton = subpanelButton;

                    //Заполняем коды в кнопке
                    subpanelButton.PanelType = overviewPanel.SelfType;
                    subpanelButton.SubpanelType = overviewSubpanel.SelfType;

                    //ТЕСТ
                    //Заполняем название кнопки
                    subpanelButton.buttonNameText.text = rComp.subpanelCode;
                    //ТЕСТ
                }
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewTab_Creation>> oT_Creation_R_F = default;
        void OTs_Creation()
        {
            //Для каждого запроса создания вкладки
            foreach (int rEntity in oT_Creation_R_F.Value)
            {
                //Берём запрос 
                ref R_OverviewTab_Creation rComp = ref oT_Creation_R_F.Pools.Inc1.Get(rEntity);

                //Создаём подпанель
                OT_Creation(ref rComp);

                //Удаляем запрос
                oT_Creation_R_F.Pools.Inc1.Del(rEntity);
            }
        }

        void OT_Creation(
            ref R_OverviewTab_Creation rComp)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Получаем тип родительской панели
            int parentPanelType = -1;
            foreach (KeyValuePair<string, int> kVP_PanelType in gameWindow.overviewPanelsTypes)
            {
                //Если код соответствует
                if (kVP_PanelType.Key == rComp.parentPanelCode)
                {
                    parentPanelType = kVP_PanelType.Value;

                    break;
                }
            }

            //Если родительская панель была найдена
            if (parentPanelType != -1)
            {
                //Берём её
                UIA_OverviewPanel overviewPanel = gameWindow.overviewPanels[parentPanelType];

                //Получаем тип родительской подпанели
                int parentSubpanelType = -1;
                foreach(KeyValuePair<string, int> kVP_SubpanelType in overviewPanel.subpanelTypes)
                {
                    //Если код соответствует
                    if(kVP_SubpanelType.Key == rComp.parentSubpanelCode)
                    {
                        parentSubpanelType = kVP_SubpanelType.Value;

                        break;
                    }
                }

                //Если родительская подпанель была найдене
                if(parentSubpanelType != -1)
                {
                    //Берём её
                    UIA_OverviewSubpanel overviewSubpanel = overviewPanel.subpanels[parentSubpanelType];

                    //Инстанциируем префаб вкладки и сразу прикрепляем к подпанели
                    UIA_OverviewTab overviewTab = GameObject.Instantiate(overviewPanel.tabPrefab, overviewSubpanel.contentPanel.transform);

                    //Преобразуем код вкладки в тип и сохраняем
                    overviewTab.SelfType = overviewSubpanel.tabs.Count;
                    overviewSubpanel.tabTypes.Add(rComp.tabCode, overviewTab.SelfType);
                    overviewSubpanel.tabs.Add(overviewTab.SelfType, overviewTab);

                    //Деактивируем вкладку после создания
                    overviewTab.RenderHide(world.Value);

                    //Инстанциируем префаб кнопки и сразу прикрепляем к группе кнопок
                    UIA_OverviewTabButton tabButton = GameObject.Instantiate(overviewPanel.tabButtonPrefab, overviewSubpanel.buttonsGroup.transform);

                    //Заполняем коды в кнопке
                    tabButton.PanelType = overviewPanel.SelfType;
                    tabButton.SubpanelType = overviewSubpanel.SelfType;
                    tabButton.TabType = overviewTab.SelfType;

                    //ТЕСТ
                    //Заполняем название кнопки
                    tabButton.buttonNameText.text = rComp.tabCode;
                    //ТЕСТ
                }
            }
        }
    }
}
