
using System;

using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

using GBB;

namespace GS.UI
{
    internal class S_Block_Update<TSortType> : VFSystem, IProtoRunSystem
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
            foreach (ProtoEntity blockEntity in block_Entities_Update_SR_I)
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

            //Если количество панелей блока меньше количества сущностей
            if (block.panelEntities.Count < blockEntities.displayedEntities.Length)
            {
                //Для каждой недостающей сущности
                for (int a = block.panelEntities.Count; a < blockEntities.displayedEntities.Length; a++)
                {
                    //Создаём новую панель
                    BEP_Creation(
                        ref block, in blockTemplate);
                }
            }

            //Для каждой отображаемой сущности
            for (int a = 0; a < blockEntities.displayedEntities.Length; a++)
            {
                //Запрашиваем обновление данных сущности
                Entity_RequestDataUpdate(
                    in blockTemplate,
                    blockEntities.displayedEntities[a].entity);
            }

            //Если количество отображаемых сущностей меньше количества панелей
            if (blockEntities.displayedEntities.Length < block.panelEntities.Count)
            {
                //Для каждой лишней панели в обратном порядке
                for (int a = block.panelEntities.Count - 1; a < blockEntities.displayedEntities.Length; a--)
                {
                    //Кэшируем панель
                    BEP_Destroy(
                        ref block,
                        block.panelEntities[a]);
                }
            }
        }

        void BEP_Creation(
            ref C_Block parentBlock, in TD_Block blockTemplate)
        {
            //Создаём новую сущность и назначаем ей компонент панели сущности
            ref C_BlockEntityPanel bEP = ref uI_A.bEP_P.NewEntity(out ProtoEntity bEPEntity);

            //Заносим панель в список блока
            parentBlock.panelEntities.Add(bEPEntity);

            //Заполняем основные данные панели
            bEP = new(0);

            //Если список кэшированных панелей не пуст, берём кэшированную
            if (UI_BlockEntityPanel.cachedPanels.Count > 0)
            {
                bEP.selfPanel = UI_BlockEntityPanel.cachedPanels[UI_BlockEntityPanel.cachedPanels.Count - 1];
                UI_BlockEntityPanel.cachedPanels.RemoveAt(UI_BlockEntityPanel.cachedPanels.Count - 1);
            }
            //Иначе создаём новую панель
            else
            {
                bEP.selfPanel = GameObject.Instantiate(uI_Data.blockEntityPanelPrefab);
            }

            //Прикрепляем панель к блоку и отображаем
            bEP.selfPanel.transform.SetParent(parentBlock.selfPanel.transform);
            bEP.selfPanel.gameObject.SetActive(true);

            //Сохраняем сущность панели
            bEP.selfPanel.selfEntity = bEPEntity;

            //Создаём массив панелей граф
            bEP.dLPanels = new UI_DataLabelPanel[blockTemplate.dataTypeIndexes.Length];

            //Для каждой графы в шаблоне
            for (int a = 0; a < blockTemplate.dataTypeIndexes.Length; a++)
            {
                //Берём шаблон графы
                ref readonly TD_DataLabel dLTemplate 
                    = ref uI_Data.dataLabels_TemplateArray[blockTemplate.dataTypeIndexes[a]];

                //Создаём новую панель графы
                bEP.dLPanels[a] = DLPanel_Creation(
                    ref bEP,
                    in dLTemplate);
                bEP.dLPanels[a].dataName.text = dLTemplate.dataTypeCode;
            }
        }

        UI_DataLabelPanel DLPanel_Creation(
            ref C_BlockEntityPanel parentBEP,
            in TD_DataLabel dLTemplate)
        {
            //Получаем новую панель
            UI_DataLabelPanel dLPanel;

            //Если список кэшированных панелей не пуст, берём кэшированную
            if (UI_DataLabelPanel.cachedPanels.Count > 0)
            {
                dLPanel = UI_DataLabelPanel.cachedPanels[UI_DataLabelPanel.cachedPanels.Count - 1];
                UI_DataLabelPanel.cachedPanels.RemoveAt(UI_DataLabelPanel.cachedPanels.Count - 1);
            }
            //Иначе создаём новую панель
            else
            {
                dLPanel = GameObject.Instantiate(uI_Data.dLPPrefab);
            }

            //Прикрепляем панель графы к панели сущности и отображаем
            dLPanel.transform.SetParent(parentBEP.selfPanel.layoutGroup.transform);
            dLPanel.gameObject.SetActive(true);

            return dLPanel;
        }

        void Entity_RequestDataUpdate(
            in TD_Block blockTemplate,
            ProtoEntity displayedEntity)
        {
            //Получаем хранилище данных сущности
            ref C_DataLabel_Container eDC = ref uI_A.dLC_P.GetOrAdd(displayedEntity, out bool added);

            //Если компонент был добавлен, заполняем его основные данные
            if (added)
            {
                eDC = new(0);
            }

            //Для каждой графы в шаблоне
            for (int a = 0; a < blockTemplate.dataTypeIndexes.Length; a++)
            {
                //Запрашиваем обновление данных
                DL_RequestUpdate(
                    displayedEntity, ref eDC,
                    blockTemplate.dataTypeIndexes[a]);
            }
        }

        void DL_RequestUpdate(
            ProtoEntity displayedEntity, ref C_DataLabel_Container eDC,
            int dataTypeIndex)
        {
            //Если у сущности ещё нет графы данного типа
            if (eDC.dataLabelEntities.ContainsKey(dataTypeIndex) == false)
            {
                //Создаём графу и получаем её сущность
                ProtoEntity dLEntity = DL_EntityCreation(
                    dataTypeIndex,
                    displayedEntity);

                //Заносим графу в хранилище
                eDC.dataLabelEntities.Add(
                    dataTypeIndex,
                    dLEntity);

                //Запрашиваем досоздание графы
                ref SR_DataLabel_Creation creationRComp = ref uI_A.dL_Creation_SR_P.Add(dLEntity);
                creationRComp = new(dataTypeIndex);
            }

            //Запрашиваем обновление графы
            ref SR_DataLabel_Update rComp = ref uI_A.dL_Update_SR_P.GetOrAdd(eDC.dataLabelEntities[dataTypeIndex]);
        }

        ProtoEntity DL_EntityCreation(
            int dataTypeIndex,
            ProtoEntity displayedEntity)
        {
            //Берём шаблон графы
            ref readonly TD_DataLabel dLTemplate
                = ref uI_Data.dataLabels_TemplateArray[dataTypeIndex];

            //Создаём новую сущность и назначаем ей главный компонент графы
            ref C_DataLabel dL = ref uI_A.dL_P.NewEntity(out ProtoEntity dLEntity);
            dL = new(displayedEntity);

            //Назначаем ей компонент-хранилище соответственно типу данных
            if(dLTemplate.dataType == DataType.Integer)
            {
                ref C_DataLabel_Value<int> dLInt = ref uI_A.dL_Int_P.Add(dLEntity);
            }
            else if(dLTemplate.dataType == DataType.Float)
            {
                ref C_DataLabel_Value<float> dLFloat = ref uI_A.dL_Float_P.Add(dLEntity);
            }
            else if(dLTemplate.dataType == DataType.String)
            {
                ref C_DataLabel_Value<string> dLString = ref uI_A.dL_String_P.Add(dLEntity);
            }

            return dLEntity;
        }

        void BEP_Destroy(
            ref C_Block parentBlock,
            ProtoEntity bEPEntity)
        {
            //Берём компонент панели
            ref C_BlockEntityPanel bEP = ref uI_A.bEP_P.Get(bEPEntity);

            //Удаляем панель из списка блока
            parentBlock.panelEntities.RemoveAt(parentBlock.panelEntities.Count - 1);

            //Заносим панель в список кэшированных
            UI_BlockEntityPanel.cachedPanels.Add(bEP.selfPanel);

            //Открепляем её от родителя и скрываем
            bEP.selfPanel.transform.SetParent(null);
            bEP.selfPanel.gameObject.SetActive(false);

            //Для каждой панели графы
            for (int a = 0; a < bEP.dLPanels.Length; a++)
            {
                //Удаляем панель графы
                DLPanel_Destroy(bEP.dLPanels[a]);
            }

            //Удаляем компонент панели
            uI_A.bEP_P.Del(bEPEntity);
        }

        void DLPanel_Destroy(
            UI_DataLabelPanel dLPanel)
        {
            //Заносим панель в список кэшированных
            UI_DataLabelPanel.cachedPanels.Add(dLPanel);

            //Открепляем её от родителя и скрываем
            dLPanel.transform.SetParent(null);
            dLPanel.gameObject.SetActive(false);
        }
    }
}
