
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_OverviewPanel_Control : GBB.VFSystem, IProtoRunSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run ()
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

        void OPs_Show()
        {
            //Для каждого запроса отображения обзорной панели
            foreach (ProtoEntity rEntity in uI_A.oP_Show_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Show rComp = ref uI_A.oP_Show_R_P.Get(rEntity);

                //Отображаем панель
                OP_Show(
                    rComp.panelType, rComp.activateContent,
                    out bool isPanelActive, out bool isContentActive);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции отображения

                //Запрашиваем обновление данных в панели
                uI_A.OverviewP_Update_R(
                    rComp.panelType, 
                    isPanelActive, isContentActive);
            }
        }

        void OSbps_Show()
        {
            //Для каждого запроса отображения обзорной подпанели
            foreach(ProtoEntity rEntity in uI_A.oSbp_Show_R_I)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Show rComp = ref uI_A.oSbp_Show_R_P.Get(rEntity);

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
                uI_A.OverviewP_Update_R(
                    rComp.panelType,
                    isPanelActive, isPanelContentActive,
                    isSubpanelActive);
            }
        }

        void OTs_Show()
        {
            //Для каждого запроса отображения обзорной вкладки
            foreach(ProtoEntity rEntity in uI_A.oT_Show_R_I)
            {
                //Берём запрос
                ref R_OverviewTab_Show rComp = ref uI_A.oT_Show_R_P.Get(rEntity);

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
                uI_A.OverviewP_Update_R(
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

            //Берём соответствующую панель
            if(uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
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
                    overviewPanel.RenderShow();
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
                        overviewPanel.Content_RenderShow();
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

            //Берём соответствующую панель
            if(uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if(uI_Data.oSbpsIndexToObjectDict.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
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
                        overviewSubpanel.RenderShow();
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
                        //    overviewSubpanel.Content_RenderShow(world);
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

            //Берём соответствующую вкладку
            if(uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if(uI_Data.oSbpsIndexToObjectDict.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Берём соответствующую вкладку
                    if(uI_Data.oTsIndexToObjectDict.TryGetValue(tabType, out UIA_OverviewTab overviewTab))
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
                            overviewTab.RenderShow();
                        }
                    }
                }
            }
        }

        void OPs_Hide()
        {
            //Для каждого запроса сокрытия обзорной панели
            foreach (ProtoEntity rEntity in uI_A.oP_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Hide rComp = ref uI_A.oP_Hide_R_P.Get(rEntity);

                //Скрываем панель
                OP_Hide(
                    rComp.panelType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        void OPs_Content_Hide()
        {
            //Для каждого запроса сокрытия контента обзорной панели
            foreach(ProtoEntity rEntity in uI_A.oP_Content_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Content_Hide rComp = ref uI_A.oP_Content_Hide_R_P.Get(rEntity);

                //Скрываем панель контента
                OP_Content_Hide(
                    rComp.panelType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        void OSbps_Hide()
        {
            //Для каждого запроса сокрытия обзорной подпанели
            foreach(ProtoEntity rEntity in uI_A.oSbp_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Hide rComp = ref uI_A.oSbp_Hide_R_P.Get(rEntity);

                //Скрываем панель
                OSbp_Hide(
                    rComp.panelType,
                    rComp.subpanelType);

                //Запрос передаётся дальше, где могут быть особые функции сокрытия
            }
        }

        void OTs_Hide()
        {
            //Для каждого запроса сокрытия обзорной вкладки
            foreach(ProtoEntity rEntity in uI_A.oT_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewTab_Hide rComp = ref uI_A.oT_Hide_R_P.Get(rEntity);

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
            //Берём соответствующую панель 
            if (uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
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
                        overviewTab.RenderHide();
                    }

                    //Убираем активную подпанель
                    overviewPanel.activeSubpanel = null;

                    //Закрываем её
                    overviewSubpanel.RenderHide();
                }

                //Закрываем панель
                overviewPanel.RenderHide();
            }
        }

        void OP_Content_Hide(
            int panelType)
        {
            //Берём соответствующую панель 
            if (uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
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
                        overviewTab.RenderHide();
                    }

                    //Убираем активную подпанель
                    overviewPanel.activeSubpanel = null;

                    //Закрываем её
                    overviewSubpanel.RenderHide();
                }

                //Закрываем панель контента
                overviewPanel.Content_RenderHide();
            }
        }

        void OSbp_Hide(
            int panelType,
            int subpanelType)
        {
            //Берём соответствующую панель
            if (uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if (uI_Data.oSbpsIndexToObjectDict.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Берём активную вкладку
                    if (overviewSubpanel.activeTab != null)
                    {
                        UIA_OverviewTab overviewTab = overviewSubpanel.activeTab;

                        //Убираем активную вкладку
                        overviewSubpanel.activeTab = null;

                        //Закрываем её
                        overviewTab.RenderHide();
                    }

                    //Убираем активную подпанель
                    overviewPanel.activeSubpanel = null;

                    //Закрываем её
                    overviewSubpanel.RenderHide();
                }
            }
        }

        void OT_Hide(
            int panelType,
            int subpanelType,
            int tabType)
        {
            //Берём соответствующую панель
            if (uI_Data.oPsIndexToObjectDict.TryGetValue(panelType, out UIA_OverviewPanel overviewPanel))
            {
                //Берём соответствующую подпанель
                if (uI_Data.oSbpsIndexToObjectDict.TryGetValue(subpanelType, out UIA_OverviewSubpanel overviewSubpanel))
                {
                    //Берём соответствующую вкладку
                    if (uI_Data.oTsIndexToObjectDict.TryGetValue(tabType, out UIA_OverviewTab overviewTab))
                    {
                        //Убираем активную вкладку
                        overviewSubpanel.activeTab = null;

                        //Закрываем её
                        overviewTab.RenderHide();
                    }
                }
            }
        }
    }
}
