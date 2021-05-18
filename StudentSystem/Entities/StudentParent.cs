using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Entities
{
    using Helpers;
    public class StudentParent : Observable
    {
        public int StudentParentId { get => id; set => Set(ref id, value); }
        private int id;

        public int ParentId { set; get; }
        public Parent Parent { get => parent; set => Set(ref parent, value); }
        private Parent parent;


        public int StudentId { set; get; }
        public Student Student { get => student; set => Set(ref student, value); }
        private Student student;
    }
}
