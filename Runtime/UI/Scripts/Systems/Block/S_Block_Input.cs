
using UnityEngine.EventSystems;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Ugui;

namespace GS.UI
{
    internal class S_Block_Input : GBB.VFSystem, IProtoRunSystem
    {
        [DI] UnityUguiAspect unityUgui_A;
        [DI] ProtoIt click_E_I = new(It.Inc<UnityUguiClickEvent>());

        [DI] A_UI uI_A;

        public void Run()
        {
            //Проверяем клики в блоках
            Blocks_ClickAction();
        }

        void Blocks_ClickAction()
        {
            //Для каждого события клика по инитерфейсу
            foreach (ProtoEntity clickEventEntity in click_E_I)
            {
                //Берём событие
                ref UnityUguiClickEvent clickEvent = ref unityUgui_A.ClickEvent.Get(clickEventEntity);

                //Проверяем, было ли совершено какое-либо действие
                bool isActionComplete = false;

                //Если название кнопки пусто
                if(clickEvent.SenderName == "")
                {
                    //Если родительский объект - блок
                    if(clickEvent.Sender.transform.parent.TryGetComponent(out UI_Block parentBlock))
                    {
                        //Если сообщающий объект - панель сущности
                        if(clickEvent.Sender.TryGetComponent(out UI_BlockEntityPanel blockEntityPanel))
                        {
                            //Запрашиваем действие блока
                            BlockEntityPanel_ClickAction_R(
                                parentBlock.selfEntity,
                                blockEntityPanel.selfEntity,
                                clickEvent.Button);
                        }
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
                    UnityEngine.Debug.LogWarning("Click! " + clickEvent.SenderName);

                    //Удаляем событие
                    unityUgui_A.ClickEvent.Del(clickEventEntity);
                }
            }
        }

        void BlockEntityPanel_ClickAction_R(
            ProtoEntity blockEntity,
            ProtoEntity bEPEntity,
            PointerEventData.InputButton inputButton)
        {
            //Создаём новую сущность и назначаем ей запрос действия клика блока
            ref SR_BlockEntityPanel_ClickAction rComp = ref uI_A.bEP_ClickAction_SR_P.Add(bEPEntity);

            //Заполняем данные запроса
            rComp = new(
                blockEntity,
                inputButton);
        }
    }
}
