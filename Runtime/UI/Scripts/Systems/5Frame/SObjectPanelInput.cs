
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace GS.UI
{
    public class SObjectPanelInput : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<RObjectPanelHide> objectPanelHideRPool = default;

        EcsWorld uguiUIWorld;
        EcsFilter clickEventUIFilter;
        EcsPool<EcsUguiClickEvent> clickEventUIPool;


        readonly EcsCustomInject<UICore> uICore = default;

        public void Init(IEcsSystems systems)
        {
            uguiUIWorld = systems.GetWorld("uguiUIEventsWorld");

            clickEventUIPool = uguiUIWorld.GetPool<EcsUguiClickEvent>();
            clickEventUIFilter = uguiUIWorld.Filter<EcsUguiClickEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            //��������� ����� � ������ �������
            ObjectPanelClickAction();
        }

        void ObjectPanelClickAction()
        {
            //���� ���� ����
            UI_GameWindow gameWindow = uICore.Value.gameWindow;

            //���� ������� ������ �������
            if (gameWindow.activeMainPanel == gameWindow.objectPanel.gameObject)
            {
                //��� ������� ������� ����� �� ����������
                foreach (int clickEventUIEntity in clickEventUIFilter)
                {
                    //���� �������
                    ref EcsUguiClickEvent clickEvent = ref clickEventUIPool.Get(clickEventUIEntity);

                    //���������, ���� �� ��������� �����-���� ��������
                    bool isActionComplete = false;

                    //���� ������ ������ �������� ������ �������
                    if (clickEvent.WidgetName == "CloseObjectPanel")
                    {
                        //����������� �������� ������ �������
                        UIData.HideObjectPanelRequest(
                            world.Value,
                            objectPanelHideRPool.Value);

                        //��������, ��� �������� ���� ���������
                        isActionComplete = true;
                    }
                    //�����, ����
                    //else if()
                    //{

                    //}

                    //���� �������� ���� ���������
                    if (isActionComplete == true)
                    {
                        UnityEngine.Debug.LogWarning("Click! " + clickEvent.WidgetName);

                        //������� �������
                        clickEventUIPool.Del(clickEventUIEntity);
                    }
                }
            }
        }
    }
}
