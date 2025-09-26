
using UnityEngine;

using GBB;

namespace GS.Agent
{
    [CreateAssetMenu]
    internal class AgentModule : GameModule
    {
        public string agentObjectType;

        public override void AddSystems(GameStartup startup)
        {
            //��������� ������� �������������
            #region PreInit
            //�������� ������� �� �������
            startup.AddPreInitSystem(new SAgentCreation());
            #endregion
            #region PostInit
            //������� �������
            startup.AddPostInitSystem(new SEventsClear());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //������ ��������� ������ �������
            AgentData agentData = startup.AddDataObject().AddComponent<AgentData>();

            //��������� � ���� ������
            AgentData.agentObjectType = agentObjectType;

            //������ ������
            startup.InjectData(agentData);
        }
    }
}
