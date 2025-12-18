
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
            startup.AddFrameSystem(new S_ObjectPanel_Input());
            #endregion

            //Добавляем системы рендеринга
            #region PreRender
            //Отображение панели объекта
            startup.AddPreRenderSystem(new S_ObjectPanel_Control());

            //Отображение панелей карты объектов
            startup.AddPreRenderSystem(new S_ObjectMapPanel_Control());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Обновление панелей в окне игры в конце каждого тика
            startup.AddPostTickSystem(new S_GameWindow_TickUpdate());
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
