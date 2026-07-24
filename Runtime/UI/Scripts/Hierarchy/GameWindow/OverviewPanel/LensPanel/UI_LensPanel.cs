
using Leopotam.EcsProto;

namespace GS.UI
{
    public class UI_LensPanel : UIA_OverviewPanel
    {
        public override void RenderHide()
        {
            UnityEngine.Debug.LogWarning("! ! !");

            base.RenderHide();
        }
    }
}
