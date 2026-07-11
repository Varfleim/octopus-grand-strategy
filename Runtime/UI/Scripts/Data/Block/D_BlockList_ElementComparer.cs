
using System.Collections.Generic;

namespace GS.UI
{
    internal class D_BlockList_ElementComparer : IComparer<D_BlockList_Element>
    {
        public D_BlockList_ElementComparer(
            int sortingValue,
            bool descending)
        {
            this.sortingValue = sortingValue;

            this.descending = descending;
        }

        internal int SortingValue
        {
            get
            {
                return sortingValue;
            }
            set
            {
                sortingValue = value;
            }
        }
        private int sortingValue;

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
            D_BlockList_Element elementA, D_BlockList_Element elementB)
        {
            int returnValue;

            if (elementA == null)
            {
                if (elementB == null)
                {
                    returnValue = 0;
                }
                else
                {
                    returnValue = -1;
                }
            }
            else
            {
                if (elementB == null)
                {
                    returnValue = 1;
                }
                else
                {
                    returnValue = elementA.elementValues[sortingValue].Item2.CompareTo(elementB.elementValues[sortingValue].Item2);
                }
            }

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
