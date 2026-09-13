
using UnityEngine.EventSystems;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;


namespace GS.UI
{
    internal class S_Block_Control : GBB.VFSystem, IProtoRunSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run()
        {
            //Выполняем действия кликов в блоках
            BlockEntityPanels_ClickAction();
        }

        void BlockEntityPanels_ClickAction()
        {
            //Для каждой сущности панели сущности с компонентом действия
            foreach(ProtoEntity bEPEntity in uI_A.bEP_ClickAction_SR_I)
            {
                //Выполняем действие клика
                BlockEntityPanel_ClickAction(bEPEntity);

                uI_A.bEP_ClickAction_SR_P.Del(bEPEntity);
            }
        }

        void BlockEntityPanel_ClickAction(
            ProtoEntity bEPEntity)
        {
            //Берём запрос и компонент панели сущности
            ref SR_BlockEntityPanel_ClickAction rComp = ref uI_A.bEP_ClickAction_SR_P.Get(bEPEntity);
            ref C_BlockEntityPanel bEP = ref uI_A.bEP_P.Get(bEPEntity);

            //Берём компонент блока
            ref C_Block block = ref uI_A.block_P.Get(rComp.blockEntity);
            //Берём шаблон блока
            ref TD_Block blockTemplate = ref uI_Data.blocks_TemplateArray[block.selfType];

            //Если нажата ЛКМ
            if(rComp.inputButton == PointerEventData.InputButton.Left)
            {
                UnityEngine.Debug.LogWarning("LMB BEP_ClickAction!");

                //Открываем вкладку, указанную в данных блока
                uI_A.OverviewT_Show_R(
                    blockTemplate.lMBActionPanelType,
                    blockTemplate.lMBActionSubpanelType,
                    blockTemplate.lMBActionTabType);
            }
            //Иначе, если нажата ПКМ
            else if(rComp.inputButton == PointerEventData.InputButton.Right)
            {
                UnityEngine.Debug.LogWarning("RMB BEP_ClickAction!");
            }
        }
    }
}
