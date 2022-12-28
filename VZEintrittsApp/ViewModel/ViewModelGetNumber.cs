using VZEintrittsApp.Domain;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelGetNumber
    {
        public Employee CurrentEmployee { get; set; }

        public ViewModelGetNumber(Employee employee)
        {
            CurrentEmployee = employee;
        }
    }
}
