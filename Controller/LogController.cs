using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Data;
using Model;

namespace Controller
{
    public class LogController
    {
        // Listar Logs
        public List<Log> ListarLogs()
        {
            List<Log> listaLogs = new List<Log>();

            OracleConnection conexao = new DataBase().ObterConexao();

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                string sql = "SELECT Id, DataHora, Evento, Usuario FROM LOGSGS ORDER BY DataHora DESC";

                using (OracleCommand command = new OracleCommand(sql, conexao))
                {
                    try
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Log log = new Log
                                {
                                    Id = reader.GetInt32(0),
                                    DataHora = reader.GetDateTime(1),
                                    Evento = reader.GetString(2),
                                    Usuario = reader.GetString(3)
                                };

                                listaLogs.Add(log);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao listar logs: " + ex.Message);
                    }
                }

                conexao.Close();
            }
            else
            {
                Console.WriteLine("Erro: conexão não foi aberta.");
            }

            return listaLogs;
        }

        // Registro de Logs
        public void RegistrarLog(Log log)
        {
            OracleConnection conexao = new DataBase().ObterConexao();

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                string sql = "INSERT INTO LOGSGS (Evento, Usuario) VALUES (:evento, :usuario)";

                using (OracleCommand command = new OracleCommand(sql, conexao))
                {
                    command.Parameters.Add(new OracleParameter("evento", log.Evento));
                    command.Parameters.Add(new OracleParameter("usuario", log.Usuario));

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Log registrado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao registrar log: " + ex.Message);
                    }
                }

                conexao.Close();
            }
            else
            {
                Console.WriteLine("Erro: conexão não foi aberta.");
            }
        }

    }
}
