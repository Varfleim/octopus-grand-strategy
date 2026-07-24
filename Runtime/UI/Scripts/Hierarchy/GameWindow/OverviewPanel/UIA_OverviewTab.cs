
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Leopotam.EcsProto;

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

        internal List<ProtoEntity> blockEntities = new();
        public VerticalLayoutGroup layoutGroup;

        public virtual void RenderShow()
        {
            //Активируем вкладку
            gameObject.SetActive(true);
        }

        public virtual void RenderUpdate()
        {

        }
        public virtual void TickUpdate()
        {

        }

        public virtual void RenderHide()
        {
            //Деактивируем вкладку
            gameObject.SetActive(false);
        }

        public virtual void FiltersAndPools_CheckFilled()
        {

        }
    }
}
