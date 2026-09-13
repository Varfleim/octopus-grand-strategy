
using System;

namespace GS.UI
{
    public struct C_Block_Entities<TSortType> 
        where TSortType : notnull, IComparable<TSortType>
    {
        public C_Block_Entities(
            int a)
        {
            displayedEntities = new D_Block_DisplayedEntity<TSortType>[0];
        }

        //TO DO
        //Как-то можно сделать так, чтобы не пересоздавать массив в каждом блоке каждый раз,
        //а обновлять его при совпадении данных
        public D_Block_DisplayedEntity<TSortType>[] displayedEntities;
    }
}
