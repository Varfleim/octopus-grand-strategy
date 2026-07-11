
using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_Block_Creation : IEcsInitSystem
    {
        readonly EcsCustomInject<UI_Data> uI_Data = default;

        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Init(IEcsSystems systems)
        {
            //Создаём блоки-списки
            BlockLists_Creation();
        }

        readonly EcsFilterInject<Inc<SR_BlockList_Creation>> bL_Creation_SR_F = default;
        void BlockLists_Creation()
        {
            //Для каждого запроса создания блока-списка
            foreach(int rEntity in bL_Creation_SR_F.Value)
            {
                //Берём запрос
                ref SR_BlockList_Creation rComp = ref bL_Creation_SR_F.Pools.Inc1.Get(rEntity);

                //Создаём блок
                BlockList_Creation(
                    rEntity,
                    ref rComp);

                //Удаляем запрос
                bL_Creation_SR_F.Pools.Inc1.Del(rEntity);
            }
        }

        readonly EcsPoolInject<C_BlockList> bL_P = default;
        void BlockList_Creation(
            int blockEntity,
            ref SR_BlockList_Creation rComp)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём родительскую панель, подпанель, вкладку
            UIA_OverviewPanel overviewPanel = gameWindow.overviewPanels[rComp.parentPanelType];
            UIA_OverviewSubpanel overviewSubpanel = overviewPanel.subpanels[rComp.parentSubpanelType];
            UIA_OverviewTab overviewTab = overviewSubpanel.tabs[rComp.parentTabType];

            //Назначаем сущности компонент блока-списка
            ref C_BlockList bL = ref bL_P.Value.Add(blockEntity);

            //Заполняем основные данные блока
            bL = new(0);

            //Инстанциируем префаб блока и сразу заносим его в панель
            bL.selfPanel = Block_Instantiate(
                uI_Data.Value.blockListPrefab,
                overviewTab.layoutGroup.transform) as UI_BlockList;

            //Сохраняем сущность блока
            bL.selfPanel.SelfEntity = blockEntity;

            //Заносим сущность блока в список блоков вкладки
            overviewTab.blockEntities.Add(blockEntity);
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
