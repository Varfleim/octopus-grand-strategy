
using System;
using System.Collections.Generic;

using Leopotam.EcsProto;

namespace GS.UI
{
    public class D_BlockList_Element
    {
        public string elementName;

        public ProtoEntity elementEntity;

        public List<Tuple<int, float>> elementValues = new();
    }
}
