using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Officers {
        public int OfficerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BadgeNumber { get; set; }
        public string Rank { get; set; }
        public string Contact { get; set; }
        public int AgencyId { get; set; }

        public Officers() { }

        public Officers(string FirstName, string LastName, int BadgeNumber, string Rank, string Contact, int AgencyId) {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BadgeNumber = BadgeNumber;
            this.Rank = Rank;
            this.Contact = Contact;
            this.AgencyId = AgencyId;

        }


        public override string ToString() {
            return $"OfficersId: {OfficerId} FirstName: {FirstName} LastName:{LastName} BadgeNumber:{BadgeNumber} Rank: {Rank} Contact:{Contact}  AgencyId: {AgencyId}) {{";
        }




    }
}
