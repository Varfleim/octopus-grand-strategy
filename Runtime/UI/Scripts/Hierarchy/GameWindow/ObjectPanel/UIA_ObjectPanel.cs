
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIA_ObjectPanel : MonoBehaviour
    {
        internal static Dictionary<int, UIA_ObjectPanel> objectPanelPrefabs = new();
        internal static Dictionary<int, List<UIA_ObjectPanel>> cachedObjectPanels = new();

        public EcsPackedEntity selfPE;
    }
}
