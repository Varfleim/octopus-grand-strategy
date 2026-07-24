
using System.Collections.Generic;

using UnityEngine;

namespace GS.UI
{
    public class UI_Data : MonoBehaviour
    {
        #region OverviewPanels
        [SerializeField]
        internal UIA_OverviewPanel[] oPsArray;
        [SerializeField]
        internal Dictionary<string, int> oPsCodeToIndexDict = new();
        [SerializeField]
        internal Dictionary<int, UIA_OverviewPanel> oPsIndexToObjectDict = new();
        [SerializeField]
        public UI_MainOverviewPanel mainOverviewPanel;
        [SerializeField]
        public UI_OutlinerPanel outlinerPanel;
        [SerializeField]
        public UI_LensPanel lensPanel;

        internal bool OP_GetByCode(
            string panelCode, out UIA_OverviewPanel oP)
        {
            oP = null;

            //≈сли панель с таким кодом существует, то возвращаем true и панель
            if(oPsCodeToIndexDict.TryGetValue(panelCode, out int panelIndex))
            {
                oP = oPsIndexToObjectDict[panelIndex];

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region OverviewSubpanels
        [SerializeField]
        internal DL_OverviewSubpanel[] oSbpsTemplateArray;
        [SerializeField]
        internal Dictionary<string, int> oSbpsCodeToIndexDict = new();
        [SerializeField]
        internal Dictionary<int, UIA_OverviewSubpanel> oSbpsIndexToObjectDict = new();

        internal bool OSbp_GetByCode(
            string subpanelCode, out UIA_OverviewSubpanel oSbp)
        {
            oSbp = null;

            //≈сли подпанель с таким кодом существует, то возвращаем true и подпанель
            if(oSbpsCodeToIndexDict.TryGetValue(subpanelCode, out int subpanelIndex))
            {
                oSbp = oSbpsIndexToObjectDict[subpanelIndex];

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region OverviewTabs
        [SerializeField]
        internal DL_OverviewTab[] oTsTemplateArray;
        [SerializeField]
        internal Dictionary<string, int> oTsCodeToIndexDict = new();
        [SerializeField]
        internal Dictionary<int, UIA_OverviewTab> oTsIndexToObjectDict = new();

        internal bool OT_GetByCode(
            string tabCode, out UIA_OverviewTab oT)
        {
            oT = null;

            //≈сли вкладка с таким кодом существует, то возвращаем true и вкладку
            if (oTsCodeToIndexDict.TryGetValue(tabCode, out int tabIndex))
            {
                oT = oTsIndexToObjectDict[tabIndex];

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        [SerializeField]
        public UI_BlockList blockListPrefab;
        [SerializeField]
        public UI_BlockList_ElementPanel blockListElementPanelPrefab;
        [SerializeField]
        public UI_BlockList_ElementValuePanel blockListElementValuePanelPrefab;
    }
}
