using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    class StudentFilter : FilterService<Student>
    {
        public override List<Func<Student, string, bool>> ParentPredicates
        {
            get;
            set;
        } = new List<Func<Student, string, bool>>()
        {
            (s, p) => eq(s.FirstName, p),
            (s, p) => eq(s.Gender, p),
            (s, p) => eq(s.Address, p),
            (s, p) => eq(s.Birthday, p),
            (s, p) => eq(s.SecondName, p),
            (s, p) => eq(s.ThirdName, p),
            (s, p) => eq(s.StudentCertificate, p),
            (s, p) => eq(s.SchoolCertificateCode, p),
            (s, p) => eq(s.School, p),
        };
        public override List<Func<Student, string>> Selects
        {
            get;
            set;
        } = new List<Func<Student, string>>()
        {
            (s) => s.FirstName.ToString(),
            (s) => s.Gender.ToString(),
            (s) => s.Address.ToString(),
            (s) => s.Birthday.ToString(),
            (s) => s.SecondName.ToString(),
            (s) => s.ThirdName.ToString(),
            (s) => s.StudentCertificate.ToString(),
            (s) => s.SchoolCertificateCode.ToString(),
            (s) => s.School.ToString(),
        };

        public override IEnumerable<Student> DbGet()
        {
            using(var db = new StudentContext())
            {
                return db.Students.ToList();
            }
        }
    }
}
