
using Leopotam.EcsLite;
using Leopotam.EcsLite.Threads;

namespace GS.UI
{
    public class SMT_BlockList_PreUpdate : EcsThreadSystem<T_BlockList_PreUpdate, 
        C_BlockList>
    {
        protected override int GetChunkSize(IEcsSystems systems)
        {
            return 1;
        }

        protected override EcsWorld GetWorld(IEcsSystems systems)
        {
            return systems.GetWorld();
        }

        protected override EcsFilter GetFilter(EcsWorld world)
        {
            return world.Filter<C_BlockList>().Inc<SR_Block_Update>().End();
        }

        protected override void SetData(IEcsSystems systems, ref T_BlockList_PreUpdate thread)
        {
            //Здесь должно быть обращение к файлу данных, описывающему параметры
        }
    }
}
