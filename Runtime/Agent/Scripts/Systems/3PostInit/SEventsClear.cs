
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.Agent
{
    public class SEventsClear : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            //������� ������� �������� �������
            AgentCreatedSelfEventsClear();
        }

        public void Run(IEcsSystems systems)
        {
            //������� ������� �������� �������
            AgentCreatedSelfEventsClear();
        }

        readonly EcsFilterInject<Inc<SEAgentCreated>> agentCreatedSEFilter = default;
        readonly EcsPoolInject<SEAgentCreated> agentCreatedSEPool = default;
        void AgentCreatedSelfEventsClear()
        {
            //��� ������� ������� �������� ������
            foreach (int eventEntity in agentCreatedSEFilter.Value)
            {
                //������� ��������� �������
                agentCreatedSEPool.Value.Del(eventEntity);
            }
        }
    }
}
