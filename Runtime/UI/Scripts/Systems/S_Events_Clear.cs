
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_Events_Clear : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            //Очищаем события панели планировщика
            OutlinerP_Events_Clear();

            //Очищаем события главной обзорной панели
            MOP_Events_Clear();

            //Очищаем события панелей сущностей
            EPs_Events_Clear();
        }

        readonly EcsFilterInject<Inc<R_OutlinerPanel_Hide>> outlinerP_Hide_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanel_Hide> outlinerP_Hide_R_P = default;
        readonly EcsFilterInject<Inc<R_OutlinerPanelTab_Show>> outlinerPT_Show_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanelTab_Show> outlinerPT_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_OutlinerPanelTab_Update>> outlinerPT_Update_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanelTab_Update> outlinerPT_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_EntityOutlinerPanel_Show>> outlinerEP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityOutlinerPanel_Show> outlinerEP_Show_R_P = default;
        void OutlinerP_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса сокрытия панели планировщика
            foreach(int rEntity in outlinerP_Hide_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OutlinerPanel Hide!");

                outlinerP_Hide_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения вкладки панели планировщика
            foreach(int rEntity in outlinerPT_Show_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OutlinerPT Show!");

                outlinerPT_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления вкладки панели планировщика
            foreach (int rEntity in outlinerPT_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OutlinerPT Update!");

                outlinerPT_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения панели сущности в планировщике
            foreach (int rEntity in outlinerEP_Show_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("EntityOutlinerPanel Show!");

                outlinerEP_Show_R_P.Value.Del(rEntity);
            }
        }

        readonly EcsFilterInject<Inc<R_MainOverviewPanel_Hide>> mOP_Hide_R_F = default;
        readonly EcsPoolInject<R_MainOverviewPanel_Hide> mOP_Hide_R_P = default;
        readonly EcsFilterInject<Inc<R_MainOverviewSubpanelTab_Show>> mOPSbpT_Show_R_F = default;
        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Show> mOPSbpT_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_MainOverviewSubpanelTab_Update>> mOPSbpT_Update_R_F = default;
        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Update> mOPSbpT_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_EntityMainOverviewPanel_Show>> mOEP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityMainOverviewPanel_Show> mOEP_Show_R_P = default;
        void MOP_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса сокрытия главной обзорной панели
            foreach (int rEntity in mOP_Hide_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("MainOverviewPanel Hide!");

                mOP_Hide_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения вкладки главной обзорной панели
            foreach (int rEntity in mOPSbpT_Show_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("MOSbpT Show!");

                mOPSbpT_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления вкладки главной обзорной панели
            foreach (int rEntity in mOPSbpT_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("MOSbpT Update!");

                mOPSbpT_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения панели сущности в главной обзорной панели
            foreach (int rEntity in mOEP_Show_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("EntityMainOverviewPanel Hide!");

                mOEP_Show_R_P.Value.Del(rEntity);
            }
        }

        readonly EcsFilterInject<Inc<R_EntityScreenPanel_Show>> eSP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Show> eSP_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_EntityScreenPanel_Update>> eSP_Update_R_F = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Update> eSP_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_EntityScreenPanel_Hide>> eSP_Hide_R_F = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Hide> eSP_Hide_R_P = default;

        readonly EcsFilterInject<Inc<R_EntityMapPanel_Show>> eMP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityMapPanel_Show> eMP_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_EntityMapPanel_Update>> eMP_Update_R_F = default;
        readonly EcsPoolInject<R_EntityMapPanel_Update> eMP_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_EntityMapPanel_Hide>> eMP_Hide_R_F = default;
        readonly EcsPoolInject<R_EntityMapPanel_Hide> eMP_Hide_R_P = default;
        void EPs_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса отображения экранной панели сущности
            foreach(int rEntity in eSP_Show_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OSP Show!");

                eSP_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления экранной панели сущности
            foreach (int rEntity in eSP_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OSP Update!");

                eSP_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса сокрытия экранной панели сущности
            foreach (int rEntity in eSP_Hide_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OSP Hide!");

                eSP_Hide_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения панели карты сущности
            foreach (int rEntity in eMP_Show_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OMP Show!");

                eMP_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления панели карты сущности
            foreach (int rEntity in eMP_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OMP Update!");

                eMP_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса сокрытия панели карты сущности
            foreach (int rEntity in eMP_Hide_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OMP Hide!");

                eMP_Hide_R_P.Value.Del(rEntity);
            }
        }
    }
}
