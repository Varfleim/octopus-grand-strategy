
using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_Block_Creation : IProtoInitSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Init(IProtoSystems systems)
        {
            //Создаём блоки-списки
            BlockLists_Creation();
        }

        void BlockLists_Creation()
        {
            //Для каждого запроса создания блока-списка
            foreach(ProtoEntity rEntity in uI_A.bL_Creation_SR_I)
            {
                //Берём запрос
                ref SR_BlockList_Creation rComp = ref uI_A.bL_Creation_SR_P.Get(rEntity);

                //Создаём блок
                BlockList_Creation(
                    rEntity,
                    ref rComp);

                //Удаляем запрос
                uI_A.bL_Creation_SR_P.Del(rEntity);
            }
        }

        void BlockList_Creation(
            ProtoEntity blockEntity,
            ref SR_BlockList_Creation rComp)
        {
            if(uI_Data.OP_GetByCode(rComp.parentPanelCode, out UIA_OverviewPanel parentOP))
            {
                if(uI_Data.OSbp_GetByCode(rComp.parentSubpanelCode, out UIA_OverviewSubpanel parentOSbp))
                {
                    if(uI_Data.OT_GetByCode(rComp.parentTabCode, out UIA_OverviewTab parentOT))
                    {
                        //Назначаем сущности компонент блока-списка
                        ref C_BlockList bL = ref uI_A.bL_P.Add(blockEntity);

                        //Заполняем основные данные блока
                        bL = new(0);

                        //Инстанциируем префаб блока и сразу заносим его во вкладку
                        bL.selfPanel = Block_Instantiate(
                            uI_Data.blockListPrefab,
                            parentOT.layoutGroup.transform) as UI_BlockList;

                        //Сохраняем сущность блока
                        bL.selfPanel.SelfEntity = blockEntity;

                        //Заносим сущность блока в список блоков вкладки
                        parentOT.blockEntities.Add(blockEntity);
                    }
                }
            }
        }

        UIA_Block Block_Instantiate(
            UIA_Block blockPrefab,
            Transform parentTransform)
        {
            //Создаём префаб блока и сразу присоединяем его к переданной вкладке
            UIA_Block blockPanel = GameObject.Instantiate(blockPrefab, parentTransform);

            //Отображаем панель
            blockPanel.gameObject.SetActive(true);

            //Возвращаем панель
            return blockPanel;
        }
    }
}
