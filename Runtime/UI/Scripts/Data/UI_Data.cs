
using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_Data : MonoBehaviour
    {
        #region OutlinerPanel
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

        internal static void OutlinerPT_Update_R(
            EcsWorld world,
            EcsPool<R_OutlinerPanelTab_Update> r_P,
            bool isSameTab)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OutlinerPanelTab_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                isSameTab);
        }
        #endregion

        #region MainOverviewPanel
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
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_MainOverviewSubpanelTab_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                mOSbpType, mOSbpTType,
                entPE);
        }

        internal static void MOSbpT_Update_R(
            EcsWorld world,
            EcsPool<R_MainOverviewSubpanelTab_Update> r_P,
            bool isSamePanel,
            bool isSameSubpanel,
            bool isSameTab,
            bool isSameEnt)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_MainOverviewSubpanelTab_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                isSamePanel,
                isSameSubpanel,
                isSameTab,
                isSameEnt);
        }
        #endregion

        #region EntityPanel
        #region EntityScreenPanel
        public static void ESP_Show_R(
            EcsWorld world,
            EcsPool<R_EntityScreenPanel_Show> r_P,
            int entScreenPanelType,
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityScreenPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                entScreenPanelType,
                entPE);
        }

        public static void ESP_Hide_R(
            EcsWorld world,
            EcsPool<R_EntityScreenPanel_Hide> r_P,
            int entScreenPanelType,
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityScreenPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                entScreenPanelType,
                entPE);
        }

        internal static void ESP_Update_R(
            EcsWorld world,
            EcsPool<R_EntityScreenPanel_Update> r_P,
            int panelType,
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityScreenPanel_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                entPE);
        }

        public static void OutlinerEP_Show_R(
            EcsWorld world,
            EcsPool<R_EntityOutlinerPanel_Show> r_P,
            int outlinerPanelTabType,
            int entOutlinerPanelType, EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityOutlinerPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                outlinerPanelTabType,
                entOutlinerPanelType, entPE);
        }

        public static void MOEP_Show_R(
            EcsWorld world,
            EcsPool<R_EntityMainOverviewPanel_Show> r_P,
            int overviewSubpanelType, int overviewSubpanelTabType,
            int entMainOverviewPanelType, EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityMainOverviewPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                overviewSubpanelType, overviewSubpanelTabType,
                entMainOverviewPanelType, entPE);
        }
        #endregion

        #region EntityMapPanel
        public static void EMP_Show_R(
            EcsWorld world,
            EcsPool<R_EntityMapPanel_Show> r_P,
            int entMapPanelType,
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityMapPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                entMapPanelType,
                entPE);
        }

        public static void EMP_Hide_R(
            EcsWorld world,
            EcsPool<R_EntityMapPanel_Hide> r_P,
            int entMapPanelType,
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityMapPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                entMapPanelType,
                entPE);
        }

        internal static void EMP_Update_R(
            EcsWorld world,
            EcsPool<R_EntityMapPanel_Update> r_P,
            int panelType,
            EcsPackedEntity entPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_EntityMapPanel_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                entPE);
        }
        #endregion
        #endregion
    }
}
