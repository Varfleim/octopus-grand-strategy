
using System;
using System.Collections.Generic;

namespace GS.UI
{
    internal class D_BlockList_ElementComparer<T> : IComparer<D_Block_DisplayedEntity<T>>
        where T : notnull, IComparable<T>
    {
        public D_BlockList_ElementComparer(
            bool descending)
        {
            this.descending = descending;
        }

        internal bool Descending
        {
            get
            {
                return descending;
            }
            set
            {
                descending = value;
            }
        }
        private bool descending;

        public int Compare(
            D_Block_DisplayedEntity<T> elementA, D_Block_DisplayedEntity<T> elementB)
        {
            int returnValue;

            returnValue = elementA.sortValue.CompareTo(elementB.sortValue);

            if(descending)
            {
                return -returnValue;
            }
            else
            {
                return returnValue;
            }
        }
    }
}
