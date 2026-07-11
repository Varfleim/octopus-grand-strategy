
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.SubjectAndObject
{
    public class S_Events_Clear : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            //Очищаем события объектов и субъектов
            Object_Events_Clear();
            Subject_Events_Clear();
        }

        public void Run(IEcsSystems systems)
        {
            //Очищаем события объектов и субъектов
            Object_Events_Clear();
            Subject_Events_Clear();
        }

        readonly EcsFilterInject<Inc<SE_Object_Created>> object_Created_SE_F = default;
        void Object_Events_Clear()
        {
            //Для каждого события создания объекта
            foreach (int eEntity in object_Created_SE_F.Value)
            {
                //Удаляем компонент события
                object_Created_SE_F.Pools.Inc1.Del(eEntity);
            }
        }

        readonly EcsFilterInject<Inc<SE_Subject_Created>> subject_Created_SE_F = default;
        void Subject_Events_Clear()
        {
            //Для каждого события создания субъекта
            foreach (int eEntity in subject_Created_SE_F.Value)
            {
                //Удаляем компонент события
                subject_Created_SE_F.Pools.Inc1.Del(eEntity);
            }
        }
    }
}
