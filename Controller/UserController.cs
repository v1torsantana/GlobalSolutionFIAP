using System;
using Oracle.ManagedDataAccess.Client;
using Data;
using Model;

namespace Controller
{
    public class UserController
    {
        public User ValidarLogin(string nomeUsuario, string senha)
        {
            User usuario = null;

            OracleConnection conexao = new DataBase().ObterConexao();

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                string sql = "SELECT Id, NomeUsuario, Senha, NomeCompleto, DataCadastro FROM USERSGS WHERE NomeUsuario = :nomeUsuario AND Senha = :senha";

                using (OracleCommand command = new OracleCommand(sql, conexao))
                {
                    command.Parameters.Add(new OracleParameter("nomeUsuario", nomeUsuario));
                    command.Parameters.Add(new OracleParameter("senha", senha));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new User
                            {
                                Id = reader.GetInt32(0),
                                NomeUsuario = reader.GetString(1),
                                Senha = reader.GetString(2),
                                NomeCompleto = reader.GetString(3),
                                DataCadastro = reader.GetDateTime(4)
                            };
                        }
                    }
                }

                conexao.Close();
            }
            else
            {
                Console.WriteLine("Erro: conexão com o banco não foi aberta.");
            }

            return usuario;
        }
    }
}
