
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIA_OverviewTab : MonoBehaviour
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

        protected bool isFiltersAndPoolsFilled;

        internal List<int> blockEntities = new();
        public VerticalLayoutGroup layoutGroup;

        public virtual void RenderShow(EcsWorld world)
        {
            //Активируем вкладку
            gameObject.SetActive(true);
        }

        public virtual void RenderUpdate(EcsWorld world)
        {

        }
        public virtual void TickUpdate(EcsWorld world)
        {

        }

        public virtual void RenderHide(EcsWorld world)
        {
            //Деактивируем вкладку
            gameObject.SetActive(false);
        }

        public virtual void FiltersAndPools_CheckFilled(EcsWorld world)
        {

        }
    }
}
