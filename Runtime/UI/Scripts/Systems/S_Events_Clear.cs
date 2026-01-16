
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

            //Очищаем события панелей объектов
            OPs_Events_Clear();
        }

        readonly EcsFilterInject<Inc<R_OutlinerPanelTab_Show>> outlinerPT_Show_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanelTab_Show> outlinerPT_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_OutlinerPanelTab_Update>> outlinerPT_Update_R_F = default;
        readonly EcsPoolInject<R_OutlinerPanelTab_Update> outlinerPT_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_ObjectOutlinerPanel_Show>> outlinerOP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectOutlinerPanel_Show> outlinerOP_Show_R_P = default;
        void OutlinerP_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса отображения вкладки панели планировщика
            foreach(int rEntity in outlinerPT_Show_R_F.Value)
            {
                //Удаляем компонент запроса
                outlinerPT_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления вкладки панели планировщика
            foreach (int rEntity in outlinerPT_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OutlinerPT Update!");

                //Удаляем компонент запроса
                outlinerPT_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения панели объекта в планировщике
            foreach(int rEntity in outlinerOP_Show_R_F.Value)
            {
                //Удаляем компонент запроса
                outlinerOP_Show_R_P.Value.Del(rEntity);
            }
        }

        readonly EcsFilterInject<Inc<R_MainOverviewSubpanelTab_Show>> mOPSbpT_Show_R_F = default;
        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Show> mOPSbpT_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_MainOverviewSubpanelTab_Update>> mOPSbpT_Update_R_F = default;
        readonly EcsPoolInject<R_MainOverviewSubpanelTab_Update> mOPSbpT_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_ObjectMainOverviewPanel_Show>> mOOP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectMainOverviewPanel_Show> mOOP_Show_R_P = default;
        void MOP_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса отображения вкладки главной обзорной панели
            foreach (int rEntity in mOPSbpT_Show_R_F.Value)
            {
                //Удаляем компонент запроса
                mOPSbpT_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления вкладки главной обзорной панели
            foreach (int rEntity in mOPSbpT_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("MOSbpT Update!");

                //Удаляем компонент запроса
                mOPSbpT_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения панели объекта в главной обзорной панели
            foreach (int rEntity in mOOP_Show_R_F.Value)
            {
                //Удаляем компонент запроса
                mOOP_Show_R_P.Value.Del(rEntity);
            }
        }

        readonly EcsFilterInject<Inc<R_ObjectScreenPanel_Show>> oSP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Show> oSP_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_ObjectScreenPanel_Update>> oSP_Update_R_F = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Update> oSP_Update_R_P = default;
        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Show>> oMP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Show> oMP_Show_R_P = default;
        readonly EcsFilterInject<Inc<R_ObjectScreenPanel_Update>> oMP_Update_R_F = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Update> oMP_Update_R_P = default;
        void OPs_Events_Clear()
        {
            //Очищаем события, которые не были удалены в GameUI

            //Для каждого запроса отображения экранной панели объекта
            foreach(int rEntity in oSP_Show_R_F.Value)
            {
                //Удаляем компонент запроса
                oSP_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления экранной панели объекта
            foreach (int rEntity in oSP_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OSP Update!");

                //Удаляем компонент запроса
                oSP_Update_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса отображения панели карты объекта
            foreach (int rEntity in oMP_Show_R_F.Value)
            {
                //Удаляем компонент запроса
                oMP_Show_R_P.Value.Del(rEntity);
            }

            //Для каждого запроса обновления панели карты объекта
            foreach (int rEntity in oMP_Update_R_F.Value)
            {
                UnityEngine.Debug.LogWarning("OMP Update!");

                //Удаляем компонент запроса
                oMP_Update_R_P.Value.Del(rEntity);
            }
        }
    }
}
