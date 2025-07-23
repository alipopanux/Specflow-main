using Lopcommerce.ApiBack;
using Lopcommerce.Regles.Models.Constantes;

namespace Lopcommerce.Regles.WebAPI.Tests.Extensions
{
    public static class StringExtensions
    {
        public static ConventionCollective? MapConventionCollective(this string conventionCollectiveId)
        {
            return conventionCollectiveId switch
            {
                "1" => new ConventionCollective
                {
                    Id = "1",
                    IdExterne = "0016",
                    Libelle = "Convention collective nationale des transports routiers et activités auxiliaires du transport",
                    Code = "0016"
                },
                "2" => new ConventionCollective
                {
                    Id = "2",
                    IdExterne = "0018",
                    Libelle = "Convention collective nationale des industries textiles",
                    Code = "0018"
                },
                "3" => new ConventionCollective
                {
                    Id = "3",
                    IdExterne = "0029",
                    Libelle = "Convention collective nationale des établissements privés d'hospitalisation, de soins, de cure et de garde à but non lucratif (FEHAP, convention de 1951)",
                    Code = "0029"
                },
                "4" => new ConventionCollective
                {
                    Id = "4",
                    IdExterne = "0043",
                    Libelle = "Convention collective nationale de l'Import-export et du Commerce international",
                    Code = "0043"
                },
                "183" => new ConventionCollective
                {
                    Id = "183",
                    IdExterne = "1557",
                    Libelle = "Convention collective nationale du commerce des articles de sports et d'équipements de loisirs (fusion entre la 1557 et la 1618)",
                    Code = "1557"
                },
                _ => null,
            };
        }

        public static CodeNaf? MapNaf(this string nafId)
        {
            return nafId switch
            {
                "2" => new CodeNaf
                {
                    Id = "2",
                    IdExterne = "0111Z",
                    Libelle = "Culture de céréales (sf riz) légumineuses, graines oléagineuses",
                    Code = "0111Z"
                },
                "1" => new CodeNaf
                {
                    Id = "1",
                    IdExterne = "0000",
                    Libelle = "Naf Inconnu",
                    Code = "0000"
                },
                "869" => new CodeNaf
                {
                    Id = "869",
                    IdExterne = "4764Z",
                    Libelle = "Commerce de détail d'articles de sport en magasin spécialisé",
                    Code = "4764Z"
                },
                _ => null,
            };
        }

        public static TypeAdherent? MapTypeAdherent(this string typeId)
        {
            return typeId switch
            {
                "15" => new TypeAdherent
                {
                    Id = "15",
                    IdExterne = "33",
                    Libelle = "Autre employeur public",
                    Code = "29"
                },
                "16" => new TypeAdherent
                {
                    Id = "16",
                    IdExterne = "84",
                    Libelle = "établissement public industriel et commercial",
                    Code = "30"
                },
                "1" => new TypeAdherent
                {
                    Id = "1",
                    IdExterne = "19",
                    Libelle = "Entreprise inscrite au répertoire des métiers ou au registre des entreprises pour l'Alsace-Moselle",
                    Code = "11"
                },
                "2" => new TypeAdherent
                {
                    Id = "2",
                    IdExterne = "20",
                    Libelle = "Entreprise inscrite uniquement au registre du commerce et des sociétés",
                    Code = "12"
                },
                "3" => new TypeAdherent
                {
                    Id = "3",
                    IdExterne = "21",
                    Libelle = "Entreprises dont les salariés relèvent de la mutualité sociale agricole",
                    Code = "13"
                },
                "4" => new TypeAdherent
                {
                    Id = "4",
                    IdExterne = "22",
                    Libelle = "Profession libérale",
                    Code = "14"
                },
                "5" => new TypeAdherent
                {
                    Id = "5",
                    IdExterne = "23",
                    Libelle = "Association",
                    Code = "15"
                },
                "6" => new TypeAdherent
                {
                    Id = "6",
                    IdExterne = "24",
                    Libelle = "Autre employeur privé",
                    Code = "16"
                },
                _ => null,
            };
        }

