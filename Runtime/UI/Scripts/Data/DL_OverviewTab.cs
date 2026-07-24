
using System;

namespace GS.UI
{
    [Serializable]
    public struct DL_OverviewTab
    {
        public DL_OverviewTab(
            string parentPanelCode, string parentSubpanelCode, 
            string selfCode)
        {
            this.parentPanelCode = parentPanelCode;
            this.parentSubpanelCode = parentSubpanelCode;
            
            this.selfCode = selfCode;
        }

        public string parentPanelCode;
        public string parentSubpanelCode;

        public string selfCode;
    }
}
