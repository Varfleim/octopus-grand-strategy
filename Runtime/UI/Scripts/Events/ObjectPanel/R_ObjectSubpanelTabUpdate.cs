
namespace GS.UI
{
    public readonly struct R_ObjectSubpanelTabUpdate
    {
        public R_ObjectSubpanelTabUpdate(
            bool isSamePanel, 
            bool isSameSubpanel, 
            bool isSameTab, 
            bool isSameObject)
        {
            this.isSamePanel = isSamePanel;
            this.isSameSubpanel = isSameSubpanel;
            this.isSameTab = isSameTab;
            this.isSameObject = isSameObject;
        }

        public readonly bool isSamePanel;
        public readonly bool isSameSubpanel;
        public readonly bool isSameTab;
        public readonly bool isSameObject;
    }
}
