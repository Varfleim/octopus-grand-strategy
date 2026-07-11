
using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_MainOverviewPanel : UIA_OverviewPanel
    {
        public override void RenderHide(EcsWorld world)
        {
            UnityEngine.Debug.LogWarning("!");

            base.RenderHide(world);
        }
    }
}
