
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public class UI_Data : MonoBehaviour
    {
        [SerializeField]
        internal List<UIA_OverviewPanel> overviewPanelsList = new();
        [SerializeField]
        public UI_MainOverviewPanel mainOverviewPanel;
        [SerializeField]
        public UI_OutlinerPanel outlinerPanel;
        [SerializeField]
        public UI_LensPanel lensPanel;

        [SerializeField]
        public UI_BlockList blockListPrefab;
        [SerializeField]
        public UI_BlockList_ElementPanel blockListElementPanelPrefab;
        [SerializeField]
        public UI_BlockList_ElementValuePanel blockListElementValuePanelPrefab;

        #region OverviewPanel
        public static void OverviewP_Show_R(
            EcsWorld world,
            EcsPool<R_OverviewPanel_Show> r_P,
            int panelType, bool activateContent)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewPanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                activateContent);
        }

        public static void OverviewSbp_Show_R(
            EcsWorld world,
            EcsPool<R_OverviewSubpanel_Show> r_P,
            int panelType,
            int subpanelType, bool activateContent)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewSubpanel_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType, activateContent);
        }

        public static void OverviewT_Show_R(
            EcsWorld world,
            EcsPool<R_OverviewTab_Show> r_P,
            int panelType,
            int subpanelType,
            int tabType)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewTab_Show rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType,
                tabType);
        }

        /// <summary>
        /// Запрос обновления обзорной панели
        /// Если в булевых передаётся False, то производится полное обновление, как при открытии панели
        /// Если в булевых передаётся True, то производится малое обновление - только тех данных, которые могли измениться
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="panelType"></param>
        /// <param name="isPanelAlreadyActive"></param>
        /// <param name="isPanelContentAlreadyActive"></param>
        /// <param name="isSubpanelAlreadyActive"></param>
        /// <param name="isTabAlreadyActive"></param>
        public static void OverviewP_Update_R(
            EcsWorld world,
            EcsPool<R_OverviewPanel_Update> r_P,
            int panelType,
            bool isPanelAlreadyActive = false, bool isPanelContentAlreadyActive = false,
            bool isSubpanelAlreadyActive = false,
            bool isTabAlreadyActive = false)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewPanel_Update rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType, 
                isPanelAlreadyActive, isPanelContentAlreadyActive,
                isSubpanelAlreadyActive,
                isTabAlreadyActive);
        }

        public static void OverviewP_Hide_R(
            EcsWorld world,
            EcsPool<R_OverviewPanel_Hide> r_P,
            int panelType)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewPanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType);
        }

        public static void OverviewP_Content_Hide_R(
            EcsWorld world,
            EcsPool<R_OverviewPanel_Content_Hide> r_P,
            int panelType)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewPanel_Content_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType);
        }

        public static void OverviewSbp_Hide_R(
            EcsWorld world,
            EcsPool<R_OverviewSubpanel_Hide> r_P,
            int panelType,
            int subpanelType)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewSubpanel_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType);
        }

        public static void OverviewT_Hide_R(
            EcsWorld world,
            EcsPool<R_OverviewTab_Hide> r_P,
            int panelType,
            int subpanelType,
            int tabType)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_OverviewTab_Hide rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType,
                tabType);
        }
        #endregion

        #region BlockPanel
        #region BlockList
        public static void Block_Update_SR(
            int blockEntity,
            EcsPool<SR_Block_Update> r_P)
        {
            //Если у сущности ещё нет запроса обновления
            if(r_P.Has(blockEntity) == false)
            {
                //Назначаем переданной сущности запрос обновления блока
                ref SR_Block_Update rComp = ref r_P.Add(blockEntity);

                //Заполняем данные запроса
                rComp = new(0);
            }
        }

        public static void BlockList_Creation_SR(
            int blockEntity,
            EcsPool<SR_BlockList_Creation> r_P,
            int parentPanelType,
            int parentSubpanelType,
            int parentTabType)
        {
            //Назначаем переданной сущности запрос создания блока-списка
            ref SR_BlockList_Creation rComp = ref r_P.Add(blockEntity);

            //Заполняем данные запроса
            rComp = new(
                parentPanelType,
                parentSubpanelType,
                parentTabType);
        }
        #endregion
        #endregion
    }
}
