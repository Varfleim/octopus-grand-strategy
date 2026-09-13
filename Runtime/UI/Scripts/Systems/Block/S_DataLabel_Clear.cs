
using System;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

using GBB;

namespace GS.UI
{
    internal class S_DataLabel_Clear<TSortType> : VFSystem, IProtoRunSystem
        where TSortType : notnull, IComparable<TSortType>
    {
        [DI] A_UI uI_A;

        ProtoPool<C_DataLabel_Value<TSortType>> dL_Value_P;
        [DI] ProtoItExc dL_WithoutUpdate_SR_I = new(It.Inc<C_DataLabel, C_DataLabel_Value<TSortType>>(), It.Exc<SR_DataLabel_Update>());

        public void SetType(
            ProtoPool<C_DataLabel_Value<TSortType>> dL_Value_P)
        {
            this.dL_Value_P = dL_Value_P;
        }

        public void Run()
        {
            //Удаляем лишние графы
            DataLabels_Clear();
        }

        void DataLabels_Clear()
        {
            //Для каждой графы без запроса обновления
            foreach(ProtoEntity dLEntity in dL_WithoutUpdate_SR_I)
            {
                DataLabel_Clear(dLEntity);
            }
        }

        void DataLabel_Clear(
            ProtoEntity dLEntity)
        {
            //Удаляем компонент значения графы и запрашиваем доудаление
            dL_Value_P.Del(dLEntity);
            ref SR_DataLabel_Destroy rComp = ref uI_A.dL_Destroy_SR_P.Add(dLEntity);
        }
    }
}
