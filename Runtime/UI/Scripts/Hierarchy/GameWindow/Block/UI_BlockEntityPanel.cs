
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Leopotam.EcsProto;

namespace GS.UI
{
    internal class UI_BlockEntityPanel : MonoBehaviour
    {
        public static List<UI_BlockEntityPanel> cachedPanels = new();

        public VerticalLayoutGroup layoutGroup;

        public ProtoEntity selfEntity;
    }
}
