
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class S_Block_PreUpdate : GBB.VFSystem, IProtoRunSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run()
        {
            //Обновляем блоки в панелях
            OPs_Blocks_Update();
        }

        void OPs_Blocks_Update()
        {
            //Для каждого запроса обновления обзорной панели
            foreach(ProtoEntity rEntity in uI_A.oP_Update_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Update rComp = ref uI_A.oP_Update_R_P.Get(rEntity);

                //Обновляем блоки в панели
                OP_Blocks_Update(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры
            }
        }

        void OP_Blocks_Update(
            ref R_OverviewPanel_Update rComp)
        {
            //Берём соответствующую панель
            UIA_OverviewPanel overviewPanel = uI_Data.oPsIndexToObjectDict[rComp.panelType];

            //Если панель активна
            if(overviewPanel.gameObject.activeInHierarchy)
            {
                //Если есть активная подпанель, берём её
                if(overviewPanel.activeSubpanel != null
                    && overviewPanel.activeSubpanel.gameObject.activeInHierarchy)
                {
                    UIA_OverviewSubpanel overviewSubpanel = overviewPanel.activeSubpanel;

                    //Если есть активная вкладка, берём её
                    if(overviewSubpanel.activeTab != null
                        && overviewSubpanel.activeTab.gameObject.activeInHierarchy)
                    {
                        UIA_OverviewTab overviewTab = overviewSubpanel.activeTab;

                        //Для каждого блока
                        for(int a = 0; a < overviewTab.blockEntities.Count; a++)
                        {
                            //Запрашиваем обновление для сущности блока
                            Block_Update_SR(overviewTab.blockEntities[a]);

                            //Обновляем блок
                            Block_Update(overviewTab.blockEntities[a]);
                        }
                    }
                }
            }
        }

        void Block_Update_SR(
            ProtoEntity blockEntity)
        {
            //Запрашиваем обновление блока
            ref SR_Block_Update rComp = ref uI_A.block_Update_SR_P.Add(blockEntity);
        }

        void Block_Update(
            ProtoEntity blockEntity)
        {
            //Берём блок
            ref C_Block block = ref uI_A.block_P.Get(blockEntity);

            //Берём шаблон блока
            ref readonly TD_Block blockTemplate = ref uI_Data.blocks_TemplateArray[block.selfType];

            //Если тип данных для сортировки - целое число и не имеет компонента списка
            if (uI_Data.dataLabels_TemplateArray[blockTemplate.dataTypeIndexes[block.sortingDataType]].dataType
                == DataType.Integer
                && uI_A.block_Int_P.Has(blockEntity) == false)
            {
                //Удаляем другие компоненты списков
                uI_A.block_Float_P.DelIfExists(blockEntity);
                uI_A.block_String_P.DelIfExists(blockEntity);

                //Создаём компонент списка для целых чисел
                ref C_Block_Entities<int> blockEntities = ref uI_A.block_Int_P.Add(blockEntity);
                blockEntities = new(0);
            }
            else if (uI_Data.dataLabels_TemplateArray[blockTemplate.dataTypeIndexes[block.sortingDataType]].dataType
                == DataType.Float
                && uI_A.block_Float_P.Has(blockEntity) == false)
            {
                //Удаляем другие компоненты списков
                uI_A.block_Int_P.DelIfExists(blockEntity);
                uI_A.block_String_P.DelIfExists(blockEntity);

                //Создаём компонент списка для дробных чисел
                ref C_Block_Entities<float> blockEntities = ref uI_A.block_Float_P.Add(blockEntity);
                blockEntities = new(0);
            }
            else if (uI_Data.dataLabels_TemplateArray[blockTemplate.dataTypeIndexes[block.sortingDataType]].dataType
                == DataType.String
                && uI_A.block_String_P.Has(blockEntity) == false)
            {
                //Удаляем другие компоненты списков
                uI_A.block_Int_P.DelIfExists(blockEntity);
                uI_A.block_Float_P.DelIfExists(blockEntity);

                //Создаём компонент списка для строк
                ref C_Block_Entities<string> blockEntities = ref uI_A.block_String_P.Add(blockEntity);
                blockEntities = new(0);
            }
        }
    }
}
