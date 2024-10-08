using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Victims {
        public int VictimId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string VictimAddress { get; set; }
        public string VictimContact { get; set; }

        public Victims() { }
        
        public Victims(string FirstName,string LastName, DateTime DateOfBirth,  string Gender, string VictimAddress, string VictimContact) {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.VictimAddress = VictimAddress;
            this.VictimContact = VictimContact;
            
        }

        public override string ToString() {
            return $"VictimId: {VictimId} FirstName:{FirstName} LastName: {LastName} DateOfBirth:{DateOfBirth} Gender: {Gender} VictimAddress: {VictimAddress} VictimContact: {VictimContact}";
        }
    }
}
