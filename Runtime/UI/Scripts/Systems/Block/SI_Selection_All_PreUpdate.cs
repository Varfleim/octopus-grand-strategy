
using System;
using System.Collections.Generic;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

using GBB;

namespace GS.UI
{
    internal class SI_Selection_All_PreUpdate<TSortType, TEntity> : VFSystem, IProtoRunSystem
        where TSortType : notnull, IComparable<TSortType>
        where TEntity : struct
    {
        [DI] ProtoIt selection_Entities_I = new(It.Inc<TEntity>());

        ProtoPool<C_Block_Entities<TSortType>> block_Entities_P;
        [DI] ProtoIt selection_Update_SR_I = new(It.Inc<C_Block_Entities<TSortType>, C_Selection_All<TEntity>, SR_Block_Update>());

        public void SetType(
            ProtoPool<C_Block_Entities<TSortType>> block_Entities_P)
        {
            this.block_Entities_P = block_Entities_P;
        }

        public void Run()
        {
            //Обновляем блоки
            Blocks_Update();
        }

        void Blocks_Update()
        {
            //Если требуется обновление блока данной выборки
            if (selection_Update_SR_I.IsEmptySlow() == false)
            {
                Blocks_All_Entities_Update();
            }
        }

        void Blocks_All_Entities_Update()
        {
            List<ProtoEntity> tempEntities = ListPool<ProtoEntity>.Get();

            //Для каждой сущности данной выборки
            foreach(ProtoEntity entity in selection_Entities_I)
            {
                tempEntities.Add(entity);
            }

            //Переносим список сущностей в массив
            D_Block_DisplayedEntity<TSortType>[] entities = new D_Block_DisplayedEntity<TSortType>[tempEntities.Count];
            for(int a = 0; a < tempEntities.Count; a++)
            {
                entities[a] = new(tempEntities[a]);
            }

            ListPool<ProtoEntity>.Add(tempEntities);

            //Для каждого блока данной выборки
            foreach(ProtoEntity blockEntity in selection_Update_SR_I)
            {
                //Берём список сущностей
                ref C_Block_Entities<TSortType> blockEntities = ref block_Entities_P.Get(blockEntity);

                blockEntities.displayedEntities = entities;
            }
        }
    }
}
