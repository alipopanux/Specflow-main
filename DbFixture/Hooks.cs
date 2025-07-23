using Lopcommerce.Regles.WebAPI.Tests.Common.Hooks;
using TechTalk.SpecFlow;

namespace Lopcommerce.Regles.WebAPI.Tests.DbFixture
{
    [Binding]
    [Scope(Tag = "AccesBdd")]
    public static class Hooks
    {
        [BeforeFeature()]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            HooksDbFixture<DbFixture>.BeforeFeature(featureContext);
        }

        [AfterFeature()]
        public static void AfterFeature(FeatureContext featureContext)
        {
            HooksDbFixture<DbFixture>.AfterFeature(featureContext);
        }

        [BeforeScenario()]
        public static void BeforeScenario(ScenarioContext scenarioContext, FeatureContext featureContext, TestContext testContext)
        {
            HooksDbFixture<DbFixture>.BeforeScenario(scenarioContext, featureContext, testContext);
        }

        [AfterScenario()]
        public static void AfterScenario(ScenarioContext scenarioContext)
        {
            HooksDbFixture<DbFixture>.AfterScenario(scenarioContext);
        }
    }
}
