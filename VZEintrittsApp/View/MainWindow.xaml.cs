using System.Windows;
using System.Windows.Input;
using VZEintrittsApp.Model;
using VZEintrittsApp.ViewModel;

namespace VZEintrittsApp.View
{
    public partial class MainWindow : Window
    {
        private readonly Repository Repository;
        public MainWindow(Repository repository)
        {
            InitializeComponent();

            Repository = repository;
            DataContext = new ViewModelUserView(Repository);
        }

        private void Document_Drop(object sender, DragEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    Repository.ImportDocument(file);
                }
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
