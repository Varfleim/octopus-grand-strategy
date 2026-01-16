
using UnityEngine;
using UnityEngine.UI;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIA_OutlinerPanelTab : MonoBehaviour
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

        public TabGroupButton selfTabButton;

        public EcsPackedEntity objectPE;

        public VerticalLayoutGroup layoutGroup;
    }
}
