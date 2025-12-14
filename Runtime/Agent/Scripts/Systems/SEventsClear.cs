
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.Agent
{
    public class SEventsClear : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            //Очищаем события создания агентов
            AgentCreatedSelfEventsClear();
        }

        public void Run(IEcsSystems systems)
        {
            //Очищаем события создания агентов
            AgentCreatedSelfEventsClear();
        }

        readonly EcsFilterInject<Inc<SEAgentCreated>> agentCreatedSEFilter = default;
        readonly EcsPoolInject<SEAgentCreated> agentCreatedSEPool = default;
        void AgentCreatedSelfEventsClear()
        {
            //Для каждого события создания агента
            foreach (int eventEntity in agentCreatedSEFilter.Value)
            {
                //Удаляем компонент события
                agentCreatedSEPool.Value.Del(eventEntity);
            }
        }
    }
}
