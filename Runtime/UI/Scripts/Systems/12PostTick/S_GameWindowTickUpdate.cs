
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_GameWindowTickUpdate : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;

        readonly EcsCustomInject<UICore> uICore = default;

        public void Run(IEcsSystems systems)
        {
            //���� ������� ���� ����
            //if (uICore.Value.activeWindow == uICore.Value.gameWindow.gameObject)
            ///����
            if(uICore.Value.gameWindow.gameObject.activeSelf == true)
            ///
            {
                //���������, �� ��������� �� ���������� � ���� ����
                GameWindowTickUpdate();
            }
        }

        void GameWindowTickUpdate()
        {
            //���� ���� ����
            UI_GameWindow gameWindow = uICore.Value.gameWindow;

            //���� ������� ������ �������
            if (gameWindow.activeMainPanel == gameWindow.objectPanel.gameObject)
            {
                //���������, ��������� �� ���������� � ������ �������
                ObjectPanelTickUpdate();
            }
        }

        readonly EcsPoolInject<RObjectSubpanelTabShow> objectSubpanelTabShowRPool = default;
        void ObjectPanelTickUpdate()
        {
            //���� ������ �������
            UI_ObjectPanel objectPanel = uICore.Value.gameWindow.objectPanel;

            //���� �������� ���������
            UIA_ObjectSubpanel activeSubpanel = objectPanel.activeSubpanel;

            //���� �������� �������
            UIA_ObjectSubpanelTab activeSubpanelTab = activeSubpanel.activeTab;

            //����������� ����������� ���� ���������
            UIData.ShowObjectSubpanelTabRequest(
                world.Value,
                objectSubpanelTabShowRPool.Value,
                activeSubpanel.SelfType, activeSubpanelTab.SelfType,
                activeSubpanelTab.objectPE);
        }
    }
}
