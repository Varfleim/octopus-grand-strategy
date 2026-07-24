
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_Events_Clear : IProtoRunSystem
    {
        [DI] A_UI uI_A;

        public void Run()
        {
            //Очищаем события обзорных панелей
            OverviewP_Events_Clear();

            //Очищаем события блоков
            Block_Events_Clear();
        }

        void OverviewP_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса отображения обзорной панели
            foreach (ProtoEntity rEntity in uI_A.oP_Show_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Show rComp = ref uI_A.oP_Show_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewP Show! PanelID: " + rComp.panelType);

                uI_A.oP_Show_R_P.Del(rEntity);
            }

            //Для каждого запроса отображения обзорной подпанели
            foreach (ProtoEntity rEntity in uI_A.oSbp_Show_R_I)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Show rComp = ref uI_A.oSbp_Show_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewSbp Show! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType);

                uI_A.oSbp_Show_R_P.Del(rEntity);
            }

            //Для каждого запроса отображения обзорной вкладки
            foreach (ProtoEntity rEntity in uI_A.oT_Show_R_I)
            {
                //Берём запрос
                ref R_OverviewTab_Show rComp = ref uI_A.oT_Show_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewT Show! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType 
                    + " TabID: " + rComp.tabType);

                uI_A.oT_Show_R_P.Del(rEntity);
            }


            //Для каждого запроса обновления обзорной панели
            foreach(ProtoEntity rEntity in uI_A.oP_Update_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Update rComp = ref uI_A.oP_Update_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning(
                    "OverviewP Update! PanelID: " + rComp.panelType 
                    + "\nPanelFullUpdate: " + !rComp.isPanelAlreadyActive + " PanelContentFullUpdate: " + !rComp.isPanelContentAlreadyActive 
                    + "\nSubpanelFullUpdate: " + !rComp.isSubpanelAlreadyActive
                    + "\nTabFullUpdate: " + !rComp.isTabAlreadyActive);

                uI_A.oP_Update_R_P.Del(rEntity);
            }


            //Для каждого запроса сокрытия обзорной панели
            foreach(ProtoEntity rEntity in uI_A.oP_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Hide rComp = ref uI_A.oP_Hide_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewP Hide! PanelID: " + rComp.panelType);

                uI_A.oP_Hide_R_P.Del(rEntity);
            }

            //Для каждого запроса сокрытия контента обзорной панели
            foreach (ProtoEntity rEntity in uI_A.oP_Content_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Content_Hide rComp = ref uI_A.oP_Content_Hide_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewP Content Hide! PanelID: " + rComp.panelType);

                uI_A.oP_Content_Hide_R_P.Del(rEntity);
            }

            //Для каждого запроса сокрытия обзорной подпанели
            foreach (ProtoEntity rEntity in uI_A.oSbp_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Hide rComp = ref uI_A.oSbp_Hide_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewSbp Hide! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType);

                uI_A.oSbp_Hide_R_P.Del(rEntity);
            }

            //Для каждого запроса сокрытия обзорной вкладки
            foreach (ProtoEntity rEntity in uI_A.oT_Hide_R_I)
            {
                //Берём запрос
                ref R_OverviewTab_Hide rComp = ref uI_A.oT_Hide_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewT Hide! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType 
                    + " TabID: " + rComp.tabType);

                uI_A.oT_Hide_R_P.Del(rEntity);
            }
        }

        void Block_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса действия блока-списка
            foreach(ProtoEntity rEntity in uI_A.bL_Action_R_I)
            {
                //Берём запрос
                ref R_BlockList_Action rComp = ref uI_A.bL_Action_R_P.Get(rEntity);
                UnityEngine.Debug.LogWarning("BlockList Action!");

                uI_A.bL_Action_R_P.Del(rEntity);
            }
        }
    }
}
