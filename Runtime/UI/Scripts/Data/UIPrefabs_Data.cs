
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
        private List<UIA_EntityScreenPanel> oSP_Prefabs = new();
        private List<UIA_EntityMapPanel> oMP_Prefabs = new();

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
            mainOverviewSubpanel.tabs.Add(
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
            UIA_EntityScreenPanel entScreenPanelPrefab)
        {
            //Заносим префаб в список и берём индекс
            oSP_Prefabs.Add(entScreenPanelPrefab);
            int prefabIndex = oSP_Prefabs.Count - 1;

            //Заносим префаб в словарь префабов
            UIA_EntityScreenPanel.entPanelPrefabs.Add(
                prefabIndex, entScreenPanelPrefab);

            //Создаём новый список в словаре списков для кэширования
            UIA_EntityScreenPanel.cachedEntPanels[prefabIndex] = new List<UIA_EntityPanel>();

            //Возвращаем индекс
            return prefabIndex;
        }

        public int OMP_Prefab_AddAndGetIndex(
            UIA_EntityMapPanel entMapPanelPrefab)
        {
            //Заносим префаб в список и берём индекс
            oMP_Prefabs.Add(entMapPanelPrefab);
            int prefabIndex = oMP_Prefabs.Count - 1;

            //Заносим префаб в словарь префабов
            UIA_EntityMapPanel.entPanelPrefabs.Add(
                prefabIndex, entMapPanelPrefab);

            //Создаём новый список в словаре списков для кэширования
            UIA_EntityMapPanel.cachedEntPanels[prefabIndex] = new List<UIA_EntityPanel>();

            //Возвращаем индекс
            return prefabIndex;
        }
    }
}
