
using UnityEngine;

using GBB;

namespace GS.UI
{
    public class UI_Submodule : GameSubmodule
    {
        [SerializeField]
        private UI_Data uI_Data;
        [SerializeField]
        private UIPrefabs_Data uIPrefabs_Data;
        [SerializeField]
        private UI_Core uI_Core;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем покадровые системы
            #region Frame
            //Ввод в главной обзорной панели
            startup.FrameSystem_Add(new S_MainOverviewPanel_Input());
            #endregion

            //Добавляем системы рендеринга
            #region PreRender
            //Управление планировщиком
            startup.PreRenderSystem_Add(new S_OutlinerPanel_Control());
            //Управление главной обзорной панелью
            startup.PreRenderSystem_Add(new S_MainOverviewPanel_Control());

            //Управление панелями объектов
            startup.PreRenderSystem_Add(new S_ObjectPanel_Control());
            #endregion
            #region PostRender
            //Очистка событий
            startup.PostRenderSystem_Add(new S_Events_Clear());
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
            startup.Data_Inject(uIPrefabs_Data);

            //Вводим данные
            startup.Data_Inject(uI_Core);
        }
    }
}
