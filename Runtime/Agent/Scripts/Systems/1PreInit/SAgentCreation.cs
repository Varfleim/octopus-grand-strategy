
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.Agent
{
    public class SAgentCreation : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<CAgent> agentPool = default;

        readonly EcsFilterInject<Inc<SRAgentCreation>> agentCreationSRFilter = default;
        readonly EcsPoolInject<SRAgentCreation> agentCreationSRPool = default;


        readonly EcsCustomInject<AgentData> agentData = default;

        public void Init(IEcsSystems systems)
        {
            //������ �������
            AgentsCreation();
        }

        void AgentsCreation()
        {
            //���������, ��������� �� �����
            bool isColorsUpdated = false;

            //��� ������� ������� �������� ������
            foreach (int agentRequestEntity in agentCreationSRFilter.Value)
            {
                //���� ������
                ref SRAgentCreation requestComp = ref agentCreationSRPool.Value.Get(agentRequestEntity);

                //������ ������
                AgentCreation(
                    ref requestComp,
                    agentRequestEntity);

                //��������, ��� ����� ���������
                isColorsUpdated = true;

                //���� ������
                ref CAgent agent = ref agentPool.Value.Get(agentRequestEntity);

                //������� ������
                agentCreationSRPool.Value.Del(agentRequestEntity);
            }

            //���� ����� ���������
            if (isColorsUpdated == true)
            {
                //��������� ������ ������ �������
                AgentColorListsUpdate();
            }
        }

        void AgentCreation(
            ref SRAgentCreation requestComp,
            int agentEntity)
        {
            //��������� ���������� �������� ��������� ������
            ref CAgent agent = ref agentPool.Value.Add(agentEntity);

            //��������� �������� ������ ������
            agent = new(
                world.Value.PackEntity(agentEntity), requestComp.agentName);

            //���������� ���������� ���� ������
            AgentColorGeneration(ref agent);

            //������ �����������, ������������ � �������� �������
            AgentCreatedSelfEvent(agentEntity);
        }

        void AgentColorGeneration(
            ref CAgent agent)
        {
            //������ ��������� ����
            Color agentColor = new Color(Random.value, Random.value, Random.value);

            //���� ������ ���� ���������� � ������� ������
            while (agentData.Value.agentUniqueColors.TryGetValue(agentColor, out EcsPackedEntity oldAgentPE) == true)
            {
                //������ ��������� ����
                agentColor = new Color(Random.value, Random.value, Random.value);
            }

            //������� ��� � �������
            agentData.Value.agentUniqueColors.Add(agentColor, agent.selfPE);
        }

        readonly EcsPoolInject<GBB.Map.RMapModeUpdateColorsListFirst> mapModeUpdateColorsListFirstRPool = default;
        void AgentColorListsUpdate()
        {
            //������� ������
            agentData.Value.agentColors.Clear();
            agentData.Value.agentColorPEs.Clear();

            //������� � ������ ���� ���������� ������
            agentData.Value.agentColors.Add(Color.white);
            agentData.Value.agentColorPEs.Add(new());

            //��� ������ ������ � �������
            foreach(KeyValuePair<Color, EcsPackedEntity> kVP in agentData.Value.agentUniqueColors)
            {
                //������� � ������ ���� � PE ������
                agentData.Value.agentColors.Add(kVP.Key);
                agentData.Value.agentColorPEs.Add(kVP.Value);

                //���� ������
                kVP.Value.Unpack(world.Value, out int agentEntity);
                ref CAgent agent = ref agentPool.Value.Get(agentEntity);

                //��������� ������ ����� ������
                agent.SetColorIndex(agentData.Value.agentColors.Count - 1);
            }

            //����������� ��������� ���������� ������ ������ ������ �����
            GBB.Map.MapModeData.MapModeUpdateColorsListFirstRequest(
                world.Value,
                mapModeUpdateColorsListFirstRPool.Value,
                AgentData.agentObjectType,
                agentData.Value.agentColors);
        }

        readonly EcsPoolInject<SEAgentCreated> agentCreatedSEPool = default;
        void AgentCreatedSelfEvent(
            int agentEntity)
        {
            //��������� ���������� �������� ������� �������� ������
            ref SEAgentCreated eventComp = ref agentCreatedSEPool.Value.Add(agentEntity);

            //��������� ������ �������
            eventComp = new(0);
        }
    }
}
