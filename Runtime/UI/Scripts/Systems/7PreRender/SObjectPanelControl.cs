
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class SObjectPanelControl : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<RObjectPanelHide>> objectPanelHideRFilter = default;
        readonly EcsPoolInject<RObjectPanelHide> objectPanelHideRPool = default;

        readonly EcsFilterInject<Inc<RObjectSubpanelTabShow>> objectSubpanelTabShowRFilter = default;
        readonly EcsPoolInject<RObjectSubpanelTabShow> objectSubpanelTabShowRPool = default;
        readonly EcsPoolInject<RObjectSubpanelTabUpdate> objectSubpanelTabUpdateRPool = default;


        readonly EcsCustomInject<UICore> uICore = default;

        public void Run(IEcsSystems systems)
        {
            //���������� ������� ��������� �������
            ShowObjectSubpanelTab();

            //�������� ������ �������
            HideObjectPanel();
        }

        void ShowObjectSubpanelTab()
        {
            //��� ������� ������� ����������� ������� ��������� �������
            foreach(int requestEntity in objectSubpanelTabShowRFilter.Value)
            {
                //���� ������
                ref RObjectSubpanelTabShow requestComp = ref objectSubpanelTabShowRPool.Value.Get(requestEntity);

                //���������� �������
                ObjectSubpanelTabShow(
                    ref requestComp,
                    out bool isSamePanel,
                    out bool isSameSubpanel,
                    out bool isSameTab,
                    out bool isSameObject);

                //����������� ���������� ������ �� �������
                UIData.UpdateObjectSubpanelTabRequest(
                    objectSubpanelTabUpdateRPool.Value,
                    requestEntity,
                    isSamePanel,
                    isSameSubpanel,
                    isSameTab,
                    isSameObject);
            }
        }

        /// <summary>
        /// �������, ������������ ����������� ������� � �����������, ���� �� ��� ��� ������� � ��� �� ������� ����������� ������
        /// </summary>
        /// <param name="requestComp"></param>
        /// <param name="isSamePanel"></param>
        /// <param name="isSameSubpanel"></param>
        /// <param name="isSameTab"></param>
        /// <param name="isSameObject"></param>
        void ObjectSubpanelTabShow(
            ref RObjectSubpanelTabShow requestComp,
            out bool isSamePanel,
            out bool isSameSubpanel,
            out bool isSameTab,
            out bool isSameObject)
        {
            //���� ����������� ��������� �������
            UIA_ObjectSubpanel objectSubpanel = UIData.objectSubpanels[requestComp.objectSubpanelType];

            //���������� ���������, ���� ����������
            ObjectSubpanelShow(
                objectSubpanel,
                out isSamePanel,
                out isSameSubpanel);


            //���� ����������� �������
            UIA_ObjectSubpanelTab requestedTab = objectSubpanel.subpanelTabs[requestComp.objectSubpanelTabType];

            //�������� �� ��������� ������������
            isSameTab = false;
            isSameObject = false;

            //���� ������� ����������� �������
            if (objectSubpanel.activeTab == requestedTab)
            {
                //���� ���� ������� �� �� ���������
                if(isSameSubpanel == true)
                {
                    //��������, ��� ���� ������� �� �� �������
                    isSameTab = true;

                    //���� ������� ���� ������� ��� ���� �� �������
                    if(objectSubpanel.activeTab.objectPE.EqualsTo(requestComp.objectPE) == true)
                    {
                        //��������, ��� ��� ������� ��� �� ������
                        isSameObject = true;
                    }
                }
            }

            //���� ���� ������� �� �� ������
            if (isSamePanel == true)
            {
                //UnityEngine.Debug.LogWarning("Same panel!");

            }
            //�����
            else
            {
                //UnityEngine.Debug.LogWarning("Not same panel!");

            }

            //���� ���� ������� �� �� ��������� 
            if (isSameSubpanel == true)
            {
                //UnityEngine.Debug.LogWarning("Same subpanel!");

            }
            //�����
            else
            {
                //UnityEngine.Debug.LogWarning("Not same subpanel!");

            }

            //���� ���� ������� �� �� �������
            if (isSameTab == true)
            {
                UnityEngine.Debug.LogWarning("Same tab!");

            }
            //�����
            else
            {
                UnityEngine.Debug.LogWarning("Not same tab!");

                //���������� ����������� �������
                objectSubpanel.tabGroup.OnTabSelected(requestedTab.selfTabButton);

                //��������� � ��� �������� �������
                objectSubpanel.activeTab = requestedTab;
            }

            //���� ��� ������� ��� �� ������
            if (isSameObject == true)
            {
                UnityEngine.Debug.LogWarning("Same object!");
            }
            //�����
            else
            {
                UnityEngine.Debug.LogWarning("Not same object!");

                //��������� ��� ��� �������� ������
                objectSubpanel.activeTab.objectPE = requestComp.objectPE;
            }
        }

        /// <summary>
        /// �������, ������������ ����������� ��������� � �����������, ���� �� ��� ��� ������������
        /// </summary>
        /// <param name="requestedSubpanel"></param>
        /// <param name="isSamePanel"></param>
        /// <param name="isSameSubpanel"></param>
        void ObjectSubpanelShow(
            UIA_ObjectSubpanel requestedSubpanel,
            out bool isSamePanel,
            out bool isSameSubpanel)
        {
            //���� ������ �������
            UI_ObjectPanel objectPanel = uICore.Value.gameWindow.objectPanel;

            //���������� ������ �������, ���� ����������
            uICore.Value.gameWindow.MainPanelActivation(
                objectPanel.gameObject,
                out isSamePanel);


            //�������� �� ��������� ������������
            isSameSubpanel = false;

            //���� ������� ����������� ���������
            if (objectPanel.activeSubpanel == requestedSubpanel)
            {
                //���� ���� ������� �� �� ������
                if (isSamePanel == true)
                {
                    //��������, ��� ������� �� �� ���������
                    isSameSubpanel = true;
                }
            }

            //���� ���� ������� �� �� ������
            if (isSamePanel == true)
            {

            }
            //�����
            else
            {

            }

            //���� ���� ������� �� �� ��������� 
            if (isSameSubpanel == true)
            {

            }
            //�����
            else
            {
                //���������� ����������� ���������

                //���� �����-���� ��������� ���� �������, �������� �
                if (objectPanel.activeSubpanel != null)
                {
                    objectPanel.activeSubpanel.gameObject.SetActive(false);
                }

                //������ ����������� ��������� ��������
                requestedSubpanel.gameObject.SetActive(true);

                //��������� � ��� �������� ���������
                objectPanel.activeSubpanel = requestedSubpanel;
            }
        }

        void HideObjectPanel()
        {
            //��� ������� ������� �������� ������ �������
            foreach(int requestEntity in objectPanelHideRFilter.Value)
            {
                //���� ������
                ref RObjectPanelHide requestComp = ref objectPanelHideRPool.Value.Get(requestEntity);

                //�������� ������
                ObjectPanelHide();

                //������� ������
                objectPanelHideRPool.Value.Del(requestEntity);
            }
        }

        void ObjectPanelHide()
        {
            //���� ������ �������
            UI_ObjectPanel objectPanel = uICore.Value.gameWindow.objectPanel;

            //�������� �������� ������� �������� ���������
            objectPanel.activeSubpanel.HideActiveTab();

            //�������� �������� ���������
            objectPanel.HideActiveSubpanel();

            //�������� �������� ������� ������, �� ���� ������ �������
            uICore.Value.gameWindow.HideMainPanel();
        }
    }
}
