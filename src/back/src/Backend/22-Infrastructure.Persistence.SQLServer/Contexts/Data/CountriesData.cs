using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.SQLServer.Contexts.Data
{
    public static class CountriesData
    {
        public static IEnumerable<CountryDao> Countries =>
            [
                new CountryDao
                {
                    Id = 1,
                    CodeIso = "AF",
                    Name = "Afghanistan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 2,
                    CodeIso = "ZA",
                    Name = "Afrique du Sud",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 3,
                    CodeIso = "AL",
                    Name = "Albanie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 4,
                    CodeIso = "DZ",
                    Name = "Algérie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 5,
                    CodeIso = "DE",
                    Name = "Allemagne",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 6,
                    CodeIso = "AD",
                    Name = "Andorre",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 7,
                    CodeIso = "AO",
                    Name = "Angola",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 8,
                    CodeIso = "AI",
                    Name = "Anguilla",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 9,
                    CodeIso = "AQ",
                    Name = "Antarctique",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 10,
                    CodeIso = "AG",
                    Name = "Antigua-et-Barbuda",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 11,
                    CodeIso = "SA",
                    Name = "Arabie saoudite",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 12,
                    CodeIso = "AR",
                    Name = "Argentine",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 13,
                    CodeIso = "AM",
                    Name = "Arménie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 14,
                    CodeIso = "AW",
                    Name = "Aruba",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 15,
                    CodeIso = "AU",
                    Name = "Australie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 16,
                    CodeIso = "AT",
                    Name = "Autriche",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 17,
                    CodeIso = "AZ",
                    Name = "Azerbaïdjan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 18,
                    CodeIso = "BS",
                    Name = "Bahamas",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 19,
                    CodeIso = "BH",
                    Name = "Bahreïn",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 20,
                    CodeIso = "BD",
                    Name = "Bangladesh",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 21,
                    CodeIso = "BB",
                    Name = "Barbade",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 22,
                    CodeIso = "BE",
                    Name = "Belgique",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 23,
                    CodeIso = "BZ",
                    Name = "Belize",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 24,
                    CodeIso = "BJ",
                    Name = "Bénin",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 25,
                    CodeIso = "BM",
                    Name = "Bermudes",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 26,
                    CodeIso = "BT",
                    Name = "Bhoutan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 27,
                    CodeIso = "BY",
                    Name = "Biélorussie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 28,
                    CodeIso = "BO",
                    Name = "Bolivie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 29,
                    CodeIso = "BA",
                    Name = "Bosnie-Herzégovine",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 30,
                    CodeIso = "BW",
                    Name = "Botswana",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 31,
                    CodeIso = "BR",
                    Name = "Brésil",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 32,
                    CodeIso = "BN",
                    Name = "Brunei",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 33,
                    CodeIso = "BG",
                    Name = "Bulgarie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 34,
                    CodeIso = "BF",
                    Name = "Burkina Faso",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 35,
                    CodeIso = "BI",
                    Name = "Burundi",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 36,
                    CodeIso = "KH",
                    Name = "Cambodge",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 37,
                    CodeIso = "CM",
                    Name = "Cameroun",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 38,
                    CodeIso = "CA",
                    Name = "Canada",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 39,
                    CodeIso = "CV",
                    Name = "Cap-Vert",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 40,
                    CodeIso = "CL",
                    Name = "Chili",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 41,
                    CodeIso = "CN",
                    Name = "Chine",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 42,
                    CodeIso = "CY",
                    Name = "Chypre",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 43,
                    CodeIso = "CO",
                    Name = "Colombie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 44,
                    CodeIso = "KM",
                    Name = "Comores",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 45,
                    CodeIso = "CG",
                    Name = "Congo",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 46,
                    CodeIso = "CD",
                    Name = "Congo (RDC)",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 47,
                    CodeIso = "KP",
                    Name = "Corée du Nord",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 48,
                    CodeIso = "KR",
                    Name = "Corée du Sud",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 49,
                    CodeIso = "CR",
                    Name = "Costa Rica",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 50,
                    CodeIso = "CI",
                    Name = "Côte d’Ivoire",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 51,
                    CodeIso = "HR",
                    Name = "Croatie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 52,
                    CodeIso = "CU",
                    Name = "Cuba",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 53,
                    CodeIso = "DK",
                    Name = "Danemark",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 54,
                    CodeIso = "DJ",
                    Name = "Djibouti",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 55,
                    CodeIso = "DM",
                    Name = "Dominique",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 56,
                    CodeIso = "EG",
                    Name = "Égypte",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 57,
                    CodeIso = "AE",
                    Name = "Émirats arabes unis",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 58,
                    CodeIso = "EC",
                    Name = "Équateur",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 59,
                    CodeIso = "ER",
                    Name = "Érythrée",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 60,
                    CodeIso = "ES",
                    Name = "Espagne",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 61,
                    CodeIso = "EE",
                    Name = "Estonie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 62,
                    CodeIso = "US",
                    Name = "États-Unis",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 63,
                    CodeIso = "ET",
                    Name = "Éthiopie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 64,
                    CodeIso = "FJ",
                    Name = "Fidji",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 65,
                    CodeIso = "FI",
                    Name = "Finlande",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 66,
                    CodeIso = "FR",
                    Name = "France",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 67,
                    CodeIso = "GA",
                    Name = "Gabon",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 68,
                    CodeIso = "GM",
                    Name = "Gambie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 69,
                    CodeIso = "GH",
                    Name = "Ghana",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 70,
                    CodeIso = "GR",
                    Name = "Grèce",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 71,
                    CodeIso = "GD",
                    Name = "Grenade",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 72,
                    CodeIso = "GT",
                    Name = "Guatemala",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 73,
                    CodeIso = "GN",
                    Name = "Guinée",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 74,
                    CodeIso = "GQ",
                    Name = "Guinée équatoriale",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 75,
                    CodeIso = "GW",
                    Name = "Guinée-Bissau",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 76,
                    CodeIso = "GY",
                    Name = "Guyana",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 77,
                    CodeIso = "HT",
                    Name = "Haïti",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 78,
                    CodeIso = "HN",
                    Name = "Honduras",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 79,
                    CodeIso = "HU",
                    Name = "Hongrie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 80,
                    CodeIso = "IN",
                    Name = "Inde",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 81,
                    CodeIso = "ID",
                    Name = "Indonésie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 82,
                    CodeIso = "IQ",
                    Name = "Irak",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 83,
                    CodeIso = "IR",
                    Name = "Iran",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 84,
                    CodeIso = "IE",
                    Name = "Irlande",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 85,
                    CodeIso = "IS",
                    Name = "Islande",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 86,
                    CodeIso = "IL",
                    Name = "Israël",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 87,
                    CodeIso = "IT",
                    Name = "Italie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 88,
                    CodeIso = "JM",
                    Name = "Jamaïque",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 89,
                    CodeIso = "JP",
                    Name = "Japon",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 90,
                    CodeIso = "JO",
                    Name = "Jordanie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 91,
                    CodeIso = "KZ",
                    Name = "Kazakhstan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 92,
                    CodeIso = "KE",
                    Name = "Kenya",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 93,
                    CodeIso = "KG",
                    Name = "Kirghizistan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 94,
                    CodeIso = "KI",
                    Name = "Kiribati",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 95,
                    CodeIso = "KW",
                    Name = "Koweït",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 96,
                    CodeIso = "LA",
                    Name = "Laos",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 97,
                    CodeIso = "LS",
                    Name = "Lesotho",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 98,
                    CodeIso = "LV",
                    Name = "Lettonie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 99,
                    CodeIso = "LB",
                    Name = "Liban",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 100,
                    CodeIso = "LR",
                    Name = "Libéria",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 101,
                    CodeIso = "LY",
                    Name = "Libye",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 102,
                    CodeIso = "LI",
                    Name = "Liechtenstein",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 103,
                    CodeIso = "LT",
                    Name = "Lituanie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 104,
                    CodeIso = "LU",
                    Name = "Luxembourg",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 105,
                    CodeIso = "MK",
                    Name = "Macédoine du Nord",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 106,
                    CodeIso = "MG",
                    Name = "Madagascar",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 107,
                    CodeIso = "MY",
                    Name = "Malaisie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 108,
                    CodeIso = "MW",
                    Name = "Malawi",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 109,
                    CodeIso = "MV",
                    Name = "Maldives",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 110,
                    CodeIso = "ML",
                    Name = "Mali",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 111,
                    CodeIso = "MT",
                    Name = "Malte",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 112,
                    CodeIso = "MA",
                    Name = "Maroc",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 113,
                    CodeIso = "MU",
                    Name = "Maurice",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 114,
                    CodeIso = "MR",
                    Name = "Mauritanie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 115,
                    CodeIso = "MX",
                    Name = "Mexique",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 116,
                    CodeIso = "FM",
                    Name = "Micronésie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 117,
                    CodeIso = "MD",
                    Name = "Moldavie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 118,
                    CodeIso = "MC",
                    Name = "Monaco",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 119,
                    CodeIso = "MN",
                    Name = "Mongolie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 120,
                    CodeIso = "ME",
                    Name = "Monténégro",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 121,
                    CodeIso = "MZ",
                    Name = "Mozambique",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 122,
                    CodeIso = "NA",
                    Name = "Namibie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 123,
                    CodeIso = "NR",
                    Name = "Nauru",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 124,
                    CodeIso = "NP",
                    Name = "Népal",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 125,
                    CodeIso = "NI",
                    Name = "Nicaragua",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 126,
                    CodeIso = "NE",
                    Name = "Niger",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 127,
                    CodeIso = "NG",
                    Name = "Nigeria",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 128,
                    CodeIso = "NO",
                    Name = "Norvège",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 129,
                    CodeIso = "NZ",
                    Name = "Nouvelle-Zélande",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 130,
                    CodeIso = "OM",
                    Name = "Oman",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 131,
                    CodeIso = "UG",
                    Name = "Ouganda",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 132,
                    CodeIso = "UZ",
                    Name = "Ouzbékistan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 133,
                    CodeIso = "PK",
                    Name = "Pakistan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 134,
                    CodeIso = "PW",
                    Name = "Palaos",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 135,
                    CodeIso = "PS",
                    Name = "Palestine",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 136,
                    CodeIso = "PA",
                    Name = "Panama",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 137,
                    CodeIso = "PG",
                    Name = "Papouasie-Nouvelle-Guinée",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 138,
                    CodeIso = "PY",
                    Name = "Paraguay",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 139,
                    CodeIso = "NL",
                    Name = "Pays-Bas",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 140,
                    CodeIso = "PE",
                    Name = "Pérou",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 141,
                    CodeIso = "PH",
                    Name = "Philippines",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 142,
                    CodeIso = "PL",
                    Name = "Pologne",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 143,
                    CodeIso = "PT",
                    Name = "Portugal",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 144,
                    CodeIso = "QA",
                    Name = "Qatar",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 145,
                    CodeIso = "RO",
                    Name = "Roumanie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 146,
                    CodeIso = "GB",
                    Name = "Royaume-Uni",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 147,
                    CodeIso = "RU",
                    Name = "Russie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 148,
                    CodeIso = "RW",
                    Name = "Rwanda",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 149,
                    CodeIso = "LC",
                    Name = "Sainte-Lucie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 150,
                    CodeIso = "VC",
                    Name = "Saint-Vincent-et-les-Grenadines",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 151,
                    CodeIso = "WS",
                    Name = "Samoa",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 152,
                    CodeIso = "SM",
                    Name = "Saint-Marin",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 153,
                    CodeIso = "ST",
                    Name = "Sao Tomé-et-Principe",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 154,
                    CodeIso = "SN",
                    Name = "Sénégal",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 155,
                    CodeIso = "RS",
                    Name = "Serbie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 156,
                    CodeIso = "SC",
                    Name = "Seychelles",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 157,
                    CodeIso = "SL",
                    Name = "Sierra Leone",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 158,
                    CodeIso = "SG",
                    Name = "Singapour",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 159,
                    CodeIso = "SK",
                    Name = "Slovaquie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 160,
                    CodeIso = "SI",
                    Name = "Slovénie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 161,
                    CodeIso = "SO",
                    Name = "Somalie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 162,
                    CodeIso = "SD",
                    Name = "Soudan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 163,
                    CodeIso = "SS",
                    Name = "Soudan du Sud",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 164,
                    CodeIso = "LK",
                    Name = "Sri Lanka",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 165,
                    CodeIso = "SE",
                    Name = "Suède",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 166,
                    CodeIso = "CH",
                    Name = "Suisse",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 167,
                    CodeIso = "SR",
                    Name = "Suriname",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 168,
                    CodeIso = "SY",
                    Name = "Syrie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 169,
                    CodeIso = "TJ",
                    Name = "Tadjikistan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 170,
                    CodeIso = "TZ",
                    Name = "Tanzanie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 171,
                    CodeIso = "TD",
                    Name = "Tchad",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 172,
                    CodeIso = "CZ",
                    Name = "Tchéquie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 173,
                    CodeIso = "TH",
                    Name = "Thaïlande",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 174,
                    CodeIso = "TL",
                    Name = "Timor oriental",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 175,
                    CodeIso = "TG",
                    Name = "Togo",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 176,
                    CodeIso = "TO",
                    Name = "Tonga",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 177,
                    CodeIso = "TT",
                    Name = "Trinité-et-Tobago",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 178,
                    CodeIso = "TN",
                    Name = "Tunisie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 179,
                    CodeIso = "TM",
                    Name = "Turkménistan",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 180,
                    CodeIso = "TR",
                    Name = "Turquie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 181,
                    CodeIso = "TV",
                    Name = "Tuvalu",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 182,
                    CodeIso = "UA",
                    Name = "Ukraine",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 183,
                    CodeIso = "UY",
                    Name = "Uruguay",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 184,
                    CodeIso = "VU",
                    Name = "Vanuatu",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 185,
                    CodeIso = "VE",
                    Name = "Venezuela",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 186,
                    CodeIso = "VN",
                    Name = "Viêt Nam",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 187,
                    CodeIso = "YE",
                    Name = "Yémen",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 188,
                    CodeIso = "ZM",
                    Name = "Zambie",
                    Coordinates = [],
                },
                new CountryDao
                {
                    Id = 189,
                    CodeIso = "ZW",
                    Name = "Zimbabwe",
                    Coordinates = [],
                },
            ];
    }
}
