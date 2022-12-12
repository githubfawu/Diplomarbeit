using System.Windows;
using VZEintrittsApp.Model;
using VZEintrittsApp.ViewModel;


namespace VZEintrittsApp.View
{
    public partial class MainWindow : Window
    {
        private readonly Repository repository = new();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModelUserView();
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
