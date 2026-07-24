
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
            //Добавляем системы инициализации
            #region Init
            //Инициализация обзорных панелей
            startup.InitSystem_Add(new S_OverviewPanel_Initialization());

            //Создание блоков 
            startup.InitSystem_Add(new S_Block_Creation());
            #endregion

            //Добавляем покадровые системы
            #region Frame
            //Ввод в обзорных панелях
            startup.FrameSystem_Add(new S_OverviewPanel_Input());
            //Ввод в главной обзорной панели
            startup.FrameSystem_Add(new S_MainOverviewPanel_Input());
            //Ввод в панели планировщика
            startup.FrameSystem_Add(new S_OutlinerPanel_Input());
            //Ввод в панели линз
            startup.FrameSystem_Add(new S_LensPanel_Input());

            //Ввод в блоках
            startup.FrameSystem_Add(new S_Block_Input());
            #endregion

            //Добавляем системы рендеринга
            #region PreRender
            //Управление обзорными панелями
            startup.PreRenderSystem_Add(new S_OverviewPanel_Control());
            #endregion
            #region Render
            //Обновление обзорных панелей
            startup.RenderSystem_Add(new S_OverviewPanel_Update());
            #endregion
            #region PostRender
            //Обновление блоков
            startup.PostRenderSystem_Add(new SMT_BlockList_PreUpdate());
            startup.PostRenderSystem_Add(new S_Block_Update());

            //Очистка событий
            startup.PostRenderSystem_Add(new S_Events_Clear());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Обновление панелей в окне игры в конце каждого тика
            startup.PostTickSystem_Add(new S_GameWindow_TickUpdate());
            #endregion
        }

        public override void Aspects_Add(
            GameStartup startup, 
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            A_UI uI_A = new();
            parentAspect.childrenAspects.Add(uI_A);
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
