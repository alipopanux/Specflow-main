//using DotNet.Testcontainers.Builders;
//using DotNet.Testcontainers.Configurations;
//using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;
//using Testcontainers.MsSql;

namespace Lopcommerce.Regles.WebAPI.Tests.DbFixture
{
    public abstract class DbFixtureBase : IDisposable
    {
        public static readonly string SQL_CONNECTION_STRING = "SqlConnectionString";
        protected readonly string baseConnectionString;
        public string Schema { get; }
        public string ConnectionString { get; }

        //private MsSqlContainer _container { get; set; }

        public DbFixtureBase()
        {
            baseConnectionString = "Data Source=localhost; Initial Catalog=DbTest; User Id=SA; Password=Adminxyz22#;TrustServerCertificate=True;";
            Schema = $"test_regle_{DateTime.Now:yyMMddhhmmss_fffffff}_{Guid.NewGuid().ToString().Substring(0, 8)}";
        }

        public async Task EnsureCreateSchema()
        {
            //await InitContainerTest();
            using (var con = new SqlConnection(baseConnectionString))
            {
                await con.OpenAsync();
                SqlCommand command = new SqlCommand("Create DATABASE DbTest", con);
                command.ExecuteNonQuery();
            }
        }

        public async void Dispose()
        {

            //await _container.StopAsync();

            using (var con = new SqlConnection(baseConnectionString))
            {
                await con.OpenAsync();
                SqlCommand command = new SqlCommand($@"DROP SCHEMA [{Schema}];", con);
                command.ExecuteNonQuery();
            }
        }

        //private async Task InitContainerTest()
        //{
        //    _container = new MsSqlBuilder()
        //        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        //        .WithEnvironment("SQLCMDDBNAME", "DB-TEST")
        //        .WithEnvironment("ACCEPT_EULA", "Y")
        //        .WithPortBinding(3400, true)
        //        .WithName("DB-TEST")
        //        .WithPassword("Adminxyz22#")
        //        .WithEnvironment("SA_PASSWORD", "Adminxyz22#")
        //        //.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3400))
        //        .Build();



        //    await _container.StartAsync();

        //    var a = _container.GetConnectionString();

        //}

        public abstract Task FillTables();

        public abstract Task CreateTables();

        public abstract Task DropTables();

        public abstract Task CleanTables();

    }
}
