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
    public class Group : Observable
    {
        public int GroupId { get => id; set => Set(ref id, value); }
        private int id;
        public string ShortName { get => shortName; set
            {
                Set(ref shortName, value);
                OnPropertyChanged(nameof(DisplayName));
            } 
        }
        private string shortName;

        public string Name { get => name; set => Set(ref name, value); }
        private string name;

        public DateTimeOffset StartYear { get => startYear; set
            {
                Set(ref startYear, value);
                OnPropertyChanged(nameof(DisplayName));
            } }
        private DateTimeOffset startYear;

        public string CodeSpeciality { get => codeSpeciality; set => Set(ref codeSpeciality, value); }
        private string codeSpeciality;


        public int Course { get => course; set => Set(ref course, value); }
        private int course;
        
        [NotMapped]
        public int CountStudents { get => Students?.Count ?? 0; set => Set(ref countStudents, value); }
        private int countStudents;

        [NotMapped]
        public string DisplayName
        {
            get => $"{ShortName}-{StartYear.Year}-{GroupNumber}"; set => Set(ref displayName, value);
        }
        private string displayName;

        public int GroupNumber { get => groupNumber; set
            {
                Set(ref groupNumber, value);
                OnPropertyChanged(nameof(DisplayName));
            } 
        }
        private int groupNumber;



        public int? DepartmentId {
            get => departmentId;
            set
            {
                Set(ref departmentId, value);
                Department?.Groups?.Remove(Department?.Groups?.First(o => Equals(o.DepartmentId, value)));
            }
        }
        private int? departmentId;
        public Department Department { get => department; set => Set(ref department, value); }
        private Department department;

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public override string ToString() {
            return ShortName;
        }

    }
}
