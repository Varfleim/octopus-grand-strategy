
using System.Collections.Generic;

using UnityEngine;

namespace GS.UI
{
    public abstract class UIA_MainOverviewSubpanel : MonoBehaviour
    {
        public int SelfType
        {
            get
            {
                return selfType;
            }
            internal set
            {
                selfType = value;
            }
        }
        private int selfType;

        internal Dictionary<int, UIA_MainOverviewSubpanelTab> subpanelTabs = new();
        public TabGroup tabGroup;

        public UIA_MainOverviewSubpanelTab activeSubpanelTab;
    }
}
