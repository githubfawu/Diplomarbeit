using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public static class BranchAndPhoneSeeder
    {
        public static List<BranchAndPhone> GetSeeds()
        {
            var list = new List<BranchAndPhone>()
            {
                new BranchAndPhone() {BranchId = 1, City = "Zürich", Company = "Früh & Partner Vermögensverwaltung AG", BranchShortName = "F&P ZH", PhoneNumber = "+41 58 958 97 96", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 2, City = "Zürich", Company = "HypothekenZentrum AG", BranchShortName = "HZ ZH", PhoneNumber = "+41 44 563 63 63", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 3, City = "Lausanne", Company = "VZ Insurance Services SA", BranchShortName = "IS LS", PhoneNumber = "+41 21 341 30 58", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 4, City = "Zug", Company = "VZ Insurance Services AG", BranchShortName = "IS ZG", PhoneNumber = "+41 44 207 25 27", Office = "Zug"},
                new BranchAndPhone() {BranchId = 5, City = "Zürich", Company = "VZ Insurance Services AG", BranchShortName = "IS ZH", PhoneNumber = "+41 44 207 25 41", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 6, City = "Bern", Company = "VZ Vorsorge AG", BranchShortName = "VoSo BE", PhoneNumber = "+41 31 329 28 05", Office = "Bern"},
                new BranchAndPhone() {BranchId = 7, City = "Lausanne", Company = "VZ Prévoyance SA", BranchShortName = "VoSo LS", PhoneNumber = "+41 21 341 30 78", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 8, City = "Zürich", Company = "VZ Vorsorge AG", BranchShortName = "VoSo ZH", PhoneNumber = "+41 44 207 27 26", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 9, City = "Lausanne", Company = "VZ Corporate Services AG", BranchShortName = "VZ CS LS", PhoneNumber = "+41 21 341 30 30", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 10, City = "Zürich", Company = "VZ Corporate Services AG", BranchShortName = "VZ CS ZH", PhoneNumber = "+41 44 207 27 27", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 11, City = "Lausanne", Company = "VZ Banque de Dèpôt", BranchShortName = "VZ DB LS", PhoneNumber = "+41 58 411 88 66", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 12, City = "Zug", Company = "VZ Depotbank AG", BranchShortName = "VZ DB ZG", PhoneNumber = "+41 58 411 88 88", Office = "Zug"},
                new BranchAndPhone() {BranchId = 13, City = "Düsseldorf", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE DU", PhoneNumber = "+49 211 54 00 56-00", Office = "Düsseldorf"},
                new BranchAndPhone() {BranchId = 14, City = "Frankfurt am Main", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE FF", PhoneNumber = "+49 69 50 50 948-0", Office = "Frankfurt"},
                new BranchAndPhone() {BranchId = 15, City = "München", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE MU", PhoneNumber = "+49 89 288 117-0", Office = "München"},
                new BranchAndPhone() {BranchId = 16, City = "Nürnberg", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE NU", PhoneNumber = "+49 911 881 889-0", Office = "Nürnberg"},
                new BranchAndPhone() {BranchId = 17, City = "Zug", Company = "VZ Holding AG", BranchShortName = "VZ Hold ZH", PhoneNumber = "+41 44 207 27 27", Office = "Zug"},
                new BranchAndPhone() {BranchId = 18, City = "Zürich", Company = "VZ Operations AG", BranchShortName = "VZ OP ZH", PhoneNumber = "+41 58 411 80 80", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 19, City = "Lausanne", Company = "VZ Rechts-und Steuerberatung AG", BranchShortName = "VZ RS LS", PhoneNumber = "+41 21 341 30 30", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 20, City = "Luzern", Company = "VZ Rechts-und Steuerberatung AG", BranchShortName = "VZ RS LU", PhoneNumber = "+41 41 220 70 80", Office = "Luzern"},
                new BranchAndPhone() {BranchId = 21, City = "Zürich", Company = "VZ Rechts-und Steuerberatung AG", BranchShortName = "VZ RS ZH", PhoneNumber = "+41 41 207 27 27", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 22, City = "London", Company = "VZ Investmend Reserach Ltd", BranchShortName = "VZ UK", PhoneNumber = "+41 44 207 27 27", Office = "London"},
                new BranchAndPhone() {BranchId = 23, City = "Zürich", Company = "VZ VersicherungsPool AG", BranchShortName = "VZ VP ZH", PhoneNumber = "+41 58 344 23 08", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 24, City = "Lausanne", Company = "VZ VersicherungsZentrum AG", BranchShortName = "VZe Lausanne", PhoneNumber = "+41 21 341 30 70", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 25, City = "Zürich", Company = "VZ VersicherungsZentrum AG", BranchShortName = "VZe ZH", PhoneNumber = "+41 44 207 20 20", Office = "Zürich"},
                new BranchAndPhone() {BranchId = 26, City = "Lausanne", Company = "VZ VersicherungsZentrum AG", BranchShortName = "VZe Lausanne", PhoneNumber = "+41 21 341 30 70", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 27, City = "Aarau", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö AA", PhoneNumber = "+41 62 825 28 28", Office = "Aarau"},
                new BranchAndPhone() {BranchId = 28, City = "Affoltern am Albis", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Affoltern a. A.", PhoneNumber = "+41 44 403 77 77", Office = "Affoltern am Albis"},
                new BranchAndPhone() {BranchId = 29, City = "Baden", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Baden", PhoneNumber = "+41 56 204 42 42", Office = "Baden"},
                new BranchAndPhone() {BranchId = 30, City = "Burgdorf", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö BD", PhoneNumber = "+41 34 420 23 23", Office = "Burgdorf"},
                new BranchAndPhone() {BranchId = 31, City = "Bern", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö BE", PhoneNumber = "+41 31 329 26 26", Office = "Bern"},
                new BranchAndPhone() {BranchId = 32, City = "Brig", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Brig", PhoneNumber = "+41 62 825 28 28", Office = "Brig"},
                new BranchAndPhone() {BranchId = 33, City = "Basel", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö BS", PhoneNumber = "+41 61 279 89 89", Office = "Basel"},
                new BranchAndPhone() {BranchId = 34, City = "Chur", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Chur", PhoneNumber = "+41 81 286 81 81", Office = "Chur"},
                new BranchAndPhone() {BranchId = 35, City = "Fribourg", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö FR", PhoneNumber = "+41 26 350 90 90", Office = "Fribourg"},
                new BranchAndPhone() {BranchId = 36, City = "Genève", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö GE", PhoneNumber = "+41 62 825 28 28", Office = "Genève"},
                new BranchAndPhone() {BranchId = 37, City = "Horgen", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö HO", PhoneNumber = "+41 43 430 36 36", Office = "Horgen"},
                new BranchAndPhone() {BranchId = 38, City = "Kreuzlingen", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Kreuzlingen", PhoneNumber = "+41 71 678 33 33", Office = "Kreuzlingen"},
                new BranchAndPhone() {BranchId = 39, City = "Lausanne", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Lausanne", PhoneNumber = "+41 21 341 30 30", Office = "Lausanne"},
                new BranchAndPhone() {BranchId = 40, City = "Lenzburg", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö LB", PhoneNumber = "+41 62 888 38 38", Office = "Lenzburg"},
                new BranchAndPhone() {BranchId = 41, City = "Liestal", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Liestal", PhoneNumber = "+41 61 925 79 79", Office = "Liestal"},
                new BranchAndPhone() {BranchId = 42, City = "Lugano", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö LG", PhoneNumber = "+41 91 912 24 13", Office = "Lugano"},
                new BranchAndPhone() {BranchId = 43, City = "Luzern", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö LU", PhoneNumber = "+41 41 220 70 70", Office = "Luzern"},
                new BranchAndPhone() {BranchId = 44, City = "Meilen", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Meilen", PhoneNumber = "+41 43 430 00 00", Office = "Meilen"},
                new BranchAndPhone() {BranchId = 45, City = "Neuchâtel", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö NE", PhoneNumber = "+41 32 854 04 04", Office = "Neuenburg"},
                new BranchAndPhone() {BranchId = 46, City = "Olten", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Olten", PhoneNumber = "+41 62 286 86 86", Office = "Olten"},
                new BranchAndPhone() {BranchId = 47, City = "Rapperswil", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Rapperswil", PhoneNumber = "+41 55 222 04 04", Office = "Rapperswil"},
                new BranchAndPhone() {BranchId = 48, City = "Rheinfelden", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Rheinfelden", PhoneNumber = "+41 61 564 88 88", Office = "Rheinfelden"},
                new BranchAndPhone() {BranchId = 49, City = "St. Gallen", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö SG", PhoneNumber = "+41 71 231 18 18", Office = "St. Gallen"},
                new BranchAndPhone() {BranchId = 50, City = "Solothurn", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö SO", PhoneNumber = "+41 32 560 30 30", Office = "Solothurn"},
                new BranchAndPhone() {BranchId = 51, City = "Sursee", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Sursee", PhoneNumber = "+41 41 924 10 10", Office = "Sursee"},
                new BranchAndPhone() {BranchId = 52, City = "Thun", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Thun", PhoneNumber = "+41 33 252 22 22", Office = "Thun"},
                new BranchAndPhone() {BranchId = 53, City = "Uster", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Uster", PhoneNumber = "+41 44 905 27 27", Office = "Uster"},
                new BranchAndPhone() {BranchId = 54, City = "Wintherthur", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö Winterthur", PhoneNumber = "+41 52 218 18 18", Office = "Winterhur"},
                new BranchAndPhone() {BranchId = 55, City = "Zug", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö ZG", PhoneNumber = "+41 41 729 11 11", Office = "Zug"},
                new BranchAndPhone() {BranchId = 56, City = "Zürich", Company = "VZ Vermögenszentrum AG", BranchShortName = "VZö ZH", PhoneNumber = "+41 44 207 27 27", Office = "Zürich"}
            };
            return list;
        }

    }
}
