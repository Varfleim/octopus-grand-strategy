
using System;

using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GS.UI
{
    public class S_Block_Update : IProtoRunSystem
    {
        [DI] A_UI uI_A;

        [DI] UI_Data uI_Data;

        public void Run()
        {
            //Обновляем блоки-списки
            BlockLists_Update();
        }

        void BlockLists_Update()
        {
            //Для каждого запроса обновления блока-списка
            foreach(ProtoEntity blockEntity in uI_A.bL_Update_SR_I)
            {
                //Берём блок
                ref C_BlockList bL = ref uI_A.bL_P.Get(blockEntity);

                //Обновляем блок
                BlockList_Update(ref bL);

                uI_A.b_Update_SR_P.Del(blockEntity);
            }
        }

        void BlockList_Update(
            ref C_BlockList bL)
        {
            //Берём панель блока
            UI_BlockList selfPanel = bL.selfPanel;

            //Если количество панелей в списке меньше количества элементов
            if(selfPanel.panels.Count < bL.currentElements.Count)
            {
                //Для каждого недостающего элемента
                for(int a = selfPanel.panels.Count; a < bL.currentElements.Count; a++)
                {
                    //Инстанциируем новую панель и заносим её в список
                    selfPanel.panels.Add(BL_ElementPanel_Instantiate(selfPanel.layoutGroup.transform));
                }
            }

            //Для каждого переданного элемента
            for(int a = 0; a < bL.currentElements.Count; a++)
            {
                //Переносим данные в соответствующую панель
                BL_ElementPanel_SetData(
                    selfPanel.panels[a],
                    bL.currentElements[a]);
            }

            //Если количество элементов в списке меньше количества панелей
            if (bL.currentElements.Count < selfPanel.panels.Count)
            {
                //Для каждого лишнего элемента в обратном порядке
                for (int a = selfPanel.panels.Count - 1; a < bL.currentElements.Count; a--)
                {
                    //Кэшируем панель
                    BL_ElementPanel_Cache(selfPanel.panels[a]);

                    //Удаляем её из списка
                    selfPanel.panels.RemoveAt(a);
                }
            }
        }

        UI_BlockList_ElementPanel BL_ElementPanel_Instantiate(
            Transform parent)
        {
            //Создаём пустую переменную для панели
            UI_BlockList_ElementPanel elementPanel;

            //Если список кэшированных панелей не пуст, то берём кэшированную
            if(UI_BlockList_ElementPanel.cachedPanels.Count > 0)
            {
                //Берём последнюю панель в списке и удаляем её из списка
                elementPanel = UI_BlockList_ElementPanel.cachedPanels[UI_BlockList_ElementPanel.cachedPanels.Count - 1];
                UI_BlockList_ElementPanel.cachedPanels.RemoveAt(UI_BlockList_ElementPanel.cachedPanels.Count - 1);
            }
            else
            {
                //Создаём новую панель
                elementPanel = GameObject.Instantiate(uI_Data.blockListElementPanelPrefab);
            }

            //Прикрепляем панель к родителю и отображаем её
            elementPanel.transform.SetParent(parent);
            elementPanel.gameObject.SetActive(true);

            //Возвращаем панель
            return elementPanel;
        }

        void BL_ElementPanel_SetData(
            UI_BlockList_ElementPanel elementPanel,
            D_BlockList_Element element)
        {
            //Заносим данные элемента в панель
            elementPanel.elementName.text = element.elementName;

            elementPanel.elementEntity = element.elementEntity;

            //Заносим параметры элемента

            //Если количество панелей в списке меньше количества параметров
            if(elementPanel.elementValues.Count < element.elementValues.Count)
            {
                //Для каждого недостающего элемента
                for(int a = elementPanel.elementValues.Count; a < element.elementValues.Count; a++)
                {
                    //Инстанциируем новую панель и заносим её в список
                    elementPanel.elementValues.Add(BL_ElementValuePanel_Instantiate(elementPanel.layoutGroup.transform));
                }
            }

            //Для каждого параметра элемента
            for(int a = 0; a < element.elementValues.Count; a++)
            {
                //Переносим данные в соответствующую панель
                BL_ElementValuePanel_SetData(
                    elementPanel.elementValues[a],
                    element.elementValues[a]);
            }

            //Если количество параметров в списке меньше количества панелей
            if(element.elementValues.Count < elementPanel.elementValues.Count)
            {
                //Для каждого лишнего элемента в обратном порядке
                for (int a = elementPanel.elementValues.Count - 1; a < element.elementValues.Count; a--)
                {
                    //Кэшируем панель
                    BL_ElementValuePanel_Cache(elementPanel.elementValues[a]);

                    //Удаляем её из списка
                    elementPanel.elementValues.RemoveAt(a);
                }
            }
        }

        void BL_ElementPanel_Cache(
            UI_BlockList_ElementPanel elementPanel)
        {
            //Заносим панель в список кэшированных
            UI_BlockList_ElementPanel.cachedPanels.Add(elementPanel);

            //Открепляем её от родителя и скрываем
            elementPanel.transform.SetParent(null);
            elementPanel.gameObject.SetActive(false);
        }

        UI_BlockList_ElementValuePanel BL_ElementValuePanel_Instantiate(
            Transform parent)
        {
            //Создаём пустую переменную для панели
            UI_BlockList_ElementValuePanel elementValuePanel;

            //Если список кэшированных панелей не пуст, то берём кэшированную
            if(UI_BlockList_ElementValuePanel.cachedPanels.Count > 0)
            {
                //Берём последнюю панель в списке и удаляем её из списка
                elementValuePanel = UI_BlockList_ElementValuePanel.cachedPanels[UI_BlockList_ElementValuePanel.cachedPanels.Count - 1];
                UI_BlockList_ElementValuePanel.cachedPanels.RemoveAt(UI_BlockList_ElementValuePanel.cachedPanels.Count - 1);
            }
            else
            {
                //Создаём новую панель
                elementValuePanel = GameObject.Instantiate(uI_Data.blockListElementValuePanelPrefab);
            }

            //Прикрепляем панель к родителю и отображаем её
            elementValuePanel.transform.SetParent(parent);
            elementValuePanel.gameObject.SetActive(true);

            //Возвращаем панель
            return elementValuePanel;
        }

        void BL_ElementValuePanel_SetData(
            UI_BlockList_ElementValuePanel elementValuePanel,
            Tuple<int, float> value)
        {
            //Заносим данные в панель
            elementValuePanel.valueName.text = value.Item1.ToString();
            elementValuePanel.value.text = value.Item2.ToString();
        }

        void BL_ElementValuePanel_Cache(
            UI_BlockList_ElementValuePanel elementValuePanel)
        {
            //Заносим панель в список кэшированных
            UI_BlockList_ElementValuePanel.cachedPanels.Add(elementValuePanel);

            //Открепляем её от родителя и скрываем
            elementValuePanel.transform.SetParent(null);
            elementValuePanel.gameObject.SetActive(false);
        }
    }
}
