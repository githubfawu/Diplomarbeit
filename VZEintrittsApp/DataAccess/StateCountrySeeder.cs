using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public static class StateCountrySeeder
    {
        public static List<StateAndCountry> GetSeeds()
        {
            var list = new List<StateAndCountry>()
            {
                new StateAndCountry() {CityId = 1, CityName = "Zürich", StateName = "Zürich", CountryCode = "CH"},
                new StateAndCountry() {CityId = 2, CityName = "Aarau", StateName = "Aargau", CountryCode = "CH"},
                new StateAndCountry() {CityId = 3, CityName = "Affoltern am Albis", StateName = "Zürich", CountryCode = "CH"},
                new StateAndCountry() {CityId = 4, CityName = "Baden", StateName = "Aargau", CountryCode = "CH"},
                new StateAndCountry() {CityId = 5, CityName = "Basel", StateName = "Basel", CountryCode = "CH"},
                new StateAndCountry() {CityId = 6, CityName = "Bern", StateName = "Bern", CountryCode = "CH"},
                new StateAndCountry() {CityId = 7, CityName = "Brig", StateName = "Wallis", CountryCode = "CH"},
                new StateAndCountry() {CityId = 8, CityName = "Burgdorf", StateName = "Bern", CountryCode = "CH"},
                new StateAndCountry() {CityId = 9, CityName = "Chur", StateName = "Graubünden", CountryCode = "CH"},
                new StateAndCountry() {CityId = 10, CityName = "Fribourg", StateName = "Fribourg", CountryCode = "CH"},
                new StateAndCountry() {CityId = 11, CityName = "Genève", StateName = "Genf", CountryCode = "CH"},
                new StateAndCountry() {CityId = 12, CityName = "Horgen", StateName = "Zürich", CountryCode = "CH"},
                new StateAndCountry() {CityId = 13, CityName = "Kreuzlingen", StateName = "Thurgau", CountryCode = "CH"},
                new StateAndCountry() {CityId = 14, CityName = "Lausanne", StateName = "Waadt", CountryCode = "CH"},
                new StateAndCountry() {CityId = 15, CityName = "Lenzburg", StateName = "Aargau", CountryCode = "CH"},
                new StateAndCountry() {CityId = 16, CityName = "Liestal", StateName = "Basel-Landschaft", CountryCode = "CH"},
                new StateAndCountry() {CityId = 17, CityName = "Lugano", StateName = "Tessin", CountryCode = "CH"},
                new StateAndCountry() {CityId = 18, CityName = "Luzern", StateName = "Luzern", CountryCode = "CH"},
                new StateAndCountry() {CityId = 19, CityName = "Meilen", StateName = "Zürich", CountryCode = "CH"},
                new StateAndCountry() {CityId = 20, CityName = "Neuchâtel", StateName = "Neuenburg", CountryCode = "CH"},
                new StateAndCountry() {CityId = 21, CityName = "Olten", StateName = "Solothurn", CountryCode = "CH"},
                new StateAndCountry() {CityId = 22, CityName = "Rapperswil", StateName = "St. Gallen", CountryCode = "CH"},
                new StateAndCountry() {CityId = 23, CityName = "Rheinfelden", StateName = "Basel-Landschaft", CountryCode = "CH"},
                new StateAndCountry() {CityId = 24, CityName = "Schaffhausen", StateName = "Schaffhausen", CountryCode = "CH"},
                new StateAndCountry() {CityId = 25, CityName = "Sion", StateName = "Wallis", CountryCode = "CH"},
                new StateAndCountry() {CityId = 26, CityName = "Solothurn", StateName = "Solothurn", CountryCode = "CH"},
                new StateAndCountry() {CityId = 27, CityName = "St. Gallen", StateName = "St. Gallen", CountryCode = "CH"},
                new StateAndCountry() {CityId = 28, CityName = "Sursee", StateName = "Luzern", CountryCode = "CH"},
                new StateAndCountry() {CityId = 29, CityName = "Thun", StateName = "Bern", CountryCode = "CH"},
                new StateAndCountry() {CityId = 30, CityName = "Uster", StateName = "Zürich", CountryCode = "CH"},
                new StateAndCountry() {CityId = 31, CityName = "Winterthur", StateName = "Zürich", CountryCode = "CH"},
                new StateAndCountry() {CityId = 32, CityName = "Zug", StateName = "Zug", CountryCode = "CH"},
                new StateAndCountry() {CityId = 33, CityName = "München", StateName = "Bayern", CountryCode = "DE"},
                new StateAndCountry() {CityId = 34, CityName = "Düsseldorf", StateName = "Nordrhein-Westfalen", CountryCode = "DE"},
                new StateAndCountry() {CityId = 35, CityName = "Frankfurt am Main", StateName = "Hessen", CountryCode = "DE"},
                new StateAndCountry() {CityId = 36, CityName = "Nürnberg", StateName = "Bayer", CountryCode = "DE"},
                new StateAndCountry() {CityId = 37, CityName = "Lörrach", StateName = "Baden-Württemberg", CountryCode = "DE"}
            };
            return list;
        }
    }
}
