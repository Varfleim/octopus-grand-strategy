
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UIData : MonoBehaviour
    {
        public static void ObjectPanel_HideRequest(
            EcsWorld world,
            EcsPool<R_ObjectPanel_Hide> requestPool)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectPanel_Hide requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        public static void ObjectSubpanelTab_ShowRequest(
            EcsWorld world,
            EcsPool<R_ObjectSubpanelTab_Show> requestPool,
            string objectSubpanelType, string objectSubpanelTabType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectSubpanelTab_Show requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectSubpanelType, objectSubpanelTabType,
                objectPE);
        }

        public static void ObjectSubpanelTab_UpdateRequest(
            EcsPool<R_ObjectSubpanelTab_Update> requestPool,
            int requestEntity,
            bool isSamePanel,
            bool isSameSubpanel,
            bool isSameTab,
            bool isSameObject)
        {
            //Назначаем сущности запрос
            ref R_ObjectSubpanelTab_Update requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isSamePanel,
                isSameSubpanel,
                isSameTab,
                isSameObject);
        }

        public static void ObjectMapPanel_ShowRequest(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Show> requestPool,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectMapPanel_Show requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }

        public static void ObjectMapPanel_HideRequest(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Hide> requestPool,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectMapPanel_Hide requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }
    }
}
