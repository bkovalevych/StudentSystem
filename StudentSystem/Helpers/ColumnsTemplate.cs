using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace StudentSystem.Helpers
{
    using Entities;
    using System.IO;
    using System.Xml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Markup;

    public class ColumnsTemplate
    {
        public List<DataGridColumn> Students => students;
        public List<DataGridColumn> Parents => parents;
        public List<DataGridColumn> StudentsParents => studentParents;
        public List<DataGridColumn> Groups => groups;
        public List<DataGridColumn> Departments => departments;
        public ColumnsTemplate() {
            Init();
        }
        private void Init() {
            students = new List<DataGridColumn>() {
                BCol("StudentId", true),
                BCol("FirstName"),
                BCol("SecondName"),
                BCol("ThirdName"),
                BCol("GroupId", GetCollection<Group>(), "DisplayName", "Group"),
                BCol("Gender", Constants.Gender),
                Dt("Birthday", true),
                BCol("Address"),
                BCol("PhoneNumber"),
                BCol("IdentificationCode"),
                BCol("PassportCode"),
                BCol("BirthdayCertificate"),
                BCol("StudentCertificate"),
                BCol("School"),
                BCol("SchoolCertificateCode"),
                BCol("AverageMarkInSchool"),
                BCol("ArmyCerificate"),
                BCol("AdditionalInfo"),
                Dt("StartStudy"),
                Dt("EndStudy")
                
            };
            parents = new List<DataGridColumn>() {
                BCol("ParentId", true),
                BCol("FirstName"),
                BCol("SecondName"),
                BCol("ThirdName"),
                BCol("Gender", Constants.Gender),
                Dt("Birthday", true),
                BCol("Address"),
                BCol("PhoneNumber"),
                BCol("Job"),
                BCol("AdditionalInfo")
            };
            studentParents = new List<DataGridColumn>() {
                 BCol("StudentParentId", true),
                 BCol("ParentId", GetCollection<Parent>(), "DisplayName", "parent"),
                 BCol("StudentId", GetCollection<Student>(), "DisplayName", "student")
            };
            groups = new List<DataGridColumn>() {
                BCol("GroupId", true),
                BCol("DepartmentId", GetCollection<Department>(), "ShortDepartmentName", "Department"),
                BCol("DisplayName", true),
                BCol("ShortName"),
                BCol("Name"),
                BCol("CountStudents", true),
                Dt("StartYear"),
                BCol("CodeSpeciality"),
                BCol("Course"),
                BCol("GroupNumber"),
                
            };
            departments = new List<DataGridColumn>() {
                BCol("DepartmentId", true),
                BCol("CountGroups", true),
                BCol("CountStudents", true),
                BCol("DepartmentName"),
                BCol("ShortDepartmentName")
            };
        }
        private List<DataGridColumn> students = new List<DataGridColumn>() {
            
        };
        private List<DataGridColumn> parents = new List<DataGridColumn>() {

        };
        private List<DataGridColumn> studentParents = new List<DataGridColumn>() {

        };
        private List<DataGridColumn> groups = new List<DataGridColumn>() {

        };
        private List<DataGridColumn> departments = new List<DataGridColumn>() {

        };

        private List<T> GetCollection<T>() {
            using (var db = new StudentContext()) {
                if (typeof(T) == typeof(Student)) {
                    return db.Students.ToList() as List<T>;
                } else if (typeof(T) == typeof(Parent)) {
                    return db.Parents.ToList() as List<T>;
                } else if (typeof(T) == typeof(Group)) {
                    return db.Groups.ToList() as List<T>;
                } else {
                    return db.Departments.ToList() as List<T>;
                }
            }
        }

        private DataGridColumn Dt(string path, bool showMothAndDay = false)
        {
            DataGridTemplateColumn result = new DataGridTemplateColumn() {
                CanUserReorder = true,
                CanUserResize = true,
                CanUserSort = true,
                Header = path
            };
            result.CellTemplate = CreateShow(path, showMothAndDay);
            return result;
        }
        private DataTemplate CreateShow(string path, bool monthAndDayVisible)
        {
            string visible = monthAndDayVisible ? "True" : "False";
            string stringReader =
            "<DataTemplate " +
               "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" " +
               "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" " +
               " >" +
               "<DatePicker Date=\"{Binding " + path + ", Mode=TwoWay}\" " +
               "MonthVisible=\""+ visible + "\" " +
               "DayVisible=\"" + visible + "\"/>" +
               "</DataTemplate>";
            return XamlReader.Load(stringReader) as DataTemplate;
        }


        private DataGridColumn BCol(string path, bool disableIt = false) {
            DataGridTextColumn column = new DataGridTextColumn() { CanUserReorder=true, CanUserSort=true, CanUserResize=true};
            column.Binding = new Windows.UI.Xaml.Data.Binding() { Path = new PropertyPath(path), TargetNullValue = "" };
            column.Header = path;
            column.IsReadOnly = false;
            column.Binding.Mode = Windows.UI.Xaml.Data.BindingMode.TwoWay;
            if (disableIt) {
                column.IsReadOnly = true;
                column.Binding.Mode = Windows.UI.Xaml.Data.BindingMode.OneWay;
            }
            return column;
        }
        private DataGridColumn BCol<T>(string path, IEnumerable<T> variants, string header = null, bool disableIt = false) {
            DataGridComboBoxColumn column = new DataGridComboBoxColumn() { CanUserReorder = true, CanUserSort = true, CanUserResize = true };
            column.Binding = new Windows.UI.Xaml.Data.Binding() { Path = new PropertyPath(path), TargetNullValue = "" };
            if (header == null) {
                header = path;
            }
            column.Header = header;
            column.ItemsSource = variants;
            column.IsReadOnly = false;
            column.Binding.Mode = Windows.UI.Xaml.Data.BindingMode.TwoWay;
            if (disableIt) {
                column.IsReadOnly = true;
                column.Binding.Mode = Windows.UI.Xaml.Data.BindingMode.OneWay;
            }
            return column;
        }

        private DataGridColumn BCol <T>(string path, IEnumerable<T> variants, string displayProp, string header, bool disableIt = false) {
            DataGridComboBoxColumn column = new DataGridComboBoxColumn() { CanUserReorder = true, CanUserSort = true, CanUserResize = true };
            column.Binding = new Windows.UI.Xaml.Data.Binding() { Path = new PropertyPath(path), TargetNullValue = "" };

            column.Header = header;
            column.ItemsSource = variants;
            if (displayProp != null) {
                column.DisplayMemberPath = displayProp;
            }
            column.IsReadOnly = false;
            column.Binding.Mode = Windows.UI.Xaml.Data.BindingMode.TwoWay;
            if (disableIt) {
                column.IsReadOnly = true;
                column.Binding.Mode = Windows.UI.Xaml.Data.BindingMode.OneWay;
            }
            return column;
        }
    }
}
