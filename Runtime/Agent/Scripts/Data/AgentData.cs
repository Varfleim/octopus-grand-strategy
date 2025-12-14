
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.Agent
{
    public class AgentData : MonoBehaviour
    {
        public string AgentObjectType
        {
            get
            {
                return agentObjectType;
            }
        }
        [SerializeField]
        private string agentObjectType;

        internal Dictionary<Color, EcsPackedEntity> agentUniqueColors = new();
        internal List<Color> agentColors = new();
        internal List<EcsPackedEntity> agentColorPEs = new();
    }
}
