
using System.Collections.Generic;

using UnityEngine;

namespace GS.UI
{
    /// <summary>
    /// Класс, хранящий функции регистрации панелей интерфейса и префабов для получения индексов
    /// </summary>
    public class UIPrefabs_Data : MonoBehaviour
    {
        private List<UI_MainOverviewSubpanel> mOSubpanels = new();
        private List<UI_MainOverviewTab> mOSbpTabs = new();
        private List<UI_OutlinerTab> outlinerPTabs = new();

        public int MOSbp_AddAndGetIndex(
            UI_MainOverviewPanel mainOverviewPanel,
            UI_MainOverviewSubpanel mainOverviewSubpanel)
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
            UI_MainOverviewSubpanel mainOverviewSubpanel,
            UI_MainOverviewTab mainOverviewSubpanelTab)
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
            UI_OutlinerTab outlinerPanelTab)
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
    }
}
