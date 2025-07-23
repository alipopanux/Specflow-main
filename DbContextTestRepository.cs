using Lopcommerce.Regles.DataAccess.Entities;

namespace Lopcommerce.Regles.WebAPI.Tests
{
    public class DbContextTestRepository(ConstanteDBContext dbContext) : IDbTestRepository
    {
        private readonly ConstanteDBContext _dbContext = dbContext;
    }
}
