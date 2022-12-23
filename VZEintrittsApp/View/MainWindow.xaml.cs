using System.Windows;
using System.Windows.Input;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Model;
using VZEintrittsApp.ViewModel;

namespace VZEintrittsApp.View
{
    public partial class MainWindow : Window
    {
        private readonly Repository repository;

        public MainWindow()
        {
            using (DbContext context = new DbContext())
            {
                context.Database.EnsureCreated();
            }
            
            InitializeComponent();

            repository = new Repository();
            DataContext = new ViewModelUserView(repository);
        }

        private void Document_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    repository.ImportDocument(file);
                }
            }
        }
    }
}
