using StudentSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Entities
{

    public class Department : Observable
    {
        public int DepartmentId { get => id; set => Set(ref id, value); }
        private int id;

        [NotMapped]
        public int CountGroups { get => Groups.Count; set => Set(ref countGroups, value); }
        private int countGroups;

        [NotMapped]
        public int CountStudents
        {
            get => Groups.Select(o => o.CountStudents).Sum(); set => Set(ref countStudents, value);
        }
        private int countStudents;


        public string DepartmentName { get => departmentName; set => Set(ref departmentName, value); }
        private string departmentName;
        
        public string ShortDepartmentName { get => shortDepartmentName; set => Set(ref shortDepartmentName, value); }
        private string shortDepartmentName;

        public ObservableCollection<Group> Groups { set; get; } = new ObservableCollection<Group>();
        public override string ToString() {
            return shortDepartmentName;
        }

    }
}
