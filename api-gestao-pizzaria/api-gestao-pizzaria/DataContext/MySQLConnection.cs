using api_gestao_pizzaria.Interfaces.DataContext;
using MySqlConnector;

namespace api_gestao_pizzaria.DataContext
{
    public class MySQLConnection : IMySQLConnection
    {
        public MySqlConnectionStringBuilder _builder { get; set; }
        public MySQLConnection()
        {
            InicializaMySqlConnectionStringBuilder();
        }
        private void InicializaMySqlConnectionStringBuilder()
        {
            _builder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "pizzaria",
                UserID = "root",
                Password = "98132478Duda",
                SslMode = MySqlSslMode.Required,
            };
        }
        public string GetConnectionString()
        {
            return _builder.ConnectionString;
        }
        }
}
