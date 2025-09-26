
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UIData : MonoBehaviour
    {
        public static Dictionary<string, UIA_ObjectSubpanel> objectSubpanels = new();

        public UICore uICore;

        public static void HideObjectPanelRequest(
            EcsWorld world,
            EcsPool<RObjectPanelHide> requestPool)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RObjectPanelHide requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        public static void ShowObjectSubpanelTabRequest(
            EcsWorld world,
            EcsPool<RObjectSubpanelTabShow> requestPool,
            string objectSubpanelType, string objectSubpanelTabType,
            EcsPackedEntity objectPE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RObjectSubpanelTabShow requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                objectSubpanelType, objectSubpanelTabType,
                objectPE);
        }

        public static void UpdateObjectSubpanelTabRequest(
            EcsPool<RObjectSubpanelTabUpdate> requestPool,
            int requestEntity,
            bool isSamePanel,
            bool isSameSubpanel,
            bool isSameTab,
            bool isSameObject)
        {
            //��������� �������� ������
            ref RObjectSubpanelTabUpdate requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                isSamePanel,
                isSameSubpanel,
                isSameTab,
                isSameObject);
        }

        public static void ShowObjectMapPanelRequest(
            EcsWorld world,
            EcsPool<RObjectMapPanelShow> requestPool,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RObjectMapPanelShow requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }

        public static void HideObjectMapPanelRequest(
            EcsWorld world,
            EcsPool<RObjectMapPanelHide> requestPool,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RObjectMapPanelHide requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }
    }
}
