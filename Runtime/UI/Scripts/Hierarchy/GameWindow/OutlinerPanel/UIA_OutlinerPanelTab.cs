
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

        public EcsPackedEntity entPE;

        public VerticalLayoutGroup layoutGroup;

        protected bool isFiltersAndPoolsFilled;

        public virtual void RenderShow(EcsWorld world)
        {

        }

        public virtual void RenderUpdate(EcsWorld world)
        {

        }
        public virtual void TickUpdate(EcsWorld world)
        {

        }

        public virtual void RenderHide(EcsWorld world)
        {

        }

        public virtual void FiltersAndPools_CheckFilled(EcsWorld world)
        {

        }
    }
}
