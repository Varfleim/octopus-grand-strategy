
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class SI_DataLabel_Creation<TDLInterlayerComp> : GBB.VFSystem, IProtoRunSystem
        where TDLInterlayerComp : struct
    {
        [DI] A_UI uI_A;

        ProtoPool<TDLInterlayerComp> dL_interlayerComponent_P;
        int dataType;

        public void SetType(
            ProtoPool<TDLInterlayerComp> dL_interlayerComponent_P, int dataType)
        {
            this.dL_interlayerComponent_P = dL_interlayerComponent_P;
            this.dataType = dataType;
        }

        public void Run()
        {
            //Создаём графы
            DataLabels_Creation();
        }

        void DataLabels_Creation()
        {
            //Для каждого запроса создания графы
            foreach(ProtoEntity dLEntity in uI_A.dL_Creation_SR_I)
            {
                //Создаём графу
                DataLabel_Creation(dLEntity);
            }
        }

        void DataLabel_Creation(
            ProtoEntity dLEntity)
        {
            //Берём запрос
            ref SR_DataLabel_Creation rComp = ref uI_A.dL_Creation_SR_P.Get(dLEntity);

            //TO DO
            //Если тип данных графы соответствует типу данных системы,
            //назначаем ей компонент соответствующих данных и удаляем запрос
            if(rComp.dataType == dataType)
            {
                ref TDLInterlayerComp interlayerComponent = ref dL_interlayerComponent_P.Add(dLEntity);

                uI_A.dL_Creation_SR_P.Del(dLEntity);
            }
        }
    }
}
