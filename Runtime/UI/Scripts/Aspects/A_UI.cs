
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class A_UI : ProtoAspectInject
    {
        public ProtoPool<R_OverviewPanel_Show> oP_Show_R_P;
        public ProtoIt oP_Show_R_I = new(It.Inc<R_OverviewPanel_Show>());

        public ProtoPool<R_OverviewPanel_Update> oP_Update_R_P;
        public ProtoIt oP_Update_R_I = new(It.Inc<R_OverviewPanel_Update>());

        public ProtoPool<R_OverviewPanel_Hide> oP_Hide_R_P;
        public ProtoIt oP_Hide_R_I = new(It.Inc<R_OverviewPanel_Hide>());

        public ProtoPool<R_OverviewPanel_Content_Hide> oP_Content_Hide_R_P;
        public ProtoIt oP_Content_Hide_R_I = new(It.Inc<R_OverviewPanel_Content_Hide>());

        public ProtoPool<R_OverviewSubpanel_Show> oSbp_Show_R_P;
        public ProtoIt oSbp_Show_R_I = new(It.Inc<R_OverviewSubpanel_Show>());

        public ProtoPool<R_OverviewSubpanel_Hide> oSbp_Hide_R_P;
        public ProtoIt oSbp_Hide_R_I = new(It.Inc<R_OverviewSubpanel_Hide>());

        public ProtoPool<R_OverviewSubpanel_Creation> oSbp_Creation_R_P;
        public ProtoIt oSbp_Creation_R_I = new(It.Inc<R_OverviewSubpanel_Creation>());

        public ProtoPool<R_OverviewTab_Show> oT_Show_R_P;
        public ProtoIt oT_Show_R_I = new(It.Inc<R_OverviewTab_Show>());

        public ProtoPool<R_OverviewTab_Hide> oT_Hide_R_P;
        public ProtoIt oT_Hide_R_I = new(It.Inc<R_OverviewTab_Hide>());

        public ProtoPool<R_OverviewTab_Creation> oT_Creation_R_P;
        public ProtoIt oT_Creation_R_I = new(It.Inc<R_OverviewTab_Creation>());

        public ProtoPool<SR_Block_Update> b_Update_SR_P;

        public ProtoPool<C_BlockList> bL_P;

        public ProtoIt bL_Update_SR_I = new(It.Inc<C_BlockList, SR_Block_Update>());

        public ProtoPool<SR_BlockList_Creation> bL_Creation_SR_P;
        public ProtoIt bL_Creation_SR_I = new(It.Inc<SR_BlockList_Creation>());

        public ProtoPool<R_BlockList_Action> bL_Action_R_P;
        public ProtoIt bL_Action_R_I = new(It.Inc<R_BlockList_Action>());

        #region OverviewPanel
        public void OverviewP_Show_R(
            int panelType, bool activateContent)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewPanel_Show rComp = ref oP_Show_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                activateContent);
        }

        public void OverviewSbp_Show_R(
            int panelType,
            int subpanelType, bool activateContent)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewSubpanel_Show rComp = ref oSbp_Show_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType, activateContent);
        }

        public void OverviewT_Show_R(
            int panelType,
            int subpanelType,
            int tabType)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewTab_Show rComp = ref oT_Show_R_P.NewEntity(out ProtoEntity rEntity);

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
        /// <param name="panelType"></param>
        /// <param name="isPanelAlreadyActive"></param>
        /// <param name="isPanelContentAlreadyActive"></param>
        /// <param name="isSubpanelAlreadyActive"></param>
        /// <param name="isTabAlreadyActive"></param>
        public void OverviewP_Update_R(
            int panelType,
            bool isPanelAlreadyActive = false, bool isPanelContentAlreadyActive = false,
            bool isSubpanelAlreadyActive = false,
            bool isTabAlreadyActive = false)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewPanel_Update rComp = ref oP_Update_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                isPanelAlreadyActive, isPanelContentAlreadyActive,
                isSubpanelAlreadyActive,
                isTabAlreadyActive);
        }

        public void OverviewP_Hide_R(
            int panelType)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewPanel_Hide rComp = ref oP_Hide_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType);
        }

        public void OverviewP_Content_Hide_R(
            int panelType)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewPanel_Content_Hide rComp = ref oP_Content_Hide_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType);
        }

        public void OverviewSbp_Hide_R(
            int panelType,
            int subpanelType)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewSubpanel_Hide rComp = ref oSbp_Hide_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType);
        }

        public void OverviewT_Hide_R(
            int panelType,
            int subpanelType,
            int tabType)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_OverviewTab_Hide rComp = ref oT_Hide_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                panelType,
                subpanelType,
                tabType);
        }
        #endregion

        #region BlockPanel
        #region BlockList
        public void Block_Update_SR(
            ProtoEntity bEntity)
        {
            //Назначаем компонент запроса обновления, если его ещё нет
            ref SR_Block_Update rComp = ref b_Update_SR_P.Add(bEntity);
            rComp = new(0);
        }

        public ProtoEntity BlockList_Creation_SR(
            string parentPanelCode,
            string parentSubpanelCode,
            string parentTabCode)
        {
            //Назначаем переданной сущности запрос создания блока-списка
            ref SR_BlockList_Creation rComp = ref bL_Creation_SR_P.NewEntity(out ProtoEntity bEntity);

            //Заполняем данные запроса
            rComp = new(
                parentPanelCode,
                parentSubpanelCode,
                parentTabCode);

            return bEntity;
        }
        #endregion
        #endregion
    }
}
