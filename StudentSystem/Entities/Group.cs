using StudentSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Entities
{
    public class Group : Observable
    {
        public int GroupId { get => id; set => Set(ref id, value); }
        private int id;
        public string ShortName { get => shortName; set => Set(ref shortName, value); }
        private string shortName;

        public string Name { get => name; set => Set(ref name, value); }
        private string name;

        public DateTime StartYear { get => startYear; set => Set(ref startYear, value); }
        private DateTime startYear;

        public string CodeSpeciality { get => codeSpeciality; set => Set(ref codeSpeciality, value); }
        private string codeSpeciality;


        public int Course { get => course; set => Set(ref course, value); }
        private int course;

        public int CountStudents { get => countStudents; set => Set(ref countStudents, value); }
        private int countStudents;

        public int GroupNumber { get => groupNumber; set => Set(ref groupNumber, value); }
        private int groupNumber;



        public int DepartmentId { get; set; }
        public Department Department { get => department; set => Set(ref department, value); }
        private Department department;

        public ObservableCollection<Student> Students { get; set; }
        public override string ToString() {
            return ShortName;
        }

    }
}
