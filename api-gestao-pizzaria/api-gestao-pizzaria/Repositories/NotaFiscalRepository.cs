using api_gestao_pizzaria.DataContext;
using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Interfaces.DataContext;
using api_gestao_pizzaria.Models;
using MySqlConnector;

namespace api_gestao_pizzaria.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly IMySQLConnection _connection;
        public NotaFiscalRepository()
        {
            _connection = new MySQLConnection();
        }
        public async Task<long> AdicionaNotaFiscal(NotaFiscal notaFiscal)
        {
            long idNotaFiscal = 0;
            try
            {
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO nota_fiscal (id" +
                                                    ", numero_nota" +
                                                    ", data_compra)" +
                                            " VALUES (default" +
                                                    ", @NumeroNota" +
                                                    ", @DataCompra);";
                        command.Parameters.AddWithValue("@NumeroNota", notaFiscal.NumeroNota);
                        command.Parameters.AddWithValue("@DataCompra", $"{notaFiscal.DataCompra.Year}-{notaFiscal.DataCompra.Month}-{notaFiscal.DataCompra.Day}");
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            idNotaFiscal = command.LastInsertedId;
                        }
                    }
                }
                return idNotaFiscal;
            }
            catch (Exception e) { return 0; }
        }
        public async Task<NotaFiscal> GetNotaFiscalPeloId(long id)
        {
            try
            {
                NotaFiscal response = null;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id" +
                                                    ", numero_nota" +
                                                    ", data_compra" +
                                            " FROM nota_fiscal" +
                                            " WHERE id=@Id;";
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                response = await PreencherPropriedade(reader);
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception e) { return null; }
        }
        public async Task<IEnumerable<NotaFiscal>> GetTodasNotaFiscais()
        {
            try
            {
                var listResponse = new List<NotaFiscal>();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id" +
                                                    ", numero_nota" +
                                                    ", data_compra" +
                                            " FROM nota_fiscal;";
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                listResponse.Add(await PreencherPropriedade(reader));
                            }
                        }
                    }
                }
                return listResponse;
            }
            catch (Exception e) { return null; }

        }
        public async Task<bool> UpdateNotaFiscal(long id, NotaFiscal notaFiscal)
        {
            int rowCount = 0;
            try
            {
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "UPDATE nota_fiscal" +
                                                    " SET numero_nota=@NumeroNota" +
                                            " WHERE id=@Id;";
                        command.Parameters.AddWithValue("@NumeroNota", notaFiscal.NumeroNota);
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }
        }
        public async Task<bool> DeleteNotaFiscal(long id)
        {
            int rowCount = 0;
            try
            {
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM nota_fiscal" +
                                            " WHERE id=@Id;";
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }
        }
        public async Task<NotaFiscal> PreencherPropriedade(MySqlDataReader reader)
        {
            long Id = reader.GetInt32(0);
            string NumeroNota;
            try
            {
                NumeroNota = reader.GetString(1);
            }
            catch
            {
                NumeroNota = "";
            }
            DateTime DataCompra;
            try
            {
                DataCompra = reader.GetDateTime(2);
            }
            catch
            {
                DataCompra = new DateTime();
            }
            return new NotaFiscal()
            {
                Id = Id,
                NumeroNota = NumeroNota,
                DataCompra = DataCompra,
            };
        }
    }
}
