
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIA_EntityPanel : MonoBehaviour
    {
        internal static Dictionary<int, UIA_EntityPanel> entPanelPrefabs = new();
        internal static Dictionary<int, List<UIA_EntityPanel>> cachedEntPanels = new();

        public EcsPackedEntity selfPE;
    }
}
