
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_OverviewPanel_Update : IProtoRunSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run()
        {
            //Обновляем панели
            OPs_Update();
        }

        void OPs_Update()
        {
            //Для каждого запроса обновления обзорной панели
            foreach(ProtoEntity rEntity in uI_A.oP_Update_R_I)
            {
                //Берём запрос
                ref R_OverviewPanel_Update rComp = ref uI_A.oP_Update_R_P.Get(rEntity);

                //Обновляем панель
                OP_Update(ref rComp);

                //Запрос передаётся дальше, переходя в модуль игры, где могут быть особые функции обновления

            }
        }

        void OP_Update(
            ref R_OverviewPanel_Update rComp)
        {
            //Берём соответствующую панель
            UIA_OverviewPanel overviewPanel = uI_Data.oPsIndexToObjectDict[rComp.panelType];

            //Если панель активна
            if(overviewPanel.gameObject.activeInHierarchy)
            {
                //Если есть активная подпанель, берём её
                if(overviewPanel.activeSubpanel != null
                    && overviewPanel.activeSubpanel.gameObject.activeInHierarchy)
                {
                    UIA_OverviewSubpanel overviewSubpanel = overviewPanel.activeSubpanel;

                    //Если есть активная вкладка, берём её
                    if(overviewSubpanel.activeTab != null
                        && overviewSubpanel.activeTab.gameObject.activeInHierarchy)
                    {
                        UIA_OverviewTab overviewTab = overviewSubpanel.activeTab;

                        //Для каждого блока
                        for(int a = 0; a < overviewTab.blockEntities.Count; a++)
                        {
                            //Запрашиваем обновление для сущности блока
                            uI_A.Block_Update_SR(overviewTab.blockEntities[a]);
                        }
                    }
                }
            }
        }
    }
}
