
using System.Collections.Generic;

using UnityEngine.UI;

namespace GS.UI
{
    public class UI_BlockList : UIA_Block
    {
        public VerticalLayoutGroup layoutGroup;

        public List<UI_BlockList_ElementPanel> panels = new();
    }
}
