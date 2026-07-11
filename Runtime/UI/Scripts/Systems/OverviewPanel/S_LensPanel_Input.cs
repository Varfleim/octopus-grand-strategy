
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace GS.UI
{
    public class S_LensPanel_Input : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        EcsWorld uguiUIWorld;
        EcsFilter clickEventUI_F;
        EcsPool<EcsUguiClickEvent> clickEventUI_P;


        readonly EcsCustomInject<UI_Data> uI_Data = default;

        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Init(IEcsSystems systems)
        {
            uguiUIWorld = systems.GetWorld("uguiUIEventsWorld");

            clickEventUI_P = uguiUIWorld.GetPool<EcsUguiClickEvent>();
            clickEventUI_F = uguiUIWorld.Filter<EcsUguiClickEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            //Проверяем клики в главной обзорной панели
            LensPanel_ClickAction();
        }

        readonly EcsPoolInject<R_OverviewPanel_Content_Hide> oP_Content_Hide_R_P = default;
        void LensPanel_ClickAction()
        {
            //Берём панель линз
            UI_LensPanel lensPanel = (UI_LensPanel)uI_Core.Value.gameWindow.overviewPanels[uI_Data.Value.lensPanel.SelfType];

            //Если активна какая-либо линза
            if (lensPanel.activeSubpanel != null)
            {
                //Для каждого события клика по интерфейсу
                foreach (int clickEventUIEntity in clickEventUI_F)
                {
                    //Берём событие
                    ref EcsUguiClickEvent clickEvent = ref clickEventUI_P.Get(clickEventUIEntity);

                    //Проверяем, было ли совершено действие
                    bool isActionComplete = false;

                    //Если нажата кнопка закрытия подпанели
                    if(clickEvent.WidgetName == "CloseLensPanel")
                    {
                        //Запрашиваем сокрытие активной подпанели
                        UI_Data.OverviewP_Content_Hide_R(
                            world.Value,
                            oP_Content_Hide_R_P.Value,
                            lensPanel.SelfType);

                        //Отмечаем, что действие было совершено
                        isActionComplete = true;
                    }
                    //Иначе, если
                    //else if()
                    //{

                    //}

                    //Если действие не совершается, то событие уходит дальше - в модуль игры


                    //Если действие было совершео
                    if (isActionComplete)
                    {
                        UnityEngine.Debug.LogWarning("Click! " + clickEvent.WidgetName);

                        //Удаляем событие
                        clickEventUI_P.Del(clickEventUIEntity);
                    }
                }
            }
        }
    }
}
