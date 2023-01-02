using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class PhoneFormatContext
    {
        private DbContext DbContext;
        public PhoneFormatContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public NumberFormat GetPhoneNumberFormat(string cityName)
        {
            return DbContext.NumberFormats.SingleOrDefault(x => x.CityName == cityName);
        }
    }
}