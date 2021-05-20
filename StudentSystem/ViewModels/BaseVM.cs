using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.ObjectModel;

namespace StudentSystem.ViewModels
{
    using Helpers;
    using Entities;
    using Services;
    using System.Windows.Input;
    using System.Diagnostics;
    using Windows.UI.Xaml;
    using Microsoft.EntityFrameworkCore;

    public class BaseVM : Observable {
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<StudentParent> StudentParents { get; set; }
        public ObservableCollection<Parent> Parents {get; set;}
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Department> Departments {get; set;}
        public ObservableCollection<Student> Report { get => report; set => Set(ref report, value); }
        private ObservableCollection<Student> report;
        public List<int> Courses => new List<int> { 1, 2 };

        private List<Observable> needDelete = new List<Observable>();
        private List<Observable> needAdd = new List<Observable>();

        public int SelectedCourse { get => selectedCourse;
            set
            {
                Set(ref selectedCourse, value);
                UpdateReport();
            }
        }
        private int selectedCourse = 1;
        public List<int> GroupNumbers => new List<int> { 1, 2 };
        public int SelectedGroupNumber { get => selectedGroupNumber; 
            set {
                Set(ref selectedGroupNumber, value);
                UpdateReport();
            } }
        private int selectedGroupNumber = 1;
        public List<string> ShortNames { get; set; }
        public string SelectedShortName { get => selectedShortName;
            set 
            {
                Set(ref selectedShortName, value);
                UpdateReport();
            } }
        private string selectedShortName;
        public int SelectedHighRowIndex
        {
            get => selectedHighRowIndex;
            set => Set(ref selectedHighRowIndex, value);
        }
        private int selectedHighRowIndex = -1;
        public int SelectedLowRowIndex
        {
            get => selectedLowRowIndex;
            set => Set(ref selectedLowRowIndex, value);
        }
        private int selectedLowRowIndex = -1;

        public IEnumerable<object> LowLevel { get => lowLevel; set => Set(ref lowLevel, value); }
        private IEnumerable<object> lowLevel;
        public Observable SelectedRow { get => selectedRow; set
            {
                Set(ref selectedRow, value);
                if (value is Department d) {
                    LowLevel = d.Groups;
                } else if (value is Group g) {
                    LowLevel = g.Students;
                } else if (value is Student s) {
                    LowLevel = s.StudentParents;
                } else if (value is Parent p) {
                    LowLevel = p.StudentParents;
                }
            }
        }
        private Observable selectedRow;

        public Observable SelectedLowRow
        {
            get => selectedLowRow; set
            {
                Set(ref selectedLowRow, value);
            }
        }
        private Observable selectedLowRow;

        public ICommand DeleteHighItemCommand => new RelayCommand(() => DeleteHighItem(SelectedRow));
        public ICommand DeleteLowtemCommand => new RelayCommand(() => DeleteLowItem(SelectedLowRow));

        private void DeleteHighItem(Observable r)
        {
            if(r != null)
            {
                needDelete.Add(r);
                if(r is Group g)
                {
                    g.Department?.Groups.Remove(g);
                    Groups.Remove(g);
                } else if(r is Department d)
                {
                    d.Groups?.Clear();
                    Departments.Remove(d);
                } else if(r is Parent p)
                {
                    p.StudentParents?.Clear();
                    Parents.Remove(p);
                } else if(r is Student s)
                {
                    s.Group?.Students?.Remove(s);
                    Students.Remove(s);
                } else if(r is StudentParent sp)
                {
                    sp.Parent?.StudentParents?.Remove(sp);
                    sp.Student?.StudentParents?.Remove(sp);
                    StudentParents.Remove(sp);
                }
                
            }
        }

        private void DeleteLowItem(Observable r)
        {
            if(r != null)
            {
                if(r is Group g)
                {
                    g.Department?.Groups.Remove(g);
                    g.Department = null;
                    g.DepartmentId = null;
                }
                else if(r is Department d)
                {
                }
                else if(r is Parent p)
                {
                    
                }
                else if(r is Student s)
                {
                    s.Group?.Students?.Remove(s);
                    s.Group = null;
                    s.GroupId = null;
                }
                else if(r is StudentParent sp)
                {
                    sp.Parent?.StudentParents?.Remove(sp);
                    sp.Student?.StudentParents?.Remove(sp);
                    StudentParents.Remove(sp);
                }

            }
        }

        public ICommand AddHighItemCommand => new RelayCommand(AddHighItem);

