using UnityEngine;

namespace GS.UI
{
    public class UIA_Block : MonoBehaviour
    {
        public int SelfEntity
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
        private int selfEntity;
    }
}
