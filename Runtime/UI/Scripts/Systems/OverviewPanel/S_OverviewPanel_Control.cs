
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_OverviewPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<R_OverviewPanel_Update> oP_Update_R_P = default;


        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run (IEcsSystems systems)
        {
            //Отображаем обзорные панели
            OPs_Show();

            //Отображаем обзорные подпанели
            OSbps_Show();

            //Отображаем обзорные вкладки
            OTs_Show();

            //Скрываем обзорные панели
            OPs_Hide();

            //Скрываем контент обзорных панелей
            OPs_Content_Hide();

            //Скрываем обзорные подпанели
            OSbps_Hide();

            //Скрываем обзорные вкладки
            OTs_Hide();
        }

        readonly EcsFilterInject<Inc<R_OverviewPanel_Show>> oP_Show_R_F = default;
        void OPs_Show()
        {
            //Для каждого запроса отображения обзорной панели
            foreach (int rEntity in oP_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Show rComp = ref oP_Show_R_F.Pools.Inc1.Get(rEntity);

                //Отображаем панель
                OP_Show(
                    rComp.panelType, rComp.activateContent,
                    out bool isPanelActive, out bool isContentActive);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции отображения

                //Запрашиваем обновление данных в панели
                UI_Data.OverviewP_Update_R(
                    world.Value,
                    oP_Update_R_P.Value,
                    rComp.panelType, 
                    isPanelActive, isContentActive);
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewSubpanel_Show>> oSbp_Show_R_F = default;
        void OSbps_Show()
        {
            //Для каждого запроса отображения обзорной подпанели
            foreach(int rEntity in oSbp_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Show rComp = ref oSbp_Show_R_F.Pools.Inc1.Get(rEntity);

                //Отображаем панель
                OP_Show(
                    rComp.panelType, true,
                    out bool isPanelActive, out bool isPanelContentActive);

                //Отображаем подпанель
                OSbp_Show(
                    rComp.panelType,
                    rComp.subpanelType, rComp.activateDefaultTab,
                    out bool isSubpanelActive);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции отображения

                //Запрашиваем обновление данных в подпанели
                UI_Data.OverviewP_Update_R(
                    world.Value,
                    oP_Update_R_P.Value,
                    rComp.panelType,
                    isPanelActive, isPanelContentActive,
                    isSubpanelActive);
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewTab_Show>> oT_Show_R_F = default;
        void OTs_Show()
        {
            //Для каждого запроса отображения обзорной вкладки
            foreach(int rEntity in oT_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewTab_Show rComp = ref oT_Show_R_F.Pools.Inc1.Get(rEntity);

                //Отображаем панель
                OP_Show(
                    rComp.panelType, true,
                    out bool isPanelActive, out bool isPanelContentActive);

                //Отображаем подпанель
                OSbp_Show(
                    rComp.panelType,
                    rComp.subpanelType, false,
                    out bool isSubpanelActive);

                //Отображаем вкладку
                OT_Show(
                    rComp.panelType,
                    rComp.subpanelType,
                    rComp.tabType,
                    out bool isTabActive);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции отображения

                //Запрашиваем обновление данных во вкладке
                UI_Data.OverviewP_Update_R(
                    world.Value,
                    oP_Update_R_P.Value,
                    rComp.panelType,
                    isPanelActive, isPanelContentActive,
                    isSubpanelActive,
                    isTabActive);
            }
        }

        void OP_Show(
            int panelType, bool activateContent,
            out bool isPanelActive, out bool isContentActive)
        {
            //Значение по умолчанию отрицательно
            isPanelActive = false;
            isContentActive = false;

            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель
            if(gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Если запрошенная панель активна
                if(overviewPanel.gameObject.activeInHierarchy)
                {
                    //Сообщаем, что панель уже была активна
                    isPanelActive = true;
                }
                else
                {
                    //Активируем запрошенную панель
                    overviewPanel.RenderShow(world.Value);
                }

                //Если требуется активировать панель контента
                if(activateContent)
                {
                    //Если панель контента активна
                    if(overviewPanel.contentPanel.activeInHierarchy)
                    {
                        //Сообщаем, что панель уже была активна
                        isContentActive = true;
                    }
                    else
                    {
                        //Активируем запрошенную панель
                        overviewPanel.Content_RenderShow(world.Value);
                    }
                }
            }
        }

        void OSbp_Show(
            int panelType,
            int subpanelType, bool activateDefaultTab,
            out bool isSubpanelActive)
        {
            //Значение по умолчанию отрицательно
            isSubpanelActive = false;

            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель
            if(gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if(overviewPanel.subpanels.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Если запрошенная подпанель активна
                    if(overviewSubpanel.gameObject.activeInHierarchy
                        && overviewPanel.activeSubpanel == overviewSubpanel)
                    {
                        //Сообщаем, что подпанель уже была активна
                        isSubpanelActive = true;
                    }
                    else
                    {
                        //Если какая-либо подпанель уже активна
                        if (overviewPanel.activeSubpanel != null)
                        {
                            //Закрываем активную подпанель
                            OSbp_Hide(
                                panelType,
                                overviewPanel.activeSubpanel.SelfType);
                        }

                        //Отмечаем подпанель как активную
                        overviewPanel.activeSubpanel = overviewSubpanel;

                        //Активируем запрошенную подпанель
                        overviewSubpanel.RenderShow(world.Value);
                    }

                    //Если требуется активировать вкладку по умолчанию
                    if(activateDefaultTab)
                    {
                        //Если панель контента активна
                        //if (overviewSubpanel.contentPanel.activeInHierarchy)
                        //{
                        //    //Сообщаем, что панель уже была активна
                        //    isSubpanelActive = true;
                        //}
                        //else
                        //{
                        //    //Активируем запрошенную панель
                        //    overviewSubpanel.Content_RenderShow(world.Value);
                        //}

                        //ТЕСТ
                        //Активируем первую вкладку
                        OT_Show(
                            panelType,
                            subpanelType,
                            0,
                            out bool isTabActive);
                        //ТЕСТ
                    }

                    //ТЕСТ
                    //Обновляем название панели в заголовке
                    overviewPanel.panelNameText.text = overviewSubpanel.SelfType.ToString();
                    //ТЕСТ
                }
            }
        }

        void OT_Show(
            int panelType,
            int subpanelType,
            int tabType,
            out bool isTabActive)
        {
            //Значение по умолчанию отрицательно
            isTabActive = false;

            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую вкладку
            if(gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if(overviewPanel.subpanels.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Берём соответствующую вкладку
                    if(overviewSubpanel.tabs.TryGetValue(tabType, out UIA_OverviewTab overviewTab))
                    {
                        //Если запрошенная вкладка активна
                        if(overviewTab.gameObject.activeInHierarchy
                            && overviewSubpanel.activeTab == overviewTab)
                        {
                            //Сообщаем, что вкладка уже была активна
                            isTabActive = true;
                        }
                        else
                        {
                            //Если какая-либо вкладка уже активна
                            if(overviewSubpanel.activeTab != null)
                            {
                                //Закрываем активную вкладку
                                OT_Hide(
                                    panelType,
                                    subpanelType,
                                    overviewSubpanel.activeTab.SelfType);
                            }

                            //Отмечаем вкладку как активную
                            overviewSubpanel.activeTab = overviewTab;

                            //Активируем запрошенную вкладку
                            overviewTab.RenderShow(world.Value);
                        }
                    }
                }
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewPanel_Hide>> oPanel_Hide_R_F = default;
        void OPs_Hide()
        {
            //Для каждого запроса сокрытия обзорной панели
            foreach (int rEntity in oPanel_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Hide rComp = ref oPanel_Hide_R_F.Pools.Inc1.Get(rEntity);

                //Скрываем панель
                OP_Hide(
                    rComp.panelType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewPanel_Content_Hide>> oPanel_Content_Hide_R_F = default;
        void OPs_Content_Hide()
        {
            //Для каждого запроса сокрытия контента обзорной панели
            foreach(int rEntity in oPanel_Content_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Content_Hide rComp = ref oPanel_Content_Hide_R_F.Pools.Inc1.Get(rEntity);

                //Скрываем панель контента
                OP_Content_Hide(
                    rComp.panelType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewSubpanel_Hide>> oSbp_Hide_R_F = default;
        void OSbps_Hide()
        {
            //Для каждого запроса сокрытия обзорной подпанели
            foreach(int rEntity in oSbp_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Hide rComp = ref oSbp_Hide_R_F.Pools.Inc1.Get(rEntity);

                //Скрываем панель
                OSbp_Hide(
                    rComp.panelType,
                    rComp.subpanelType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        readonly EcsFilterInject<Inc<R_OverviewTab_Hide>> oT_Hide_R_F = default;
        void OTs_Hide()
        {
            //Для каждого запроса сокрытия обзорной вкладки
            foreach(int rEntity in oT_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewTab_Hide rComp = ref oT_Hide_R_F.Pools.Inc1.Get(rEntity);

                //Скрываем панель
                OT_Hide(
                    rComp.panelType,
                    rComp.subpanelType,
                    rComp.tabType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        void OP_Hide(
            int panelType)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель 
            if (gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём активную подпанель
                if (overviewPanel.activeSubpanel != null)
                {
                    UIA_OverviewSubpanel overviewSubpanel = overviewPanel.activeSubpanel;

                    //Берём активную вкладку
                    if (overviewSubpanel.activeTab != null)
                    {
                        UIA_OverviewTab overviewTab = overviewSubpanel.activeTab;

                        //Убираем активную вкладку
                        overviewSubpanel.activeTab = null;

                        //Закрываем её
                        overviewTab.RenderHide(world.Value);
                    }

                    //Убираем активную подпанель
                    overviewPanel.activeSubpanel = null;

                    //Закрываем её
                    overviewSubpanel.RenderHide(world.Value);
                }

                //Закрываем панель
                overviewPanel.RenderHide(world.Value);
            }
        }

        void OP_Content_Hide(
            int panelType)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель 
            if (gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём активную подпанель
                if (overviewPanel.activeSubpanel != null)
                {
                    UIA_OverviewSubpanel overviewSubpanel = overviewPanel.activeSubpanel;

                    //Берём активную вкладку
                    if (overviewSubpanel.activeTab != null)
                    {
                        UIA_OverviewTab overviewTab = overviewSubpanel.activeTab;

                        //Убираем активную вкладку
                        overviewSubpanel.activeTab = null;

                        //Закрываем её
                        overviewTab.RenderHide(world.Value);
                    }

                    //Убираем активную подпанель
                    overviewPanel.activeSubpanel = null;

                    //Закрываем её
                    overviewSubpanel.RenderHide(world.Value);
                }

                //Закрываем панель контента
                overviewPanel.Content_RenderHide(world.Value);
            }
        }

        void OSbp_Hide(
            int panelType,
            int subpanelType)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель
            if (gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if (overviewPanel.subpanels.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Берём активную вкладку
                    if (overviewSubpanel.activeTab != null)
                    {
                        UIA_OverviewTab overviewTab = overviewSubpanel.activeTab;

                        //Убираем активную вкладку
                        overviewSubpanel.activeTab = null;

                        //Закрываем её
                        overviewTab.RenderHide(world.Value);
                    }

                    //Убираем активную подпанель
                    overviewPanel.activeSubpanel = null;

                    //Закрываем её
                    overviewSubpanel.RenderHide(world.Value);
                }
            }
        }

        void OT_Hide(
            int panelType,
            int subpanelType,
            int tabType)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель
            if (gameWindow.overviewPanels.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if (overviewPanel.subpanels.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Берём соответствующую вкладку
                    if (overviewSubpanel.tabs.TryGetValue(tabType, out UIA_OverviewTab overviewTab))
                    {
                        //Убираем активную вкладку
                        overviewSubpanel.activeTab = null;

                        //Закрываем её
                        overviewTab.RenderHide(world.Value);
                    }
                }
            }
        }
    }
}
