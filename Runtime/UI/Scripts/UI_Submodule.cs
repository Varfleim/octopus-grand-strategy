
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
            startup.InitSystem_Add(
                System_New<S_OverviewPanel_Initialization>(SystemWeight.SystemWeight));

            //Создание блоков 
            startup.InitSystem_Add(
                System_New<S_Block_Creation>(SystemWeight.SystemWeight));
            #endregion

            //Добавляем покадровые системы
            #region Frame
            //Ввод в обзорных панелях
            startup.FrameSystem_Add(
                System_New<S_OverviewPanel_Input>(SystemWeight.SystemWeight));
            //Ввод в главной обзорной панели
            startup.FrameSystem_Add(
                System_New<S_MainOverviewPanel_Input>(SystemWeight.SystemWeight));
            //Ввод в панели планировщика
            startup.FrameSystem_Add(
                System_New<S_OutlinerPanel_Input>(SystemWeight.SystemWeight));
            //Ввод в панели линз
            startup.FrameSystem_Add(
                System_New<S_LensPanel_Input>(SystemWeight.SystemWeight));

            //Ввод в блоках
            startup.FrameSystem_Add(
                System_New<S_Block_Input>(SystemWeight.SystemWeight));
            #endregion

            //Добавляем системы рендеринга
            #region Render
            //Управление обзорными панелями
            startup.RenderSystem_Add(
                System_New<S_OverviewPanel_Control>(SystemWeight.PreSystemWeight));

            //Обновление обзорных панелей
            startup.RenderSystem_Add(
                System_New<S_OverviewPanel_Update>(SystemWeight.SystemWeight));

            //Обновление блоков
            startup.RenderSystem_Add(
                System_New<SMT_BlockList_PreUpdate>(SystemWeight.PostSystemWeight));
            startup.RenderSystem_Add(
                System_New<S_Block_Update>(SystemWeight.PostSystemWeight));

            //Очистка событий
            startup.RenderSystem_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем потиковые системы
            #region Tick
            //Обновление панелей в окне игры в конце каждого тика
            startup.TickSystem_Add(
                System_New<S_GameWindow_TickUpdate>(SystemWeight.PostSystemWeight));
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
