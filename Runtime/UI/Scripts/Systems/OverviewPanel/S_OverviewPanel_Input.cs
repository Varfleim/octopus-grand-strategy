
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Ugui;

namespace GS.UI
{
    public class S_OverviewPanel_Input : GBB.VFSystem, IProtoRunSystem
    {
        [DI] UnityUguiAspect unityUgui_A;
        [DI] ProtoIt click_E_I = new(It.Inc<UnityUguiClickEvent>());
        
        [DI] A_UI uI_A;

        public void Run()
        {
            //Проверяем клики в обзорных панелях
            OPs_ClickAction();
        }

        void OPs_ClickAction()
        {
            //Для каждого события клика по интерфейсу
            foreach (ProtoEntity clickEventEntity in click_E_I)
            {
                //Берём событие
                ref UnityUguiClickEvent clickEvent = ref unityUgui_A.ClickEvent.Get(clickEventEntity);

                //Проверяем, было ли совершено какое-либо действие
                bool isActionComplete = false;

                //Если название кнопки пусто
                if(clickEvent.SenderName == "")
                {
                    //Если объект - кнопка подпанели
                    if(clickEvent.Sender.TryGetComponent(out UIA_OverviewSubpanelButton oSbpButton))
                    {
                        //Если панель не активна
                        if(oSbpButton.isSubpanelActive == false)
                        {
                            //Запрашиваем активацию той подпанели, на которую ссылается кнопка
                            uI_A.OverviewSbp_Show_R(
                                oSbpButton.PanelType,
                                oSbpButton.SubpanelType, true);
                        }
                        else
                        {
                            //Запрашиваем деактивацию той панели, на которую ссылается кнопка
                            uI_A.OverviewP_Content_Hide_R(oSbpButton.PanelType);
                        }
                    }
                    //Иначе, если объект - кнопка вкладки
                    else if(clickEvent.Sender.TryGetComponent(out UIA_OverviewTabButton oTButton))
                    {
                        //Запрашиваем активацию той вкладки, на которую ссылается кнопка
                        uI_A.OverviewT_Show_R(
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
                    UnityEngine.Debug.LogWarning("Click! " + clickEvent.SenderName);

                    //Удаляем событие
                    unityUgui_A.ClickEvent.Del(clickEventEntity);
                }
            }
        }
    }
}
