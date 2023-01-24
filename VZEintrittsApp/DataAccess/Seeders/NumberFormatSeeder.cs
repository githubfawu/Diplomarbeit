using System.Collections.Generic;
using VZEintrittsApp.Model.Domain;


namespace VZEintrittsApp.DataAccess.Seeders
{
    public static class NumberFormatSeeder
    {
        public static List<NumberFormat> GetSeeds()
        {
            var list = new List<NumberFormat>()
            {
                new NumberFormat() { CityName = "Aarau", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Affoltern am Albis", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Baden", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Basel", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Bern", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Brig", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Burgdorf", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Chur", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Düsseldorf", Formats = new Dictionary<int, string>() {{2, " "}, {6, " "}, {9, " "}, {12, " "}, {15, "-"}}},
                new NumberFormat() { CityName = "Frankfurt am Main", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {8, " "}, {11, " "}, {15, "-"}}},
                new NumberFormat() { CityName = "Fribourg", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Genève", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Horgen", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Kreuzlingen", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Lausanne", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Lenzburg", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Liestal", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "London", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {10, " "}, {13, " "}}},
                new NumberFormat() { CityName = "Lugano", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Luzern", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Meilen", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "München", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}, {15, " "}}},
                new NumberFormat() { CityName = "Neuchâtel", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Nürnberg", Formats = new Dictionary<int, string>() {{2, " "}, {6, " "}, {10, " "}, {14, "-"}}},
                new NumberFormat() { CityName = "Olten", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Rapperswil", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Rheinfelden", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Solothurn", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "St. Gallen", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Sursee", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Thun", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Uster", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Wintherthur", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Zug", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}},
                new NumberFormat() { CityName = "Zürich", Formats = new Dictionary<int, string>() {{2, " "}, {5, " "}, {9, " "}, {12, " "}}}
            };
            return list;
        }
    }
}
