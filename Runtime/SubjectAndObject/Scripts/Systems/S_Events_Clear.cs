
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.SubjectAndObject
{
    public class S_Events_Clear : GBB.VFSystem, IProtoInitSystem, IProtoRunSystem
    {
        [DI] A_SubjectAndObject subjectAndObject_A;

        public void Init(IProtoSystems systems)
        {
            //Очищаем события объектов и субъектов
            Object_Events_Clear();
            Subject_Events_Clear();
        }

        public void Run()
        {
            //Очищаем события объектов и субъектов
            Object_Events_Clear();
            Subject_Events_Clear();
        }

        void Object_Events_Clear()
        {
            //Для каждого события создания объекта
            foreach (ProtoEntity eEntity in subjectAndObject_A.obj_Created_SE_I)
            {
                //Удаляем компонент события
                subjectAndObject_A.obj_Created_SE_P.Del(eEntity);
            }
        }

        void Subject_Events_Clear()
        {
            //Для каждого события создания субъекта
            foreach (ProtoEntity eEntity in subjectAndObject_A.subj_Created_SE_I)
            {
                //Удаляем компонент события
                subjectAndObject_A.subj_Created_SE_P.Del(eEntity);
            }
        }
    }
}
