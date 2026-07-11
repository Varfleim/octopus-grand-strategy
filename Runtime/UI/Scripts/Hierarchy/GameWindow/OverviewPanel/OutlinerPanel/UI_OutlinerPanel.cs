using System.Collections.Generic;

using TMPro;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_OutlinerPanel : UIA_OverviewPanel
    {
        internal Dictionary<int, UI_OutlinerTab> tabs = new();

        public override void RenderHide(EcsWorld world)
        {
            UnityEngine.Debug.LogWarning("! !");

            base.RenderHide(world);
        }
    }
}
