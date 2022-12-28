
using System.ComponentModel.DataAnnotations;

namespace VZEintrittsApp.Domain
{
    public class BranchesAndPhoneNumbers
    {
        [Key]
        public int BranchId { get; set; }
        public string BranchShortName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Office { get; set; }
    }
}
