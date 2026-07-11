
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace GS.UI
{
    public class S_OverviewPanel_Input : IEcsInitSystem, IEcsRunSystem
    {
        EcsWorldInject world = default;

        EcsWorld uguiUIWorld;
        EcsFilter clickEventUI_F;
        EcsPool<EcsUguiClickEvent> clickEventUI_P;

        public void Init(IEcsSystems systems)
        {
            uguiUIWorld = systems.GetWorld("uguiUIEventsWorld");

            clickEventUI_P = uguiUIWorld.GetPool<EcsUguiClickEvent>();
            clickEventUI_F = uguiUIWorld.Filter<EcsUguiClickEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            //Проверяем клики в обзорных панелях
            OPs_ClickAction();
        }

        readonly EcsPoolInject<R_OverviewPanel_Content_Hide> oP_Content_Hide_R_P = default;
        readonly EcsPoolInject<R_OverviewSubpanel_Show> oSbp_Show_R_P = default;
        readonly EcsPoolInject<R_OverviewTab_Show> oT_Show_R_P = default;
        void OPs_ClickAction()
        {
            //Для каждого события клика по интерфейсу
            foreach (int clickEventEntity in clickEventUI_F)
            {
                //Берём событие
                ref EcsUguiClickEvent clickEvent = ref clickEventUI_P.Get(clickEventEntity);

                //Проверяем, было ли совершено какое-либо действие
                bool isActionComplete = false;

                //Если название кнопки пусто
                if(clickEvent.WidgetName == "")
                {
                    //Если объект - кнопка подпанели
                    if(clickEvent.Sender.TryGetComponent(out UIA_OverviewSubpanelButton oSbpButton))
                    {
                        //Если панель не активна
                        if(oSbpButton.isSubpanelActive == false)
                        {
                            //Запрашиваем активацию той подпанели, на которую ссылается кнопка
                            UI_Data.OverviewSbp_Show_R(
                                world.Value,
                                oSbp_Show_R_P.Value,
                                oSbpButton.PanelType,
                                oSbpButton.SubpanelType, true);
                        }
                        else
                        {
                            //Запрашиваем деактивацию той панели, на которую ссылается кнопка
                            UI_Data.OverviewP_Content_Hide_R(
                                world.Value,
                                oP_Content_Hide_R_P.Value,
                                oSbpButton.PanelType);
                        }
                    }
                    //Иначе, если объект - кнопка вкладки
                    else if(clickEvent.Sender.TryGetComponent(out UIA_OverviewTabButton oTButton))
                    {
                        //Запрашиваем активацию той вкладки, на которую ссылается кнопка
                        UI_Data.OverviewT_Show_R(
                            world.Value,
                            oT_Show_R_P.Value,
                            oTButton.PanelType,
                            oTButton.SubpanelType,
                            oTButton.TabType);
                    }
                }
                //Если
                //if()
                //{

                //}

                //Если действие не совершается, то событие уходит дальше

                //Если действие было совершено
                if(isActionComplete)
                {
                    UnityEngine.Debug.LogWarning("Click! " + clickEvent.WidgetName);

                    //Удаляем событие
                    clickEventUI_P.Del(clickEventEntity);
                }
            }
        }
    }
}
