using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Helpers
{
    public static class Constants
    {
        public static string GenderMale => "чоловік";
        public static string GenderFemale => "жінка";
        public static List<string> Gender => new List<string>() { GenderFemale, GenderMale };
    }
}
