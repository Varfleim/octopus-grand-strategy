
using System.Collections.Generic;

namespace GS.UI
{
    public struct C_BlockList 
    {
        public C_BlockList(
            int a)
        {
            selfPanel = null;

            currentElements = new();

            //ТЕСТ
            comparer = new(0, false);
            //ТЕСТ
        }

        public UI_BlockList selfPanel;

        public List<D_BlockList_Element> currentElements;

        internal D_BlockList_ElementComparer comparer;

        public void List_Set(
            List<DT_BlockList_Element> tempElements)
        {
            //Если длина основного списка меньше переданного
            if(currentElements.Count < tempElements.Count)
            {
                //Для каждого недостающего элемента
                for(int a = currentElements.Count; a < tempElements.Count; a++)
                {
                    //Добавляем элемент к основному списку
                    currentElements.Add(new D_BlockList_Element());

                    UnityEngine.Debug.LogWarning("+++");
                }
            }

            //Для каждого переданного элемента
            for(int a = 0; a < tempElements.Count; a++)
            {
                //Переносим данные
                Element_Set(
                    currentElements[a],
                    tempElements[a]);
            }

            //Если длина переданного списка меньше длины основного
            if(tempElements.Count < currentElements.Count)
            {
                //Для каждого лишнего элемента в обратном порядке
                for(int a = currentElements.Count - 1; a >= tempElements.Count; a--)
                {
                    //Удаляем элемент из основного списка
                    currentElements.RemoveAt(a);

                    UnityEngine.Debug.LogWarning("---");
                }
            }
        }

        void Element_Set(
            D_BlockList_Element element,
            DT_BlockList_Element tempElement)
        {
            //Переносим данные
            element.elementName = tempElement.elementName;

            element.elementEntity = tempElement.elementEntity;

            //Если количество параметров в списке больше количества параметров
            if(element.elementValues.Count < tempElement.elementValues.Count)
            {
                //Для каждого недостающего параметра
                for(int a = element.elementValues.Count; a < tempElement.elementValues.Count; a++)
                {
                    //Добавляем новый параметр в список
                    element.elementValues.Add(new(-1, 0));
                }
            }

            //Для каждого параметра временного элемента
            for(int a = 0; a < tempElement.elementValues.Count; a++)
            {
                //Переносим данные
                element.elementValues[a] = tempElement.elementValues[a];
            }

            //Если количество параметров меньше количества параметров в списке
            if (tempElement.elementValues.Count < element.elementValues.Count)
            {
                //Для каждого лишнего элемента в обратном порядке
                for(int a = element.elementValues.Count - 1; a < tempElement.elementValues.Count; a--)
                {
                    //Удаляем параметр из списка
                    element.elementValues.RemoveAt(a);
                }
            }
        }
    }
}
