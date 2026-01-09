
using UnityEngine;

using GBB;

namespace GS.UI
{
    public class UI_Submodule : GameSubmodule
    {
        [SerializeField]
        private UI_Data uI_Data;
        [SerializeField]
        private UI_Core uI_Core;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем покадровые системы
            #region Frame
            //Ввод в панели объекта
            startup.FrameSystem_Add(new S_ObjectPanel_Input());
            #endregion

            //Добавляем системы рендеринга
            #region PreRender
            //Отображение панели объекта
            startup.PreRenderSystem_Add(new S_ObjectPanel_Control());

            //Отображение панелей карты объектов
            startup.PreRenderSystem_Add(new S_ObjectMapPanel_Control());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Обновление панелей в окне игры в конце каждого тика
            startup.PostTickSystem_Add(new S_GameWindow_TickUpdate());
            #endregion
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(uI_Data);

            //Вводим данные
            startup.Data_Inject(uI_Core);
        }
    }
}
