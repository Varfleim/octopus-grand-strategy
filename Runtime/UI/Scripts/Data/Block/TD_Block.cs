using System;

namespace GS.UI
{
    [Serializable]
    public struct TD_Block
    {
        public string selectionCode;

        public int[] dataTypeIndexes;

        public int lMBActionPanelType;
        public int lMBActionSubpanelType;
        public int lMBActionTabType;
    }
}
