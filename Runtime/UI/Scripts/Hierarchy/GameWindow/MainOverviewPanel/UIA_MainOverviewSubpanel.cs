
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

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

        internal Dictionary<int, UIA_MainOverviewSubpanelTab> tabs = new();
        public TabGroup tabGroup;

        public UIA_MainOverviewSubpanelTab activeSubpanelTab;

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
