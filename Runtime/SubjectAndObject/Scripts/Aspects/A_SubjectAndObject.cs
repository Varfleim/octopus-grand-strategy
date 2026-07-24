
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.SubjectAndObject
{
    public class A_SubjectAndObject : ProtoAspectInject
    {
        public ProtoPool<C_Object> obj_P;

        public ProtoPool<SR_Object_Creation> obj_Creation_SR_P;
        public ProtoIt obj_Creation_SR_I = new(It.Inc<SR_Object_Creation>());

        public ProtoPool<SE_Object_Created> obj_Created_SE_P;
        public ProtoIt obj_Created_SE_I = new(It.Inc<C_Object, SE_Object_Created>());

        public ProtoPool<C_Subject> subj_P;

        public ProtoPool<SR_Subject_Creation> subj_Creation_SR_P;
        public ProtoIt subj_Creation_SR_I = new(It.Inc<SR_Subject_Creation>());

        public ProtoPool<SE_Subject_Created> subj_Created_SE_P;
        public ProtoIt subj_Created_SE_I = new(It.Inc<C_Subject, SE_Subject_Created>());

        public void Object_Creation_SR(
            ProtoEntity objEntity)
        {
            //Назначаем переданной сущности запрос создания объекта
            ref SR_Object_Creation rComp = ref obj_Creation_SR_P.Add(objEntity);

            //Заполняем данные запроса
            rComp = new(0);
        }

        public void Subject_Creation_SR(
            ProtoEntity subjEntity)
        {
            //Назначаем переданной сущности запрос создания субъекта
            ref SR_Subject_Creation rComp = ref subj_Creation_SR_P.Add(subjEntity);

            //Заполняем данные запроса
            rComp = new(0);
        }
    }
}
