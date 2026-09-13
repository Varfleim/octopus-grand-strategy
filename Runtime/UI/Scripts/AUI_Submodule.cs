
using Leopotam.EcsProto;

using GBB;

namespace GS.UI
{
    public abstract class AUI_Submodule : GameSubmodule
    {
        internal static A_UI uI_A;

        #region Blocks
        protected SystemWeight BlockPreUpdateWeight
        {
            get
            {
                return blocksUpdateStartWeight;
            }
        }
        protected SystemWeight BlockUpdateWeight
        {
            get
            {
                return blocksUpdateStartWeight + 1;
            }
        }
        protected SystemWeight BlockPostUpdateWeight
        {
            get
            {
                return blocksUpdateStartWeight + 2;
            }
        }
        private const SystemWeight blocksUpdateStartWeight = SystemWeight.PreSystemWeight;

        protected VFSystem BlockCreationSystem_New<TSelectionComp>(
            ProtoPool<TSelectionComp> selectionComp_P, string selectionCode) where TSelectionComp : struct
        {
            SI_Selection_Creation<TSelectionComp> system = System_New<SI_Selection_Creation<TSelectionComp>>(SystemWeight.SystemWeight);

            system.SetType(
                selectionComp_P,
                selectionCode);

            return system;
        }

        protected VFSystem[] SelectionAllPreUpdateSystem_New<TEntity>()
            where TEntity : struct
        {
            SystemWeight systemWeight = BlockPreUpdateWeight;
            VFSystem[] systems = new VFSystem[3];

            SI_Selection_All_PreUpdate<int, TEntity> systemInt
                = System_New<SI_Selection_All_PreUpdate<int, TEntity>>(systemWeight);
            systemInt.SetType(uI_A.block_Int_P);
            systems[0] = systemInt;

            SI_Selection_All_PreUpdate<float, TEntity> systemFloat
                = System_New<SI_Selection_All_PreUpdate<float, TEntity>>(systemWeight);
            systemFloat.SetType(uI_A.block_Float_P);
            systems[1] = systemFloat;

            SI_Selection_All_PreUpdate<string, TEntity> systemString
                = System_New<SI_Selection_All_PreUpdate<string, TEntity>>(systemWeight);
            systemString.SetType(uI_A.block_String_P);
            systems[2] = systemString;

            return systems;
        }

        protected VFSystem DataLabelCreationSystem_New<TDLInterlayerComp>(
            ProtoPool<TDLInterlayerComp> dL_InterlayerComponent_P, int dataType) where TDLInterlayerComp : struct
        {
            SI_DataLabel_Creation<TDLInterlayerComp> system = System_New<SI_DataLabel_Creation<TDLInterlayerComp>>(BlockUpdateWeight);

            system.SetType(
                dL_InterlayerComponent_P, dataType);

            return system;
        }

        protected VFSystem DataLabelClearSystem_New<TDLInterlayerComp>(
            ProtoPool<TDLInterlayerComp> dL_InterlayerComponent_P, int dataType) where TDLInterlayerComp : struct
        {
            SI_DataLabel_Clear<TDLInterlayerComp> system = System_New<SI_DataLabel_Clear<TDLInterlayerComp>>(BlockUpdateWeight);

            system.SetType(
                dL_InterlayerComponent_P, dataType);

            return system;
        }
        #endregion
    }
}
