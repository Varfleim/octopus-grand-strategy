
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.SubjectAndObject
{
    public class S_Subject_Creation : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            //Создаём субъекты
            Subjects_Creation();
        }

        public void Run(IEcsSystems systems)
        {
            //Создаём субъекты
            Subjects_Creation();
        }

        readonly EcsFilterInject<Inc<SR_Subject_Creation>> subject_Creation_SR_F = default;
        void Subjects_Creation()
        {
            //Для каждого запроса создания субъекта
            foreach (int subjectRequestEntity in subject_Creation_SR_F.Value)
            {
                //Берём запрос
                ref SR_Subject_Creation rComp = ref subject_Creation_SR_F.Pools.Inc1.Get(subjectRequestEntity);

                //Создаём субъект
                Subject_Creation(
                    ref rComp,
                    subjectRequestEntity);

                //Удаляем запрос
                subject_Creation_SR_F.Pools.Inc1.Del(subjectRequestEntity);
            }
        }

        readonly EcsPoolInject<C_Subject> subject_P = default;
        void Subject_Creation(
            ref SR_Subject_Creation rComp,
            int subjectEntity)
        {
            //Назначаем сущность компонент субъекта и заполняем его данные
            ref C_Subject subj = ref subject_P.Value.Add(subjectEntity);
            subj = new(0);

            UnityEngine.Debug.LogWarning("Subject Created!");

            //Создаём самособытие, сообщающее о создании субъекта
            Subject_Created_SE(
                subjectEntity);
        }

        readonly EcsPoolInject<SE_Subject_Created> subject_Created_SE_P = default;
        void Subject_Created_SE(
            int subjectEntity)
        {
            //Назначаем сущности компонент события и заполняем его данные
            ref SE_Subject_Created eComp = ref subject_Created_SE_P.Value.Add(subjectEntity);
            eComp = new(0);
        }
    }
}
