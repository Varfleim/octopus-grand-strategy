
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GS.UI
{
    public abstract class UIA_ObjectMapPanel : MonoBehaviour
    {
        public static Dictionary<string, UIA_ObjectMapPanel> objectMapPanelPrefabs = new();
        public static Dictionary<string, List<UIA_ObjectMapPanel>> cachedObjectMapPanels = new();

        public EcsPackedEntity selfPE;

        public EcsPackedEntity parentProvincePE;

        public static void Panel_Cache(
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels,
            string objectMapPanelType)
        {
            //Берём панель
            UIA_ObjectMapPanel currentObjectMapPanel = objectDisplayedMapPanels.objectMapPanels[objectMapPanelType];

            //Заносим панель в список кэшированных
            cachedObjectMapPanels[objectMapPanelType].Add(currentObjectMapPanel);

            //Скрываем панель и обнуляем родительский объект
            currentObjectMapPanel.gameObject.SetActive(false);
            currentObjectMapPanel.transform.SetParent(null);

            //Удаляем ссылку на панель
            objectDisplayedMapPanels.objectMapPanels.Remove(objectMapPanelType);
        }

        public static void Panel_Instantiate(
            EcsPackedEntity objectPE,
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels,
            string objectMapPanelType)
        {
            //Создаём пустую переменную для панели
            UIA_ObjectMapPanel currentObjectMapPanel;

            //Если список кэшированных панелей не пуст, то берём кэшированную
            if(cachedObjectMapPanels[objectMapPanelType].Count > 0)
            {
                //Берём список кэшированных панелей
                List<UIA_ObjectMapPanel> cachedPanels = cachedObjectMapPanels[objectMapPanelType];

                //Берём последнюю панель в списке и удаляем её из списка
                currentObjectMapPanel = cachedPanels[cachedPanels.Count - 1];
                cachedPanels.RemoveAt(cachedPanels.Count - 1);

            }
            //Иначе
            else
            {
                //Создаём новую панель
                currentObjectMapPanel = Instantiate(objectMapPanelPrefabs[objectMapPanelType]);
            }

            //Обновляем основные данные панели
            currentObjectMapPanel.Panel_Update(objectPE);
            currentObjectMapPanel.parentProvincePE = new();

            //Отображаем новую панель
            currentObjectMapPanel.gameObject.SetActive(true);

            //Заносим её в словарь панелей объекта
            objectDisplayedMapPanels.objectMapPanels.Add(objectMapPanelType, currentObjectMapPanel);
        }

        public void Panel_Update(
            EcsPackedEntity objectPE)
        {
            selfPE = objectPE;
        }
    }
}
