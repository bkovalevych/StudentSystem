using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    public class GroupSorterService : SorterService<Group>
    {
        protected override Dictionary<string, Comparison<Group>> Comparisons
        {
            set; get;
        }
        public GroupSorterService()
        {
            Comparisons = new Dictionary<string, Comparison<Group>>()
            {
                ["CodeSpeciality"] = (o1, o2) => o1.CodeSpeciality.CompareTo(o2.CodeSpeciality),
                ["CountStudents"] = (o1, o2) => o1.CountStudents.CompareTo(o2.CountStudents),
                ["Course"] = (o1, o2) => o1.Course.CompareTo(o2.Course),
                ["Department"] = (o1, o2) => o1.Department?.ShortDepartmentName.CompareTo(o2.Department?.ShortDepartmentName) ?? -1,
                ["DisplayName"] = (o1, o2) => o1.DisplayName.CompareTo(o2.DisplayName),
                ["GroupId"] = (o1, o2) => o1.GroupId.CompareTo(o2.GroupId),
                ["GroupNumber"] = (o1, o2) => o1.GroupNumber.CompareTo(o2.GroupNumber),
                ["Name"] = (o1, o2) => o1.Name.CompareTo(o2.Name),
                ["ShortName"] = (o1, o2) => o1.ShortName.CompareTo(o2.ShortName),
                ["StartYear"] = (o1, o2) => o1.StartYear.CompareTo(o2.StartYear)
            };
        }
    }
}
