
using System.Collections.Generic;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_EntityPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;

        readonly EcsPoolInject<C_EntityDisplayedScreenPanels> eDSPs_P = default;
        readonly EcsPoolInject<C_EntityDisplayedMapPanels> eDMPs_P = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем экранные панели сущностей
            ESPs_Show();

            //Отображаем панели карты сущностей
            EMPs_Show();

            //Скрываем экранные панели сущностей
            ESPs_Hide();

            //Скрываем панели карты сущностей
            EMPs_Hide();
        }

        readonly EcsFilterInject<Inc<R_EntityScreenPanel_Show>> eSP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Show> eSP_Show_R_P = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Update> eSP_Update_R_P = default;
        void ESPs_Show()
        {
            //Для каждого запроса отображения экранной панели сущности
            foreach(int rEntity in eSP_Show_R_F.Value)
            {
                //Берём запрос и сущность
                ref R_EntityScreenPanel_Show rComp = ref eSP_Show_R_P.Value.Get(rEntity);
                rComp.entPE.Unpack(world.Value, out int entEntity);

                //Если запрошенную панель требуется отобразить
                if (ESP_Exist(
                    entEntity,
                    rComp.panelType) == false) 
                {
                    //Отображаем панель
                    ESP_Show(
                        ref rComp,
                        entEntity);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными

                //Запрашиваем обновление данных панели
                UI_Data.ESP_Update_R(
                    world.Value,
                    eSP_Update_R_P.Value,
                    rComp.panelType,
                    rComp.entPE);
            }
        }

        void ESP_Show(
            ref R_EntityScreenPanel_Show rComp,
            int entEntity)
        {
            //Если у сущности нет компонента экранных панелей
            if(eDSPs_P.Value.Has(entEntity) == false)
            {
                //Создаём его
                EDSPs_Creation(entEntity);
            }

            //Берём компонент экранных панелей сущности
            ref C_EntityDisplayedScreenPanels oDSPs = ref eDSPs_P.Value.Get(entEntity);

            //Создаём панель
            UIA_EntityScreenPanel entPanel = ESP_Instantiate(
                rComp.entPE,
                rComp.panelType);

            //Заносим панель в словарь панелей сущностей
            oDSPs.entPanels.Add(
                rComp.panelType,
                entPanel);
        }

        readonly EcsFilterInject<Inc<R_EntityScreenPanel_Hide>> eSP_Hide_R_F = default;
        readonly EcsPoolInject<R_EntityScreenPanel_Hide> eSP_Hide_R_P = default;
        void ESPs_Hide()
        {
            //Для каждого запроса сокрытия экранной панели сущности
            foreach(int rEntity in eSP_Hide_R_F.Value)
            {
                //Берём запрос и сущность
                ref R_EntityScreenPanel_Hide rComp = ref eSP_Hide_R_P.Value.Get(rEntity);
                rComp.entPE.Unpack(world.Value, out int entEntity);

                //Если запрошенная панель есть у сущности
                if(ESP_Exist(
                    entEntity,
                    rComp.panelType))
                {
                    //Скрываем её
                    ESP_Hide(
                        ref rComp,
                        entEntity);
                }

                //Запрос передаётся дальше, в модуль игры, где могут быть особые функции сокрытия
            }
        }

        void ESP_Hide(
            ref R_EntityScreenPanel_Hide rComp,
            int entEntity)
        {
            //Берём компонент экранных панелей
            ref C_EntityDisplayedScreenPanels oDSPs = ref eDSPs_P.Value.Get(entEntity);

            //Кэшируем запрошенную панель
            ESP_Cache(
                ref oDSPs, rComp.panelType);

            //Если у сущности больше нет экранных панелей
            if (oDSPs.entPanels.Count == 0)
            {
                //Удаляем компонент экранных панелей
                EDSPs_Remove(entEntity);
            }
        }

        void EDSPs_Creation(
            int entEntity)
        {
            //Назначаем сущности компонент экранных панелей и заполняем его данные
            ref C_EntityDisplayedScreenPanels oDSPs = ref eDSPs_P.Value.Add(entEntity);
            oDSPs = new(0);
        }

        void EDSPs_Remove(
            int entEntity)
        {
            //Удаляем компонент экранных панелей
            eDSPs_P.Value.Del(entEntity);
        }

        bool ESP_Exist(
            int entEntity,
            int entScreenPanelType)
        {
            //Если у сущности есть компонент экранных панелей 
            if (eDSPs_P.Value.Has(entEntity))
            {
                //Берём компонент 
                ref C_EntityDisplayedScreenPanels oDSPs = ref eDSPs_P.Value.Get(entEntity);

                //Если у сущности есть панель запрошенного типа
                if (oDSPs.entPanels.ContainsKey(entScreenPanelType))
                {
                    //Запрошенная панель уже есть у сущности
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

        readonly EcsFilterInject<Inc<R_EntityMapPanel_Show>> eMP_Show_R_F = default;
        readonly EcsPoolInject<R_EntityMapPanel_Show> eMP_Show_R_P = default;
        readonly EcsPoolInject<R_EntityMapPanel_Update> eMP_Update_R_P = default;
        void EMPs_Show()
        {
            //Для каждого запроса отображения панели карты сущности
            foreach (int rEntity in eMP_Show_R_F.Value)
            {
                //Берём запрос и сущность
                ref R_EntityMapPanel_Show rComp = ref eMP_Show_R_P.Value.Get(rEntity);
                rComp.entPE.Unpack(world.Value, out int entEntity);

                //Если запрошенную панель требуется отобразить
                if (EMP_Exist(
                    entEntity,
                    rComp.panelType) == false)
                {
                    //Отображаем панель
                    EMP_Show(
                        ref rComp,
                        entEntity);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где панель уже заполняется данными

                //Запрашиваем обновление данных панели
                UI_Data.EMP_Update_R(
                    world.Value,
                    eMP_Update_R_P.Value,
                    rComp.panelType,
                    rComp.entPE);
            }
        }

        void EMP_Show(
            ref R_EntityMapPanel_Show rComp,
            int entEntity)
        {
            //Если у сущности нет компонента панелей карты
            if (eDMPs_P.Value.Has(entEntity) == false)
            {
                //Создаём его
                EDMPs_Creation(entEntity);
            }

            //Берём компонент панелей карты сущности
            ref C_EntityDisplayedMapPanels oDMPs = ref eDMPs_P.Value.Get(entEntity);

            //Создаём панель
            UIA_EntityMapPanel entPanel = EMP_Instantiate(
                rComp.entPE,
                rComp.panelType);

            //Заносим панель в словарь панелей сущности
            oDMPs.entPanels.Add(
                rComp.panelType,
                entPanel);
        }

        readonly EcsFilterInject<Inc<R_EntityMapPanel_Hide>> eMP_Hide_R_F = default;
        readonly EcsPoolInject<R_EntityMapPanel_Hide> eMP_Hide_R_P = default;
        void EMPs_Hide()
        {
            //Для каждого запроса сокрытия панели карты сущности
            foreach (int rEntity in eMP_Hide_R_F.Value)
            {
                //Берём запрос и сущность
                ref R_EntityMapPanel_Hide rComp = ref eMP_Hide_R_P.Value.Get(rEntity);
                rComp.entPE.Unpack(world.Value, out int entEntity);

                //Если запрошенная панель есть у сущности
                if (EMP_Exist(
                    entEntity,
                    rComp.panelType))
                {
                    //Скрываем её
                    EMP_Hide(
                        ref rComp,
                        entEntity);
                }

                //Запрос передаётся дальше, в модуль игры, где могут быть особые функции сокрытия
            }
        }

        void EMP_Hide(
            ref R_EntityMapPanel_Hide rComp,
            int entEntity)
        {
            //Берём компонент панелей карты
            ref C_EntityDisplayedMapPanels oDMPs = ref eDMPs_P.Value.Get(entEntity);

            //Кэшируем запрошенную панель
            EMP_Cache(
                ref oDMPs, rComp.panelType);

            //Если у сущности больше нет панелей карты
            if (oDMPs.entPanels.Count == 0)
            {
                //Удаляем компонент панелей карты
                EDMPs_Remove(entEntity);
            }
        }

        void EDMPs_Creation(
            int entEntity)
        {
            //Назначаем сущности компонент панелей карты и заполняем его данные
            ref C_EntityDisplayedMapPanels oDMPs = ref eDMPs_P.Value.Add(entEntity);
            oDMPs = new(0);
        }

        void EDMPs_Remove(
            int entEntity)
        {
            //Удаляем компонент панелей карты
            eDMPs_P.Value.Del(entEntity);
        }

        bool EMP_Exist(
            int entEntity,
            int entMapPanelType)
        {
            //Если у сущности есть компонент панелей карты
            if (eDMPs_P.Value.Has(entEntity))
            {
                //Берём компонент 
                ref C_EntityDisplayedMapPanels oDMPs = ref eDMPs_P.Value.Get(entEntity);

                //Если у сущности есть панель запрошенного типа
                if (oDMPs.entPanels.ContainsKey(entMapPanelType))
                {
                    //Запрошенная панель уже есть у сущности
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

        UIA_EntityScreenPanel ESP_Instantiate(
            EcsPackedEntity entPE,
            int entScreenPanelType)
        {
            //Создаём панель
            UIA_EntityPanel entPanel = EP_Instantiate(
                entPE,
                entScreenPanelType);

            //Возвращаем её как экранную панель
            return entPanel as UIA_EntityScreenPanel;
        }

        UIA_EntityMapPanel EMP_Instantiate(
            EcsPackedEntity entPE,
            int entMapPanelType)
        {
            //Создаём панель
            UIA_EntityPanel entPanel = EP_Instantiate(
                entPE,
                entMapPanelType);

            //Возвращаем её как экранную панель
            return entPanel as UIA_EntityMapPanel;
        }

        UIA_EntityPanel EP_Instantiate(
            EcsPackedEntity entPE,
            int panelType)
        {
            //Создаём пустую переменную для панели
            UIA_EntityPanel entPanel;

            //Если список кэшированных панелей не пуст, то берём кэшированную
            if (UIA_EntityPanel.cachedEntPanels[panelType].Count > 0)
            {
                //Берём список кэшированных панелей
                List<UIA_EntityPanel> cachedPanels = UIA_EntityPanel.cachedEntPanels[panelType];

                //Берём последнюю панель в списке и удаляем её из списка
                entPanel = cachedPanels[cachedPanels.Count - 1];
                cachedPanels.RemoveAt(cachedPanels.Count - 1);
            }
            else
            {
                //Создаём новую панель
                entPanel = UnityEngine.GameObject.Instantiate(UIA_EntityPanel.entPanelPrefabs[panelType]);
            }

            //Сохраняем PE сущности
            entPanel.selfPE = entPE;

            //Отображаем панель
            entPanel.gameObject.SetActive(true);

            //Возвращаем панель
            return entPanel;
        }

        void ESP_Cache(
            ref C_EntityDisplayedScreenPanels oDSPs,
            int entScreenPanelType)
        {
            //Берём панель запрошенного типа
            UIA_EntityScreenPanel entPanel = oDSPs.entPanels[entScreenPanelType];

            //Кэшируем её
            EP_Cache(
                entPanel, entScreenPanelType);

            //Удаляем ссылку на панель
            oDSPs.entPanels.Remove(entScreenPanelType);
        }

        void EMP_Cache(
            ref C_EntityDisplayedMapPanels oDMPs,
            int entMapPanelType)
        {
            //Берём панель запрошенного типа
            UIA_EntityMapPanel entPanel = oDMPs.entPanels[entMapPanelType];

            //Кэшируем её
            EP_Cache(
                entPanel, entMapPanelType);

            //Удаляем ссылку на панель
            oDMPs.entPanels.Remove(entMapPanelType);
        }

        void EP_Cache(
            UIA_EntityPanel entPanel, int panelType)
        {
            //Заносим панель в список кэшированных
            UIA_EntityPanel.cachedEntPanels[panelType].Add(entPanel);

            //Скрываем панель и обнуляем родительскую сущность
            entPanel.gameObject.SetActive(false);
            entPanel.transform.SetParent(null);
        }
    }
}
