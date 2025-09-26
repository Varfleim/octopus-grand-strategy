
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class SObjectMapPanelControl : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<CObjectDisplayedMapPanels> objectDisplayedMapPanelsPool = default;


        readonly EcsFilterInject<Inc<RObjectMapPanelShow>> objectMapPanelShowRFilter = default;
        readonly EcsPoolInject<RObjectMapPanelShow> objectMapPanelShowRPool = default;

        readonly EcsFilterInject<Inc<RObjectMapPanelHide>> objectMapPanelHideRFilter = default;
        readonly EcsPoolInject<RObjectMapPanelHide> objectMapPanelHideRPool = default;

        public void Run(IEcsSystems systems)
        {
            //���������� ������ ����� ��������
            ShowObjectMapPanel();

            //�������� ������ ����� ��������
            HideObjectMapPanel();
        }

        void ShowObjectMapPanel()
        {
            //��� ������� ������� ����������� ������ ����� �������
            foreach (int requestEntity in objectMapPanelShowRFilter.Value)
            {
                //���� ������
                ref RObjectMapPanelShow requestComp = ref objectMapPanelShowRPool.Value.Get(requestEntity);

                //���� ����������� ������ ��������� ����������
                if (ObjectMapPanelExist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == false)
                {
                    //���������� ������
                    ObjectMapPanelShow(ref requestComp);
                }

                //������ ��������� ������, �������� � ������ �������� ����������
            }
        }

        void ObjectMapPanelShow(
            ref RObjectMapPanelShow requestComp)
        {
            //���� �������� �������
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);

            //���� � ������� ��� ���������� ������� �����
            if(objectDisplayedMapPanelsPool.Value.Has(objectEntity) == false)
            {
                //������ ���
                ObjectDisplayedMapPanelsCreate(objectEntity);
            }

            //���� ��������� ������� ����� �������
            ref CObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

            //������ ������
            UIAObjectMapPanel.InstantiatePanel(
                requestComp.objectPE,
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);
        }

        void ObjectDisplayedMapPanelsCreate(
            int objectEntity)
        {
            //��������� ������� ��������� ������� �����
            ref CObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Add(objectEntity);

            //��������� �������� ������ ����������
            objectDisplayedMapPanels = new(0);
        }

        void HideObjectMapPanel()
        {
            //��� ������� ������� �������� ������ ����� �������
            foreach(int requestEntity in objectMapPanelHideRFilter.Value)
            {
                //���� ������
                ref RObjectMapPanelHide requestComp = ref objectMapPanelHideRPool.Value.Get(requestEntity);

                //���� ����������� ������ ���� � �������
                if(ObjectMapPanelExist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == true)
                {
                    //�������� �
                    ObjectMapPanelHide(ref requestComp);
                }

                //������� ������
                objectMapPanelHideRPool.Value.Del(requestEntity);
            }
        }

        void ObjectMapPanelHide(
            ref RObjectMapPanelHide requestComp)
        {
            //���� �������� ������� � ��������� ������� �����
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);
            ref CObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

            //�������� ����������� ������
            UIAObjectMapPanel.CachePanel(
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);

            //���� � ������� ������ ��� ������� �����
            if(objectDisplayedMapPanels.objectMapPanels.Count == 0)
            {
                //������� ��������� ������� �����
                ObjectDisplayedMapPanelsRemove(objectEntity);
            }
        }

        void ObjectDisplayedMapPanelsRemove(
            int objectEntity)
        {
            //������� ��������� ������� �����
            objectDisplayedMapPanelsPool.Value.Del(objectEntity);
        }

        bool ObjectMapPanelExist(
            EcsPackedEntity objectPE,
            string objectMapPanelType)
        {
            //���� �������� �������
            objectPE.Unpack(world.Value, out int objectEntity);

            //���� � ������� ���� ��������� ������� �����
            if (objectDisplayedMapPanelsPool.Value.Has(objectEntity) == true)
            {
                //���� ���������
                ref CObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

                //���� � ������� ���� ������ ������������ ����
                if (objectDisplayedMapPanels.objectMapPanels.ContainsKey(objectMapPanelType) == true)
                {
                    //����������� ������ ��� ���� � �������
                    return true;
                }
                //�����
                else
                {
                    //����������� ������ ����� �����������
                    return false;
                }
            }
            //�����
            else
            {
                //����������� ������ ����� ����� �����������
                return false;
            }
        }
    }
}
