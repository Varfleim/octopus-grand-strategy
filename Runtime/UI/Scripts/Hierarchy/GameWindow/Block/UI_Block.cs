
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsProto;

namespace GS.UI
{
    internal class UI_Block : MonoBehaviour
    {
        public static List<UI_Block> cachedPanels = new();

        public ProtoEntity selfEntity;
    }
}