        private void AddHighItem()
        {
            switch(selectedVariant.Variant)
            {
                case nameof(Students):
                    var s = new Student();
                    Students.Insert(0, s);
                    needAdd.Add(s);
                    break;
                case nameof(Parents):
                    var p = new Parent();
                    Parents.Insert(0, p);
                    needAdd.Add(p);
                    break;
                case nameof(StudentParents):
                    var v = new StudentParent();
                    StudentParents.Insert(0, v);
                    needAdd.Add(v);
                    break;
                case nameof(Groups):
                    var g = new Group();
                    Groups.Insert(0, g);
                    needAdd.Add(g);
                    break;
                case nameof(Departments):
                    var d = new Department();
                    Departments.Insert(0, d);
                    needAdd.Add(d);
                    break;
                case "Report":
                    return;
            }
            SelectedHighRowIndex = 0;
        }

        public ICommand AddLowItemCommand => new RelayCommand(AddLowItem);

        private void AddLowItem()
        {
            var r = SelectedRow;
            if(r != null)
            {
                if(r is Group g)
                {
                    if(g.Students == null)
                    {
                        g.Students = new ObservableCollection<Student>();
                    }
                    var v = new Student() {Group = g, GroupId = g.GroupId };
                    g.Students.Insert(0, v);
                    needAdd.Add(v);
                }
                else if(r is Department d)
                {
                    if(d.Groups == null)
                    {
                        d.Groups = new ObservableCollection<Group>();
                    }
                    var v = new Group() { Department = d, DepartmentId = d.DepartmentId };
                    d.Groups.Insert(0, v);
                    needAdd.Add(v);
                }
                else if(r is Parent p)
                {
                    if(p.StudentParents == null)
                    {
                        p.StudentParents = new ObservableCollection<StudentParent>();
                    }
                    var v = new StudentParent() { Parent = p, ParentId = p.ParentId };
                    p.StudentParents.Insert(0, v);
                    needAdd.Add(v);
                }
                else if(r is Student s)
                {
                    if(s.StudentParents == null)
                    {
                        s.StudentParents = new ObservableCollection<StudentParent>();
                    }
                    var v = new StudentParent() { Student = s, StudentId = s.StudentId };
                    s.StudentParents.Insert(0, v);
                    needAdd.Add(v);
                }
                else if(r is StudentParent sp)
                {
                    return;
                }
            }
            SelectedLowRowIndex = 0;
        }



        public List<ItemChoose> Variants => variants;
        private readonly List<ItemChoose> variants;
        public string Query
        {
            get => query;
            set
            {
                SelectedFilter?.SearchParent(value);
                Set(ref query, value);
            }
        }
        private string query;

        public ICommand SearchCommand => new RelayCommand((o) =>
        {
            if(o is string param && SelectedFilter != null)
            {
                var finded = SelectedFilter.SearchParent(param);
                switch(selectedVariant.Variant)
                {
                    case nameof(Students):
                        Students.Clear();
                        Add(Students, (IEnumerable<Student>)finded);
                        break;
                    case nameof(Parents):
                        //Add(columns, columnsTemplate.Parents);
                        break;
                    case nameof(StudentParents):
                        
                        break;
                    case nameof(Groups):
                        Groups.Clear();
                        Add(Groups, (IEnumerable<Group>)finded);
                        break;
                    case nameof(Departments):
                        Departments.Clear();
                        Add(Departments, (IEnumerable<Department>)finded);
                        break;
                    case "Report":
                        break;
                }
            }
        });

        public ICommand SaveAllCommand => new RelayCommand(SaveAll);
        private void SaveAll()
        {
            using(var db = new StudentContext())
            {
                db.AddRange(needAdd);
                Groups.Where(o => !Equals(o.DepartmentId, o.Department?.DepartmentId))
                    .ToList().ForEach(g =>
                    {
                        var d = Departments.First(o => Equals(o.DepartmentId, g.DepartmentId));
                        g.Department = d;
                        d?.Groups?.Add(g);
                    });
                db.Departments.UpdateRange(Departments.Where(o => !needAdd.Contains(o)));
                db.Groups.UpdateRange(Groups.Where(o => !needAdd.Contains(o)));
                db.Parents.UpdateRange(Parents.Where(o => !needAdd.Contains(o)));
                db.Students.UpdateRange(Students.Where(o => !needAdd.Contains(o)));
                db.StudentParents.UpdateRange(StudentParents.Where(o => !needAdd.Contains(o)));
                try
                {
                    db.RemoveRange(needDelete);
                } catch(Exception o)
                {
                    Trace.WriteLine(o.Message);
                }
                needAdd.Clear();
                needDelete.Clear();
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Trace.WriteLine("[Delete ERROR]" + e.Message);
                }
            }
        }
        public ICommand RefreshCommand => new RelayCommand(() =>
        {
            UpdateCollections();
        });
        public IFilterService<Observable> SelectedFilter
        {
            get => selectedFilter;
            set => Set(ref selectedFilter, value);
        }
        private IFilterService<Observable> selectedFilter;
        public ISorterService<Observable> SorterService
        {
            get => sorterService;
            set => Set(ref sorterService, value);
        }
        private ISorterService<Observable> sorterService;
        public Visibility ShowReport
        {
            get => showReport;
            set => Set(ref showReport, value);
        }
        private Visibility showReport = Visibility.Collapsed;

