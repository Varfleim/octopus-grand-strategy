
using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace GS.UI
{
    public class S_Block_Input : IEcsInitSystem, IEcsRunSystem
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
            //Проверяем клики в блоках
            Blocks_ClickAction();
        }

        void Blocks_ClickAction()
        {
            //Для каждого события клика по инитерфейсу
            foreach (int clickEventEntity in clickEventUI_F)
            {
                //Берём событие
                ref EcsUguiClickEvent clickEvent = ref clickEventUI_P.Get(clickEventEntity);

                //Проверяем, было ли совершено какое-либо действие
                bool isActionComplete = false;

                //Если название кнопки пусто
                if(clickEvent.WidgetName == "")
                {
                    //Если родительский объект - блок-список
                    if(clickEvent.Sender.transform.parent.TryGetComponent(out UI_BlockList parentBlockList))
                    {
                        //Запрашиваем действие этого блока, указывая источник события
                        BlockList_Action_R(
                            parentBlockList.SelfEntity,
                            clickEvent.Sender);
                    }
                }
                //Если
                //if()
                //{

                //}

                //Если действие не совершается, то событие уходит дальше

                //Если действие было совершено
                if (isActionComplete)
                {
                    UnityEngine.Debug.LogWarning("Click! " + clickEvent.WidgetName);

                    //Удаляем событие
                    clickEventUI_P.Del(clickEventEntity);
                }
            }
        }

        readonly EcsPoolInject<R_BlockList_Action> bL_Action_R_P = default;
        void BlockList_Action_R(
            int blockEntity,
            GameObject actionObject)
        {
            //Создаём новую сущность и назначаем ей запрос действия блока-списка
            int rEntity = world.Value.NewEntity();
            ref R_BlockList_Action rComp = ref bL_Action_R_P.Value.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                blockEntity,
                actionObject);
        }
    }
}
