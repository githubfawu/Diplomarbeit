using System.Linq;

namespace VZEintrittsApp.DataAccess.Contexts
{
    public class NoteContext
    {
        private DbContext DbContext;
        public NoteContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public string GetNote(string branchName)
        {
            var note = DbContext.Notes.SingleOrDefault(n => n.BranchName == branchName);
            return note.Text;
        }
    }
}