        public static SpecificiteAdherent? MapSpecificiteAdherent(this string typeSpecifiqueId)
        {
            return typeSpecifiqueId switch
            {
                "1" => new SpecificiteAdherent
                {
                    Id = "1",
                    IdExterne = "34",
                    Libelle = "Entreprise de travail temporaire",
                    Code = "1"
                },
                "2" => new SpecificiteAdherent
                {
                    Id = "2",
                    IdExterne = "35",
                    Libelle = "Groupement d'employeurs",
                    Code = "2"
                },
                "3" => new SpecificiteAdherent
                {
                    Id = "3",
                    IdExterne = "36",
                    Libelle = "Employeur saisonnier",
                    Code = "3"
                },
                "5" => new SpecificiteAdherent
                {
                    Id = "5",
                    IdExterne = "38",
                    Libelle = "Aucun de ces cas",
                    Code = "0"
                },
                _ => null,
            };
        }

        public static SituationAvantContrat? MapSituationAvantContrat(this string situationAvantContratId)
        {
            return situationAvantContratId switch
            {
                "1" => new SituationAvantContrat
                {
                    Id = "1",
                    IdExterne = "1",
                    Libelle = "1 - Scolaire",
                    Code = "1"
                },
                "2" => new SituationAvantContrat
                {
                    Id = "2",
                    IdExterne = "2",
                    Libelle = "2 - PréApprentissage",
                    Code = "2"
                },
                "24" => new SituationAvantContrat
                {
                    Id = "24",
                    IdExterne = "24",
                    Libelle = "4 - Contrat d?apprentissage",
                    Code = "4"
                },
                "28" => new SituationAvantContrat
                {
                    Id = "28",
                    IdExterne = "28",
                    Libelle = "8 - En formation, au CFA sans contrat sous statut de stagiaire de la formation professionnelle, suite à rupture (5° de L6231-2 du code du travail)",
                    Code = "8"
                },
                "14" => new SituationAvantContrat
                {
                    Id = "14",
                    IdExterne = "24",
                    Libelle = "4 - Contrat d’apprentissage",
                    Code = "4"
                },
                _ => null,
            };
        }

        public static Diplome? MapDiplome(this string diplomeId)
        {
            return diplomeId switch
            {
                "22" => new Diplome
                {
                    Id = "22",
                    IdExterne = "513",
                    Libelle = "13 - Aucun diplôme ni titre professionnel",
                    Code = "13"
                },
                "23" => new Diplome
                {
                    Id = "23",
                    IdExterne = "525",
                    Libelle = "25 - Diplôme national du Brevet (DNB)",
                    Code = "25"
                },
                "24" => new Diplome
                {
                    Id = "24",
                    IdExterne = "526",
                    Libelle = "26 - Certificat de formation générale",
                    Code = "26"
                },
                "21" => new Diplome
                {
                    Id = "21",
                    IdExterne = "572",
                    Libelle = "72 - Master recherche/DEA",
                    Code = "72"
                },
                "33" => new Diplome
                {
                    Id = "4",
                    IdExterne = "533",
                    Libelle = "33 - CAP",
                    Code = "33"
                },
                "34" => new Diplome
                {
                    Id = "5",
                    IdExterne = "534",
                    Libelle = "34 - BEP",
                    Code = "34"
                },
                "41" => new Diplome
                {
                    Id = "8",
                    IdExterne = "541",
                    Libelle = "41 - Baccalauréat professionnel",
                    Code = "41"
                },
                "17" => new Diplome
                {
                    Id = "17",
                    IdExterne = "563",
                    Libelle = "63 - Licence générale",
                    Code = "63"
                },
                "13" => new Diplome
                {
                    Id = "13",
                    IdExterne = "555",
                    Libelle = "55 - Diplôme Universitaire de technologie",
                    Code = "55"
                },
                _ => null,
            };
        }

