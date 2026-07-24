using System.Collections.Generic;

using Leopotam.EcsProto;

namespace GS.UI
{
    public class UI_OutlinerPanel : UIA_OverviewPanel
    {
        internal Dictionary<int, UI_OutlinerTab> tabs = new();

        public override void RenderHide()
        {
            UnityEngine.Debug.LogWarning("! !");

            base.RenderHide();
        }
    }
}
