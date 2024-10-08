using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Suspects {
        public int SuspectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string SuspectAddress { get; set; }
        public string SuspectContact { get; set; }

        public Suspects() { }

        public Suspects(string FirstName, string LastName, DateTime DateOfBirth, string Gender, string SuspectAddress, string SuspectContact) {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.SuspectAddress = SuspectAddress;
            this.SuspectContact = SuspectContact;

        }

        public override string ToString() {
            return $"SuspectId: {SuspectId} FirstName:{FirstName} LastName: {LastName} DateOfBirth:{DateOfBirth} Gender: {Gender} SuspectAddress: {SuspectAddress} SuspectContact: {SuspectContact}";
        }
    }
}