        public static DerniereClasse? MapDerniereClasse(this string classeId)
        {
            return classeId switch
            {
                "8" => new DerniereClasse
                {
                    Id = "8",
                    IdExterne = "16",
                    Libelle = "L'apprenti a achevé le 1er cycle de l'enseignement secondaire (collège)",
                    Code = "40"
                },
                "7" => new DerniereClasse
                {
                    Id = "7",
                    IdExterne = "15",
                    Libelle = "L'apprenti a suivi la 3è année du cycle mais ne l'a pas validée (échec aux examens, interruption ou abandon de formation)",
                    Code = "32"
                },
                "5" => new DerniereClasse
                {
                    Id = "5",
                    IdExterne = "13",
                    Libelle = "L'apprenti a suivi la 2è année du cycle mais ne l'a pas validée (échec aux examens, interruption ou abandon de formation)",
                    Code = "22"
                },
                "3" => new DerniereClasse
                {
                    Id = "3",
                    IdExterne = "11",
                    Libelle = "L'apprenti a suivi la 1ère année du cycle mais ne l'a pas validée (échec aux examens, interruption ou abandon de formation)",
                    Code = "12"
                },
                "1" => new DerniereClasse
                {
                    Id = "1",
                    IdExterne = "9",
                    Libelle = "L'apprenti a suivi la dernière année du cycle de formation et a obtenu le diplôme ou titre",
                    Code = "1"
                },
                "6" => new DerniereClasse
                {
                    Id = "6",
                    IdExterne = "14",
                    Libelle = "L'apprenti a suivi la 3è année du cycle et l'a validée (examens réussis mais année non diplômante, cycle adapté)",
                    Code = "31"
                },
                _ => null,
            };
        }

        public static Sexe? MapSexe(this string sexeId)
        {
            return sexeId switch
            {
                "3" => new Sexe
                {
                    Id = "3",
                    IdExterne = "2",
                    Libelle = "Féminin",
                    Code = "F"
                },
                "2" => new Sexe
                {
                    Id = "2",
                    IdExterne = "1",
                    Libelle = "Masculin",
                    Code = "M"
                },
                _ => null,
            };
        }

        public static Departement? MapDepartement(this string departementId)
        {
            return departementId switch
            {
                "2" => new Departement
                {
                    Id = "2",
                    IdExterne = "02",
                    Libelle = "02 - AISNE",
                    Code = "02"
                },
                "3" => new Departement
                {
                    Id = "3",
                    IdExterne = "03",
                    Libelle = "03 - ALLIER",
                    Code = "03"
                },
                "62" => new Departement
                {
                    Id = "62",
                    IdExterne = "61",
                    Libelle = "61 - ORNE",
                    Code = "61"
                },
                "36" => new Departement
                {
                    Id = "36",
                    IdExterne = "35",
                    Libelle = "35 - ILLE ET VILAINE",
                    Code = "35"
                },
                "59" => new Departement
                {
                    Id = "59",
                    IdExterne = "59",
                    Libelle = "59 - NORD",
                    Code = "59"
                },
                "99" => new Departement
                {
                    Id = "99",
                    IdExterne = "99",
                    Libelle = "ETRANGER",
                    Code = "99"
                },
                "100" => new Departement
                {
                    Id = "100",
                    IdExterne = "974",
                    Libelle = "974 - LA REUNION",
                    Code = "974"
                },
                _ => null,
            };
        }

        public static RegimeSocial? MapRegimeSocial(this string regimeSocialId)
        {
            return regimeSocialId switch
            {
                "1" => new RegimeSocial
                {
                    Id = "1",
                    IdExterne = "47",
                    Libelle = "MSA",
                    Code = "1"
                },
                "2" => new RegimeSocial
                {
                    Id = "2",
                    IdExterne = "48",
                    Libelle = "URSSAF",
                    Code = "2"
                },
                _ => null,
            };
        }

        public static Nationalite? MapNationalite(this string nationaliteId)
        {
            return nationaliteId switch
            {
                "1" => new Nationalite
                {
                    Id = "1",
                    IdExterne = "0",
                    Libelle = "1 Française",
                    Code = "FR"
                },
                "2" => new Nationalite
                {
                    Id = "2",
                    IdExterne = "1",
                    Libelle = "2 Union Européenne",
                    Code = "UN"
                },
                "3" => new Nationalite
                {
                    Id = "3",
                    IdExterne = "2",
                    Libelle = "3 Etranger Hors Union Européenne",
                    Code = "ET"
                },
                _ => null,
            };
        }

