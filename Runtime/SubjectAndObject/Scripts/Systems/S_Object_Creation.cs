
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.SubjectAndObject
{
    public class S_Object_Creation : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            //Создаём объекты
            Objects_Creation();
        }

        public void Run(IEcsSystems systems)
        {
            //Создаём объекты
            Objects_Creation();
        }

        readonly EcsFilterInject<Inc<SR_Object_Creation>> object_Creation_SR_F = default;
        void Objects_Creation()
        {
            //Для каждого запроса создания объекта
            foreach (int objectRequestEntity in object_Creation_SR_F.Value)
            {
                //Берём запрос
                ref SR_Object_Creation rComp = ref object_Creation_SR_F.Pools.Inc1.Get(objectRequestEntity);

                //Создаём объект
                Object_Creation(
                    ref rComp,
                    objectRequestEntity);

                //Удаляем запрос
                object_Creation_SR_F.Pools.Inc1.Del(objectRequestEntity);
            }
        }

        readonly EcsPoolInject<C_Object> object_P = default;
        void Object_Creation(
            ref SR_Object_Creation rComp,
            int objectEntity)
        {
            //Назначаем сущность компонент объекта и заполняем его данные
            ref C_Object obj = ref object_P.Value.Add(objectEntity);
            obj = new(0);

            UnityEngine.Debug.LogWarning("Object Created!");

            //Создаём самособытие, сообщающее о создании объекта
            Object_Created_SE(
                objectEntity);
        }

        readonly EcsPoolInject<SE_Object_Created> object_Created_SE_P = default;
        void Object_Created_SE(
            int objectEntity)
        {
            //Назначаем сущности компонент события и заполняем его данные
            ref SE_Object_Created eComp = ref object_Created_SE_P.Value.Add(objectEntity);
            eComp = new(0);
        }
    }
}
