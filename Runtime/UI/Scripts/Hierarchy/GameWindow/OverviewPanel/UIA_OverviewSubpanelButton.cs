
using UnityEngine;

using TMPro;

namespace GS.UI
{
    public abstract class UIA_OverviewSubpanelButton : MonoBehaviour
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

        internal bool isSubpanelActive;
    }
}
