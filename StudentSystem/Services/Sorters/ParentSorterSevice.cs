using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Entities;
    class ParentSorterSevice : SorterService<Parent>
    {
        protected override Dictionary<string, Comparison<Parent>> Comparisons
        {
            get;
            set;
        }
        public ParentSorterSevice()
        {
            Comparisons = new Dictionary<string, Comparison<Parent>>()
            {
                ["Job"] = (o1, o2) => o1.Job.CompareTo(o2.Job),
                ["ParentId"] = (o1, o2) => o1.ParentId.CompareTo(o2.ParentId),
                ["PhoneNumber"] = (o1, o2) => o1.PhoneNumber.CompareTo(o2.PhoneNumber),
                ["SecondName"] = (o1, o2) => o1.SecondName.CompareTo(o2.SecondName),
                ["ThirdName"] = (o1, o2) => o1.ThirdName.CompareTo(o2.ThirdName),
                ["Gender"] = (o1, o2) => o1.Gender.CompareTo(o2.Gender),
                ["FirstName"] = (o1, o2) => o1.FirstName.CompareTo(o2.FirstName),
                ["Birthday"] = (o1, o2) => o1.Birthday.CompareTo(o2.Birthday),
                ["Address"] = (o1, o2) => o1.Address.CompareTo(o2.Address),
                ["AdditionalInfo"] = (o1, o2) => o1.AdditionalInfo.CompareTo(o2.AdditionalInfo),
            };
        }
    }
}
