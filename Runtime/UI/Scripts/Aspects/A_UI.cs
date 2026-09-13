
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class A_UI : ProtoAspectInject
    {
        internal ProtoWorld world;

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

        #region Blocks
        internal ProtoPool<C_Block> block_P;
        public ProtoPool<C_Block_Entities<int>> block_Int_P;
        public ProtoPool<C_Block_Entities<float>> block_Float_P;
        public ProtoPool<C_Block_Entities<string>> block_String_P;

        internal ProtoPool<SR_Block_Creation> block_Creation_SR_P;
        internal ProtoIt block_Creation_SR_I = new(It.Inc<SR_Block_Creation>());

        internal ProtoPool<SR_Block_Update> block_Update_SR_P;
        internal ProtoIt block_Update_SR_I = new(It.Inc<C_Block, SR_Block_Update>());

        internal ProtoPool<C_BlockEntityPanel> bEP_P;

        internal ProtoPool<SR_BlockEntityPanel_ClickAction> bEP_ClickAction_SR_P;
        internal ProtoIt bEP_ClickAction_SR_I = new(It.Inc<C_BlockEntityPanel, SR_BlockEntityPanel_ClickAction>());

        internal ProtoPool<C_DataLabel_Container> dLC_P;

        public ProtoPool<C_DataLabel> dL_P;
        public ProtoPool<C_DataLabel_Value<int>> dL_Int_P;
        public ProtoPool<C_DataLabel_Value<float>> dL_Float_P;
        public ProtoPool<C_DataLabel_Value<string>> dL_String_P;

        internal ProtoPool<SR_DataLabel_Creation> dL_Creation_SR_P;
        internal ProtoIt dL_Creation_SR_I = new(It.Inc<SR_DataLabel_Creation>());

        public ProtoPool<SR_DataLabel_Update> dL_Update_SR_P;

        internal ProtoPool<SR_DataLabel_Destroy> dL_Destroy_SR_P;
        #endregion

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

        #region Blocks
        public void Block_Creation_SR(
            string parentPanelCode,
            string parentSubpanelCode,
            string parentTabCode,
            int blockType)
        {
            //Назначаем переданной сущности запрос создания блока
            ref SR_Block_Creation rComp = ref block_Creation_SR_P.NewEntity();

            //Заполняем данные запроса
            rComp = new(
                parentPanelCode, parentSubpanelCode, parentTabCode,
                blockType);
        }

        public void Block_InterlayerComponentCreation<T>(
            ProtoEntity blockEntity,
            ProtoPool<T> interlayerComponent_P) where T : struct
        {
            //Назначаем сущности компонент соответствующего типа
            ref T interlayerComponent = ref interlayerComponent_P.Add(blockEntity);
        }

        public void DL_InterlayerComponentCreation<T>(
            ProtoEntity dLEntity,
            ProtoPool<T> interlayerComponent_P) where T : struct
        {
            //Назначаем сущности компонент соответствующего типа
            ref T interlayerComponent = ref interlayerComponent_P.Add(dLEntity);
        }
        #endregion
    }
}
