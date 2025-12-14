
using UnityEngine;

using GBB;

namespace GS.Agent
{
    internal class AgentSubmodule : GameSubmodule
    {
        [SerializeField]
        private AgentData agentData;

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
            //Вводим данные
            startup.InjectData(agentData);
        }
    }
}
