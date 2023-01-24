using System.ComponentModel.DataAnnotations;

namespace VZEintrittsApp.Model.RecordEntity
{
    public class SavedFile
    {
        private string fileName;
        [Key]
        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        private byte[] file;
        public byte[] File
        {
            get => file;
            set => file = value;
        }
    }
}
