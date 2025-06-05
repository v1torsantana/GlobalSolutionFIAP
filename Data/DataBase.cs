using System;
using Oracle.ManagedDataAccess.Client;

namespace Data
{
    public class DataBase
    {
        private readonly string _connectionString = "User Id=rm99586;Password=070105;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public OracleConnection ObterConexao()
        {
            OracleConnection conexao = new OracleConnection(_connectionString);

            try
            {
                conexao.Open();
                Console.WriteLine("Conexão aberta com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao abrir conexão: " + ex.Message);
            }

            return conexao;
        }
    }
}
