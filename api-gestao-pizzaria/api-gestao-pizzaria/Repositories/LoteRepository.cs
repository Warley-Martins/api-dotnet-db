using api_gestao_pizzaria.DataContext;
using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Interfaces.DataContext;
using api_gestao_pizzaria.Models;
using MySqlConnector;

namespace api_gestao_pizzaria.Repositories
{
    public class LoteRepository : ILoteRepository
    {
        private readonly IMySQLConnection _connection;
        public LoteRepository()
        {
            _connection = new MySQLConnection();
        }
        public async Task<long> CreateLote(Lote lote, long ingredienteId = 0)
        {
            long idLote = 0;
            using (var conn = new MySqlConnection(_connection.GetConnectionString()))
            {
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO lote " +
                                                "(id" +
                                                ", quantidade_disponivel" +
                                                ", quantidade_total_inicial" +
                                                ", numero_lote" +
                                                ", descricao" +
                                                ", validade" +
                                                ", data_compra" +
                                                ", fk_id_ingrediente)" +
                                            " VALUES " +
                                                " (default" +
                                                ", @QuantidadeDisponivel" +
                                                ", @QuantidadeTotalInicial" +
                                                ", @NumeroLote" +
                                                ", @Descricao" +
                                                ", @Validade" +
                                                ", @DataCompra " +
                                                ", @IdIngrediente)";
                    command.Parameters.AddWithValue("@QuantidadeDisponivel", lote.QuantidadeDisponivel);
                    command.Parameters.AddWithValue("@QuantidadeTotalInicial", lote.QuantidadeTotalInicial);
                    command.Parameters.AddWithValue("@NumeroLote", lote.NumeroLote);
                    command.Parameters.AddWithValue("@Descricao", lote.Descricao);
                    command.Parameters.AddWithValue("@DataCompra", $"{lote.DataCompra.Year}-{lote.DataCompra.Month}-{lote.DataCompra.Day}");
                    command.Parameters.AddWithValue("@Validade", $"{lote.Validade.Year}-{lote.Validade.Month}-{lote.Validade.Day}");
                    command.Parameters.AddWithValue("@IdIngrediente", ingredienteId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        idLote = command.LastInsertedId;
                    }
                }
            }
            return idLote;
        }
        public async Task<bool> DeleteLote(long id)
        {
            try
            {
                int rowCount = 0;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM lote WHERE id=@Id;";
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<Lote> GetLotePeloId(long id)
        {
            try
            {
                Lote response = new Lote();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = $"SELECT id" +
                                                    ", quantidade_disponivel" +
                                                    ", quantidade_total_inicial" +
                                                    ", numero_lote" +
                                                    ", descricao" +
                                                    ", validade" +
                                                    ", data_compra" +
                                                " FROM lote" +
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
        public async Task<IEnumerable<Lote>> GetTodosLotesPeloIngredienteId(long IngredienteId)
        {
            try
            {
                var listResponse = new List<Lote>();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id" +
                                                    ", quantidade_disponivel" +
                                                    ", quantidade_total_inicial" +
                                                    ", numero_lote" +
                                                    ", descricao" +
                                                    ", validade" +
                                                    ", data_compra" +
                                            " FROM lote" +
                                            " WHERE fk_id_ingrediente=@IngredienteId;";
                        command.Parameters.AddWithValue("@IngredienteId", IngredienteId);
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
        public async Task<IEnumerable<Lote>> GetTodosLotes()
        {
            try
            {
                var listResponse = new List<Lote>();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id" +
                                                    ", quantidade_disponivel" +
                                                    ", quantidade_total_inicial" +
                                                    ", numero_lote" +
                                                    ", descricao" +
                                                    ", validade" +
                                                    ", data_compra" +
                                            " FROM lote;";
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
        public async Task<bool> UpdateLote(long id, Lote lote)
        {
            try
            {
                int rowCount = 0;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "UPDATE lote" +
                                              " SET quantidade_disponivel=@QuantidadeDisponivel" +
                                              " ,descricao=@Descricao" +
                                              " WHERE id=@Id;";
                        command.Parameters.AddWithValue("@QuantidadeDisponivel", lote.QuantidadeDisponivel);
                        command.Parameters.AddWithValue("@Descricao", lote.Descricao);
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }
        }
        private async Task<Lote> PreenchePropriedades(MySqlDataReader reader)
        {
            long Id = reader.GetInt32(0);
            int QuantidadeDisponivel;
            try
            {
                QuantidadeDisponivel = reader.GetInt32(1);
            }
            catch
            {
                QuantidadeDisponivel = 0;

            }
            int QuantidadeTotalInicial;
            try
            {
                QuantidadeTotalInicial = reader.GetInt32(2);
            }
            catch 
            {
                QuantidadeTotalInicial = 0;
            }
            string NumeroLote;
            try
            {
                NumeroLote = reader.GetString(3);
            }
            catch
            {
                NumeroLote = "";
            }
            string Descricao;
            try
            {
                Descricao = reader.GetString(4);
            }
            catch
            {
                Descricao = "";
            }
            DateTime Validade;
            try
            {
                Validade = reader.GetDateTime(5);
            }
            catch
            {
                Validade = new DateTime();
            }
            DateTime DataCompra;
            try
            {
                DataCompra = reader.GetDateTime(6);
            }
            catch
            {
                DataCompra = new DateTime();
            }
            return new Lote()
            {
                Id = Id,
                QuantidadeDisponivel = QuantidadeDisponivel ,
                QuantidadeTotalInicial = QuantidadeTotalInicial ,
                NumeroLote = NumeroLote ,
                Descricao = Descricao ,
                Validade = Validade ,
                DataCompra = DataCompra ,
            };
        } 
    }
}
