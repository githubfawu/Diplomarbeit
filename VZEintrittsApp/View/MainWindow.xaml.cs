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

                var record = new Record
                {
                    EmployeeNr = 1,
                    Abbreviation = "Test",
                    Status = RecordStatus.Offen,
                    EntryDate = new DateTime(2022, 11, 11),
                    AssociatedFile = "test.pdf"
                };
                
                var exists = context.Records.Find(record.EmployeeNr = record.EmployeeNr);
                if (exists == null)
                {
                    context.Records.Add(record);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Objekt nicht gespeichert da bereits vorhanden.");
                }
            }
            
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
