using Prism.Mvvm;

namespace VZEintrittsApp.Model.Domain
{
    public class Note : BindableBase
    {
        private int noteId;
        public int NoteId
        {
            get => noteId;
            set => SetProperty(ref noteId, value);
        }

        private string branchName;
        public string BranchName
        {
            get => branchName;
            set => SetProperty(ref branchName, value);
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
    }
}
