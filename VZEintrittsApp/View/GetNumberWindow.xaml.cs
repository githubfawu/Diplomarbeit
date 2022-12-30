
using System.Windows;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;
using VZEintrittsApp.ViewModel;

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
            DataContext = new ViewModelGetNumber(CurrentEmployee, Repository);
        }
    }
}
