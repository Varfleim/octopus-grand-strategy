
using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class S_Block_Creation : GBB.VFSystem, IProtoInitSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

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

                //Запрос передаётся дальше, переходя в модуль игры
            }
        }

        void Block_Creation(
            ProtoEntity blockEntity)
        {
            //Берём запрос
            ref SR_Block_Creation rComp = ref uI_A.block_Creation_SR_P.Get(blockEntity);

            if (uI_Data.OP_GetByCode(rComp.parentPanelCode, out UIA_OverviewPanel parentOP))
            {
                if(uI_Data.OSbp_GetByCode(rComp.parentSubpanelCode, out UIA_OverviewSubpanel parentOSbp))
                {
                    if(uI_Data.OT_GetByCode(rComp.parentTabCode, out UIA_OverviewTab parentOT))
                    {
                        //Назначаем сущности компонент блока
                        ref C_Block block = ref uI_A.block_P.Add(blockEntity);

                        //Заполняем основные данные блока
                        block = new(rComp.blockType);

                        //Если список кэшированных панелей не пуст, то берём кэшированную
                        if(UI_Block.cachedPanels.Count > 0)
                        {
                            block.selfPanel = UI_Block.cachedPanels[UI_Block.cachedPanels.Count - 1];
                            UI_Block.cachedPanels.RemoveAt(UI_Block.cachedPanels.Count - 1);
                        }
                        //Иначе создаём новую панель
                        else
                        {
                            block.selfPanel = GameObject.Instantiate(uI_Data.blockPanelPrefab);
                        }

                        //Прикрепляем панель ко вкладке и активируем
                        block.selfPanel.transform.SetParent(parentOT.layoutGroup.transform);
                        block.selfPanel.gameObject.SetActive(true);

                        //Сохраняем сущность блока
                        block.selfPanel.selfEntity = blockEntity;

                        //Заносим сущность блока в список блоков вкладки
                        parentOT.blockEntities.Add(blockEntity);
                    }
                }
            }
        }
    }
}
