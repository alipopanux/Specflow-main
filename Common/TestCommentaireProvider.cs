using static Lopcommerce.Regles.WebAPI.Tests.Common.Constantes.Constantes;

namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestCommentaireProvider : ICommentaireProvider
    {
        private readonly string _key;
        public TestCommentaireProvider(string key)
        {
            _key = key;
        }

        public string GetCommentaire()
        {
            switch (_key)
            {
                case var value when value == CommentairesMotif.AGE_APPRENTI:

                    return @"Pour les contrats conclus depuis le 1er janvier 2019, l’âge plafond d’entrée en apprentissage est de 29 ans révolus.

La conclusion d’un tel contrat est possible jusqu’à la veille des 30 ans du postulant et ce, même si la date de début d’exécution intervient postérieurement à ses 30 ans. Cependant, le délai qui sépare la date de conclusion et la date de début d’exécution du contrat doit être raisonnable.

Deux catégories de personnes peuvent conclure un contrat d’apprentissage au-delà de la limite de 30 ans (article L.6222-2, alinéa 1, 3° et 5°) :
• Les personnes bénéficiaires de la qualité de travailleur handicapé (RQTH)
• Les sportifs de haut niveau (liste prévue à l’article L.221-2, alinéa 1 du Code du sport)

En outre, la limite d'âge de 29 ans révolus ne s’applique pas dans les cas suivants :
• Le contrat d’apprentissage est conclu par une personne qui a un projet de création ou de reprise d'entreprise dont la réalisation est subordonnée à l'obtention du diplôme ou titre visé (article L.6222-1, alinéa 1, 4°)
• Le contrat d’apprentissage est conclu suite à l’échec à l’obtention du diplôme ou titre professionnel visé par le précédent contrat. Le nouveau contrat doit être signé avec un employeur différent et sa durée ne peut pas excéder 1 an (articles L.6222-11, alinéa 1, 2° et D.6222-1-2)
• Le contrat d’apprentissage fait suite à précédent un contrat ayant permis à l’apprenti d’obtenir le diplôme ou titre professionnel préparé et vise un niveau de diplôme ou titre supérieur. L'âge de l'apprenti au moment de la conclusion du nouveau contrat doit être de 35 ans au plus et sa date de conclusion doit intervenir au plus tard dans les 12 mois suivant la fin du précédent contrat (articles L.6222-2, alinéa 1, 1° et D.6222-1, alinéa 1, 1° et 2°)
• Le contrat d’apprentissage a été rompu pour des causes indépendantes de la volonté de l'apprenti (cessation d'activité de l'employeur, faute de l'employeur ou manquements répétés à ses obligations, mise en œuvre de la procédure de suspension de l'exécution du contrat d'apprentissage) ou suite à une inaptitude physique et temporaire constatée par le médecin du travail. L'âge de l'apprenti au moment de la conclusion du nouveau contrat doit être de 35 ans au plus et sa date de conclusion doit intervenir au plus tard dans les 12 mois suivant la fin du précédent contrat (articles L.6222-2, alinéa 1, 2° et D.6222-1, alinéa 1, 1° et 2°)";

                case var value when value == CommentairesMotif.DATE_FORMATION:

                    return @"La date de début de la formation pratique chez l'employeur ne peut être postérieure de plus de 3 mois au début d'exécution du contrat (article L.6222-12, alinéa 2 du Code du travail).

La date de début de la formation en CFA ne peut être postérieure de plus de 3 mois au début d'exécution du contrat (article L.6222-12, alinéa 3 du Code du travail).

Par dérogation, une personne qui n'a pas été engagée par un employeur, peut débuter un cycle de formation en apprentissage. La durée de formation en CFA sans contrat d’apprentissage est limitée à 3 mois. La personne bénéficie pendant cette période du statut de stagiaire de la formation professionnelle.";

                default:

                    return null;

            }
        }
    }

    public interface ICommentaireProvider
    {
        string GetCommentaire();
    }
}
