
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class SObjectMapPanelControl : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ObjectDisplayedMapPanels> objectDisplayedMapPanelsPool = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели карты объектов
            ShowObjectMapPanel();

            //Скрываем панели карты объектов
            HideObjectMapPanel();
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanelShow>> objectMapPanelShowRFilter = default;
        readonly EcsPoolInject<R_ObjectMapPanelShow> objectMapPanelShowRPool = default;
        void ShowObjectMapPanel()
        {
            //Для каждого запроса отображения панели карты объекта
            foreach (int requestEntity in objectMapPanelShowRFilter.Value)
            {
                //Берём запрос
                ref R_ObjectMapPanelShow requestComp = ref objectMapPanelShowRPool.Value.Get(requestEntity);

                //Если запрошенную панель требуется отобразить
                if (ObjectMapPanelExist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == false)
                {
                    //Отображаем панель
                    ObjectMapPanelShow(ref requestComp);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где уже панель заполняется данными
            }
        }

        void ObjectMapPanelShow(
            ref R_ObjectMapPanelShow requestComp)
        {
            //Берём сущность объекта
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);

            //Если у объекта нет компонента панелей карты
            if(objectDisplayedMapPanelsPool.Value.Has(objectEntity) == false)
            {
                //Создаём его
                ObjectDisplayedMapPanelsCreate(objectEntity);
            }

            //Берём компонент панелей карты объекта
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

            //Создаём панель
            UIA_ObjectMapPanel.InstantiatePanel(
                requestComp.objectPE,
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);
        }

        void ObjectDisplayedMapPanelsCreate(
            int objectEntity)
        {
            //Назначаем объекту компонент панелей карты и заполняем его данные
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Add(objectEntity);
            objectDisplayedMapPanels = new(0);
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanelHide>> objectMapPanelHideRFilter = default;
        readonly EcsPoolInject<R_ObjectMapPanelHide> objectMapPanelHideRPool = default;
        void HideObjectMapPanel()
        {
            //Для каждого запроса сокрытия панели карты объекта
            foreach(int requestEntity in objectMapPanelHideRFilter.Value)
            {
                //Берём запрос
                ref R_ObjectMapPanelHide requestComp = ref objectMapPanelHideRPool.Value.Get(requestEntity);

                //Если запрошенная панель есть у объекта
                if(ObjectMapPanelExist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == true)
                {
                    //Скрываем её
                    ObjectMapPanelHide(ref requestComp);
                }

                //Удаляем запрос
                objectMapPanelHideRPool.Value.Del(requestEntity);
            }
        }

        void ObjectMapPanelHide(
            ref R_ObjectMapPanelHide requestComp)
        {
            //Берём сущность объекта и компонент панелей карты
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

            //Кэшируем запрошенную панель
            UIA_ObjectMapPanel.CachePanel(
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);

            //Если у объекта больше нет панелей карты
            if(objectDisplayedMapPanels.objectMapPanels.Count == 0)
            {
                //Удаляем компонент панелей карты
                ObjectDisplayedMapPanelsRemove(objectEntity);
            }
        }

        void ObjectDisplayedMapPanelsRemove(
            int objectEntity)
        {
            //Удаляем компонент панелей карты
            objectDisplayedMapPanelsPool.Value.Del(objectEntity);
        }

        bool ObjectMapPanelExist(
            EcsPackedEntity objectPE,
            string objectMapPanelType)
        {
            //Берём сущность объекта
            objectPE.Unpack(world.Value, out int objectEntity);

            //Если у объекта есть компонент панелей карты
            if (objectDisplayedMapPanelsPool.Value.Has(objectEntity) == true)
            {
                //Берём компонент
                ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

                //Если у объекта есть панель запрошенного типа
                if (objectDisplayedMapPanels.objectMapPanels.ContainsKey(objectMapPanelType) == true)
                {
                    //Запрошенная панель уже есть у объекта
                    return true;
                }
                //Иначе
                else
                {
                    //Запрошенная панель карты отсутствует
                    return false;
                }
            }
            //Иначе
            else
            {
                //Запрошенная панель карты точно отсутствует
                return false;
            }
        }
    }
}
