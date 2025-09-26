
using UnityEngine;

using GBB;

namespace GS.UI
{
    [CreateAssetMenu]
    public class UIModule : GameModule
    {
        public override void AddSystems(GameStartup startup)
        {
            //Добавляем покадровые системы
            #region Frame
            //Ввод в панели объекта
            startup.AddFrameSystem(new SObjectPanelInput());
            #endregion

            //Добавляем системы рендеринга
            #region PreRender
            //Отображение панели объекта
            startup.AddPreRenderSystem(new SObjectPanelControl());

            //Отображение панелей карты объектов
            startup.AddPreRenderSystem(new SObjectMapPanelControl());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Обновление панелей в окне игры в конце каждого тика
            startup.AddPostTickSystem(new S_GameWindowTickUpdate());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //Берём компонент данных UI
            UIData uIData = startup.GetComponentInChildren<UIData>();

            //Вводим данные
            startup.InjectData(uIData);

            //Берём главный объект интерфейса
            UICore uICore = uIData.uICore;

            //Вводим данные
            startup.InjectData(uICore);
        }
    }
}
