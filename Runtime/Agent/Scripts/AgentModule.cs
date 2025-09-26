
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
            //Добавляем системы инициализации
            #region PreInit
            //Создание агентов по запросу
            startup.AddPreInitSystem(new SAgentCreation());
            #endregion
            #region PostInit
            //Очистка событий
            startup.AddPostInitSystem(new SEventsClear());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //Создаём компонент данных агентов
            AgentData agentData = startup.AddDataObject().AddComponent<AgentData>();

            //Переносим в него данные
            AgentData.agentObjectType = agentObjectType;

            //Вводим данные
            startup.InjectData(agentData);
        }
    }
}
