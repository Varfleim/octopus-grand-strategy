
using UnityEngine;

using GBB;

namespace GS.Agent
{
    internal class AgentSubmodule : GameSubmodule
    {
        [SerializeField]
        private AgentData agentData;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region PreInit
            //Создание агентов по запросу
            startup.PreInitSystem_Add(new SAgentCreation());
            #endregion
            #region PostInit
            //Очистка событий
            startup.PostInitSystem_Add(new SEventsClear());
            #endregion
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(agentData);
        }
    }
}
