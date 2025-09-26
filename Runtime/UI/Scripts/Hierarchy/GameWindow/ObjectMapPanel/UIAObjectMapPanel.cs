
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIAObjectMapPanel : MonoBehaviour
    {
        public static Dictionary<string, UIAObjectMapPanel> objectMapPanelPrefabs = new();
        public static Dictionary<string, List<UIAObjectMapPanel>> cachedObjectMapPanels = new();

        public EcsPackedEntity selfPE;

        public EcsPackedEntity parentProvincePE;

        public static void CachePanel(
            ref CObjectDisplayedMapPanels objectDisplayedMapPanels,
            string objectMapPanelType)
        {
            //���� ������
            UIAObjectMapPanel currentObjectMapPanel = objectDisplayedMapPanels.objectMapPanels[objectMapPanelType];

            //������� ������ � ������ ������������
            cachedObjectMapPanels[objectMapPanelType].Add(currentObjectMapPanel);

            //�������� ������ � �������� ������������ ������
            currentObjectMapPanel.gameObject.SetActive(false);
            currentObjectMapPanel.transform.SetParent(null);

            //������� ������ �� ������
            objectDisplayedMapPanels.objectMapPanels.Remove(objectMapPanelType);
        }

        public static void InstantiatePanel(
            EcsPackedEntity objectPE,
            ref CObjectDisplayedMapPanels objectDisplayedMapPanels,
            string objectMapPanelType)
        {
            //������ ������ ���������� ��� ������
            UIAObjectMapPanel currentObjectMapPanel;

            //���� ������ ������������ ������� �� ����, �� ���� ������������
            if(cachedObjectMapPanels[objectMapPanelType].Count > 0)
            {
                //���� ������ ������������ �������
                List<UIAObjectMapPanel> cachedPanels = cachedObjectMapPanels[objectMapPanelType];

                //���� ��������� ������ � ������ � ������� � �� ������
                currentObjectMapPanel = cachedPanels[cachedPanels.Count - 1];
                cachedPanels.RemoveAt(cachedPanels.Count - 1);

            }
            //�����
            else
            {
                //������ ����� ������
                currentObjectMapPanel = Instantiate(objectMapPanelPrefabs[objectMapPanelType]);
            }

            //��������� �������� ������ ������
            currentObjectMapPanel.UpdatePanel(objectPE);
            currentObjectMapPanel.parentProvincePE = new();

            //���������� ����� ������
            currentObjectMapPanel.gameObject.SetActive(true);

            //������� � � ������� ������� �������
            objectDisplayedMapPanels.objectMapPanels.Add(objectMapPanelType, currentObjectMapPanel);
        }

        public void UpdatePanel(
            EcsPackedEntity objectPE)
        {
            selfPE = objectPE;
        }
    }
}
