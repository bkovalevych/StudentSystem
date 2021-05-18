using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.ViewModels
{
    public class ItemChoose
    {
        public string Variant { get; set; }
        public IEnumerable<object> Val { get; set; }
        public override string ToString() {
            return Variant;
        }
    }
}
