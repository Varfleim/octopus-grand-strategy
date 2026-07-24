
using System;

namespace GS.UI
{
    [Serializable]
    public struct DL_OverviewSubpanel
    {
        public DL_OverviewSubpanel(
            string parentPanelCode,
            string selfCode,
            bool hasButton)
        {
            this.parentPanelCode = parentPanelCode;

            this.selfCode = selfCode;

            this.hasButton = hasButton;
        }

        public string parentPanelCode;

        public string selfCode;

        public bool hasButton;
    }
}
