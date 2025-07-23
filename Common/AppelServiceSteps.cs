using FluentAssertions;
using Lopcommerce.ApiBack;
using Lopcommerce.Regles.ApiAccess.ExternalApi;
using Lopcommerce.Regles.Models.Domain;
using Lopcommerce.Regles.WebAPI.Tests.Extensions;
using Lopcommerce.Regles.WebAPI.ViewModels.Request;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    [Binding]
    public class AppelServiceSteps
    {
        private readonly TestContext _testContext;
        private static readonly JsonSerializerSettings jsonSettings;

        static AppelServiceSteps()
        {
            jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new StringEnumConverter());
        }

        public AppelServiceSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given(@"quand j'appelle la base pour la date de fin du contrat initial, j'ai le retour suivant : '(.*)'")]
        public void WhenJAppelleDateFinContrat(string dateFinContratInitial)
        {
            _testContext.MockCerfaRepository
                .Setup(p => p.GetDateFinContratCerfaInitialAvenant(It.IsAny<int?>()))
                .Returns(Task.FromResult(Convert.ToDateTime(dateFinContratInitial)));
        }


        [Given(@"quand j'appelle la base pour le siret de l'of contrat initial, j'ai le retour suivant : '(.*)'")]
        public void WhenJAppelleOfSiret(string ofSiret)
        {
            _testContext.MockCerfaRepository
                .Setup(p => p.GetSiretOFCerfaInitialAvenant(It.IsAny<int?>()))
                .Returns(Task.FromResult(ofSiret));
        }

        [Given (@"quand j'appelle la base pour le diplome vise du contrat initial , j'ai le retour suivant : '(.*)'")]
        public void WhenJAppelleDiplomeVise(string diplomeVise)
        {
            _testContext.MockCerfaRepository
                .Setup(p => p.GetDernierDiplomeViseCerfaInitialAvenant(It.IsAny<int?>()))
                .Returns(Task.FromResult(diplomeVise));
        }

        [Given(@"quand j'appelle la base pour le siret de l'entreprise du contrat initial, j'ai le retour suivant : '(.*)'")]
        public void WhenJAppelleOfEntreprise(string siretEntreprise)
        {
            _testContext.MockCerfaRepository
                .Setup(p => p.GetSiretEntrepriseCerfaInitialAvenant(It.IsAny<int?>()))
                .Returns(Task.FromResult(siretEntreprise));
        }

        [Given (@"quand j'appelle la base pour le dossier initial , j'ai le retour suivant :")]
        public void WhenJAppelleDossierInitial(Table table)
        {
            var demande = table.CreateComplexInstance<Demande>();
            _testContext.MockCerfaRepository
                .Setup(p => p.GetDemandeCerfaInitialAvenant(It.IsAny<int?>()))
                .Returns(Task.FromResult(demande));
        }

        [Given(@"quand j'appelle la base pour un apprenti rqth, j'ai le retour suivant :")]
        public void WhenJAppelleApprentiRqth(Table table)
        {
            var isRqthString = table.Rows[0]["valeur"];
            bool isRqth;
            if (!bool.TryParse(isRqthString, out isRqth))
            {
                throw new ArgumentException($"La valeur '{isRqthString}' n'est pas un booléen valide.");
            }

            _testContext.MockCerfaRepository
                .Setup(p => p.GetRqthCerfaInitialAvenant(It.IsAny<int>()))
                .Returns(Task.FromResult(isRqth));
        }

        [When(@"j'appelle l'url '(.*)' en mode '(.*)'")]
        public async Task WhenJAppelleUrlEnMode(string uri, string methodeHttp)
        {
            string content = JsonConvert.SerializeObject(_testContext.Request, jsonSettings);

            var httpMessage = new HttpRequestMessage(new HttpMethod(methodeHttp), uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            _testContext.Reponse = await _testContext.Client.SendAsync(httpMessage);
        }

        [When(@"j'appelle l'url '(.*)' en mode '(.*)' avec le body suivant")]
        public async Task WhenJAppelleUrlEnModeAvecBody(string uri, string methodeHttp, Table table)
        {
            _testContext.Request = table.CreateComplexInstance<RequestCerfaTags>();
            string content = JsonConvert.SerializeObject(_testContext.Request, jsonSettings);

            var httpMessage = new HttpRequestMessage(new HttpMethod(methodeHttp), uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };


            _testContext.Reponse = await _testContext.Client.SendAsync(httpMessage);
        }

        [Given(@"quand j'appelle le service Deca pour un apprenti, j'ai le retour suivant :")]
        public void WhenJAppelleServiceDeca(Table table)
        {
            var demande = table.CreateComplexSetCustom<Demande>();
            _testContext.MockDecaApiService
                .Setup(p => p.GetContratsByAlternantAsync(It.IsAny<StagiareDemande>()))
                .Returns(Task.FromResult(demande.ToList()));
        }

        [When(@"j'appelle l'url '(.*)' en mode '(.*)' avec le body pour commun suivant")]
        public async Task WhenJAppelleUrlEnModeAvecBodyCommun(string uri, string methodeHttp, Table table)
        {
            _testContext.Request = table.CreateComplexInstance<RequestCerfaFront>();
            string content = JsonConvert.SerializeObject(_testContext.Request, jsonSettings);

            var httpMessage = new HttpRequestMessage(new HttpMethod(methodeHttp), uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };


            _testContext.Reponse = await _testContext.Client.SendAsync(httpMessage);
        }

        [When(@"j'appelle l'url '(.*)' en mode '(.*)' avec les parametres suivants")]
        public async Task WhenJAppelleUrlEnMode(string uri, string methodeHttp, Table table)
        {
            if (!uri.Contains("?"))
                uri += "?";

            foreach (var row in table.Rows)
            {
                var value = row["Valeur"];
                foreach (var valueRetriever in Service.Instance.ValueRetrievers)
                {
                    if (valueRetriever.CanRetrieve(new KeyValuePair<string, string>(row["Champs"], row["Valeur"]), typeof(string), typeof(string)))
                    {
                        value = valueRetriever.Retrieve(new KeyValuePair<string, string>(row["Champs"], row["Valeur"]), typeof(string), typeof(string))?.ToString();
                        if (DateTime.TryParse(value, out DateTime date))
                            value = date.ToString();
                        break;
                    }
                }

                if (value != null)
                    uri += $"&{row["Champs"]}={value}";
            }

            await WhenJAppelleUrlEnMode(uri, methodeHttp);
        }

        [When(@"j'appelle l'url '(.*)' avec champ date '(.*)'=(.*) en mode 'GET'")]
        public async Task WhenJUrlAvecChampDateEnMode(string uri, string fieldName, string fieldValue)
        {
            string content = JsonConvert.SerializeObject(_testContext.Request, jsonSettings);

            if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldValue))
            {
                uri += $"&{fieldName}={fieldValue}";
            }

            var httpMessage = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json"),

            };

            _testContext.Reponse = await _testContext.Client.SendAsync(httpMessage);
        }

        [Then(@"je dois avoir une réponse http '(.*)'")]
        public void ThenJeDoisAvoirUneReponseHttp(HttpStatusCode codeHttp)
        {
#if DEBUG
            var reponse = _testContext.Reponse.Content.ReadAsStringAsync().Result;
#endif
            _testContext.Reponse.StatusCode.Should().Be(codeHttp);
        }


        [Then(@"je dois avoir une réponse http '(.*)' sans erreurs")]
        public async Task ThenJeDoisAvoirUneReponseHttpSansErreurs(HttpStatusCode codeHttp)
        {
            var response = await _testContext.ObtenirReponseApi<IEnumerable<Notification>>();
            _testContext.Reponse.StatusCode.Should().Be(codeHttp);
            response.Should().BeEmpty();
        }

        [Then(@"je dois avoir une réponse http '(.*)' avec l'object suivant :")]
        public async Task ThenJeDoisAvoirUneReponseHttp(HttpStatusCode codeHttp, Table table)
        {
            var response = await _testContext.ObtenirReponseApi<IEnumerable<Notification>>();
            var result = table.CreateComplexSetCustom<Notification>();
            _testContext.Reponse.StatusCode.Should().Be(codeHttp);
            response.Should().BeEquivalentTo(result);
        }


        [Then(@"je dois avoir le code d'erreur '(.*)'")]
        public async Task ThenJeDoisAvoirLeCodeDErreur(string codeErreur)
        {
            var response = await _testContext.ObtenirReponseApi<string>();
            response.Should().BeEquivalentTo(codeErreur);
        }
    }
}
