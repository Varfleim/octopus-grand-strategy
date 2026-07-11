
using Leopotam.EcsLite.Threads;

namespace GS.UI
{
    public struct T_BlockList_PreUpdate : IEcsThread<
        C_BlockList>
    {
        int[] b_E;

        C_BlockList[] bL_P;
        int[] bL_I;

        public void Init(
            int[] entities,
            C_BlockList[] pool1, int[] indices1)
        {
            b_E = entities;

            bL_P = pool1;
            bL_I = indices1;
        }

        public void Execute(int threadId, int fromIndex, int beforeIndex)
        {
            //Для каждого блока-списка, требующего обновления
            for(int a = fromIndex; a < beforeIndex; a++)
            {
                //Берём блок
                int bEntity = b_E[a];
                ref C_BlockList bL = ref bL_P[bL_I[bEntity]];

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
