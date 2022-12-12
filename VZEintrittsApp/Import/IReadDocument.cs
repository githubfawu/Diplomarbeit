using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import
{
    public interface IReadDocument
    {
        public List<Employee> ProcessDocument(string file);
    }
}
