
using UnityEngine;

using GBB;

namespace GS.SubjectAndObject
{
    public class SubjectAndObject_Submodule : GameSubmodule
    {
        [SerializeField]
        private SubjectAndObject_Data subjectAndObject_Data;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region Init
            //Создание объектов
            startup.InitSystems_Add(
                System_New<S_Object_Creation>(SystemWeight.PreSystemWeight));
            //Создание субъектов
            startup.InitSystems_Add(
                System_New<S_Subject_Creation>(SystemWeight.PreSystemWeight));

            //Очистка событий
            startup.InitSystems_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем потиковые системы
            #region Tick
            //Создание объектов
            startup.TickSystems_Add(
                System_New<S_Object_Creation>(SystemWeight.PreSystemWeight));
            //Создание субъектов
            startup.TickSystems_Add(
                System_New<S_Subject_Creation>(SystemWeight.PreSystemWeight));

            //Очистка событий
            startup.TickSystems_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion
        }

        public override void Aspects_Add(
            GameStartup startup, 
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            A_SubjectAndObject subjectAndObject_A = new();
            parentAspect.childrenAspects.Add(subjectAndObject_A);
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(subjectAndObject_Data);
        }
    }
}