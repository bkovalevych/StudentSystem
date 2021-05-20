using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.ResourcesInitData
{
    using Helpers;
    using Entities;
    using System.IO;

    public class ContentPresenter
    {
        public List<Parent> Parents
        {
            get;
        } = new List<Parent>();
        public List<Student> Students
        {
            get;
        } = new List<Student>();
        public List<StudentParent> StudentParents
        {
            get;
        } = new List<StudentParent>();
        private Dictionary<string, string> streetsByFamily = new Dictionary<string, string>();
        public ContentPresenter()
        {
            InitStudents();
            InitParents();
            InitSudentParents();
        }
        private void InitParents()
        {
            int index = 0;
            Parents.Clear();
            
            rawParents.Split('\n')
                .ToList()
                .ForEach(rawName =>
                {
                    var names = rawName.Split(' ');
                    Parents.Add(new Parent()
                    {
                        FirstName = names[1],
                        SecondName = names[0],
                        ThirdName = names[2],
                        Job = names[3],
                        Gender = names[4],
                        PhoneNumber = $"380{index}0060{index}",
                        AdditionalInfo = "",
                        Address = streetsByFamily[names[0]],
                        Birthday = new DateTimeOffset(
                            new DateTime(
                                1985 - (index % 5),
                                11 - (index % 5),
                                27 - (index % 21), 0, 0, 0))
                    });
                    ++index;
                });
        }

        private void InitStudents()
        {
            int index = 0;
            Students.Clear();
            int indexAddress = 0;
            var streets = rawAddress.Split('\n');
            rawStudents.Split('\n')
                .ToList()
                .ForEach(rawName =>
                {
                    
                    var names = rawName.Split(' ');
                    if(!streetsByFamily.ContainsKey(names[0]))
                    {
                        streetsByFamily.Add(names[0], streets[indexAddress++]);
                    }
                    var st = new Student()
                    {
                        FirstName = names[1],
                        SecondName = names[0],
                        ThirdName = names[2],
                        Gender = names[3],
                        StudentCertificate = $"0000{index}",
                        ArmyCerificate = names[3] == "чоловік" ? $"000{index}0" : "",
                        BirthdayCertificate = $"0000{index}00",
                        IdentificationCode = $"0000{index}000",
                        PassportCode = $"123{index}0",
                        PhoneNumber = $"380{index}00505",
                        School = $"Школа № {100 + (index % 10)}",
                        SchoolCertificateCode = $"MT 000{index}012{index}",
                        AdditionalInfo = index % 10 == 0 ? "льгота" : "default",
                        AverageMarkInSchool = 12 - (index % 6),
                        Address = streetsByFamily[names[0]],
                        Birthday = new DateTimeOffset(
                            new DateTime(
                                2004 - (index % 4),
                                11 - (index % 6),
                                26 - (index % 21), 0, 0, 0))
                    };
                    Students.Add(st);
                    ++index;
                });
        }

        private void InitSudentParents()
        {
            StudentParents.Clear();
            Students.Join(Parents, s => s.SecondName, p => p.SecondName, 
                (s, p) => new StudentParent() 
                { 
                    Parent = p, 
                    Student = s, 
                    ParentId = p.ParentId, 
                    StudentId = s.StudentId 
                })
                .ToList()
                .ForEach(sp => StudentParents.Add(sp));
        }
        

        private string rawStudents =
@"Блажко Ус Тимурович чоловік
Касьяненко Дибач Тимурович чоловік
Навроцький Сонцедар Устимович чоловік
Навроцький Альберт Устимович чоловік
Навроцький Світ Устимович чоловік
Копитко Живосил Орестови чоловік
Брилинський Лук'ян Омелянович чоловік
Трощинський Живослав Полянович чоловік
Озаркевич Назарій Давидович чоловік
Палій Святояр Найденович чоловік
Зленко Дарибог Милославович чоловік
Ломиковський Любослава Янович жінка
Ломиковський Яволода Янович жінка
Свашенко Йосип Борисович чоловік
Міщенко Яросвіт Янович чоловік
Міщенко Надія Янович жінка
Грабовець Щастибог Світанович чоловік
Грабовець Густомисл Світанович чоловік
Могиленко Сологуба Федорівна жінка
Ніколайчук Красун Антонович чоловік
Долинський Подоляна Леонідович жінка
Долинський Стефаній Леонідович чоловік
Пащенко Силолюб Зорянович чоловік
Пащенко П'єр Зорянович чоловік
Царенко Єгора Зорянович жінка
Бурачинський Федір Омелянович чоловік
Бурачинський Честислав Омелянович чоловік
Гринько Олелько Олегович чоловік
Гринько Божейко Олегович чоловік
Сильченко Уличана Костянтинович жінка";
        private string rawParents =
@"Блажко Тимур Устимович бухгалтер чоловік
Блажко Олена Орестович вчитель жінка
Касьяненко Тимур Полянович викладач чоловік
Касьяненко Світлана Полянович програміст жінка
Навроцький Устим Леонідович агент чоловік
Навроцький Олена Петрівна офіцер жінка
Копитко Орест Гнатович держСлужбовець чоловік
Копитко Юлія Гнатович депутат жінка
Брилинський Омелян Кузьмич слідчий чоловік
Брилинський Просковья Кузьмич режисер жінка
Трощинський Полян Дрестович копач чоловік
Трощинський Гнатка Дрестович слюсар жінка
Озаркевич Давид Петрович менеджер чоловік
Озаркевич Ганна Петрович офіціант жінка
Палій Найден Лаврентійович прибиральник чоловік
Палій Дарина Лаврентійович доктор жінка
Зленко Милослав Давидович хірург чоловік
Зленко Рима Давидович педіатр жінка
Ломиковський Ян Назарович бізнесмен чоловік
Ломиковський Любов Янович політик жінка
Свашенко Борис Тарасович матДопомога чоловік
Міщенко Ян Любославович безробітний чоловік
Міщенко Яна Іллівна господарка жінка
Грабовець Світан Георгієвич фермер чоловік
Грабовець Світана Євгенівна господиня жінка
Ніколайчук Антон Олегович продавець чоловік
Долинський Леонід Андрійович касир чоловік
Долинський Зухра Матросівна співачка жінка
Пащенко Зорян Мстиславович тракторист чоловік
Пащенко Зоряна Пуерович водій жінка
Царенко Єгор Зорянович фермер чоловік
Бурачинський Омелян Олександрович бармен чоловік
Бурачинський Мелашка Едуардівна кухар жінка
Гринько Олег Сергійович лодМайстер чоловік
Гринько Наталія Петрівна вахтер жінка";
        private string rawAddress =
@"
Киевская Ул., дом 51
Володимира Великого Вул., дом 7, кв. 162
Антипова Ул., дом 1, кв. 77
Енгелса Ул., дом 97, кв. 4
Енгелса 2 Пер., дом 5
Маршала Рыбалко Ул., дом 7, кв. 30
Красногвардеыскиы Пр., дом 12, кв. 86
Юныкх Ленинтсев Ул., дом 31
Гагарина Ул., дом 26, кв. 56
Карбышева Ул., дом 28, кв. 52
Толстого Вул., дом 8
Бакха Ул., дом 13, кв. 8
Толстого Вул., дом 8
ТСиолковского Ул., дом 25
Заря Коммунизма Ул., дом 26
Антонича Вул., дом 8, кв. 22
Маркса К. Ул., дом 11, кв. 2
Панфилова Пр., дом 27, кв. 2
Глушко Академика Пр., дом 28, кв. 26
1 Конноы Армии Ул., дом 82, кв. 11
Селская Ул., дом 14
";
    }
}
