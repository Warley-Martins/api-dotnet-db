using api_gestao_pizzaria.DataContext;
using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Interfaces.DataContext;
using api_gestao_pizzaria.Models;
using MySqlConnector;

namespace api_gestao_pizzaria.Repositories
{
    public class ItemNotaFiscalRepository : IItemNotaFiscalRepository
    {
        private readonly IMySQLConnection _connection;
        public ItemNotaFiscalRepository()
        {
            _connection = new MySQLConnection();
        }
        public async Task<long> CreateItemNotaFiscal(ItemNotaFiscal itemNotaFiscal, long idLote, long idNotaFiscal)
        {
            long idItemNotaFiscal = 0;
            try
            {
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO item_nota_fiscal (id" +
                                                    ", valor_unitario_pago" +
                                                    ", fk_id_nota_fiscal" +
                                                    ", fk_id_lote)" +
                                            " VALUES (default" +
                                            ", @ValorUnitarioPago" +
                                            ", @NotaFiscalId" +
                                            ", @LoteId);";
                        command.Parameters.AddWithValue("@ValorUnitarioPago", itemNotaFiscal.ValorUnitarioPago);
                        command.Parameters.AddWithValue("@LoteId", idLote);
                        command.Parameters.AddWithValue("@NotaFiscalId", idNotaFiscal );
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            idItemNotaFiscal = command.LastInsertedId;
                        }
                    }
                }
                return idItemNotaFiscal;
            }
            catch (Exception e) { return 0; }
        }
        public async Task<ItemNotaFiscal> GetItemNotaFiscalPeloId(long id)
        {
            try
            {
                ItemNotaFiscal itemNotaFiscal = null;
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT item_nota_fiscal.id" +
                                                    ", item_nota_fiscal.valor_unitario_pago" +
                                                    ", lote.id" +
                                                    ", lote.quantidade_disponivel" +
                                                    ", lote.quantidade_total_inicial" +
                                                    ", lote.numero_lote" +
                                                    ", lote.descricao" +
                                                    ", lote.validade" +
                                                    ", lote.data_compra" +
                                                    " FROM item_nota_fiscal" +
                                                    " LEFT JOIN lote ON (item_nota_fiscal.fk_id_lote=lote.id)" +
                                               " WHERE item_nota_fiscal.id=@Id;";
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                itemNotaFiscal = await PreenchePropriedades(reader);
                            }
                        }
                    }
                }
                return itemNotaFiscal;
            }
            catch (Exception e) { return null; }
        }
        public async Task<List<ItemNotaFiscal>> GetItensNotaFiscalPeloIdNotaFiscal(long idNotaFiscal)
        {
            try
            {
                var listResponse = new List<ItemNotaFiscal>();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT item_nota_fiscal.id" +
                                                    ", item_nota_fiscal.valor_unitario_pago" +
                                                    ", lote.id" +
                                                    ", lote.quantidade_disponivel" +
                                                    ", lote.quantidade_total_inicial" +
                                                    ", lote.numero_lote" +
                                                    ", lote.descricao" +
                                                    ", lote.validade" +
                                                    ", lote.data_compra" +
                                                    " FROM item_nota_fiscal" +
                                                    " LEFT JOIN lote ON (item_nota_fiscal.fk_id_lote=lote.id)" +
                                               " WHERE item_nota_fiscal.fk_id_nota_fiscal=@IdNotaFiscal;";
                        command.Parameters.AddWithValue("@IdNotaFiscal", idNotaFiscal);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var itemNotaFiscal = await PreenchePropriedades(reader);
                                listResponse.Add(itemNotaFiscal);
                            }
                        }
                    }
                }
                return listResponse;
            }
            catch (Exception e) { return null; }
        }
        public async Task<bool> DeleteItemNotaFiscal(long id)
        {
            int rowCount = 0;
            try
            {
                var listResponse = new List<ItemNotaFiscal>();
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM item_nota_fiscal" +
                                              " WHERE Iid=@Id";
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }

        }
        public async Task<bool> UpdateItemNotaFiscal(long id, ItemNotaFiscal itemNotaFiscal)
        {
            int rowCount = 0;
            try
            {
                using (var conn = new MySqlConnection(_connection.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "UPDATE item_nota_fiscal" +
                                              " SET valor_unitario_pago=@ValorUnitarioPago" +
                                              " WHERE id=@Id;";
                        command.Parameters.AddWithValue("@ValorUnitarioPago", itemNotaFiscal.ValorUnitarioPago);
                        command.Parameters.AddWithValue("@Id", id);
                        rowCount = await command.ExecuteNonQueryAsync();
                    }
                }
                return rowCount > 0;
            }
            catch (Exception e) { return false; }
        }
        private async Task<ItemNotaFiscal> PreenchePropriedades(MySqlDataReader reader)
        {
            long id = reader.GetInt32(0);
            double ValorUnitarioPago = reader.GetDouble(1);
            long idLote = reader.GetInt32(2);
            int QuantidadeDisponivel;
            try
            {
                QuantidadeDisponivel = reader.GetInt32(3);
            }
            catch
            {
                QuantidadeDisponivel = 0;

            }
            int QuantidadeTotalInicial;
            try
            {
                QuantidadeTotalInicial = reader.GetInt32(4);
            }
            catch
            {
                QuantidadeTotalInicial = 0;
            }
            string NumeroLote;
            try
            {
                NumeroLote = reader.GetString(5);
            }
            catch
            {
                NumeroLote = "";
            }
            string Descricao;
            try
            {
                Descricao = reader.GetString(6);
            }
            catch
            {
                Descricao = "";
            }
            DateTime Validade;
            try
            {
                Validade = reader.GetDateTime(7);
            }
            catch
            {
                Validade = new DateTime();
            }
            DateTime DataCompra;
            try
            {
                DataCompra = reader.GetDateTime(8);
            }
            catch
            {
                DataCompra = new DateTime();
            }
            return new ItemNotaFiscal()
            {
                Id = reader.GetInt32(0),
                ValorUnitarioPago = reader.GetDouble(1),
                Lote = new Lote()
                {
                    Id = reader.GetInt32(2),
                    QuantidadeDisponivel = reader.GetInt32(3),
                    QuantidadeTotalInicial = reader.GetInt32(4),
                    NumeroLote = reader.GetString(5),
                    Descricao = reader.GetString(6),
                    Validade = reader.GetDateTime(7),
                    DataCompra = reader.GetDateTime(8)
                }
            };
        }

    }
}
