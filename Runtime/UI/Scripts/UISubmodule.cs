
using UnityEngine;

using GBB;

namespace GS.UI
{
    public class UISubmodule : GameSubmodule
    {
        [SerializeField]
        private UIData uIData;
        [SerializeField]
        private UI_Core uICore;

        public override void AddSystems(GameStartup startup)
        {
            //Добавляем покадровые системы
            #region Frame
            //Ввод в панели объекта
            startup.AddFrameSystem(new SObjectPanelInput());
            #endregion

            //Добавляем системы рендеринга
            #region PreRender
            //Отображение панели объекта
            startup.AddPreRenderSystem(new SObjectPanelControl());

            //Отображение панелей карты объектов
            startup.AddPreRenderSystem(new SObjectMapPanelControl());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Обновление панелей в окне игры в конце каждого тика
            startup.AddPostTickSystem(new SGameWindowTickUpdate());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //Вводим данные
            startup.InjectData(uIData);

            //Вводим данные
            startup.InjectData(uICore);
        }
    }
}
