
using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_LensPanel : UIA_OverviewPanel
    {
        public override void RenderHide(EcsWorld world)
        {
            UnityEngine.Debug.LogWarning("! ! !");

            base.RenderHide(world);
        }
    }
}
