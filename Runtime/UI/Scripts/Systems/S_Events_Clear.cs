
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class S_Events_Clear : GBB.VFSystem, IProtoRunSystem
    {
        [DI] A_UI uI_A;

        public void Run()
        {
            //Очищаем события обзорных панелей
            OverviewP_Events_Clear();

            //Очищаем события блоков
            Block_Events_Clear();

#if DEBUG
            //Очищаем события, которые могут дойти сюда только во время разработки

            //Очищаем события блоков
            Block_Debug_Events_Clear();

            //Очищаем события граф
            DL_Debug_Events_Clear();
#endif
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

            //Для каждого действия клика панели сущности
            foreach(ProtoEntity bEPEntity in uI_A.bEP_ClickAction_SR_I)
            {
                //Берём запрос
                ref SR_BlockEntityPanel_ClickAction rComp = ref uI_A.bEP_ClickAction_SR_P.Get(bEPEntity);
                UnityEngine.Debug.LogWarning("BlockEntityPanel ClickAction!");

                uI_A.bEP_ClickAction_SR_P.Del(bEPEntity);
            }
        }

        void Block_Debug_Events_Clear()
        {
            //Для каждого запроса создания блока
            foreach (ProtoEntity blockEntity in uI_A.block_Creation_SR_I)
            {
                ref SR_Block_Creation rComp = ref uI_A.block_Creation_SR_P.Get(blockEntity);

                UnityEngine.Debug.LogError(
                    //TO DO
                    "Запрос создания блока не был обработан в прослойках! \n" +
                    rComp.ToString());

                uI_A.block_Creation_SR_P.Del(blockEntity);
            }

            //Для каждого запроса обновления блока
            foreach(ProtoEntity blockEntity in uI_A.block_Update_SR_I)
            {
                ref SR_Block_Update rComp = ref uI_A.block_Update_SR_P.Get(blockEntity);

                UnityEngine.Debug.LogError(
                    //TO DO
                    "Запрос обновления блока не был обработан! \n" +
                    rComp.ToString());

                uI_A.block_Update_SR_P.Del(blockEntity);
            }
        }

        [DI] ProtoIt dL_Update_SR_I = new(It.Inc<C_DataLabel, SR_DataLabel_Update>());
        [DI] ProtoIt dL_Destroy_SR_I = new(It.Inc<SR_DataLabel_Destroy>());
        void DL_Debug_Events_Clear()
        {
            //Для каждого запроса создания графы
            foreach(ProtoEntity dLEntity in uI_A.dL_Creation_SR_I)
            {
                ref SR_DataLabel_Creation rComp = ref uI_A.dL_Creation_SR_P.Get(dLEntity);

                UnityEngine.Debug.LogError(
                    //TO DO
                    "Запрос создания графы не был обработан в прослойках! \n" +
                    rComp.ToString());

                uI_A.dL_Creation_SR_P.Del(dLEntity);
            }

            //Для каждого запроса обновления графы
            foreach (ProtoEntity dLEntity in dL_Update_SR_I)
            {
                ref SR_DataLabel_Update rComp = ref uI_A.dL_Update_SR_P.Get(dLEntity);

                UnityEngine.Debug.LogError(
                    //TO DO
                    "Запрос обновления графы не был обработан в прослойках! \n" +
                    rComp.ToString());

                uI_A.dL_Update_SR_P.Del(dLEntity);
            }

            //Для каждого запроса удаления графы
            foreach (ProtoEntity dLEntity in dL_Destroy_SR_I)
            {
                ref SR_DataLabel_Destroy rComp = ref uI_A.dL_Destroy_SR_P.Get(dLEntity);

                UnityEngine.Debug.LogError(
                    //TO DO
                    "Запрос удаления графы не был обработан в прослойках! \n" +
                    rComp.ToString());

                uI_A.dL_Destroy_SR_P.Del(dLEntity);
            }
        }
    }
}
