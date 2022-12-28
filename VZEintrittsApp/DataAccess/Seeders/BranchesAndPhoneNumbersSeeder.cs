using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess.Seeders
{
    public static class BranchesAndPhoneNumbersSeeder
    {
        public static List<BranchesAndPhoneNumbers> GetSeeds()
        {
            var list = new List<BranchesAndPhoneNumbers>()
            {
                new BranchesAndPhoneNumbers() {BranchId = 1, City = "Zürich", Company = "Früh & Partner Vermögensverwaltung AG", BranchShortName = "F&P ZH", PhoneNumber = "+41 58 958 97 96", Office = "Zürich", FaxNumber = "+41 44 207 25 26"},
                new BranchesAndPhoneNumbers() {BranchId = 2, City = "Zürich", Company = "HypothekenZentrum AG", BranchShortName = "HZ ZH", PhoneNumber = "+41 44 563 63 63", Office = "Zürich", FaxNumber = "+41 44 563 63 64"},
                new BranchesAndPhoneNumbers() {BranchId = 3, City = "Lausanne", Company = "VZ Insurance Services SA", BranchShortName = "IS LS", PhoneNumber = "+41 21 341 30 58", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 4, City = "Zug", Company = "VZ Insurance Services AG", BranchShortName = "IS ZG", PhoneNumber = "+41 44 207 25 27", Office = "Zug", FaxNumber = "+41 44 207 24 44"},
                new BranchesAndPhoneNumbers() {BranchId = 5, City = "Zürich", Company = "VZ Insurance Services AG", BranchShortName = "IS ZH", PhoneNumber = "+41 44 207 25 41", Office = "Zürich", FaxNumber = "+41 44 207 24 44"},
                new BranchesAndPhoneNumbers() {BranchId = 6, City = "Bern", Company = "VZ Vorsorge AG", BranchShortName = "VoSo BE", PhoneNumber = "+41 31 329 28 05", Office = "Bern", FaxNumber = "+41 31 329 28 88"},
                new BranchesAndPhoneNumbers() {BranchId = 7, City = "Lausanne", Company = "VZ Prévoyance SA", BranchShortName = "VoSo LS", PhoneNumber = "+41 21 341 30 78", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 8, City = "Zürich", Company = "VZ Vorsorge AG", BranchShortName = "VoSo ZH", PhoneNumber = "+41 44 207 27 26", Office = "Zürich", FaxNumber = "+41 44 207 29 29"},
                new BranchesAndPhoneNumbers() {BranchId = 9, City = "Lausanne", Company = "VZ Corporate Services AG", BranchShortName = "VZ CS LS", PhoneNumber = "+41 21 341 30 30", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 10, City = "Zürich", Company = "VZ Corporate Services AG", BranchShortName = "VZ CS ZH", PhoneNumber = "+41 44 207 27 27", Office = "Zürich", FaxNumber = "+41 44 207 27 28"},
                new BranchesAndPhoneNumbers() {BranchId = 11, City = "Lausanne", Company = "VZ Banque de Dèpôt", BranchShortName = "VZ DB LS", PhoneNumber = "+41 58 411 88 66", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 12, City = "Zug", Company = "VZ Depotbank AG", BranchShortName = "VZ DB ZG", PhoneNumber = "+41 58 411 88 88", Office = "Zug", FaxNumber = "+41 58 411 80 81"},
                new BranchesAndPhoneNumbers() {BranchId = 13, City = "Düsseldorf", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE DU", PhoneNumber = "+49 211 54 00 56-00", Office = "Düsseldorf", FaxNumber = "+49 211 5400 5610"},
                new BranchesAndPhoneNumbers() {BranchId = 14, City = "Frankfurt am Main", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE FF", PhoneNumber = "+49 69 50 50 948-0", Office = "Frankfurt", FaxNumber = "+49 69 50 50 948 1"},
                new BranchesAndPhoneNumbers() {BranchId = 15, City = "München", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE MU", PhoneNumber = "+49 89 288 117-0", Office = "München", FaxNumber = "+49 89 288 117-10"},
                new BranchesAndPhoneNumbers() {BranchId = 16, City = "Nürnberg", Company = "VZ VermögensZentrum Bank AG", BranchShortName = "VZ DE NU", PhoneNumber = "+49 911 881 889-0", Office = "Nürnberg", FaxNumber = "+49 911 881 889 10"},
                new BranchesAndPhoneNumbers() {BranchId = 17, City = "Zug", Company = "VZ Holding AG", BranchShortName = "VZ Hold ZH", PhoneNumber = "+41 44 207 27 27", Office = "Zug", FaxNumber = "+41 44 207 27 28"},
                new BranchesAndPhoneNumbers() {BranchId = 18, City = "Zürich", Company = "VZ Operations AG", BranchShortName = "VZ OP ZH", PhoneNumber = "+41 58 411 80 80", Office = "Zürich", FaxNumber = "+41 58 411 80 81"},
                new BranchesAndPhoneNumbers() {BranchId = 19, City = "Lausanne", Company = "VZ Rechts-und Steuerberatung AG", BranchShortName = "VZ RS LS", PhoneNumber = "+41 21 341 30 30", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 20, City = "Luzern", Company = "VZ Rechts-und Steuerberatung AG", BranchShortName = "VZ RS LU", PhoneNumber = "+41 41 220 70 80", Office = "Luzern", FaxNumber = "+41 44 207 27 21"},
                new BranchesAndPhoneNumbers() {BranchId = 21, City = "Zürich", Company = "VZ Rechts-und Steuerberatung AG", BranchShortName = "VZ RS ZH", PhoneNumber = "+41 41 207 27 27", Office = "Zürich", FaxNumber = "+41 44 207 27 28"},
                new BranchesAndPhoneNumbers() {BranchId = 22, City = "London", Company = "VZ Investmend Reserach Ltd", BranchShortName = "VZ UK", PhoneNumber = "+41 44 207 27 27", Office = "London", FaxNumber = "+41 44 207 27 28"},
                new BranchesAndPhoneNumbers() {BranchId = 23, City = "Zürich", Company = "VZ VersicherungsPool AG", BranchShortName = "VZ VP ZH", PhoneNumber = "+41 58 344 23 08", Office = "Zürich", FaxNumber = "+41 58 344 20 01"},
                new BranchesAndPhoneNumbers() {BranchId = 24, City = "Lausanne", Company = "VZ VersicherungsZentrum AG", BranchShortName = "VZe Lausanne", PhoneNumber = "+41 21 341 30 70", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 25, City = "Zürich", Company = "VZ VersicherungsZentrum AG", BranchShortName = "VZe ZH", PhoneNumber = "+41 44 207 20 20", Office = "Zürich", FaxNumber = "+41 44 207 27 37"},
                new BranchesAndPhoneNumbers() {BranchId = 26, City = "Aarau", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö AA", PhoneNumber = "+41 62 825 28 28", Office = "Aarau", FaxNumber = "+41 62 825 28 30"},
                new BranchesAndPhoneNumbers() {BranchId = 27, City = "Affoltern am Albis", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Affoltern a. A.", PhoneNumber = "+41 44 403 77 77", Office = "Affoltern am Albis", FaxNumber = "+41 44 403 77 78"},
                new BranchesAndPhoneNumbers() {BranchId = 28, City = "Baden", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Baden", PhoneNumber = "+41 56 204 42 42", Office = "Baden", FaxNumber = "+41 56 204 42 43"},
                new BranchesAndPhoneNumbers() {BranchId = 29, City = "Burgdorf", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö BD", PhoneNumber = "+41 34 420 23 23", Office = "Burgdorf", FaxNumber = "+41 34 420 23 24"},
                new BranchesAndPhoneNumbers() {BranchId = 30, City = "Bern", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö BE", PhoneNumber = "+41 31 329 26 26", Office = "Bern", FaxNumber = "+41 31 329 26 27"},
                new BranchesAndPhoneNumbers() {BranchId = 31, City = "Brig", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Brig", PhoneNumber = "+41 62 825 28 28", Office = "Brig", FaxNumber = "+41 27 921 12 22"},
                new BranchesAndPhoneNumbers() {BranchId = 32, City = "Basel", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö BS", PhoneNumber = "+41 61 279 89 89", Office = "Basel", FaxNumber = "+41 61 279 89 90"},
                new BranchesAndPhoneNumbers() {BranchId = 33, City = "Chur", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Chur", PhoneNumber = "+41 81 286 81 81", Office = "Chur", FaxNumber = "+41 81 286 81 82"},
                new BranchesAndPhoneNumbers() {BranchId = 34, City = "Fribourg", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö FR", PhoneNumber = "+41 26 350 90 90", Office = "Fribourg", FaxNumber = "+41 26 350 90 91"},
                new BranchesAndPhoneNumbers() {BranchId = 35, City = "Genève", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö GE", PhoneNumber = "+41 62 825 28 28", Office = "Genève", FaxNumber = "+41 22 595 15 16"},
                new BranchesAndPhoneNumbers() {BranchId = 36, City = "Horgen", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö HO", PhoneNumber = "+41 43 430 36 36", Office = "Horgen", FaxNumber = "+41 43 430 36 37"},
                new BranchesAndPhoneNumbers() {BranchId = 37, City = "Kreuzlingen", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Kreuzlingen", PhoneNumber = "+41 71 678 33 33", Office = "Kreuzlingen", FaxNumber = "+41 71 678 33 34"},
                new BranchesAndPhoneNumbers() {BranchId = 38, City = "Lausanne", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Lausanne", PhoneNumber = "+41 21 341 30 30", Office = "Lausanne", FaxNumber = "+41 21 341 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 39, City = "Lenzburg", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö LB", PhoneNumber = "+41 62 888 38 38", Office = "Lenzburg", FaxNumber = "+41 62 888 38 39"},
                new BranchesAndPhoneNumbers() {BranchId = 40, City = "Lugano", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö LG", PhoneNumber = "+41 91 912 24 13", Office = "Lugano", FaxNumber = "+41 91 912 24 25"},
                new BranchesAndPhoneNumbers() {BranchId = 41, City = "Liestal", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Liestal", PhoneNumber = "+41 61 925 79 79", Office = "Liestal", FaxNumber = "+41 61 925 79 80"},
                new BranchesAndPhoneNumbers() {BranchId = 42, City = "Luzern", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö LU", PhoneNumber = "+41 41 220 70 70", Office = "Luzern", FaxNumber = "+41 41 220 70 71"},
                new BranchesAndPhoneNumbers() {BranchId = 43, City = "Meilen", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Meilen", PhoneNumber = "+41 43 430 00 00", Office = "Meilen", FaxNumber = "+41 43 430 00 01"},
                new BranchesAndPhoneNumbers() {BranchId = 44, City = "Neuchâtel", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö NE", PhoneNumber = "+41 32 854 04 04", Office = "Neuenburg", FaxNumber = "+41 32 854 04 05"},
                new BranchesAndPhoneNumbers() {BranchId = 45, City = "Olten", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Olten", PhoneNumber = "+41 62 286 86 86", Office = "Olten", FaxNumber = "+41 62 286 86 87"},
                new BranchesAndPhoneNumbers() {BranchId = 46, City = "Rapperswil", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Rapperswil", PhoneNumber = "+41 55 222 04 04", Office = "Rapperswil", FaxNumber = "+41 55 222 04 05"},
                new BranchesAndPhoneNumbers() {BranchId = 47, City = "Rheinfelden", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Rheinfelden", PhoneNumber = "+41 61 564 88 88", Office = "Rheinfelden", FaxNumber = "+41 61 564 88 89"},
                new BranchesAndPhoneNumbers() {BranchId = 48, City = "St. Gallen", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö SG", PhoneNumber = "+41 71 231 18 18", Office = "St. Gallen", FaxNumber = "+41 71 231 18 19"},
                new BranchesAndPhoneNumbers() {BranchId = 49, City = "Solothurn", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö SO", PhoneNumber = "+41 32 560 30 30", Office = "Solothurn", FaxNumber = "+41 32 560 30 31"},
                new BranchesAndPhoneNumbers() {BranchId = 50, City = "Sursee", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Sursee", PhoneNumber = "+41 41 924 10 10", Office = "Sursee", FaxNumber = "+41 41 924 10 11"},
                new BranchesAndPhoneNumbers() {BranchId = 51, City = "Thun", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Thun", PhoneNumber = "+41 33 252 22 22", Office = "Thun", FaxNumber = "+41 33 252 22 23"},
                new BranchesAndPhoneNumbers() {BranchId = 52, City = "Uster", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Uster", PhoneNumber = "+41 44 905 27 27", Office = "Uster", FaxNumber = "+41 44 905 27 28"},
                new BranchesAndPhoneNumbers() {BranchId = 53, City = "Wintherthur", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö Winterthur", PhoneNumber = "+41 52 218 18 18", Office = "Winterhur", FaxNumber = "+41 52 218 18 19"},
                new BranchesAndPhoneNumbers() {BranchId = 54, City = "Zug", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö ZG", PhoneNumber = "+41 41 729 11 11", Office = "Zug", FaxNumber = "+41 41 726 11 12"},
                new BranchesAndPhoneNumbers() {BranchId = 55, City = "Zürich", Company = "VZ VermögensZentrum AG", BranchShortName = "VZö ZH", PhoneNumber = "+41 44 207 27 27", Office = "Zürich", FaxNumber = "+41 44 207 27 28"}
            };
            return list;
        }

    }
}
