
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class SI_Selection_Creation<TSelectionComp> : GBB.VFSystem, IProtoInitSystem
        where TSelectionComp : struct
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        ProtoPool<TSelectionComp> selectionComp_P;
        string selectionCode;

        public void SetType(
            ProtoPool<TSelectionComp> selectionComp_P,
            string selectionCode)
        {
            this.selectionComp_P = selectionComp_P;
            this.selectionCode = selectionCode;
        }

        public void Init(IProtoSystems systems)
        {
            //Создаём блоки
            Blocks_Creation();
        }

        void Blocks_Creation()
        {
            //Для каждого запроса создания блока
            foreach(ProtoEntity blockEntity in uI_A.block_Creation_SR_I)
            {
                //Создаём блок
                Block_Creation(blockEntity);
            }
        }

        void Block_Creation(
            ProtoEntity blockEntity)
        {
            //Берём запрос
            ref SR_Block_Creation rComp = ref uI_A.block_Creation_SR_P.Get(blockEntity);

            //Берём шаблон блока
            ref readonly TD_Block block_Template = ref uI_Data.blocks_TemplateArray[rComp.blockType];

            //TO DO
            //Если тип выборки блока соответствует типу выборки системы,
            //назначаем ему компонент соответствующей выборки и удаляем запрос
            if(block_Template.selectionCode == selectionCode)
            {
                ref TSelectionComp interlayerComponent = ref selectionComp_P.Add(blockEntity);

                uI_A.block_Creation_SR_P.Del(blockEntity);
            }
        }
    }
}
