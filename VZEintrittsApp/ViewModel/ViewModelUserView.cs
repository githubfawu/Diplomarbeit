using System;
using System.Collections.ObjectModel;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Enums;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelUserView : BindableBase
    {
        public DelegateCommand UpdateCommand { get; set; }
        public ObservableCollection<Record> RecordList { get; set; }

        public ViewModelUserView()
        {
            //UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => SelectedRecord);

            var record = new Record
            {
                EmployeeNr = 1,
                Abbreviation = "Test",
                Status = RecordStatus.Offen,
                EntryDate = new DateTime(2022, 11, 11)
            };

            var record2 = new Record
            {
                EmployeeNr = 2,
                Abbreviation = "Test2",
                Status = RecordStatus.Abgeschlossen,
                EntryDate = new DateTime(2022, 10, 1)
            };

            RecordList = new ObservableCollection<Record> { record, record2 };
        }

        private Record selectedRecord;
        public Record SelectedRecord
        {
            get => selectedRecord;
            set => SetProperty(ref selectedRecord, value);
        }


        //private bool CanExecute()
        //{
        //    return !String.IsNullOrEmpty(SelectedRecord.Abbreviation);
        //}

        //private void Execute()
        //{
        //    MessageBox.Show("Speichert...");
        //}



    }
}
