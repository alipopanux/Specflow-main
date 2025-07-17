using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using NUnit.Framework;
using PIM_API.Tests.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIM_DataAccess.DB_CONTEXT;
using System.Security.Claims;

namespace PIM_API.Tests
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
            GetDbContext<PIM_DB_Context>());

       
        public TestContext(DefaultWebAppFactory webAppFactory)
        {
            _webAppFactory = webAppFactory;
            ConfigureService(svc =>
            {
                var inputClaims = new List<Claim> { new Claim(ClaimTypes.Name, "Ali AMADJAR") };
                svc.AddDbContext<AAAAAAA>();
                svc.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();
                // Enregistrer l'Authentification
                //svc.AddAuthentication("Test")
                //    .AddScheme<TestAuthenticationSchemeOptions, TestAuthHandler>(
                //        "Test", options => { options.Claims = inputClaims; });
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
