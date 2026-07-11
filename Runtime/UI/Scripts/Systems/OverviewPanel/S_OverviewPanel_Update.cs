
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_OverviewPanel_Update : IEcsRunSystem
    {
        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Обновляем панели
            OPs_Update();
        }

        readonly EcsFilterInject<Inc<R_OverviewPanel_Update>> oP_Update_R_F = default;
        void OPs_Update()
        {
            //Для каждого запроса обновления обзорной панели
            foreach(int rEntity in oP_Update_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Update rComp = ref oP_Update_R_F.Pools.Inc1.Get(rEntity);

                //Обновляем панель
                OP_Update(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции обновления

            }
        }

        readonly EcsPoolInject<SR_Block_Update> b_Update_SR_P = default;
        void OP_Update(
            ref R_OverviewPanel_Update rComp)
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uI_Core.Value.gameWindow;

            //Берём соответствующую панель
            UIA_OverviewPanel overviewPanel = gameWindow.overviewPanels[rComp.panelType];

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
                            UI_Data.Block_Update_SR(
                                overviewTab.blockEntities[a],
                                b_Update_SR_P.Value);
                        }
                    }
                }
            }
        }
    }
}
