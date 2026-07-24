
using Leopotam.EcsProto;

namespace GS.UI
{
    public class UI_MainOverviewPanel : UIA_OverviewPanel
    {
        public override void RenderHide()
        {
            UnityEngine.Debug.LogWarning("!");

            base.RenderHide();
        }
    }
}
