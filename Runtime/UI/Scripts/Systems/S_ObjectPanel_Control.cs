
using System.Collections.Generic;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_ObjectPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;

        readonly EcsPoolInject<C_ObjectDisplayedScreenPanels> oDSPs_P = default;
        readonly EcsPoolInject<C_ObjectDisplayedMapPanels> oDMPs_P = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем экранные панели объектов
            OSPs_Show();

            //Отображаем панели карты объектов
            OMPs_Show();

            //Скрываем экранные панели объектов
            OSPs_Hide();

            //Скрываем панели карты объектов
            OMPs_Hide();
        }

        readonly EcsFilterInject<Inc<R_ObjectScreenPanel_Show>> oSP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Show> oSP_Show_R_P = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Update> oSP_Update_R_P = default;
        void OSPs_Show()
        {
            //Для каждого запроса отображения экранной панели объекта
            foreach(int rEntity in oSP_Show_R_F.Value)
            {
                //Берём запрос и сущность объекта
                ref R_ObjectScreenPanel_Show rComp = ref oSP_Show_R_P.Value.Get(rEntity);
                rComp.objectPE.Unpack(world.Value, out int objectEntity);

                //Если запрошенную панель требуется отобразить
                if (OSP_Exist(
                    objectEntity,
                    rComp.panelType) == false) 
                {
                    //Отображаем панель
                    OSP_Show(
                        ref rComp,
                        objectEntity);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными

                //Запрашиваем обновление данных панели
                UI_Data.OSP_Update_R(
                    world.Value,
                    oSP_Update_R_P.Value,
                    rComp.panelType,
                    rComp.objectPE);
            }
        }

        void OSP_Show(
            ref R_ObjectScreenPanel_Show rComp,
            int objectEntity)
        {
            //Если у объекта нет компонента экранных панелей
            if(oDSPs_P.Value.Has(objectEntity) == false)
            {
                //Создаём его
                ODSPs_Creation(objectEntity);
            }

            //Берём компонент экранных панелей объекта
            ref C_ObjectDisplayedScreenPanels oDSPs = ref oDSPs_P.Value.Get(objectEntity);

            //Создаём панель
            UIA_ObjectScreenPanel objectPanel = OSP_Instantiate(
                rComp.objectPE,
                rComp.panelType);

            //Заносим панель в словарь панелей объекта
            oDSPs.objectPanels.Add(
                rComp.panelType,
                objectPanel);
        }

        readonly EcsFilterInject<Inc<R_ObjectScreenPanel_Hide>> oSP_Hide_R_F = default;
        readonly EcsPoolInject<R_ObjectScreenPanel_Hide> oSP_Hide_R_P = default;
        void OSPs_Hide()
        {
            //Для каждого запроса сокрытия экранной панели объекта
            foreach(int rEntity in oSP_Hide_R_F.Value)
            {
                //Берём запрос и сущность объекта
                ref R_ObjectScreenPanel_Hide rComp = ref oSP_Hide_R_P.Value.Get(rEntity);
                rComp.objectPE.Unpack(world.Value, out int objectEntity);

                //Если запрошенная панель есть у объекта
                if(OSP_Exist(
                    objectEntity,
                    rComp.panelType))
                {
                    //Скрываем её
                    OSP_Hide(
                        ref rComp,
                        objectEntity);
                }

                //Удаляем запрос
                oSP_Hide_R_P.Value.Del(rEntity);
            }
        }

        void OSP_Hide(
            ref R_ObjectScreenPanel_Hide rComp,
            int objectEntity)
        {
            //Берём компонент экранных панелей
            ref C_ObjectDisplayedScreenPanels oDSPs = ref oDSPs_P.Value.Get(objectEntity);

            //Кэшируем запрошенную панель
            OSP_Cache(
                ref oDSPs, rComp.panelType);

            //Если у объекта больше нет экранных панелей
            if (oDSPs.objectPanels.Count == 0)
            {
                //Удаляем компонент экранных панелей
                ODSPs_Remove(objectEntity);
            }
        }

        void ODSPs_Creation(
            int objectEntity)
        {
            //Назначаем объекту компонент экранных панелей и заполняем его данные
            ref C_ObjectDisplayedScreenPanels oDSPs = ref oDSPs_P.Value.Add(objectEntity);
            oDSPs = new(0);
        }

        void ODSPs_Remove(
            int objectEntity)
        {
            //Удаляем компонент экранных панелей
            oDSPs_P.Value.Del(objectEntity);
        }

        bool OSP_Exist(
            int objectEntity,
            int objectScreenPanelType)
        {
            //Если у объекта есть компонент экранных панелей 
            if(oDSPs_P.Value.Has(objectEntity))
            {
                //Берём компонент 
                ref C_ObjectDisplayedScreenPanels oDSPs = ref oDSPs_P.Value.Get(objectEntity);

                //Если у объекта есть панель запрошенного типа
                if(oDSPs.objectPanels.ContainsKey(objectScreenPanelType))
                {
                    //Запрошенная панель уже есть у объекта
                    return true;
                }
                else
                {
                    //Запрошенная панель отсутствует
                    return false;
                }
            }
            else
            {
                //Запрошенная панель отсутствует
                return false;
            }
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Show>> oMP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Show> oMP_Show_R_P = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Update> oMP_Update_R_P = default;
        void OMPs_Show()
        {
            //Для каждого запроса отображения панели карты объекта
            foreach (int rEntity in oMP_Show_R_F.Value)
            {
                //Берём запрос и сущность объекта
                ref R_ObjectMapPanel_Show rComp = ref oMP_Show_R_P.Value.Get(rEntity);
                rComp.objectPE.Unpack(world.Value, out int objectEntity);

                //Если запрошенную панель требуется отобразить
                if (OMP_Exist(
                    objectEntity,
                    rComp.panelType) == false)
                {
                    //Отображаем панель
                    OMP_Show(
                        ref rComp,
                        objectEntity);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными

                //Запрашиваем обновление данных панели
                UI_Data.OMP_Update_R(
                    world.Value,
                    oMP_Update_R_P.Value,
                    rComp.panelType,
                    rComp.objectPE);
            }
        }

        void OMP_Show(
            ref R_ObjectMapPanel_Show rComp,
            int objectEntity)
        {
            //Если у объекта нет компонента панелей карты
            if(oDMPs_P.Value.Has(objectEntity) == false)
            {
                //Создаём его
                ODMPs_Creation(objectEntity);
            }

            //Берём компонент панелей карты объекта
            ref C_ObjectDisplayedMapPanels oDMPs = ref oDMPs_P.Value.Get(objectEntity);

            //Создаём панель
            UIA_ObjectMapPanel objectPanel = OMP_Instantiate(
                rComp.objectPE,
                rComp.panelType);

            //Заносим панель в словарь панелей объекта
            oDMPs.objectPanels.Add(
                rComp.panelType,
                objectPanel);
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Hide>> oMP_Hide_R_F = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Hide> oMP_Hide_R_P = default;
        void OMPs_Hide()
        {
            //Для каждого запроса сокрытия панели карты объекта
            foreach (int rEntity in oMP_Hide_R_F.Value)
            {
                //Берём запрос и сущность объекта
                ref R_ObjectMapPanel_Hide rComp = ref oMP_Hide_R_P.Value.Get(rEntity);
                rComp.objectPE.Unpack(world.Value, out int objectEntity);

                //Если запрошенная панель есть у объекта
                if(OMP_Exist(
                    objectEntity,
                    rComp.panelType))
                {
                    //Скрываем её
                    OMP_Hide(
                        ref rComp,
                        objectEntity);
                }

                //Удаляем запрос
                oMP_Hide_R_P.Value.Del(rEntity);
            }
        }

        void OMP_Hide(
            ref R_ObjectMapPanel_Hide rComp,
            int objectEntity)
        {
            //Берём компонент панелей карты
            ref C_ObjectDisplayedMapPanels oDMPs = ref oDMPs_P.Value.Get(objectEntity);

            //Кэшируем запрошенную панель
            OMP_Cache(
                ref oDMPs, rComp.panelType);

            //Если у объекта больше нет панелей карты
            if (oDMPs.objectPanels.Count == 0)
            {
                //Удаляем компонент панелей карты
                ODMPs_Remove(objectEntity);
            }
        }

        void ODMPs_Creation(
            int objectEntity)
        {
            //Назначаем объекту компонент панелей карты и заполняем его данные
            ref C_ObjectDisplayedMapPanels oDMPs = ref oDMPs_P.Value.Add(objectEntity);
            oDMPs = new(0);
        }

        void ODMPs_Remove(
            int objectEntity)
        {
            //Удаляем компонент панелей карты
            oDMPs_P.Value.Del(objectEntity);
        }

        bool OMP_Exist(
            int objectEntity,
            int objectMapPanelType)
        {
            //Если у объекта есть компонент панелей карты
            if (oDMPs_P.Value.Has(objectEntity))
            {
                //Берём компонент 
                ref C_ObjectDisplayedMapPanels oDMPs = ref oDMPs_P.Value.Get(objectEntity);

                //Если у объекта есть панель запрошенного типа
                if (oDMPs.objectPanels.ContainsKey(objectMapPanelType))
                {
                    //Запрошенная панель уже есть у объекта
                    return true;
                }
                else
                {
                    //Запрошенная панель отсутствует
                    return false;
                }
            }
            else
            {
                //Запрошенная панель отсутствует
                return false;
            }
        }

        UIA_ObjectScreenPanel OSP_Instantiate(
            EcsPackedEntity objectPE,
            int objectScreenPanelType)
        {
            //Создаём панель
            UIA_ObjectPanel objectPanel = OP_Instantiate(
                objectPE,
                objectScreenPanelType);

            //Возвращаем её как экранную панель
            return objectPanel as UIA_ObjectScreenPanel;
        }

        UIA_ObjectMapPanel OMP_Instantiate(
            EcsPackedEntity objectPE,
            int objectMapPanelType)
        {
            //Создаём панель
            UIA_ObjectPanel objectPanel = OP_Instantiate(
                objectPE,
                objectMapPanelType);

            //Возвращаем её как экранную панель
            return objectPanel as UIA_ObjectMapPanel;
        }

        UIA_ObjectPanel OP_Instantiate(
            EcsPackedEntity objectPE,
            int panelType)
        {
            //Создаём пустую переменную для панели
            UIA_ObjectPanel objectPanel;

            //Если список кэшированных панелей не пуст, то берём кэшированную
            if (UIA_ObjectPanel.cachedObjectPanels[panelType].Count > 0)
            {
                //Берём список кэшированных панелей
                List<UIA_ObjectPanel> cachedPanels = UIA_ObjectPanel.cachedObjectPanels[panelType];

                //Берём последнюю панель в списке и удаляем её из списка
                objectPanel = cachedPanels[cachedPanels.Count - 1];
                cachedPanels.RemoveAt(cachedPanels.Count - 1);
            }
            else
            {
                //Создаём новую панель
                objectPanel = UnityEngine.GameObject.Instantiate(UIA_ObjectPanel.objectPanelPrefabs[panelType]);
            }

            //Сохраняем PE объекта
            objectPanel.selfPE = objectPE;

            //Отображаем панель
            objectPanel.gameObject.SetActive(true);

            //Возвращаем панель
            return objectPanel;
        }

        void OSP_Cache(
            ref C_ObjectDisplayedScreenPanels oDSPs,
            int objectScreenPanelType)
        {
            //Берём панель запрошенного типа
            UIA_ObjectScreenPanel objectPanel = oDSPs.objectPanels[objectScreenPanelType];

            //Кэшируем её
            OP_Cache(
                objectPanel, objectScreenPanelType);

            //Удаляем ссылку на панель
            oDSPs.objectPanels.Remove(objectScreenPanelType);
        }

        void OMP_Cache(
            ref C_ObjectDisplayedMapPanels oDMPs,
            int objectMapPanelType)
        {
            //Берём панель запрошенного типа
            UIA_ObjectMapPanel objectPanel = oDMPs.objectPanels[objectMapPanelType];

            //Кэшируем её
            OP_Cache(
                objectPanel, objectMapPanelType);

            //Удаляем ссылку на панель
            oDMPs.objectPanels.Remove(objectMapPanelType);
        }

        void OP_Cache(
            UIA_ObjectPanel objectPanel, int panelType)
        {
            //Заносим панель в список кэшированных
            UIA_ObjectPanel.cachedObjectPanels[panelType].Add(objectPanel);

            //Скрываем панель и обнуляем родительский объект
            objectPanel.gameObject.SetActive(false);
            objectPanel.transform.SetParent(null);
        }
    }
}
