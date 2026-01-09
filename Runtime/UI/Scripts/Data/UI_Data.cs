
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_Data : MonoBehaviour
    {
        public static void ObjectPanel_Hide_R(
            EcsWorld world,
            EcsPool<R_ObjectPanel_Hide> r_P)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectPanel_Hide requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        public static void ObjectSubpanelTab_Show_R(
            EcsWorld world,
            EcsPool<R_ObjectSubpanelTab_Show> r_P,
            string objectSubpanelType, string objectSubpanelTabType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectSubpanelTab_Show requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectSubpanelType, objectSubpanelTabType,
                objectPE);
        }

        public static void OSbpT_Update_R(
            EcsPool<R_ObjectSubpanelTab_Update> r_P,
            int requestEntity,
            bool isSamePanel,
            bool isSameSubpanel,
            bool isSameTab,
            bool isSameObject)
        {
            //Назначаем сущности запрос
            ref R_ObjectSubpanelTab_Update requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isSamePanel,
                isSameSubpanel,
                isSameTab,
                isSameObject);
        }

        public static void ObjectMapPanel_Show_R(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Show> r_P,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectMapPanel_Show requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }

        public static void ObjectMapPanel_Hide_R(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Hide> r_P,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectMapPanel_Hide requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }
    }
}
