using System.Collections.Generic;
using VZEintrittsApp.Model.RecordEntity;
using Employee = VZEintrittsApp.Model.EmployeeEntity.Employee;

namespace VZEintrittsApp.Import
{
    public interface IReadDocument
    {
        public List<Employee> ReadUsers(string file);
        public List<Record> ReadRecords(string file);
    }
}
