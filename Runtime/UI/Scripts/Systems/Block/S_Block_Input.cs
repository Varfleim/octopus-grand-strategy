
using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Ugui;

namespace GS.UI
{
    public class S_Block_Input : GBB.VFSystem, IProtoRunSystem
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
                    UnityEngine.Debug.LogWarning("Click! " + clickEvent.SenderName);

                    //Удаляем событие
                    unityUgui_A.ClickEvent.Del(clickEventEntity);
                }
            }
        }

        void BlockList_Action_R(
            ProtoEntity bEntity,
            GameObject actionObject)
        {
            //Создаём новую сущность и назначаем ей запрос действия блока-списка
            ref R_BlockList_Action rComp = ref uI_A.bL_Action_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                bEntity,
                actionObject);
        }
    }
}
