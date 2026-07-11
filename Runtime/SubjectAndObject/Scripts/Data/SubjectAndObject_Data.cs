
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.SubjectAndObject
{
    public class SubjectAndObject_Data : MonoBehaviour
    {
        public static void Object_Creation_SR(
            EcsPool<SR_Object_Creation> sR_P,
            int objEntity)
        {
            //Назначаем сущности запрос создания объекта и заполняем его данные
            ref SR_Object_Creation rComp = ref sR_P.Add(objEntity);
            rComp = new(0);
        }

        public static void Subject_Creation_SR(
            EcsPool<SR_Subject_Creation> sR_P,
            int subjEntity)
        {
            //Назначаем сущности запрос создания субъекта и заполняем его данные
            ref SR_Subject_Creation rComp = ref sR_P.Add(subjEntity);
            rComp = new(0);
        }
    }
}
