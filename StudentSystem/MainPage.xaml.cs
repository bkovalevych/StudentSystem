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
    using Windows.UI.Popups;
    using ResourcesInitData;
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public BaseVM vm;
        public GroupingViewModel grouping;
        public MainPage()
        {
            this.InitializeComponent();
            //DeleteAll();
            //AddData();
            vm = new BaseVM(dtGrid.Columns, lowLevel.Columns);
            vm.RefreshCommand.Execute(null);
            grouping = new GroupingViewModel(() => vm.Students);
        }

        public void DeleteAll()
        {
            using(var db = new StudentContext())
            {
                db.RemoveRange(db.Students);
                db.RemoveRange(db.StudentParents);
                db.RemoveRange(db.Parents);
                db.RemoveRange(db.Groups);
                db.RemoveRange(db.Departments);
                db.SaveChanges();
            }
        }

        public void AddData() {
            using(StudentContext db = new StudentContext()) {
                List<Department> departments = new List<Department>() { 
                    new Department() {
                        DepartmentName = "Комп'ютерні науки",
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
                for (int course = 1; course <= 2; ++course) {
                    for (int groupNumber = 1; groupNumber <= 2; ++groupNumber) {
                        groups.Add(
                        new Group() {
                            Course = course,
                            CodeSpeciality = "121",
                            Department = departments[0],
                            GroupNumber = groupNumber,
                            ShortName = "ПЗПІ",
                            Name = "Програмна Інженерія",
                            StartYear = new DateTimeOffset(new DateTime(DateTime.Now.Year - course, 1, 1))
                        });
                        groups.Add(new Group() {
                            Course = course,
                            CodeSpeciality = "125",
                            Department = departments[0],
                            GroupNumber = groupNumber,
                            ShortName = "ШІ",
                            Name = "Штучний Інтелект",
                            StartYear = new DateTimeOffset(new DateTime(DateTime.Now.Year - course, 1, 1))
                        });
                        groups.Add(new Group() {
                            Course = course,
                            CodeSpeciality = "122",
                            Department = departments[1],
                            GroupNumber = groupNumber,
                            ShortName = "КН",
                            Name = "Комп'ютерні науки (Алгоритми)",
                            StartYear = new DateTimeOffset(new DateTime(DateTime.Now.Year - course, 1, 1))
                        });
                    }
                }
                db.AddRange(groups);
                db.SaveChanges();
                var cp = new ContentPresenter();
                for(int index = 0; index < cp.Students.Count; ++index)
                {
                    var s = cp.Students[index];
                    s.Group = groups[index % groups.Count];
                    s.GroupId = s.Group.GroupId;
                    s.StartStudy = s.Group.StartYear;
                    s.EndStudy = s.Group.StartYear.AddYears(5);
                }
                db.AddRange(cp.Parents);
                db.AddRange(cp.Students);
                db.SaveChanges();
                db.AddRange(cp.StudentParents);
                db.SaveChanges();
            }
        }

        private async void MakeReport(object sender, RoutedEventArgs e) {
            if(vm.SelectedShortName != null)
            {
                await ToExcel.Some(vm.Report);
            }
            else
            {
                var b = new MessageDialog("Select group");
                await b.ShowAsync();
            }
            
        }

        private async void MakeGlobalReport(object sender, RoutedEventArgs e) {
            await ToExcel.Some(vm.Students);
        }
    }
}
