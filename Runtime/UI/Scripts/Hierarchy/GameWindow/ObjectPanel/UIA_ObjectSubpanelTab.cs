
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UIA_ObjectSubpanelTab : MonoBehaviour
    {
        public string SelfType
        {
            get
            {
                return selfType;
            }
        }
        string selfType;

        public TabGroupButton selfTabButton;

        public EcsPackedEntity objectPE;

        public void SetSelfType(
            string selfType)
        {
            this.selfType = selfType;
        }
    }
}
