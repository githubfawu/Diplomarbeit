using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VZEintrittsApp.Domain
{
    public class NumberFormat
    {
        [Key]
        public string CityName { get; set; }
        public Dictionary<int, string> Formats { get; set; } = new Dictionary<int, string>();
    }
}
