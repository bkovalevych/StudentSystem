using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    using System.Text.RegularExpressions;
    using Group = Entities.Group;

    public class GroupFilterService : FilterService<Group>
    {
        
        public override List<Func<Group, string, bool>> ParentPredicates
        {
            get;
            set;
        } = new List<Func<Group, string, bool>>()
        {
            (g, param) => eq(g.GroupId, param),
            (g, param) => eq(g.GroupNumber, param),
            (g, param) => eq(g.Name, param),
            (g, param) => eq(g.ShortName, param),
            (g, param) => eq(g.StartYear, param),
            (g, param) => eq(g.Course, param),
            (g, param) => eq(g.CodeSpeciality, param)
        };
        public override List<Func<Group, string>> Selects
        {
            get;
            set;
        } = new List<Func<Group, string>>()
        {
            (g) => g.GroupId.ToString(),
            (g) => g.GroupNumber.ToString(),
            (g) => g.Name.ToString(),
            (g) => g.ShortName.ToString(),
            (g) => g.StartYear.ToString(),
            (g) => g.Course.ToString(),
            (g) => g.CodeSpeciality.ToString(),
        };

        public override IEnumerable<Group> DbGet()
        {
            using(var db = new StudentContext())
            {
                return db.Groups.ToList();
            }
        }
    }
}
