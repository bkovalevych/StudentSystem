using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace StudentSystem
{
    using Entities;
    using Helpers;
    using ViewModels;
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public BaseVM vm;
        public MainPage()
        {
            this.InitializeComponent();
            vm = new BaseVM(dtGrid.Columns);
            //AddData();
        }

        public void AddData() {
            using(StudentContext db = new StudentContext()) {
                List<Department> departments = new List<Department>() { 
                    new Department() {
                        DepartmentName = "Коип'ютерні науки",
                        ShortDepartmentName = "КН"
                    },
                    new Department() {
                        DepartmentName = "Інформаційні Технології Комп'ютерні Науки",
                        ShortDepartmentName = "ІТКН"
                    }
                };
                db.AddRange(departments);
                db.SaveChanges();

                List<Group> groups = new List<Group>();
                for (int course = 1; course <= 4; ++course) {
                    for (int groupNumber = 1; groupNumber <= 5; ++groupNumber) {
                        groups.Add(
                        new Group() {
                            Course = course,
                            CodeSpeciality = "121",
                            Department = departments[0],
                            GroupNumber = groupNumber,
                            ShortName = "ПЗПІ",
                            Name = "Програмна Інженерія",
                            StartYear = new DateTime(DateTime.Now.Year - course, 1, 1)
                        });
                        groups.Add(new Group() {
                            Course = course,
                            CodeSpeciality = "125",
                            Department = departments[0],
                            GroupNumber = groupNumber,
                            ShortName = "ШІ",
                            Name = "Штучний Інтелект",
                            StartYear = new DateTime(DateTime.Now.Year - course, 1, 1)
                        });
                        groups.Add(new Group() {
                            Course = course,
                            CodeSpeciality = "122",
                            Department = departments[1],
                            GroupNumber = groupNumber,
                            ShortName = "КН",
                            Name = "Комп'ютерні науки",
                            StartYear = new DateTime(DateTime.Now.Year - course, 1, 1)
                        });
                    }
                }
                db.AddRange(groups);
                db.SaveChanges();
                List<Parent> parents = new List<Parent>();
                for (int index = 0; index < 10; ++index) {
                    parents.Add(new Parent() {
                        AdditionalInfo = "default",
                        Address = $"address {index % 5}",
                        Birthday = new DateTime(1980 - index, index + 1, index + 1),
                        FirstName = $"ParentName{index}",
                        SecondName = $"ParentSecondName{index}",
                        ThirdName = $"ParentThirdName{index}",
                        Gender = Constants.Gender[index % 2],
                        Job = $"job {index}",
                        PhoneNumber = $"203405603{index}"
                    });
                }
                db.AddRange(parents);
                db.SaveChanges();

                List<Student> students = new List<Student>();
                for (int index = 0; index < 20; ++index) {
                    students.Add(new Student() {
                        AdditionalInfo = "default",
                        Address = $"address {index % 5}",
                        Birthday = new DateTime(1980 - index, (index % 12) + 1, index + 1),
                        FirstName = $"StudentName{index}",
                        SecondName = $"StudentSecondName{index}",
                        ThirdName = $"StudentThirdName{index}",
                        Gender = Constants.Gender[index % 2],
                        ArmyCerificate = (index % 2 == 0) ? "" : $"123456{index}",
                        PhoneNumber = $"203405603{index}",
                        AverageMarkInSchool = 5.0 + (index % 6),
                        BirthdayCertificate = $"2560{index}156{index}",
                        Group = groups[index % 10],
                        IdentificationCode = $"2560{index}000{index}",
                        EndStudy = groups[index % 10].StartYear.AddYears(5),
                        PassportCode = $"МТ260{index}020{index}3",
                        School = $"school{index}",
                        StartStudy = groups[index % 10].StartYear,
                        SchoolCertificateCode = $"2560{index}{index}123",
                        StudentCertificate = $"2560{index + 3}{index + 1}123"
                    });
                }
                db.AddRange(students);
                db.SaveChanges();
                List<StudentParent> studentParents = new List<StudentParent>();
                for (int index = 0; index < 20; ++index) {
                    studentParents.Add(new StudentParent() {
                        Parent = parents[index % 10],
                        Student = students[index]
                    });
                }
                db.AddRange(studentParents);
                db.SaveChanges();
            }
        }

        private async void MakeReport(object sender, RoutedEventArgs e) {
            //await ToExcel.Some(vm.Report);
        }

        private async void MakeGlobalReport(object sender, RoutedEventArgs e) {
            await ToExcel.Some(vm.Students);
        }
    }
}
