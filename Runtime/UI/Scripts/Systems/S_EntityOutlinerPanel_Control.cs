
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_EntityOutlinerPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели сущностей в планировщике
            OutlinerOPs_Show();
        }

        readonly EcsFilterInject<Inc<R_EntityOutlinerPanel_Show>> outlinerEP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityOutlinerPanel_Show> outlinerEP_Show_R_P = default;
        void OutlinerOPs_Show()
        {
            //Для каждого запроса отображения панели сущности в планировщике
            foreach(int rEntity in outlinerEP_Show_R_F.Value)
            {
                //Берём запрос и сущность
                ref R_EntityOutlinerPanel_Show rComp = ref outlinerEP_Show_R_P.Value.Get(rEntity);

                //Отображаем панель
                OutlinerOP_Show(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными
            }
        }

        readonly EcsPoolInject<C_EntityDisplayedScreenPanels> eDSPs_P = default;
        void OutlinerOP_Show(
            ref R_EntityOutlinerPanel_Show rComp)
        {
            //Берём панель планировщика и запрошенную вкладку
            UI_OutlinerPanel outlinerPanel = uI_Core.Value.gameWindow.outlinerPanel;
            UIA_OutlinerPanelTab outlinerPanelTab = outlinerPanel.tabs[rComp.entOutlinerPanelType];

            //Берём сущность и компонент отображаемых экранных панелей
            rComp.entPE.Unpack(world.Value, out int entEntity);
            ref C_EntityDisplayedScreenPanels oDSP = ref eDSPs_P.Value.Get(entEntity);

            //Берём запрошенную панель
            UIA_EntityScreenPanel entScreenPanel = oDSP.entPanels[rComp.entOutlinerPanelType];

            //Назначаем панели родительскую сущность - LG родительской вкладки
            entScreenPanel.transform.SetParent(outlinerPanelTab.layoutGroup.transform);
        }
    }
}
