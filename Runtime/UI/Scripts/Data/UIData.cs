
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UIData : MonoBehaviour
    {
        public static void HideObjectPanelRequest(
            EcsWorld world,
            EcsPool<R_ObjectPanelHide> requestPool)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectPanelHide requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        public static void ShowObjectSubpanelTabRequest(
            EcsWorld world,
            EcsPool<R_ObjectSubpanelTabShow> requestPool,
            string objectSubpanelType, string objectSubpanelTabType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectSubpanelTabShow requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectSubpanelType, objectSubpanelTabType,
                objectPE);
        }

        public static void UpdateObjectSubpanelTabRequest(
            EcsPool<R_ObjectSubpanelTabUpdate> requestPool,
            int requestEntity,
            bool isSamePanel,
            bool isSameSubpanel,
            bool isSameTab,
            bool isSameObject)
        {
            //Назначаем сущности запрос
            ref R_ObjectSubpanelTabUpdate requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isSamePanel,
                isSameSubpanel,
                isSameTab,
                isSameObject);
        }

        public static void ShowObjectMapPanelRequest(
            EcsWorld world,
            EcsPool<R_ObjectMapPanelShow> requestPool,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectMapPanelShow requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }

        public static void HideObjectMapPanelRequest(
            EcsWorld world,
            EcsPool<R_ObjectMapPanelHide> requestPool,
            string objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ObjectMapPanelHide requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                objectMapPanelType,
                objectPE);
        }
    }
}
