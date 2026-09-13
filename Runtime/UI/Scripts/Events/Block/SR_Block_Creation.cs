
namespace GS.UI
{
    internal readonly struct SR_Block_Creation
    {
        internal SR_Block_Creation(
            string parentPanelCode, string parentSubpanelCode, string parentTabCode,
            int blockType)
        {
            this.parentPanelCode = parentPanelCode;
            this.parentSubpanelCode = parentSubpanelCode;
            this.parentTabCode = parentTabCode;

            this.blockType = blockType;
        }

        public readonly string parentPanelCode;
        public readonly string parentSubpanelCode;
        public readonly string parentTabCode;

        public readonly int blockType;

        public override string ToString()
        {
            return $"{nameof(parentPanelCode)}: {parentPanelCode} \n" +
                $"{nameof(parentPanelCode)}: {parentSubpanelCode} \n" +
                $"{nameof(parentTabCode)}: {parentTabCode} \n" +
                $"{nameof(blockType)}: {blockType}";
        }
    }
}
