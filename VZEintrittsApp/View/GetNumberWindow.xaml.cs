
using System.Windows;
using VZEintrittsApp.Domain;
using VZEintrittsApp.ViewModel;

namespace VZEintrittsApp.View
{
    public partial class GetNumberWindow : Window
    {
        private Employee CurrentEmployee { get; set; }
        public GetNumberWindow(Employee employee)
        {
            InitializeComponent();
            CurrentEmployee = employee; 
            DataContext = new ViewModelGetNumber(CurrentEmployee);
        }
    }
}
