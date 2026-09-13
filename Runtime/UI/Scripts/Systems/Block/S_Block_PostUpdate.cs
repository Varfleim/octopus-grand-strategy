
using System;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class S_Block_PostUpdate<TSortType> : GBB.VFSystem, IProtoRunSystem
        where TSortType : notnull, IComparable<TSortType>
    {
        [DI] A_UI uI_A;

        ProtoPool<C_Block_Entities<TSortType>> block_Entities_P;
        [DI] ProtoIt block_Entities_Update_SR_I = new(It.Inc<C_Block, C_Block_Entities<TSortType>, SR_Block_Update>());

        [DI] UI_Data uI_Data;

        public void SetType(
            ProtoPool<C_Block_Entities<TSortType>> block_Entities_P)
        {
            this.block_Entities_P = block_Entities_P;
        }

        public void Run()
        {
            //Обновляем блоки
            Blocks_Update();
        }

        void Blocks_Update()
        {
            //Для каждого блока с запросом обновления
            foreach(ProtoEntity blockEntity in block_Entities_Update_SR_I)
            {
                //Обновляем блок
                Block_Update(blockEntity);

                uI_A.block_Update_SR_P.Del(blockEntity);
            }
        }

        void Block_Update(
            ProtoEntity blockEntity)
        {
            //Берём блок и список сущностей
            ref C_Block block = ref uI_A.block_P.Get(blockEntity);
            ref C_Block_Entities<TSortType> blockEntities = ref block_Entities_P.Get(blockEntity);

            //Берём шаблон блока
            ref readonly TD_Block blockTemplate = ref uI_Data.blocks_TemplateArray[block.selfType];

            //Для каждой отображаемой сущности
            for(int a = 0; a < blockEntities.displayedEntities.Length; a++)
            {
                //Отображаем данные сущности
                Entity_DisplayData(
                    in blockTemplate,
                    block.panelEntities[a],
                    blockEntities.displayedEntities[a].entity);
            }
        }

        void Entity_DisplayData(
            in TD_Block blockTemplate,
            ProtoEntity bEPEntity,
            ProtoEntity displayedEntity)
        {
            //Берём панель сущности
            ref C_BlockEntityPanel bEP = ref uI_A.bEP_P.Get(bEPEntity);

            //Берём хранилище данных сущности
            ref C_DataLabel_Container eDC = ref uI_A.dLC_P.Get(displayedEntity);

            //Для каждой графы в шаблоне
            for(int a = 0; a < blockTemplate.dataTypeIndexes.Length; a++)
            {
                //Берём шаблон графы
                ref readonly TD_DataLabel dLTemplate
                    = ref uI_Data.dataLabels_TemplateArray[blockTemplate.dataTypeIndexes[a]];

                //Отображаем данные графы в соответствующей панели
                DL_DisplayData(
                    dLTemplate, bEP.dLPanels[a],
                    eDC.dataLabelEntities[blockTemplate.dataTypeIndexes[a]]);
            }
        }

        void DL_DisplayData(
            TD_DataLabel dLTemplate, UI_DataLabelPanel dLPanel,
            ProtoEntity dLEntity)
        {
            //Берём компонент-хранилище соответственно типу данных
            if(dLTemplate.dataType == DataType.Integer)
            {
                ref C_DataLabel_Value<int> dLInt = ref uI_A.dL_Int_P.Get(dLEntity);
                dLPanel.dataValue.text = dLInt.DataValue.ToString();
            }
            else if(dLTemplate.dataType == DataType.Float)
            {
                ref C_DataLabel_Value<float> dLFloat = ref uI_A.dL_Float_P.Get(dLEntity);
                dLPanel.dataValue.text = dLFloat.DataValue.ToString();
            }
            else if(dLTemplate.dataType == DataType.String)
            {
                ref C_DataLabel_Value<string> dLString = ref uI_A.dL_String_P.Get(dLEntity);
                dLPanel.dataValue.text = dLString.DataValue.ToString();
            }
        }
    }
}
