using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;

    class DepartmentFilterService : FilterService<Department>
    {
        public override List<Func<Department, string, bool>> ParentPredicates
        {
            get;
            set;
        } = new List<Func<Department, string, bool>>()
        {
            (d, param) => eq(d.DepartmentId, param),
            (d, param) => eq(d.DepartmentName, param),
            (d, param) => eq(d.ShortDepartmentName, param)
        };
        public override List<Func<Department, string>> Selects
        {
            get;
            set;
        } = new List<Func<Department, string>>()
        {
            (d) => d.DepartmentId.ToString(),
            (d) => d.DepartmentName.ToString(),
            (d) => d.ShortDepartmentName.ToString(),
        };

        public override IEnumerable<Department> DbGet()
        {
            using(var db = new StudentContext())
            {
                return db.Departments.ToList();
            }
        }
    }
}
