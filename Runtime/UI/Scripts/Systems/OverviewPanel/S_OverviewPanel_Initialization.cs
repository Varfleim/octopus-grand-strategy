
using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_OverviewPanel_Initialization : GBB.VFSystem, IProtoInitSystem
    {
        [DI] UI_Data uI_Data;

        public void Init(IProtoSystems systems)
        {
            //Заносим обзорные панели в словарь
            OPs_Initialization();

            //ТЕСТ            
            //Создаём запрос
            //uI_A.OverviewP_Show_R(
            //    uI_Data.mainOverviewPanel.SelfType,
            //    true);

            //uI_A.OverviewP_Show_R(
            //    uI_Data.outlinerPanel.SelfType,
            //    true);
            //uI_A.OverviewP_Show_R(
            //    uI_Data.outlinerPanel.SelfType,
            //    true);

            //uI_A.OverviewP_Show_R(
            //    uI_Data.lensPanel.SelfType,
            //    true);
            //ТЕСТ

            //Создаём обзорные подпанели
            OSbps_Creation();

            //Создаём обзорные вкладки
            OTs_Creation();
        }

        void OPs_Initialization()
        {
            //Для каждой обзорной панели в списке
            for(int a = 0; a < uI_Data.oPsArray.Length; a++)
            {
                //Берём панель
                UIA_OverviewPanel oP = uI_Data.oPsArray[a];

                //Заносим панель в словари и сохраняем её индекс
                oP.SelfType = uI_Data.oPsCodeToIndexDict.Count;
                uI_Data.oPsCodeToIndexDict.Add(oP.selfCode, oP.SelfType);
                uI_Data.oPsIndexToObjectDict.Add(oP.SelfType, oP);
            }
        }

        void OSbps_Creation()
        {
            //Для каждого шаблона подпанели
            for (int a = 0; a < uI_Data.oSbpsTemplateArray.Length; a++)
            {
                //Берём шаблон
                ref readonly DL_OverviewSubpanel oSbpTemplate = ref uI_Data.oSbpsTemplateArray[a];

                //Создаём подпанель
                OSbp_Creation(in oSbpTemplate);
            }
        }

        void OSbp_Creation(
            in DL_OverviewSubpanel oSbpTemplate)
        {
            //Если панель с указанным кодом существует, берём её
            if(uI_Data.OP_GetByCode(oSbpTemplate.parentPanelCode, out UIA_OverviewPanel parentOP))
            {
                //Инстанциируем префаб подпанели и сразу прикрепляем к панели
                UIA_OverviewSubpanel oSbp = GameObject.Instantiate(parentOP.subpanelPrefab, parentOP.subpanelsGroup.transform);

                //Заносим подпанель в словари и сохраняем индекс
                oSbp.SelfType = uI_Data.oSbpsCodeToIndexDict.Count;
                uI_Data.oSbpsCodeToIndexDict.Add(oSbpTemplate.selfCode, oSbp.SelfType);
                uI_Data.oSbpsIndexToObjectDict.Add(oSbp.SelfType, oSbp);

                //Деактивируем подпанель после создания
                oSbp.RenderHide();

                //Если подпанель должна иметь кнопку
                if(oSbpTemplate.hasButton)
                {
                    //Инстанциируем префаб кнопки и сразу прикрепляем к группе кнопок
                    UIA_OverviewSubpanelButton oSbpButton = GameObject.Instantiate(parentOP.subpanelButtonPrefab, parentOP.buttonsGroup.transform);
                    oSbp.selfButton = oSbpButton;

                    //Заполняем индексы в кнопке
                    oSbpButton.PanelType = parentOP.SelfType;
                    oSbpButton.SubpanelType = oSbp.SelfType;

                    //ТЕСТ
                    //Заполняем название кнопки
                    oSbpButton.buttonNameText.text = oSbpTemplate.selfCode;
                    //ТЕСТ
                }
            }
            //Иначе сообщаем об её отсутствии
            else
            {
                Debug.LogError("Обзорная панель с указанным кодом не найдена!");
            }
        }

        void OTs_Creation()
        {
            //Для каждого шаблона панели
            for(int a = 0; a < uI_Data.oTsTemplateArray.Length; a++)
            {
                //Берём шаблон
                ref readonly DL_OverviewTab oTTemplate = ref uI_Data.oTsTemplateArray[a];

                //Создаём вкладку
                OT_Creation(in oTTemplate);
            }
        }

        void OT_Creation(
            in DL_OverviewTab oTTemplate)
        {
            //Если панель с указанным кодом существует, берём её
            if (uI_Data.OP_GetByCode(oTTemplate.parentPanelCode, out UIA_OverviewPanel parentOP))
            {
                //Если подпанель с указанным кодом существует, берём её
                if (uI_Data.OSbp_GetByCode(oTTemplate.parentSubpanelCode, out UIA_OverviewSubpanel parentOSbp))
                {
                    //Инстанциируем префаб вкладки и сразу прикрепляем к подпанели
                    UIA_OverviewTab oT = GameObject.Instantiate(parentOP.tabPrefab, parentOSbp.contentPanel.transform);

                    //Заносим вкладку в словари и сохраняем индекс
                    oT.SelfType = uI_Data.oTsCodeToIndexDict.Count;
                    uI_Data.oTsCodeToIndexDict.Add(oTTemplate.selfCode, oT.SelfType);
                    uI_Data.oTsIndexToObjectDict.Add(oT.SelfType, oT);

                    //Деактивируем вкладку после создания
                    oT.RenderHide();

                    //Инстанциируем префаб кнопки и сразу же прикрепляем к группе кнопок
                    UIA_OverviewTabButton oTButton = GameObject.Instantiate(parentOP.tabButtonPrefab, parentOSbp.buttonsGroup.transform);

                    //Заполняем индексы в кнопке
                    oTButton.PanelType = parentOP.SelfType;
                    oTButton.SubpanelType = parentOSbp.SelfType;
                    oTButton.TabType = oT.SelfType;

                    //ТЕСТ
                    //Заполняем название кнопки
                    oTButton.buttonNameText.text = oTTemplate.selfCode;
                    //ТЕСТ
                }
                //Иначе сообщаем об её отсутствии
                else
                {
                    Debug.LogError("Обзорная подпанель с указанным кодом не найдена!");
                }
            }
            //Иначе сообщаем об её отсутствии
            else
            {
                Debug.LogError("Обзорная панель с указанным кодом не найдена!");
            }
        }
    }
}
