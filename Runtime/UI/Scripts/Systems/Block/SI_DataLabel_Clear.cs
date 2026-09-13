
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    internal class SI_DataLabel_Clear<TDLInterlayerComp> : GBB.VFSystem, IProtoRunSystem
        where TDLInterlayerComp : struct
    {
        [DI] A_UI uI_A;

        ProtoPool<TDLInterlayerComp> dL_interlayerComponent_P;
        [DI] ProtoIt dL_InterlayerComponent_Destroy_SR_I = new(It.Inc<C_DataLabel, TDLInterlayerComp, SR_DataLabel_Destroy>());
        int dataType;

        public void SetType(
            ProtoPool<TDLInterlayerComp> dL_interlayerComponent_P, int dataType)
        {
            this.dL_interlayerComponent_P = dL_interlayerComponent_P;
            this.dataType = dataType;
        }

        public void Run()
        {
            //Удаляем лишние графы
            DataLabels_Clear();
        }

        void DataLabels_Clear()
        {
            //Для каждой графы с компонентом данной прослойки с запросом удаления
            foreach(ProtoEntity dLEntity in dL_InterlayerComponent_Destroy_SR_I)
            {
                //Удаляем графу
                DataLabel_Clear(dLEntity);
            }
        }

        void DataLabel_Clear(
            ProtoEntity dLEntity)
        {
            //Берём графу
            ref C_DataLabel dL = ref uI_A.dL_P.Get(dLEntity);

            //Берём хранилище данных сущности
            ref C_DataLabel_Container dLC = ref uI_A.dLC_P.Get(dL.displayedEntity);
            //Удаляем сущность графы из хранилища
            dLC.dataLabelEntities.Remove(dataType);

            //Если хранилище данных пусто, удаляем его
            if(dLC.dataLabelEntities.Count == 0)
            {
                uI_A.dLC_P.Del(dL.displayedEntity);
            }

            //Удаляем компонент графы, компонент прослойки и запрос
            uI_A.dL_P.Del(dLEntity);
            dL_interlayerComponent_P.Del(dLEntity);
            uI_A.dL_Destroy_SR_P.Del(dLEntity);
        }
    }
}
