using Lopcommerce.Regles.WebAPI.Tests.DbFixture;
using TechTalk.SpecFlow;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.Hooks
{
    public static class HooksDbFixture<TDBFixture> where TDBFixture : DbFixtureBase, new()
    {
        public static void BeforeFeature(FeatureContext featureContext)
        {
            var dbFixture = new TDBFixture();
            featureContext.Set(dbFixture);
            dbFixture.EnsureCreateSchema().Wait();
            dbFixture.CreateTables().Wait();
        }

        public static void AfterFeature(FeatureContext featureContext)
        {
            var dbf = featureContext.Get<TDBFixture>();
            dbf.DropTables().Wait();
            dbf.Dispose();
        }

        public static void BeforeScenario(ScenarioContext scenarioContext, FeatureContext featureContext, TestContext testContext)
        {
            var dbFixture = featureContext.Get<TDBFixture>();
            dbFixture.FillTables().Wait();
            scenarioContext.Set(dbFixture);
        }

        public static void AfterScenario(ScenarioContext scenarioContext)
        {
            var dbFixture = scenarioContext.Get<TDBFixture>();
            dbFixture.CleanTables().Wait();
        }
    }
}
