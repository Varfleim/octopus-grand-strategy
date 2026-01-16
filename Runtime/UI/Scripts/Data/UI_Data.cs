
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_Data : MonoBehaviour
    {
        public static void MOP_Hide_R(
            EcsWorld world,
            EcsPool<R_MainOverviewPanel_Hide> r_P)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_MainOverviewPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(0);
        }

        public static void MOSbpT_Show_R(
            EcsWorld world,
            EcsPool<R_MainOverviewSubpanelTab_Show> r_P,
            int mOSbpType, int mOSbpTType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_MainOverviewSubpanelTab_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                mOSbpType, mOSbpTType,
                objectPE);
        }

        public static void MOSbpT_Update_R(
            EcsPool<R_MainOverviewSubpanelTab_Update> r_P,
            int rEntity,
            bool isSamePanel,
            bool isSameSubpanel,
            bool isSameTab,
            bool isSameObject)
        {
            //Назначаем сущности запрос
            ref R_MainOverviewSubpanelTab_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                isSamePanel,
                isSameSubpanel,
                isSameTab,
                isSameObject);
        }

        public static void OutlinerP_Hide_R(
            EcsWorld world,
            EcsPool<R_OutlinerPanel_Hide> r_P)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OutlinerPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(0);
        }

        public static void OutlinerPT_Show_R(
            EcsWorld world,
            EcsPool<R_OutlinerPanelTab_Show> r_P,
            int outlinerPTType)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OutlinerPanelTab_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                outlinerPTType);
        }

        public static void OutlinerPT_Update_R(
            EcsPool<R_OutlinerPanelTab_Update> r_P,
            int rEntity,
            bool isSameTab)
        {
            //Назначаем сущности запрос
            ref R_OutlinerPanelTab_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                isSameTab);
        }

        public static void OSP_Show_R(
            EcsWorld world,
            EcsPool<R_ObjectScreenPanel_Show> r_P,
            int objectScreenPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_ObjectScreenPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                objectScreenPanelType,
                objectPE);
        }

        public static void OSP_Hide_R(
            EcsWorld world,
            EcsPool<R_ObjectScreenPanel_Hide> r_P,
            int objectScreenPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_ObjectScreenPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                objectScreenPanelType,
                objectPE);
        }

        public static void OSP_Update_R(
            EcsWorld world,
            EcsPool<R_ObjectScreenPanel_Update> r_P,
            int panelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_ObjectScreenPanel_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                objectPE);
        }

        public static void OutlinerOP_Show_R(
            EcsPool<R_ObjectOutlinerPanel_Show> r_P,
            int rEntity,
            int outlinerPanelTabType,
            int objectOutlinerPanelType, EcsPackedEntity objectPE)
        {
            //Назначаем сущности запрос
            ref R_ObjectOutlinerPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                outlinerPanelTabType,
                objectOutlinerPanelType, objectPE);
        }

        public static void MOOP_Show_R(
            EcsPool<R_ObjectMainOverviewPanel_Show> r_P,
            int rEntity,
            int overviewSubpanelType, int overviewSubpanelTabType,
            int objectMainOverviewPanelType, EcsPackedEntity objectPE)
        {
            //Назначаем сущности запрос
            ref R_ObjectMainOverviewPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                overviewSubpanelType, overviewSubpanelTabType,
                objectMainOverviewPanelType, objectPE);
        }

        public static void OMP_Show_R(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Show> r_P,
            int objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_ObjectMapPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                objectMapPanelType,
                objectPE);
        }

        public static void OMP_Hide_R(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Hide> r_P,
            int objectMapPanelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_ObjectMapPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                objectMapPanelType,
                objectPE);
        }

        public static void OMP_Update_R(
            EcsWorld world,
            EcsPool<R_ObjectMapPanel_Update> r_P,
            int panelType,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_ObjectMapPanel_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                objectPE);
        }
    }
}
