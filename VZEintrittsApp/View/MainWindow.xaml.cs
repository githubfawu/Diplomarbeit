using System;
using System.Windows;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Enums;
using VZEintrittsApp.Model;
using VZEintrittsApp.ViewModel;

namespace VZEintrittsApp.View
{
    public partial class MainWindow : Window
    {
        private readonly Repository repository = new();

        public MainWindow()
        {
            using (DBContext context = new DBContext())
            {
                context.Database.EnsureCreated();
            }
            
            InitializeComponent();

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
