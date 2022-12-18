using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import
{
    public interface IReadDocument
    {
        public List<Employee> ReadUsers(string file);
        public List<Record> ReadRecords(string file);
    }
}
