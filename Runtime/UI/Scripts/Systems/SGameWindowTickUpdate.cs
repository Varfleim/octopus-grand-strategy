
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class SGameWindowTickUpdate : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<UI_Core> uICore = default;

        public void Run(IEcsSystems systems)
        {
            //Если активно окно игры
            //if (uICore.Value.activeWindow == uICore.Value.gameWindow.gameObject)
            ///ТЕСТ
            if(uICore.Value.gameWindow.gameObject.activeSelf == true)
            ///
            {
                //Проверяем, не требуется ли обновление в окне игры
                GameWindowTickUpdate();
            }
        }

        void GameWindowTickUpdate()
        {
            //Берём окно игры
            UI_GameWindow gameWindow = uICore.Value.gameWindow;

            //Если активна панель объекта
            if (gameWindow.activeMainPanel == gameWindow.objectPanel.gameObject)
            {
                //Проверяем, требуется ли обновление в панели объекта
                ObjectPanelTickUpdate();
            }
        }

        readonly EcsPoolInject<R_ObjectSubpanelTabShow> objectSubpanelTabShowRPool = default;
        void ObjectPanelTickUpdate()
        {
            //Берём панель объекта
            UI_ObjectPanel objectPanel = uICore.Value.gameWindow.objectPanel;

            //Берём активную подпанель
            UIA_ObjectSubpanel activeSubpanel = objectPanel.activeSubpanel;

            //Берём активную вкладку
            UIA_ObjectSubpanelTab activeSubpanelTab = activeSubpanel.activeTab;

            //Запрашиваем отображение этой подпанели
            UIData.ShowObjectSubpanelTabRequest(
                world.Value,
                objectSubpanelTabShowRPool.Value,
                activeSubpanel.SelfType, activeSubpanelTab.SelfType,
                activeSubpanelTab.objectPE);
        }
    }
}
