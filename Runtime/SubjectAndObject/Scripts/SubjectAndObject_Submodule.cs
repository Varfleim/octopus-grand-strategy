
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
            #region PreInit
            //Создание объектов
            startup.PreInitSystem_Add(new S_Object_Creation());
            //Создание субъектов
            startup.PreInitSystem_Add(new S_Subject_Creation());
            #endregion
            #region PostInit
            //Очистка событий
            startup.PostInitSystem_Add(new S_Events_Clear());
            #endregion

            //Добавляем потиковые системы
            #region PreTick
            //Создание объектов
            startup.PreTickSystem_Add(new S_Object_Creation());
            //Создание субъектов
            startup.PreTickSystem_Add(new S_Subject_Creation());
            #endregion
            #region PostTick
            //Очистка событий
            startup.PostTickSystem_Add(new S_Events_Clear());
            #endregion
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(subjectAndObject_Data);
        }
    }
}