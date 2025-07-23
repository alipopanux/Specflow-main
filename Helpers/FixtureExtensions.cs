using AutoFixture;
using AutoFixture.Kernel;

namespace Lopcommerce.Regles.WebAPI.Tests.Helpers
{
    public static class FixtureExtensions
    {
        public static object Create(this Fixture fixture, Type t)
        {
            return CallByReflection(fixture, t, "Create");
        }

        public static object CreateMany(this Fixture fixture, Type t)
        {
            return CallByReflection(fixture, t, "CreateMany");
        }

        private static object CallByReflection(Fixture fixture, Type t, string method)
        {
            var specimenType = typeof(SpecimenFactory);
            var methods = specimenType.GetMethod(method, new[] { typeof(ISpecimenBuilder) });
            var genericMethod = methods.MakeGenericMethod(t);

            return genericMethod.Invoke(null, new[] { fixture });
        }

    }
}
