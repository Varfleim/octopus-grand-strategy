
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_ObjectMainOverviewPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели объектов в главной обзорной панели
            MainOverviewOPs_Show();
        }

        readonly EcsFilterInject<Inc<R_ObjectMainOverviewPanel_Show>> mOOP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectMainOverviewPanel_Show> mOOP_Show_R_P = default;
        void MainOverviewOPs_Show()
        {
            //Для каждого запроса отображения панели объекта в главной обзорной панели
            foreach(int rEntity in mOOP_Show_R_F.Value)
            {
                //Берём запрос и сущность объекта
                ref R_ObjectMainOverviewPanel_Show rComp = ref mOOP_Show_R_P.Value.Get(rEntity);

                //Отображаем панель
                MainOverviewOP_Show(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными
            }
        }

        readonly EcsPoolInject<C_ObjectDisplayedScreenPanels> oDSPs_P = default;
        void MainOverviewOP_Show(
            ref R_ObjectMainOverviewPanel_Show rComp)
        {
            //Берём главную обзорну панель, запрошенную подпанель и вкладку
            UI_MainOverviewPanel mainOverviewPanel = uI_Core.Value.gameWindow.mainOverviewPanel;
            UIA_MainOverviewSubpanel mainOverviewSubpanel = mainOverviewPanel.subpanels[rComp.overviewSubpanelType];
            UIA_MainOverviewSubpanelTab mainOverviewSubpanelTab = mainOverviewSubpanel.tabs[rComp.overviewSubpanelTabType];

            //Берём сущность объекта и компонент отображаемых экранных панелей
            rComp.objectPE.Unpack(world.Value, out int objectEntity);
            ref C_ObjectDisplayedScreenPanels oDSP = ref oDSPs_P.Value.Get(objectEntity);

            //Берём запрошенную панель
            UIA_ObjectScreenPanel objectScreenPanel = oDSP.objectPanels[rComp.objectMainOverviewPanelType];

            //Назначаем панели родительский объект - LG родительской вкладки
            objectScreenPanel.transform.SetParent(mainOverviewSubpanelTab.layoutGroup.transform);
        }
    }
}
