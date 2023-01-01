using System.Collections.Generic;
using System.Linq;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class AttributeNotationContext
    {
        private DbContext DbContext;
        public AttributeNotationContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<AttributeNotations> GetAllEntries()
        {
            var allNotations = (from a in DbContext.AttributeNotations select a).ToList();
            return allNotations;
        }
    }
}
