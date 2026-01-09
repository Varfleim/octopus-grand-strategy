
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GS.UI
{
    public class S_ObjectMapPanel_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ObjectDisplayedMapPanels> oDMP_P = default;

        public void Run(IEcsSystems systems)
        {
            //Отображаем панели карты объектов
            OMPs_Show();

            //Скрываем панели карты объектов
            OMPs_Hide();
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Show>> oMP_Show_R_F = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Show> oMP_Show_R_P = default;
        void OMPs_Show()
        {
            //Для каждого запроса отображения панели карты объекта
            foreach (int requestEntity in oMP_Show_R_F.Value)
            {
                //Берём запрос
                ref R_ObjectMapPanel_Show requestComp = ref oMP_Show_R_P.Value.Get(requestEntity);

                //Если запрошенную панель требуется отобразить
                if (OMP_Exist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == false)
                {
                    //Отображаем панель
                    OMP_Show(ref requestComp);
                }

                //Запрос передаётся дальше, переходя в модуль игры, где уже панель заполняется данными
            }
        }

        void OMP_Show(
            ref R_ObjectMapPanel_Show requestComp)
        {
            //Берём сущность объекта
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);

            //Если у объекта нет компонента панелей карты
            if(oDMP_P.Value.Has(objectEntity) == false)
            {
                //Создаём его
                ODMP_Creation(objectEntity);
            }

            //Берём компонент панелей карты объекта
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref oDMP_P.Value.Get(objectEntity);

            //Создаём панель
            UIA_ObjectMapPanel.Panel_Instantiate(
                requestComp.objectPE,
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);
        }

        void ODMP_Creation(
            int objectEntity)
        {
            //Назначаем объекту компонент панелей карты и заполняем его данные
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref oDMP_P.Value.Add(objectEntity);
            objectDisplayedMapPanels = new(0);
        }

        readonly EcsFilterInject<Inc<R_ObjectMapPanel_Hide>> oMP_Hide_R_F = default;
        readonly EcsPoolInject<R_ObjectMapPanel_Hide> oMP_Hide_R_P = default;
        void OMPs_Hide()
        {
            //Для каждого запроса сокрытия панели карты объекта
            foreach(int requestEntity in oMP_Hide_R_F.Value)
            {
                //Берём запрос
                ref R_ObjectMapPanel_Hide requestComp = ref oMP_Hide_R_P.Value.Get(requestEntity);

                //Если запрошенная панель есть у объекта
                if(OMP_Exist(
                    requestComp.objectPE,
                    requestComp.objectMapPanelType) == true)
                {
                    //Скрываем её
                    OMP_Hide(ref requestComp);
                }

                //Удаляем запрос
                oMP_Hide_R_P.Value.Del(requestEntity);
            }
        }

        void OMP_Hide(
            ref R_ObjectMapPanel_Hide requestComp)
        {
            //Берём сущность объекта и компонент панелей карты
            requestComp.objectPE.Unpack(world.Value, out int objectEntity);
            ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref oDMP_P.Value.Get(objectEntity);

            //Кэшируем запрошенную панель
            UIA_ObjectMapPanel.Panel_Cache(
                ref objectDisplayedMapPanels,
                requestComp.objectMapPanelType);

            //Если у объекта больше нет панелей карты
            if(objectDisplayedMapPanels.objectMapPanels.Count == 0)
            {
                //Удаляем компонент панелей карты
                ODMP_Remove(objectEntity);
            }
        }

        void ODMP_Remove(
            int objectEntity)
        {
            //Удаляем компонент панелей карты
            oDMP_P.Value.Del(objectEntity);
        }

        bool OMP_Exist(
            EcsPackedEntity objectPE,
            string objectMapPanelType)
        {
            //Берём сущность объекта
            objectPE.Unpack(world.Value, out int objectEntity);

            //Если у объекта есть компонент панелей карты
            if (oDMP_P.Value.Has(objectEntity) == true)
            {
                //Берём компонент
                ref C_ObjectDisplayedMapPanels objectDisplayedMapPanels = ref oDMP_P.Value.Get(objectEntity);

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
