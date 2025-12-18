
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_ObjectMapPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ObjectDisplayedMapPanels> objectDisplayedMapPanelsPool = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели карты объектов
            ObjectMapPanels_Show();

            //Скрываем панели карты объектов
            ObjectMapPanels_Hide();
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Show>> objectMapPanelShowRFilter = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Show> objectMapPanelShowRPool = default;
        void ObjectMapPanels_Show()
        {
            //Для каждого запроса отображения панели карты объекта
            foreach (int requestEntity in objectMapPanelShowRFilter.Value)
            {
                //Берём запрос
                ref R_ObjectMapPanel_Show requestComp = ref objectMapPanelShowRPool.Value.Get(requestEntity);

                //Если запрошенную панель требуется отобразить
                if (ObjectMapPanel_Exist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == false)
                {
                    //Отображаем панель
                    ObjectMapPanel_Show(ref requestComp);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где уже панель заполняется данными
            }
        }

        void ObjectMapPanel_Show(
            ref R_ObjectMapPanel_Show requestComp)
        {
            //Берём сущность объекта
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);

            //Если у объекта нет компонента панелей карты
            if(objectDisplayedMapPanelsPool.Value.Has(objectEntity) == false)
            {
                //Создаём его
                ObjectDisplayedMapPanels_Creation(objectEntity);
            }

            //Берём компонент панелей карты объекта
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

            //Создаём панель
            UIA_ObjectMapPanel.Panel_Instantiate(
                requestComp.objectPE,
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);
        }

        void ObjectDisplayedMapPanels_Creation(
            int objectEntity)
        {
            //Назначаем объекту компонент панелей карты и заполняем его данные
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Add(objectEntity);
            objectDisplayedMapPanels = new(0);
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Hide>> objectMapPanelHideRFilter = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Hide> objectMapPanelHideRPool = default;
        void ObjectMapPanels_Hide()
        {
            //Для каждого запроса сокрытия панели карты объекта
            foreach(int requestEntity in objectMapPanelHideRFilter.Value)
            {
                //Берём запрос
                ref R_ObjectMapPanel_Hide requestComp = ref objectMapPanelHideRPool.Value.Get(requestEntity);

                //Если запрошенная панель есть у объекта
                if(ObjectMapPanel_Exist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == true)
                {
                    //Скрываем её
                    ObjectMapPanel_Hide(ref requestComp);
                }

                //Удаляем запрос
                objectMapPanelHideRPool.Value.Del(requestEntity);
            }
        }

        void ObjectMapPanel_Hide(
            ref R_ObjectMapPanel_Hide requestComp)
        {
            //Берём сущность объекта и компонент панелей карты
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref objectDisplayedMapPanelsPool.Value.Get(objectEntity);

            //Кэшируем запрошенную панель
            UIA_ObjectMapPanel.Panel_Cache(
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);

            //Если у объекта больше нет панелей карты
            if(objectDisplayedMapPanels.objectMapPanels.Count == 0)
            {
                //Удаляем компонент панелей карты
                ObjectDisplayedMapPanels_Remove(objectEntity);
            }
        }

        void ObjectDisplayedMapPanels_Remove(
            int objectEntity)
        {
            //Удаляем компонент панелей карты
            objectDisplayedMapPanelsPool.Value.Del(objectEntity);
        }

        bool ObjectMapPanel_Exist(
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
