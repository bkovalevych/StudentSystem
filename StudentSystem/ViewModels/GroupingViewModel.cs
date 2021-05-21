using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace StudentSystem.ViewModels
{
    using Helpers;
    using Entities;
    using Services;
    
    public class GroupingViewModel : Observable
    {
        public struct TreeItem
        {
            public string Key
            {
                get; set;
            }
            public IEnumerable<TreeItem> Students
            {
                get; set;
            }
        }

        public GroupingViewModel(Func<IEnumerable<Student>> getStudents)
        {
            this.getStudents = getStudents;
        }
        public IEnumerable<TreeItem> Collection
        {
            get => collection; set => Set(ref collection, value);
        }
        private IEnumerable<TreeItem> collection = new List<TreeItem>();

        public List<string> Variants => new List<string>()
        {
            "Group",
            "Department",
            "StartYear",
            "BirthDay"
        };

        private Dictionary<string, Func<IEnumerable<Student>, IEnumerable<TreeItem>>> Actions => 
            new Dictionary<string, Func<IEnumerable<Student>, IEnumerable<TreeItem>>>()
        {
            ["Group"] = getByGroups,
            ["Department"] = getByDepartments,
            ["StartYear"] = getByStartYear,
            ["BirthDay"] = getByBithday
        };

        private readonly Func<IEnumerable<Student>, IEnumerable<TreeItem>> getByGroups = (students) =>
        {
            return students.GroupBy(o => o.Group?.DisplayName ?? "no Group").Select(o => new TreeItem()
            {
                Key = o.Key,
                Students = o.OrderBy(order => order.SecondName).Select(o2 => new TreeItem() { Key = o2.DisplayName })
            });
        };

        private readonly Func<IEnumerable<Student>, IEnumerable<TreeItem>> getByDepartments = (students) =>
        {
            return students.GroupBy(o => o.Group?.Department?.ShortDepartmentName ?? "no Department").Select(o => new TreeItem()
            {
                Key = o.Key,
                Students = o.OrderBy(order => order.SecondName).Select(o2 => new TreeItem() { Key = o2.DisplayName })
            });
        };
        private readonly Func<IEnumerable<Student>, IEnumerable<TreeItem>> getByStartYear = (students) =>
        {
            return students.GroupBy(o => o.StartStudy.Year.ToString()).Select(o => new TreeItem()
            {
                Key = o.Key,
                Students = o.OrderBy(order => order.SecondName).Select(o2 => new TreeItem() { Key = o2.DisplayName })
            });
        };

        private readonly Func<IEnumerable<Student>, IEnumerable<TreeItem>> getByBithday = (students) =>
        {
            return students.GroupBy(o => o.Birthday.Year.ToString()).Select(o => new TreeItem()
            {
                Key = o.Key,
                Students = o.OrderBy(order => order.SecondName).Select(o2 => new TreeItem() { Key = o2.DisplayName })
            });
        };

        public string SelectedVariant
        {
            get => selectedVariant; set
            {
                if(Actions.ContainsKey(value) && getStudents != null)
                {
                    Collection = Actions[value](getStudents());
                }
                Set(ref selectedVariant, value);
            }
        }
        private string selectedVariant;

        private Func<IEnumerable<Student>> getStudents;
    }
}
