
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Threads;

namespace GS.UI
{
    public class SMT_BlockList_PreUpdate : GBB.VFSystem, IProtoRunSystem
    {
        [DI] A_UI uI_A;

        ProtoThreadHandler threadHandler;

        public void Run()
        {
            //Запускаем параллельную обработку
            uI_A.bL_Update_SR_I.RunParallel(threadHandler ??= Threads_Handling, 1);
        }

        void Threads_Handling(ProtoThreadIt threadIt)
        {
            //Для каждого блока-списка, требующего обновления
            foreach(ProtoEntity bEntity in threadIt)
            {
                //Берём блок
                ref C_BlockList bL = ref uI_A.bL_P.Get(bEntity);

                //Сортируем элементы по указанному параметру
                Elements_Sort(ref bL);
            }
        }

        void Elements_Sort(
            ref C_BlockList bL)
        {
            bL.currentElements.Sort(bL.comparer);
        }
    }
}
