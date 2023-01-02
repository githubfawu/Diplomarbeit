
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

        public SubsidiaryCompany? GetAppropriateSubsidiaryCompany(string cityName, string companyName)
        {
            try
            {
                return DbContext.SubsidiaryCompanies.SingleOrDefault(x => x.CityName == cityName & x.CompanyName == companyName);
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden der Beschreibung oder des Büros!");
                return null;
            }
        }

        public SubsidiaryCompany? GetSubsidiaryCompanyFromDescription(string description)
        {
            try
            {
                return DbContext.SubsidiaryCompanies.SingleOrDefault(x => x.BranchNameForDescription == description);
            }
            catch
            {
                MessageBox.Show("Fehler beim Finden der passenden Niederlassung/Firma!");
                return null;
            }
        }

        public List<SubsidiaryCompany?> GetAllSubsidiaryCompanies()
        {
            try
            {
                var allSubsidiaries = (from a in DbContext.SubsidiaryCompanies select a).ToList();
                return allSubsidiaries;
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden der Tochterfirmen/Standorte");
                return null;
            }
        }
    }
}
