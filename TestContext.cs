using FluentAssertions;
using Lopcommerce.Regles.ApiAccess.ExternalApi;
using Lopcommerce.Regles.DataAccess.Entities;
using Lopcommerce.Regles.WebAPI.Tests.Mapping;
using Lopcommerce.Regles.WebAPI.Tests.Mocks;
using Lopcommerce.Regles.WebAPI.Tests.Remuneration.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Lopcommerce.Regles.DataAccess.Repository;

namespace Lopcommerce.Regles.WebAPI.Tests
{
    public class TestContext
    {
        private HttpClient _client;
        private readonly DefaultWebAppFactory _webAppFactory;
        private readonly IDictionary<Type, object> _requestsParams = new Dictionary<Type, object>();
        public HttpClient Client => _client ??= _webAppFactory.CreateClient();
        public object Request { get; internal set; }
        public HttpResponseMessage Reponse { get; internal set; }
        public IDbTestRepository DbTestRepository => new DbContextTestRepository(
            GetDbContext<ConstanteDBContext>());
        public Mock<ICerfaRepository> MockCerfaRepository { get; }

        public Mock<IDecaApiService> MockDecaApiService { get; }

        public TestContext(DefaultWebAppFactory webAppFactory)
        {
            _webAppFactory = webAppFactory;
            MockCerfaRepository = new Mock<ICerfaRepository>();
            MockDecaApiService = new Mock<IDecaApiService>();
            ConfigureService(svc =>
            {
                svc.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                svc.AddSingleton<IConstanteService, ConstanteService>();
                svc.AddSingleton<IBackApiService, MockBackApiService>();
                svc.AddSingleton<IReferentielApiService, MockReferentielApiService>();
                svc.AddSingleton<IRemunerationApiService, MockRemunerationApiService>();
                svc.AddSingleton<IRemunerationService, RemunerationService>();
                svc.AddSingleton<IOrganismeFormationRepository, MockOrganismeFormationRepository>();
                svc.AddSingleton(MockDecaApiService.Object);
                svc.AddSingleton(MockCerfaRepository.Object);
            });
        }

        public T GetDbContext<T>() where T : DbContext
        {
            var dbContext = _webAppFactory.Services.GetRequiredService<T>();
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        public void ConfigureService(Action<IServiceCollection> action)
        {
            _webAppFactory.AddConfigServices(action);
        }

        public void ValidateRequestParams<TType>(TType expectedRequestParams)
        {
            Type requestType = typeof(TType);
            Assert.True(_requestsParams.ContainsKey(requestType));
            _requestsParams[requestType].Should().BeEquivalentTo(expectedRequestParams, opt => opt.RespectingRuntimeTypes());
        }

        public void AddConfiguration(string key, string value)
        {
            _webAppFactory.AddConfiguration(key, value);
        }

        public void AssertNoRequest<TType>()
        {
            Type requestType = typeof(TType);
            _requestsParams.ContainsKey(requestType).Should().BeFalse();
        }

        public async Task<TType> ObtenirReponseApi<TType>(JsonSerializerSettings jsonSettings = null)
        {
            var reponse = await Reponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TType>(reponse, jsonSettings);
        }

        public async Task<TType> ObtenirReponseApiAvecFormatDate<TType>()
        {
            var reponse = await Reponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TType>(reponse, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        public void Dispose()
        {
            _webAppFactory.Dispose();
        }
    }
}