        public static Civilite? MapCivilite(this string civiliteId)
        {
            return civiliteId switch
            {
                "1" => new Civilite
                {
                    Id = "1",
                    IdExterne = "1",
                    Libelle = "Monsieur",
                    Code = "1"
                },
                "2" => new Civilite
                {
                    Id = "2",
                    IdExterne = "2",
                    Libelle = "Madame",
                    Code = "2"
                },
                _ => null,
            };
        }

        public static DiplomeMA? MapDiplomeMA(this string diplomeMaId)
        {
            return diplomeMaId switch
            {
                "1" => new DiplomeMA
                {
                    Id = "1",
                    IdExterne = "1",
                    Libelle = "1 - Niveau impossible à définir par référence aux autres niveaux",
                    Code = "1 - Aquisition de savoir"
                },
                "2" => new DiplomeMA
                {
                    Id = "2",
                    IdExterne = "2",
                    Libelle = "2 - Sorties de CPA, CLIPA ou sorties de collège avant la 3e(équivalent au niveau VI de l’Éducation nationale)",
                    Code = "2 - 1er niveau de maîtrise de compétences"
                },
                "9" => new DiplomeMA
                {
                    Id = "9",
                    IdExterne = "9",
                    Libelle = "CAP,BEP",
                    Code = "CAP,BEP"
                },
                "13" => new DiplomeMA
                {
                    Id = "13",
                    IdExterne = "82",
                    Libelle = "Master, DEA, DESS, diplôme d'ingénieur",
                    Code = "CAP,BEP"
                },
                _ => null,
            };
        }

        public static TypeContratOuAvenant? MapContrat(this string contratId)
        {
            return contratId switch
            {
                "22" => new TypeContratOuAvenant
                {
                    Id = "22",
                    IdExterne = "22",
                    Libelle = "31 - Modification de la situation juridique de l'employeur",
                    Code = "31"
                },
                "23" => new TypeContratOuAvenant
                {
                    Id = "23",
                    IdExterne = "23",
                    Libelle = "32 - Changement d'employeur dans le cadre d'un contrat saisonnier",
                    Code = "32"
                },
                "24" => new TypeContratOuAvenant
                {
                    Id = "24",
                    IdExterne = "24",
                    Libelle = "33 - Prolongation du contrat suite à un échec à l'examen de l?apprenti",
                    Code = "33"
                },
                "25" => new TypeContratOuAvenant
                {
                    Id = "25",
                    IdExterne = "25",
                    Libelle = "34 - Prolongation du contrat suite à la reconnaissance de l'apprenti comme travailleur handicapé",
                    Code = "34"
                },
                "26" => new TypeContratOuAvenant
                {
                    Id = "26",
                    IdExterne = "26",
                    Libelle = "35 - Modification du diplôme préparé par l'apprenti",
                    Code = "35"
                },
                "27" => new TypeContratOuAvenant
                {
                    Id = "27",
                    IdExterne = "27",
                    Libelle = "36 - Autres changements : changement de maître d'apprentissage, de durée de travail hebdomadaire, réduction de durée, etc.",
                    Code = "36"
                },
                "28" => new TypeContratOuAvenant
                {
                    Id = "28",
                    IdExterne = "28",
                    Libelle = "37 - Modification du lieu d'exécution du contrat",
                    Code = "37"
                },
                "30" => new TypeContratOuAvenant
                {
                    Id = "30",
                    IdExterne = "30",
                    Libelle = "38 - Modification du lieu principal de réalisation de la formation théorique",
                    Code = "38"
                },
                "19" => new TypeContratOuAvenant
                {
                    Id = "19",
                    IdExterne = "19",
                    Libelle = "21 - Nouveau contrat avec un apprenti qui a terminé son précédent contrat auprès d'un même employeur",
                    Code = "21"
                },
                "20" => new TypeContratOuAvenant
                {
                    Id = "20",
                    IdExterne = "20",
                    Libelle = "22 - Nouveau contrat avec un apprenti qui a terminé son précédent contrat auprès d'un autre employeur",
                    Code = "22"
                },
                "21" => new TypeContratOuAvenant
                {
                    Id = "21",
                    IdExterne = "21",
                    Libelle = "23 - Nouveau contrat avec un apprenti dont le précédent contrat a été rompu",
                    Code = "23"
                },
                "18" => new TypeContratOuAvenant
                {
                    Id = "18",
                    IdExterne = "18",
                    Libelle = "11 - Premier contrat d'apprentissage de l'apprenti",
                    Code = "11"
                },
                "11" => new TypeContratOuAvenant
                {
                    Id = "11",
                    IdExterne = "18",
                    Libelle = "11 - Premier contrat d'apprentissage de l'apprenti",
                    Code = "11"
                },
                _ => null,
            };
        }

