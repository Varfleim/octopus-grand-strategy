
using System.Collections.Generic;

using UnityEngine;

namespace GS.UI
{
    /// <summary>
    /// Класс, хранящий функции регистрации панелей интерфейса и префабов для получения индексов
    /// </summary>
    public class UIPrefabs_Data : MonoBehaviour
    {
        private List<UIA_MainOverviewSubpanel> mOSubpanels = new();
        private List<UIA_MainOverviewSubpanelTab> mOSbpTabs = new();
        private List<UIA_OutlinerPanelTab> outlinerPTabs = new();
        private List<UIA_ObjectScreenPanel> oSP_Prefabs = new();
        private List<UIA_ObjectMapPanel> oMP_Prefabs = new();

        public int MOSbp_AddAndGetIndex(
            UI_MainOverviewPanel mainOverviewPanel,
            UIA_MainOverviewSubpanel mainOverviewSubpanel)
        {
            //Заносим подпанель в список и берём индекс
            mOSubpanels.Add(mainOverviewSubpanel);
            int subpanelIndex = mOSubpanels.Count - 1;

            //Заносим индекс в данные подпанели
            mainOverviewSubpanel.SelfType = subpanelIndex;

            //Заносим подпанель в словарь главной обзорной панели
            mainOverviewPanel.subpanels.Add(
                subpanelIndex, mainOverviewSubpanel);

            //Возвращаем индекс
            return subpanelIndex;
        }

        public int MOSbpT_AddAndGetIndex(
            UIA_MainOverviewSubpanel mainOverviewSubpanel,
            UIA_MainOverviewSubpanelTab mainOverviewSubpanelTab)
        {
            //Заносим вкладку в список и берём индекс
            mOSbpTabs.Add(mainOverviewSubpanelTab);
            int tabIndex = mOSbpTabs.Count - 1;

            //Заносим индекс в данные вкладки
            mainOverviewSubpanelTab.SelfType = tabIndex;

            //Заносим вкладку в словарь родительской подпанели
            mainOverviewSubpanel.subpanelTabs.Add(
                tabIndex, mainOverviewSubpanelTab);

            //Возвращаем индекс
            return tabIndex;
        }

        public int OutlinerPT_AddAndGetIndex(
            UI_OutlinerPanel outlinerPanel,
            UIA_OutlinerPanelTab outlinerPanelTab)
        {
            //Заносим вкладку в список и берём индекс
            outlinerPTabs.Add(outlinerPanelTab);
            int tabIndex = outlinerPTabs.Count - 1;

            //Заносим индекс в данные вкладки
            outlinerPanelTab.SelfType = tabIndex;

            //Заносим вкладку в словарь панели планировщика
            outlinerPanel.tabs.Add(tabIndex, outlinerPanelTab);

            //Возвращаем индекс
            return tabIndex;
        }

        public int OSP_Prefab_AddAndGetIndex(
            UIA_ObjectScreenPanel objectScreenPanelPrefab)
        {
            //Заносим префаб в список и берём индекс
            oSP_Prefabs.Add(objectScreenPanelPrefab);
            int prefabIndex = oSP_Prefabs.Count - 1;

            //Заносим префаб в словарь префабов
            UIA_ObjectPanel.objectPanelPrefabs.Add(
                prefabIndex, objectScreenPanelPrefab);

            //Возвращаем индекс
            return prefabIndex;
        }

        public int OMP_Prefab_AddAndGetIndex(
            UIA_ObjectMapPanel objectMapPanelPrefab)
        {
            //Заносим префаб в список и берём индекс
            oMP_Prefabs.Add(objectMapPanelPrefab);
            int prefabIndex = oMP_Prefabs.Count - 1;

            //Заносим префаб в словарь префабов
            UIA_ObjectPanel.objectPanelPrefabs.Add(
                prefabIndex, objectMapPanelPrefab);

            //Возвращаем индекс
            return prefabIndex;
        }
    }
}
