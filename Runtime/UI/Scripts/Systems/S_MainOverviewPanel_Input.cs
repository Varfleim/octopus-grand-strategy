
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace GS.UI
{
    public class S_MainOverviewPanel_Input : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        EcsWorld uguiUIWorld;
        EcsFilter clickEventUI_F;
        EcsPool<EcsUguiClickEvent> clickEventUI_P;


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
            MainOverviewPanel_ClickAction();
        }

        readonly EcsPoolInject<R_MainOverviewPanel_Hide> mOPanel_Hide_R_P = default;
        void MainOverviewPanel_ClickAction()
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Если активна главная обзорная панель
            if (gameWindow.activeMainPanel == gameWindow.mainOverviewPanel.gameObject)
            {
                //Для каждого события клика по интерфейсу
                foreach (int clickEventUIEntity in clickEventUI_F)
                {
                    //Берём событие
                    ref EcsUguiClickEvent clickEvent = ref clickEventUI_P.Get(clickEventUIEntity);

                    //Проверяем, было ли совершено какое-либо действие
                    bool isActionComplete = false;

                    //Если нажата кнопка закрытия панели
                    if (clickEvent.WidgetName == "CloseMainOverviewPanel")
                    {
                        //Запрашиваем её сокрытие
                        UI_Data.MOP_Hide_R(
                            world.Value,
                            mOPanel_Hide_R_P.Value);

                        //Отмечаем, что действие было совершено
                        isActionComplete = true;
                    }
                    //Иначе, если
                    //else if()
                    //{

                    //}

                    //Если действие не совершается, то событие уходит дальше - в модуль игры

                    //Если действие было совершено
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
