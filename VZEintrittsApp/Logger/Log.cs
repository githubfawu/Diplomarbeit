using System;
using System.ComponentModel.DataAnnotations;

namespace VZEintrittsApp.Logger
{
    public class Log
    {
        [Key]
        public int? LogNr { get; set; }
        public DateTime Timestamp { get; set; }
        public string Originator { get; set; }
        public string SelectedEmployee { get; set; }
        public string RecordEvent { get; set; }
    }
}
