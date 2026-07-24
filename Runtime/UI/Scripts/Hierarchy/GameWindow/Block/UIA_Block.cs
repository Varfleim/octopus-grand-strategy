
using UnityEngine;

using Leopotam.EcsProto;

namespace GS.UI
{
    public class UIA_Block : MonoBehaviour
    {
        public ProtoEntity SelfEntity
        {
            get
            {
                return selfEntity;
            }
            internal set
            {
                selfEntity = value;
            }
        }
        private ProtoEntity selfEntity;
    }
}
