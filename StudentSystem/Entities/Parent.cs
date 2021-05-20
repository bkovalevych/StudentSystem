using StudentSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Entities
{
    public class Parent : Observable
    {
        public int ParentId { get => id; set => Set(ref id, value); }
        private int id;
        public string FirstName { get => firstName; set
            {
                Set(ref firstName, value);
                OnPropertyChanged(nameof(DisplayName));
            } }
        private string firstName;

        public string SecondName { get => secondName; set
            {
                Set(ref secondName, value);
                OnPropertyChanged(nameof(DisplayName));
            } }
        private string secondName;

        public string ThirdName { get => thirdName; set => Set(ref thirdName, value); }
        private string thirdName;

        public string Gender { get => gender; set => Set(ref gender, value); }
        private string gender;

        public DateTimeOffset Birthday { get => birthday; set => Set(ref birthday, value); }
        private DateTimeOffset birthday;

        public string Address { get => address; set => Set(ref address, value); }
        private string address;
        public string Job { get => job; set => Set(ref job, value); }
        private string job;

        public string PhoneNumber { get => phoneNumber; set => Set(ref phoneNumber, value); }
        private string phoneNumber;

        public string AdditionalInfo { get => additionalInfo; set => Set(ref additionalInfo, value); }
        private string additionalInfo;

        [NotMapped]
        public string DisplayName
        {
            get => $"{ParentId} {SecondName} {FirstName}";
            set => Set(ref displayName, value);

        }
        private string displayName;

        public ObservableCollection<StudentParent> StudentParents { get; set; } = new ObservableCollection<StudentParent>();
        public override string ToString() {
            return $"{firstName} {secondName} {DateTime.Now.Year - birthday.Year} years";
        }
    }
}
