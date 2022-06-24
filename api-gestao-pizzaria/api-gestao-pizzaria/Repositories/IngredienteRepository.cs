using api_gestao_pizzaria.DataContext;
using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Interfaces.DataContext;
using api_gestao_pizzaria.Models;
using MySqlConnector;

namespace api_gestao_pizzaria.Repositories
{
    public class IngredienteRepository : IIngredienteRepository
    {
        private readonly IMySQLConnection _connection;
        public IngredienteRepository()
        {
            _connection = new MySQLConnection();
        }
        public async Task<Ingrediente> GetIngredientePeloId(long id)
        {
            try
            {
                Ingrediente response = null;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id" +
                                                    ", nome" +
                                                    ", descricao" +
                                            " FROM ingrediente" +
                                            " WHERE id=@Id;";
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                response = await PreenchePropriedades(reader);
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception e) { return null; }
        }
        public async Task<IEnumerable<Ingrediente>> GetTodosIngredientes()
        {
            try
            {
                var listResponse = new List<Ingrediente>();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id" +
                                                    ", nome" +
                                                    ", descricao" +
                                            " FROM ingrediente;";
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                listResponse.Add(await PreenchePropriedades(reader));
                            }
                        }
                    }
                }
                return listResponse;
            }
            catch (Exception e) { return null; }

        }
        public async Task<long> CreateIngrediente(Ingrediente ingrediente)
        {
            long ingredienteId = 0;
            try
            {
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO ingrediente (id" +
                                                    ", nome" +
                                                    ", descricao)" +
                                            " VALUES (default" +
                                            ", @Nome" +
                                            ", @Descricao);";
                        command.Parameters.AddWithValue("@Nome", ingrediente.Nome);
                        command.Parameters.AddWithValue("@Descricao", ingrediente.Descricao);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            ingredienteId = command.LastInsertedId;
                        }
                    }
                }
                return ingredienteId;
            }
            catch (Exception e) { return 0; }
        }        
        public async Task<bool> DeleteIngrediente(long id)
        {
            try
            {
                int rowCount = 0;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM ingrediente" +
                                                    " WHERE id=@Id";
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }
        }
        public async Task<bool> UpdateIngrediente(long id, Ingrediente ingrediente)
        {
            try
            {
                int rowCount = 0;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "UPDATE ingrediente" +
                                                    " SET nome=@Nome" +
                                                    ", descricao=@Descricao" +
                                                    " WHERE id=@Id";
                        command.Parameters.AddWithValue("@Nome", ingrediente.Nome);
                        command.Parameters.AddWithValue("@Descricao", ingrediente.Descricao);
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }
        }
        private async Task<Ingrediente> PreenchePropriedades(MySqlDataReader reader)
        {
            long Id = reader.GetInt32(0);
            
            string Nome;
            try
            {
                Nome = reader.GetString(1);
            }
            catch
            {
                Nome = "";
            }
            string Descricao;
            try
            {
                Descricao = reader.GetString(2);
            }
            catch
            {
                Descricao = "";
            }
           
            return new Ingrediente()
            {
                Id = Id,
                Nome = Nome,
                Descricao = Descricao
            };
        }

    }
}
