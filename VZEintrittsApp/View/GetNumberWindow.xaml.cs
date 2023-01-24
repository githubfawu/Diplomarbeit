
using System.Windows;
using VZEintrittsApp.Model;
using VZEintrittsApp.ViewModel;
using Employee = VZEintrittsApp.Model.Employee.Employee;

namespace VZEintrittsApp.View
{
    public partial class GetNumberWindow : Window
    {
        private Employee CurrentEmployee { get; set; }
        private readonly Repository Repository;
        public GetNumberWindow(Employee employee, Repository repository)
        {
            InitializeComponent();
            CurrentEmployee = employee;
            Repository = repository;
            var vm = new ViewModelGetNumber(CurrentEmployee, Repository);
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();
        }
    }
}