        public ICommand SortCommand => new RelayCommand((o) =>
        {
            if(o is DataGridColumn dg)
            {
                SorterService?.Sort(SelectedVariant.Val, dg);
            }
        });

        public ItemChoose SelectedVariant { get => selectedVariant;
            set
            { 
                columns.Clear();
                lowColumns.Clear();
                Set(ref selectedVariant, value);
                columns.Clear();
                lowColumns.Clear();
                LowLevel = null;
                ShowReport = Visibility.Collapsed;
                switch(value.Variant) {
                    case nameof(Students):
                        SelectedFilter = new StudentFilter();
                        SorterService = new StudentSorterService();
                        
                        Add(columns, columnsTemplate.Students);
                        Add(lowColumns, columnsTemplate.StudentsParents);
                        break;
                    case nameof(Parents):
                        Add(columns, columnsTemplate.Parents);
                        Add(lowColumns, columnsTemplate.StudentsParents);
                        break;
                    case nameof(StudentParents):
                        Add(columns, columnsTemplate.StudentsParents);
                        break;
                    case nameof(Groups):
                        SelectedFilter = new GroupFilterService();
                        Add(columns, columnsTemplate.Groups);
                        Add(lowColumns, columnsTemplate.Students);
                        break;
                    case nameof(Departments):
                        SelectedFilter = new DepartmentFilterService();
                        Add(columns, columnsTemplate.Departments);
                        Add(lowColumns, columnsTemplate.Groups);
                        break;
                    case "Report":
                        Add(columns, columnsTemplate.Students);
                        UpdateReport();
                        ShowReport = Visibility.Visible;
                        break;
                }
                

            }
        }
        private ItemChoose selectedVariant;
        private readonly ObservableCollection<DataGridColumn> columns;
        private readonly ObservableCollection<DataGridColumn> lowColumns;

        private readonly ColumnsTemplate columnsTemplate;
        public BaseVM(ObservableCollection<DataGridColumn> columns, ObservableCollection<DataGridColumn> lowColumns) {
            this.columns = columns;
            this.lowColumns = lowColumns;
            InitCollection();
            UpdateCollections();
            variants = new List<ItemChoose>() {
                new ItemChoose() { Variant = nameof(Students), Val = Students },
                new ItemChoose() { Variant = nameof(Parents), Val = Parents },
                new ItemChoose() { Variant = nameof(StudentParents), Val = StudentParents },
                new ItemChoose() { Variant = nameof(Groups), Val = Groups },
                new ItemChoose() { Variant = nameof(Departments), Val = Departments },
                new ItemChoose() { Variant = "Report", Val = Report}
            };
            columnsTemplate = new ColumnsTemplate();
            SelectedVariant = Variants[0];
            LowLevel = new ObservableCollection<Observable>();
            
        }

        private void UpdateReport() {
            var new_report = Students.Where(s => s.Group.Course == SelectedCourse
            && s.Group.ShortName == SelectedShortName 
            && s.Group.GroupNumber == SelectedGroupNumber);
            Report.Clear();
            Add(Report, new_report);
        }
        
        private void InitCollection() {
            Students = new ObservableCollection<Student>();
            Report = new ObservableCollection<Student>();
            StudentParents = new ObservableCollection<StudentParent>();
            Parents = new ObservableCollection<Parent>();
            Groups = new ObservableCollection<Group>();
            Departments = new ObservableCollection<Department>();
        }
        private void UpdateCollections() {
            using(var db = new StudentContext()) {
                Students.Clear();
                Add(Students, db.Students);
                StudentParents.Clear();
                Add(StudentParents, db.StudentParents);
                Parents.Clear();
                Add(Parents, db.Parents);
                Groups.Clear();
                Add(Groups, db.Groups);
                Departments.Clear();
                Add(Departments, db.Departments);
                ShortNames = Groups.Select((g) => g.ShortName).Distinct().ToList();
            }
        }
        private void Add<T>(ObservableCollection<T> src, IEnumerable<T> needAdd) {
            foreach (var e in needAdd) {
                src.Add(e);
            }
        }

    }
}
