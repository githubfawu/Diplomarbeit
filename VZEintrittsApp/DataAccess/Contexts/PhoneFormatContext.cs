using System.Linq;
using VZEintrittsApp.Model.Domain;

namespace VZEintrittsApp.DataAccess.Contexts
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