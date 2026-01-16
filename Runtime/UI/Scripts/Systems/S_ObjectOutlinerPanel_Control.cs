
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_ObjectOutlinerPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<UI_Core> uI_Core = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели объектов в планировщике
            OutlinerOPs_Show();
        }

        readonly EcsFilterInject<Inc<R_ObjectOutlinerPanel_Show>> outlinerOP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectOutlinerPanel_Show> outlinerOP_Show_R_P = default;
        void OutlinerOPs_Show()
        {
            //Для каждого запроса отображения панели объекта в планировщике
            foreach(int rEntity in outlinerOP_Show_R_F.Value)
            {
                //Берём запрос и сущность объекта
                ref R_ObjectOutlinerPanel_Show rComp = ref outlinerOP_Show_R_P.Value.Get(rEntity);

                //Отображаем панель
                OutlinerOP_Show(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными
            }
        }

        readonly EcsPoolInject<C_ObjectDisplayedScreenPanels> oDSPs_P = default;
        void OutlinerOP_Show(
            ref R_ObjectOutlinerPanel_Show rComp)
        {
            //Берём панель планировщика и запрошенную вкладку
            UI_OutlinerPanel outlinerPanel = uI_Core.Value.gameWindow.outlinerPanel;
            UIA_OutlinerPanelTab outlinerPanelTab = outlinerPanel.tabs[rComp.objectOutlinerPanelType];

            //Берём сущность объекта и компонент отображаемых экранных панелей
            rComp.objectPE.Unpack(world.Value, out int objectEntity);
            ref C_ObjectDisplayedScreenPanels oDSP = ref oDSPs_P.Value.Get(objectEntity);

            //Берём запрошенную панель
            UIA_ObjectScreenPanel objectScreenPanel = oDSP.objectPanels[rComp.objectOutlinerPanelType];

            //Назначаем панели родительский объект - LG родительской вкладки
            objectScreenPanel.transform.SetParent(outlinerPanelTab.layoutGroup.transform);
        }
    }
}
