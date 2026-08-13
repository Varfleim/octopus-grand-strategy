
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.SubjectAndObject
{
    public class S_Subject_Creation : GBB.VFSystem, IProtoInitSystem, IProtoRunSystem
    {
        [DI] A_SubjectAndObject subjectAndObject_A;

        public void Init(IProtoSystems systems)
        {
            //Создаём субъекты
            Subjects_Creation();
        }

        public void Run()
        {
            //Создаём субъекты
            Subjects_Creation();
        }

        void Subjects_Creation()
        {
            //Для каждого запроса создания субъекта
            foreach (ProtoEntity subjReqEntity in subjectAndObject_A.subj_Creation_SR_I)
            {
                //Берём запрос
                ref SR_Subject_Creation rComp = ref subjectAndObject_A.subj_Creation_SR_P.Get(subjReqEntity);

                //Создаём субъект
                Subject_Creation(
                    ref rComp,
                    subjReqEntity);

                //Удаляем запрос
                subjectAndObject_A.subj_Creation_SR_P.Del(subjReqEntity);
            }
        }

        void Subject_Creation(
            ref SR_Subject_Creation rComp,
            ProtoEntity subjEntity)
        {
            //Назначаем сущности компонент субъекта и заполняем его данные
            ref C_Subject subj = ref subjectAndObject_A.subj_P.Add(subjEntity);
            subj = new(0);

            UnityEngine.Debug.LogWarning("Subject Created!");

            //Создаём самособытие, сообщающее о создании субъекта
            Subject_Created_SE(subjEntity);
        }

        void Subject_Created_SE(
            ProtoEntity subjEntity)
        {
            //Назначаем сущности компонент события и заполняем его данные
            ref SE_Subject_Created eComp = ref subjectAndObject_A.subj_Created_SE_P.Add(subjEntity);
            eComp = new(0);
        }
    }
}
