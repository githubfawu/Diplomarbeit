using System.ComponentModel.DataAnnotations;

namespace VZEintrittsApp.Domain
{
    public class StateAndCountry
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryCode { get; set; }
    }
}
