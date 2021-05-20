using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Entities
{
    using Helpers;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student : Observable
    {
        public int StudentId { get => id; set => Set(ref id, value); }
        private int id;

        public string FirstName { get => firstName; set
            {
                Set(ref firstName, value);
                OnPropertyChanged(nameof(DisplayName));
            } }
        private string firstName;

        public string SecondName
        {
            get => secondName; set 
            {
                Set(ref secondName, value);
                OnPropertyChanged(nameof(DisplayName));
            } 
        }
        private string secondName;

        public string ThirdName { get => thirdName; set => Set(ref thirdName, value); }
        private string thirdName;

        public string Gender { get => gender; set => Set(ref gender, value); }
        private string gender;

        public DateTimeOffset Birthday { get => birthday; set => Set(ref birthday, value); }
        private DateTimeOffset birthday;

        public string Address { get => address; set => Set(ref address, value); }
        private string address;

        public string PhoneNumber { get => phoneNumber; set => Set(ref phoneNumber, value); }
        private string phoneNumber;

        public string IdentificationCode { get => identificationCode; set => Set(ref identificationCode, value); }
        private string identificationCode;

        public string PassportCode { get => passportCode; set => Set(ref passportCode, value); }
        private string passportCode;

        public string BirthdayCertificate { get => birthdayCertificate; set => Set(ref birthdayCertificate, value); }
        private string birthdayCertificate;

        public string StudentCertificate { get => studentCertificate; set => Set(ref studentCertificate, value); }
        private string studentCertificate;

        public string School { get => school; set => Set(ref school, value); }
        private string school;

        public string SchoolCertificateCode { get => schoolCertificateCode; set => Set(ref schoolCertificateCode, value); }
        private string schoolCertificateCode;



        public double AverageMarkInSchool { get => averageMarkInSchool; set => Set(ref averageMarkInSchool, value); }
        private double averageMarkInSchool;

        public string ArmyCerificate { get => armyCerificate; set => Set(ref armyCerificate, value); }
        private string armyCerificate;

        public string AdditionalInfo { get => additionalInfo; set => Set(ref additionalInfo, value); }
        private string additionalInfo;

        public DateTimeOffset StartStudy { get => startStudy; set => Set(ref startStudy, value); }
        private DateTimeOffset startStudy;


        public DateTimeOffset EndStudy { get => endStudy; set => Set(ref endStudy, value); }
        private DateTimeOffset endStudy;

        public int? GroupId
        {
            get => groupId; set
            {
                Set(ref groupId, value);
            }
        }
        private int? groupId;
        public Group Group { get => group; set => Set(ref group, value); }
        private Group group;
        
        [NotMapped]
        public string DisplayName
        {
            get => $"{StudentId} {SecondName} {FirstName}";
            set => Set(ref displayName, value);
            
        }
        private string displayName;

        public ObservableCollection<StudentParent> StudentParents { get; set; } = new ObservableCollection<StudentParent>();
        public override string ToString() {
            return $"{firstName} {secondName}, group {Group}, {DateTime.Now.Year - birthday.Year} years";
        }
    }
}
