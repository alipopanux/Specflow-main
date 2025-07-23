using Microsoft.Data.SqlClient;

namespace Lopcommerce.Regles.WebAPI.Tests.DbFixture
{
    public class DbFixture : DbFixtureBase
    {
        public override async Task CreateTables()
        {
            using (var con = new SqlConnection(baseConnectionString))
            {
                string PhysicalRootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string script = File.ReadAllText(PhysicalRootDirectory + @"/Scripts/Create_Tables.sql");
                await con.OpenAsync();
                SqlCommand command = new SqlCommand(script, con);
                command.ExecuteNonQuery();
            }
        }

        public override async Task FillTables()
        {
            //using (var con = new SqlConnection(baseConnectionString))
            //{
            //    string PhysicalRootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            //    string script = File.ReadAllText(PhysicalRootDirectory + @"/Scripts/Fill_Tables.sql");
            //    await con.OpenAsync();
            //    SqlCommand command = new SqlCommand(script, con);
            //    command.ExecuteNonQuery();
            //}
        }

        public override async Task CleanTables()
        {
            using (var con = new SqlConnection(baseConnectionString))
            {
                string PhysicalRootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string script = File.ReadAllText(PhysicalRootDirectory + @"/Scripts/Clean_Tables.sql");
                await con.OpenAsync();
                SqlCommand command = new SqlCommand(script, con);
                command.ExecuteNonQuery();
            }
        }

        public override async Task DropTables()
        {
            using (var con = new SqlConnection(baseConnectionString))
            {
                string PhysicalRootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string script = File.ReadAllText(PhysicalRootDirectory + @"/Scripts/Drop_Tables.sql");
                await con.OpenAsync();
                SqlCommand command = new SqlCommand(script, con);
                command.ExecuteNonQuery();
            }
        }
    }
}
