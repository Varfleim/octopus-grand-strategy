
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.SubjectAndObject
{
    public class S_Object_Creation : IProtoInitSystem, IProtoRunSystem
    {
        [DI] A_SubjectAndObject subjectAndObject_A;

        public void Init(IProtoSystems systems)
        {
            //Создаём объекты
            Objects_Creation();
        }

        public void Run()
        {
            //Создаём объекты
            Objects_Creation();
        }

        void Objects_Creation()
        {
            //Для каждого запроса создания объекта
            foreach (ProtoEntity objReqEntity in subjectAndObject_A.obj_Creation_SR_I)
            {
                //Берём запрос
                ref SR_Object_Creation rComp = ref subjectAndObject_A.obj_Creation_SR_P.Get(objReqEntity);

                //Создаём объект
                Object_Creation(
                    ref rComp,
                    objReqEntity);

                //Удаляем запрос
                subjectAndObject_A.obj_Creation_SR_P.Del(objReqEntity);
            }
        }

        void Object_Creation(
            ref SR_Object_Creation rComp,
            ProtoEntity objEntity)
        {
            //Назначаем сущности компонент объекта и заполняем его данные
            ref C_Object obj = ref subjectAndObject_A.obj_P.Add(objEntity);
            obj = new(0);

            UnityEngine.Debug.LogWarning("Object Created!");

            //Создаём самособытие, сообщающее о создании объекта
            Object_Created_SE(objEntity);
        }

        void Object_Created_SE(
            ProtoEntity objEntity)
        {
            //Назначаем сущности компонент события и заполняем его данные
            ref SE_Object_Created eComp = ref subjectAndObject_A.obj_Created_SE_P.Add(objEntity);
            eComp = new(0);
        }
    }
}
