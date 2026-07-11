
using System;
using System.Collections.Generic;

namespace GS.UI
{
    public class DT_BlockList_Element
    {
        public string elementName;

        public int elementEntity;

        public List<Tuple<int, float>> elementValues = new();
    }
}
