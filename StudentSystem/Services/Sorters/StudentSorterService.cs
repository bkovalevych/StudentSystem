using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    using StudentSystem.Helpers;

    public class StudentSorterService : SorterService<Student>
    {
        protected override Dictionary<string, Comparison<Student>> Comparisons
        {
            get;
            set;
        }
        public StudentSorterService()
        {
            Comparisons = new Dictionary<string, Comparison<Student>>()
            {
                ["StudentId"] = (o1, o2) => o1.StudentId.CompareTo(o2.StudentId),
                ["AdditionalInfo"] = (o1, o2) => o1.AdditionalInfo.CompareTo(o2.AdditionalInfo),
                ["Address"] = (o1, o2) => o1.Address.CompareTo(o2.Address),
                ["ArmyCerificate"] = (o1, o2) => o1.ArmyCerificate.CompareTo(o2.ArmyCerificate),
                ["AverageMarkInSchool"] = (o1, o2) => o1.AverageMarkInSchool.CompareTo(o2.AverageMarkInSchool),
                ["Birthday"] = (o1, o2) => o1.Birthday.CompareTo(o2.Birthday),
                ["BirthdayCertificate"] = (o1, o2) => o1.BirthdayCertificate.CompareTo(o2.BirthdayCertificate),
                ["EndStudy"] = (o1, o2) => o1.EndStudy.CompareTo(o2.EndStudy),
                ["FirstName"] = (o1, o2) => o1.FirstName.CompareTo(o2.FirstName),
                ["Gender"] = (o1, o2) => o1.Gender.CompareTo(o2.Gender),
                ["Group"] = (o1, o2) => o1.Group.ShortName.CompareTo(o2.Group.ShortName),
                ["IdentificationCode"] = (o1, o2) => o1.IdentificationCode.CompareTo(o2.IdentificationCode),
                ["PassportCode"] = (o1, o2) => o1.PassportCode.CompareTo(o2.PassportCode),
                ["PhoneNumber"] = (o1, o2) => o1.PhoneNumber.CompareTo(o2.PhoneNumber),
                ["School"] = (o1, o2) => o1.School.CompareTo(o2.School),
                ["SchoolCertificateCode"] = (o1, o2) => o1.SchoolCertificateCode.CompareTo(o2.SchoolCertificateCode),
                ["SecondName"] = (o1, o2) => o1.SecondName.CompareTo(o2.SecondName),
                ["StartStudy"] = (o1, o2) => o1.StartStudy.CompareTo(o2.StartStudy),
                ["StudentCertificate"] = (o1, o2) => o1.StudentCertificate.CompareTo(o2.StudentCertificate),
                ["ThirdName"] = (o1, o2) => o1.ThirdName.CompareTo(o2.ThirdName)
            };
        }
    }
}
