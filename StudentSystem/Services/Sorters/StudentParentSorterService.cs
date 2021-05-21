using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    public class StudentParentSorterService : SorterService<StudentParent>
    {
        protected override Dictionary<string, Comparison<StudentParent>> Comparisons
        {
            get;
            set;
        }

        public StudentParentSorterService()
        {
            Comparisons = new Dictionary<string, Comparison<StudentParent>>()
            {
                ["Parent"] = (o1, o2) => o1.ParentId.CompareTo(o2.ParentId),
                ["Student"] = (o1, o2) => o1.StudentId.CompareTo(o2.StudentId),
                ["StudentParentId"] = (o1, o2) => o1.StudentParentId.CompareTo(o2.StudentParentId)
            };
        }
    }
}
