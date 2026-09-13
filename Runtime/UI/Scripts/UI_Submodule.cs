
using UnityEngine;

using GBB;

namespace GS.UI
{
    public class UI_Submodule : AUI_Submodule
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
            startup.InitSystems_Add(
                System_New<S_OverviewPanel_Initialization>(SystemWeight.SystemWeight));

            //Создание блоков 
            startup.InitSystems_Add(
                System_New<S_Block_Creation>(SystemWeight.SystemWeight));

            //Очистка событий
            startup.InitSystems_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем покадровые системы
            #region Frame
            //Ввод в обзорных панелях
            startup.FrameSystems_Add(
                System_New<S_OverviewPanel_Input>(SystemWeight.SystemWeight));
            //Ввод в главной обзорной панели
            startup.FrameSystems_Add(
                System_New<S_MainOverviewPanel_Input>(SystemWeight.SystemWeight));
            //Ввод в панели планировщика
            startup.FrameSystems_Add(
                System_New<S_OutlinerPanel_Input>(SystemWeight.SystemWeight));
            //Ввод в панели линз
            startup.FrameSystems_Add(
                System_New<S_LensPanel_Input>(SystemWeight.SystemWeight));

            //Ввод в блоках
            startup.FrameSystems_Add(
                System_New<S_Block_Input>(SystemWeight.SystemWeight));
            //Обработка ввода в блоках
            startup.FrameSystems_Add(
                System_New<S_Block_Control>(SystemWeight.SystemWeight));
            #endregion

            //Добавляем системы рендеринга
            #region Render
            //Управление обзорными панелями
            startup.RenderSystems_Add(
                System_New<S_OverviewPanel_Control>(SystemWeight.StartSystemWeight));

            //Предварительное обновление блоков - запрос их обновления через активные вкладки
            startup.RenderSystems_Add(
                System_New<S_Block_PreUpdate>(BlockPreUpdateWeight));

            //Обновление блоков - создание панелей для сущностей и граф для информации
            startup.RenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                BlockUpdateSystem_New());

            //Обновление блоков
            startup.RenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                MTBlockPostUpdateSystem_New());
            startup.RenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                BlockPostUpdateSystem_New());

            //Очистка событий
            startup.RenderSystems_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем потиковые системы
            #region Tick
            //Обновление панелей в окне игры в конце каждого тика
            startup.TickSystems_Add(
                System_New<S_GameWindow_TickUpdate>(SystemWeight.PostSystemWeight));
            #endregion

            #region TickRender
            //Предварительное обновление блоков - запрос их обновления через активные вкладки
            startup.TickRenderSystems_Add(
                System_New<S_Block_PreUpdate>(BlockPreUpdateWeight));

            //Обновление блоков - создание панелей для сущностей и граф для информации
            startup.TickRenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                BlockUpdateSystem_New());
            //Очистка граф
            startup.TickRenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                DataLabelClearSystem_New());

            //Обновление блоков
            startup.TickRenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                MTBlockPostUpdateSystem_New());
            startup.TickRenderGroupSystem_Add(
                new BlockUpdate_Solver(),
                BlockPostUpdateSystem_New());

            //Очистка событий
            startup.TickRenderSystems_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion
        }

        public override void Aspects_Add(
            GameStartup startup, 
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            A_UI uI_A = new();
            parentAspect.childrenAspects.Add(uI_A);
            AUI_Submodule.uI_A = uI_A;
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

        VFSystem[] BlockUpdateSystem_New()
        {
            SystemWeight systemWeight = BlockUpdateWeight;
            VFSystem[] systems = new VFSystem[3];

            S_Block_Update<int> systemInt = System_New<S_Block_Update<int>>(systemWeight);
            systemInt.SetType(uI_A.block_Int_P);
            systems[0] = systemInt;
            S_Block_Update<float> systemFloat = System_New<S_Block_Update<float>>(systemWeight);
            systemFloat.SetType(uI_A.block_Float_P);
            systems[1] = systemFloat;
            S_Block_Update<string> systemString = System_New<S_Block_Update<string>>(systemWeight);
            systemString.SetType(uI_A.block_String_P);
            systems[2] = systemString;

            return systems;
        }

        VFSystem[] DataLabelClearSystem_New()
        {
            SystemWeight systemWeight = BlockUpdateWeight;
            VFSystem[] systems = new VFSystem[3];

            S_DataLabel_Clear<int> systemInt = System_New<S_DataLabel_Clear<int>>(systemWeight);
            systemInt.SetType(uI_A.dL_Int_P);
            systems[0] = systemInt;
            S_DataLabel_Clear<float> systemFloat = System_New<S_DataLabel_Clear<float>>(systemWeight);
            systemFloat.SetType(uI_A.dL_Float_P);
            systems[1] = systemFloat;
            S_DataLabel_Clear<string> systemString = System_New<S_DataLabel_Clear<string>>(systemWeight);
            systemString.SetType(uI_A.dL_String_P);
            systems[2] = systemString;

            return systems;
        }

        VFSystem[] MTBlockPostUpdateSystem_New()
        {
            SystemWeight systemWeight = BlockPostUpdateWeight;
            VFSystem[] systems = new VFSystem[3];

            SMT_Block_PostUpdate<int> systemInt = System_New<SMT_Block_PostUpdate<int>>(systemWeight);
            systemInt.SetType(
                uI_A.block_Int_P,
                uI_A.dL_Int_P);
            systems[0] = systemInt;
            SMT_Block_PostUpdate<float> systemFloat = System_New<SMT_Block_PostUpdate<float>>(systemWeight);
            systemFloat.SetType(
                uI_A.block_Float_P,
                uI_A.dL_Float_P);
            systems[1] = systemFloat;
            SMT_Block_PostUpdate<string> systemString = System_New<SMT_Block_PostUpdate<string>>(systemWeight);
            systemString.SetType(
                uI_A.block_String_P,
                uI_A.dL_String_P);
            systems[2] = systemString;

            return systems;
        }

        VFSystem[] BlockPostUpdateSystem_New()
        {
            SystemWeight systemWeight = BlockPostUpdateWeight;
            VFSystem[] systems = new VFSystem[3];

            S_Block_PostUpdate<int> systemInt = System_New<S_Block_PostUpdate<int>>(systemWeight);
            systemInt.SetType(uI_A.block_Int_P);
            systems[0] = systemInt;
            S_Block_PostUpdate<float> systemFloat = System_New<S_Block_PostUpdate<float>>(systemWeight);
            systemFloat.SetType(uI_A.block_Float_P);
            systems[1] = systemFloat;
            S_Block_PostUpdate<string> systemString = System_New<S_Block_PostUpdate<string>>(systemWeight);
            systemString.SetType(uI_A.block_String_P);
            systems[2] = systemString;

            return systems;
        }
    }
}
