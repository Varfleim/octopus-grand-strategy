
using UnityEngine;

using TMPro;

namespace GS.UI
{
    public abstract class UIA_OverviewTabButton : MonoBehaviour
    {
        public TextMeshProUGUI buttonNameText;

        internal int PanelType
        {
            get
            {
                return panelType;
            }
            set
            {
                panelType = value;
            }
        }
        private int panelType;

        internal int SubpanelType
        {
            get
            {
                return subpanelType;
            }
            set
            {
                subpanelType = value;
            }

        }
        private int subpanelType;

        internal int TabType
        {
            get
            {
                return tabType;
            }
            set
            {
                tabType = value;
            }

        }
        private int tabType;
    }
}
