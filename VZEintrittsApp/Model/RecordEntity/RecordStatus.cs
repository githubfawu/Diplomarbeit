using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;

namespace VZEintrittsApp.Model.RecordEntity
{
    public class RecordStatus : BindableBase
    {
        [Key] 
        private int recordStatusId;
        public int RecordStatusId
        {
            get => recordStatusId;
            set
            {
                if (value != recordStatusId) SetProperty(ref recordStatusId, value);
            }
        }

        private string recordStatusName;
        public string RecordStatusName
        {
            get => recordStatusName;
            set
            {
                if (value != recordStatusName) SetProperty(ref recordStatusName, value);
            }
        }
    }
}