        public static TypeDerogationContrat? MapDerogation(this string derogationId)
        {
            return derogationId switch
            {
                "1" => new TypeDerogationContrat
                {
                    Id = "1",
                    IdExterne = "1",
                    Libelle = "Age de l'apprenti inférieur à 16 ans",
                    Code = "11"
                },
                "2" => new TypeDerogationContrat
                {
                    Id = "2",
                    IdExterne = "2",
                    Libelle = "Age supérieur à 29 ans : cas spécifiques prévus dans le code du travail",
                    Code = "12"
                },
                "7" => new TypeDerogationContrat
                {
                    Id = "7",
                    IdExterne = "7",
                    Libelle = "Cumul de dérogations",
                    Code = "50"
                },
                "3" => new TypeDerogationContrat
                {
                    Id = "3",
                    IdExterne = "3",
                    Libelle = "Réduction de la durée du contrat ou de la période d'apprentissage",
                    Code = "21"
                },
                "4" => new TypeDerogationContrat
                {
                    Id = "4",
                    IdExterne = "4",
                    Libelle = "Allongement de la durée du contrat ou de la période d'apprentissage",
                    Code = "22"
                },
                _ => null,
            };
        }

        public static ModeContractuel? MapModeContractuel(this string modeContractuelId)
        {
            return modeContractuelId switch
            {
                "2" => new ModeContractuel
                {
                    Id = "2",
                    IdExterne = "44",
                    Libelle = "Dans le cadre d’un CDI",
                    Code = "2"
                },
                "3" => new ModeContractuel
                {
                    Id = "3",
                    IdExterne = "45",
                    Libelle = "Entreprise de travail temporaire",
                    Code = "3"
                },
                "1" => new ModeContractuel
                {
                    Id = "1",
                    IdExterne = "43",
                    Libelle = "A durée limitée",
                    Code = "1"
                },
                _ => null,
            };
        }

        public static TypeRemuneration? MapRemuneration(this string remunerationId)
        {
            return remunerationId switch
            {
                "1" => new TypeRemuneration
                {
                    Id = "1",
                    IdExterne = "59",
                    Libelle = null,
                    Code = "1"
                },
                "2" => new TypeRemuneration
                {
                    Id = "2",
                    IdExterne = "60",
                    Libelle = null,
                    Code = "2"
                },
                _ => null,
            };
        }

        public static CodeDiplome? MapCodeDiplome(this string codeDiplomeId)
        {
            return codeDiplomeId switch
            {
                "13512105" => new CodeDiplome
                {
                    Id = "13512105",
                    IdExterne = "13512105",
                    Libelle = "GEOMATIQUE (MASTER)",
                    Code = "13512105"
                },
                "465" => new CodeDiplome
                {
                    Id = "465",
                    IdExterne = "13531076",
                    Libelle = "DROIT, ECONOMIE, GESTION : GESTION DE PRODUCTION, LOGISTIQUE, ACHATS (MASTER PARIS 12)",
                    Code = "13531076"
                },
                _ => null,
            };
        }

        public static CodeRncp? MapCodeRncp(this string codeRcnp)
        {
            return codeRcnp switch
            {
                "12470" => new CodeRncp
                {
                    Id = "12470",
                    IdExterne = "12470",
                    Libelle = "RcnpTest1",
                    Code = "12470",
                    DateFinEnregistrement = DateTime.Now.AddMonths(8)
                },
                "111205555555555555555555" => new CodeRncp
                {
                    Id = "111205555555555555555555",
                    IdExterne = "111205555555555555555555",
                    Libelle = "RcnpTest2",
                    Code = "111205555555555555555555",
                    DateFinEnregistrement = DateTime.Now.AddMonths(8)
                },
                _ => null,
            }; ; ;
        }

    }
}
