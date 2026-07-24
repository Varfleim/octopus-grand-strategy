
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Ugui;

namespace GS.UI
{
    public class S_MainOverviewPanel_Input : IProtoRunSystem
    {
        [DI] UnityUguiAspect unityUgui_A;
        [DI] ProtoIt click_E_I = new(It.Inc<UnityUguiClickEvent>());

        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run()
        {
            //Проверяем клики в главной обзорной панели
            MainOverviewPanel_ClickAction();
        }

        void MainOverviewPanel_ClickAction()
        {
            //Берём главную обзорную панель
            UI_MainOverviewPanel mainOverviewPanel = (UI_MainOverviewPanel)uI_Data.oPsIndexToObjectDict[uI_Data.mainOverviewPanel.SelfType];

            //Если она активна
            if (mainOverviewPanel.gameObject.activeInHierarchy)
            {
                //Для каждого события клика по интерфейсу
                foreach (ProtoEntity clickEventUIEntity in click_E_I)
                {
                    //Берём событие
                    ref UnityUguiClickEvent clickEvent = ref unityUgui_A.ClickEvent.Get(clickEventUIEntity);

                    //Проверяем, было ли совершено какое-либо действие
                    bool isActionComplete = false;

                    //Если нажата кнопка закрытия панели
                    if (clickEvent.SenderName == "CloseMainOverviewPanel")
                    {
                        //Запрашиваем её сокрытие
                        uI_A.OverviewP_Content_Hide_R(mainOverviewPanel.SelfType);

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
