
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_EntityMainOverviewPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели сущностей в главной обзорной панели
            MainOverviewOPs_Show();
        }

        readonly EcsFilterInject<Inc<R_EntityMainOverviewPanel_Show>> mOEP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityMainOverviewPanel_Show> mOEP_Show_R_P = default;
        void MainOverviewOPs_Show()
        {
            //Для каждого запроса отображения панели сущности в главной обзорной панели
            foreach(int rEntity in mOEP_Show_R_F.Value)
            {
                //Берём запрос и сущность
                ref R_EntityMainOverviewPanel_Show rComp = ref mOEP_Show_R_P.Value.Get(rEntity);

                //Отображаем панель
                MainOverviewOP_Show(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными
            }
        }

        readonly EcsPoolInject<C_EntityDisplayedScreenPanels> eDSPs_P = default;
        void MainOverviewOP_Show(
            ref R_EntityMainOverviewPanel_Show rComp)
        {
            //Берём главную обзорную панель, запрошенную подпанель и вкладку
            UI_MainOverviewPanel mainOverviewPanel = uI_Core.Value.gameWindow.mainOverviewPanel;
            UIA_MainOverviewSubpanel mainOverviewSubpanel = mainOverviewPanel.subpanels[rComp.overviewSubpanelType];
            UIA_MainOverviewSubpanelTab mainOverviewSubpanelTab = mainOverviewSubpanel.tabs[rComp.overviewSubpanelTabType];

            //Берём сущность и компонент отображаемых экранных панелей
            rComp.entPE.Unpack(world.Value, out int entEntity);
            ref C_EntityDisplayedScreenPanels oDSP = ref eDSPs_P.Value.Get(entEntity);

            //Берём запрошенную панель
            UIA_EntityScreenPanel entityScreenPanel = oDSP.entPanels[rComp.entMainOverviewPanelType];

            //Назначаем панели родительскую сущность - LG родительской вкладки
            entityScreenPanel.transform.SetParent(mainOverviewSubpanelTab.layoutGroup.transform);
        }
    }
}
