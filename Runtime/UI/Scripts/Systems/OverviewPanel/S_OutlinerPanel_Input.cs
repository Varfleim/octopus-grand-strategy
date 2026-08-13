
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Ugui;

namespace GS.UI
{
    public class S_OutlinerPanel_Input : GBB.VFSystem, IProtoRunSystem
    {
        [DI] UnityUguiAspect unityUgui_A;
        [DI] ProtoIt click_E_I = new(It.Inc<UnityUguiClickEvent>());

        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run()
        {
            //Проверяем клики в панели планировщика
            OutlinerPanel_ClickAction();
        }

        void OutlinerPanel_ClickAction()
        {
            //Берём панель планировщика
            UI_OutlinerPanel outlinerPanel = (UI_OutlinerPanel)uI_Data.oPsIndexToObjectDict[uI_Data.outlinerPanel.SelfType];

            //Если она активна
            if(outlinerPanel.gameObject.activeInHierarchy)
            {
                //Для каждого события клика по интефейсу
                foreach(ProtoEntity clickEventUIEntity in click_E_I)
                {
                    //Берём событие
                    ref UnityUguiClickEvent clickEvent = ref unityUgui_A.ClickEvent.Get(clickEventUIEntity);

                    //Проверяем, было ли совершено какое-либо действие
                    bool isActionComplete = false;

                    //Если нажата кнопка закрытия панели
                    if (clickEvent.SenderName == "CloseOutlinerPanel")
                    {
                        //Запрашиваем её сокрытие
                        uI_A.OverviewP_Content_Hide_R(outlinerPanel.SelfType);

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
                        UnityEngine.Debug.LogWarning("Click! " + clickEvent.SenderName);

                        //Удаляем событие
                        unityUgui_A.ClickEvent.Del(clickEventUIEntity);
                    }
                }
            }
        }
    }
}
