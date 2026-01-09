
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace GS.UI
{
    public class S_ObjectPanel_Input : IEcsInitSystem, IEcsRunSystem
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
            //Проверяем клики в панели объекта
            ObjectPanel_ClickAction();
        }

        readonly EcsPoolInject<R_ObjectPanel_Hide> objectPanel_Hide_R_P = default;
        void ObjectPanel_ClickAction()
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Если активна панель объекта
            if (gameWindow.activeMainPanel == gameWindow.objectPanel.gameObject)
            {
                //Для каждого события клика по интерфейсу
                foreach (int clickEventUIEntity in clickEventUI_F)
                {
                    //Берём событие
                    ref EcsUguiClickEvent clickEvent = ref clickEventUI_P.Get(clickEventUIEntity);

                    //Проверяем, было ли совершено какое-либо действие
                    bool isActionComplete = false;

                    //Если нажата кнопка закрытия панели объекта
                    if (clickEvent.WidgetName == "CloseObjectPanel")
                    {
                        //Запрашиваем сокрытие панели объекта
                        UI_Data.ObjectPanel_Hide_R(
                            world.Value,
                            objectPanel_Hide_R_P.Value);

                        //Отмечаем, что действие было совершено
                        isActionComplete = true;
                    }
                    //Иначе, если
                    //else if()
                    //{

                    //}

                    //Если действие не совершается, то событие уходит дальше - в модуль игры

                    //Если действие было совершено
                    if (isActionComplete == true)
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
