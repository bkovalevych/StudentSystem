using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    public class DepartmentSorterService : SorterService<Department>
    {
        protected override Dictionary<string, Comparison<Department>> Comparisons
        {
            get; set;
        }

        public DepartmentSorterService()
        {
            Comparisons = new Dictionary<string, Comparison<Department>>()
            {
                ["CountGroups"] = (o1, o2) => o1.CountGroups.CompareTo(o2.CountGroups),
                ["CountStudents"] = (o1, o2) => o1.CountStudents.CompareTo(o2.CountStudents),
                ["DepartmentId"] = (o1, o2) => o1.DepartmentId.CompareTo(o2.DepartmentId),
                ["DepartmentName"] = (o1, o2) => o1.DepartmentName.CompareTo(o2.DepartmentName),
                ["ShortDepartmentName"] = (o1, o2) => o1.ShortDepartmentName.CompareTo(o2.ShortDepartmentName)
            };
        }
    }
}
