
using System;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Threads;

namespace GS.UI
{
    internal class SMT_Block_PostUpdate<TSortType> : GBB.VFSystem, IProtoRunSystem
        where TSortType : notnull, IComparable<TSortType>
    {
        [DI] A_UI uI_A;

        ProtoPool<C_Block_Entities<TSortType>> block_Entities_P;
        [DI] ProtoIt block_Entities_Update_SR_I = new(It.Inc<C_Block, C_Block_Entities<TSortType>, SR_Block_Update>());

        ProtoPool<C_DataLabel_Value<TSortType>> dL_Value_P;

        [DI] UI_Data uI_Data;

        ProtoThreadHandler threadHandler;

        public void SetType(
            ProtoPool<C_Block_Entities<TSortType>> block_Entities_P,
            ProtoPool<C_DataLabel_Value<TSortType>> dL_Value_P)
        {
            this.block_Entities_P = block_Entities_P;

            this.dL_Value_P = dL_Value_P;
        }

        public void Run()
        {
            //Запускаем параллельную обработку
            block_Entities_Update_SR_I.RunParallel(
                threadHandler ??= Blocks_Update, 
                uI_Data.BlocksUpdatePerThreadCount);
        }

        void Blocks_Update(ProtoThreadIt threadIt)
        {
            //Для каждого блока с запросом обновления
            foreach(ProtoEntity blockEntity in threadIt)
            {
                //Обновляем блок
                Block_Update(blockEntity);
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

            //Определяем, по какой графе идёт сортировка
            int dataTypeIndex = blockTemplate.dataTypeIndexes[block.sortingDataType];

            //Для каждой отображаемой сущности
            for (int a = 0; a < blockEntities.displayedEntities.Length; a++)
            {
                //Берём хранилище данных сущности
                ref C_DataLabel_Container dLC = ref uI_A.dLC_P.Get(blockEntities.displayedEntities[a].entity);

                //Берём графу, по которой идёт сортировка
                ref C_DataLabel_Value<TSortType> dLValue 
                    = ref dL_Value_P.Get(dLC.dataLabelEntities[dataTypeIndex]);

                //Переносим её значение в структуру для сортировки
                blockEntities.displayedEntities[a].sortValue = dLValue.DataValue;
            }

            //Сортируем массив сущностей
            Array.Sort(blockEntities.displayedEntities, new D_BlockList_ElementComparer<TSortType>(block.descendingSort));
        }
    }
}
