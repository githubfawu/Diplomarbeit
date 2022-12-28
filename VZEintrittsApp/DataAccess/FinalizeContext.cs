using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class FinalizeContext
    {
        private DbContext DbContext;
        public FinalizeContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public StateAndCountry? GetStateAndCountry(string cityName)
        {
            if (DbContext.StatesAndCountries.SingleOrDefault(x => x.CityName == cityName) != null)
            {
                return DbContext.StatesAndCountries.SingleOrDefault(x => x.CityName == cityName);
            }
            else
            {
                MessageBox.Show("Fehler beim Laden des Kantons oder des Landes!");
                return null;
            }
        }

        public BranchesAndPhoneNumbers? GetDescriptionAndOffice(string cityName, string companyName)
        {
            try
            {
                return DbContext.BranchAndPhones.SingleOrDefault(x => x.City == cityName & x.Company == companyName);
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden der Beschreibung oder des Büros!");
                return null;
            }
        }
    }
}
