namespace api_gestao_pizzaria.Interfaces.DataContext
{
    public interface IMySQLConnection
    {
        private void InicializaMySqlConnectionStringBuilder() { }
        public string GetConnectionString();
    }
}
