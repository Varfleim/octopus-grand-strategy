
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_Events_Clear : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            //Очищаем события обзорных панелей
            OverviewP_Events_Clear();

            //Очищаем события блоков
            Block_Events_Clear();
        }

        readonly EcsFilterInject<Inc<R_OverviewPanel_Show>> oP_Show_R_F = default;
        readonly EcsFilterInject<Inc<R_OverviewSubpanel_Show>> oSbp_Show_R_F = default;
        readonly EcsFilterInject<Inc<R_OverviewTab_Show>> oT_Show_R_F = default;

        readonly EcsFilterInject<Inc<R_OverviewPanel_Update>> oP_Update_R_F = default;

        readonly EcsFilterInject<Inc<R_OverviewPanel_Hide>> oP_Hide_R_F = default;
        readonly EcsFilterInject<Inc<R_OverviewPanel_Content_Hide>> oP_Content_Hide_R_F = default;
        readonly EcsFilterInject<Inc<R_OverviewSubpanel_Hide>> oSbp_Hide_R_F = default;
        readonly EcsFilterInject<Inc<R_OverviewTab_Hide>> oT_Hide_R_F = default;
        void OverviewP_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса отображения обзорной панели
            foreach (int rEntity in oP_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Show rComp = ref oP_Show_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewP Show! PanelID: " + rComp.panelType);

                oP_Show_R_F.Pools.Inc1.Del(rEntity);
            }

            //Для каждого запроса отображения обзорной подпанели
            foreach (int rEntity in oSbp_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Show rComp = ref oSbp_Show_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewSbp Show! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType);

                oSbp_Show_R_F.Pools.Inc1.Del(rEntity);
            }

            //Для каждого запроса отображения обзорной вкладки
            foreach (int rEntity in oT_Show_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewTab_Show rComp = ref oT_Show_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewT Show! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType 
                    + " TabID: " + rComp.tabType);

                oT_Show_R_F.Pools.Inc1.Del(rEntity);
            }


            //Для каждого запроса обновления обзорной панели
            foreach(int rEntity in oP_Update_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Update rComp = ref oP_Update_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning(
                    "OverviewP Update! PanelID: " + rComp.panelType 
                    + "\nPanelFullUpdate: " + !rComp.isPanelAlreadyActive + " PanelContentFullUpdate: " + !rComp.isPanelContentAlreadyActive 
                    + "\nSubpanelFullUpdate: " + !rComp.isSubpanelAlreadyActive
                    + "\nTabFullUpdate: " + !rComp.isTabAlreadyActive);

                oP_Update_R_F.Pools.Inc1.Del(rEntity);
            }


            //Для каждого запроса сокрытия обзорной панели
            foreach(int rEntity in oP_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Hide rComp = ref oP_Hide_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewP Hide! PanelID: " + rComp.panelType);

                oP_Hide_R_F.Pools.Inc1.Del(rEntity);
            }

            //Для каждого запроса сокрытия контента обзорной панели
            foreach (int rEntity in oP_Content_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewPanel_Content_Hide rComp = ref oP_Content_Hide_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewP Content Hide! PanelID: " + rComp.panelType);

                oP_Content_Hide_R_F.Pools.Inc1.Del(rEntity);
            }

            //Для каждого запроса сокрытия обзорной подпанели
            foreach (int rEntity in oSbp_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewSubpanel_Hide rComp = ref oSbp_Hide_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewSbp Hide! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType);

                oSbp_Hide_R_F.Pools.Inc1.Del(rEntity);
            }

            //Для каждого запроса сокрытия обзорной вкладки
            foreach (int rEntity in oT_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_OverviewTab_Hide rComp = ref oT_Hide_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("OverviewT Hide! PanelID: " + rComp.panelType 
                    + " SubpanelID: " + rComp.subpanelType 
                    + " TabID: " + rComp.tabType);

                oT_Hide_R_F.Pools.Inc1.Del(rEntity);
            }
        }

        readonly EcsFilterInject<Inc<R_BlockList_Action>> bL_Action_R_F = default;
        void Block_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса действия блока-списка
            foreach(int rEntity in bL_Action_R_F.Value)
            {
                //Берём запрос
                ref R_BlockList_Action rComp = ref bL_Action_R_F.Pools.Inc1.Get(rEntity);
                UnityEngine.Debug.LogWarning("BlockList Action!");

                bL_Action_R_F.Pools.Inc1.Del(rEntity);
            }
        }
    }
}
