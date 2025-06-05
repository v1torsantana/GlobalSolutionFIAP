using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Data;
using Model;

namespace Controller
{
    public class FalhaController
    {
        // Registrar Falha e Cadastro de Logs
        public void RegistrarFalha(Falha falha)
        {
            OracleConnection conexao = new DataBase().ObterConexao();

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                string sql = "INSERT INTO FALHASGS (TipoFalha, Descricao, UsuarioRegistro) VALUES (:tipoFalha, :descricao, :usuarioRegistro)";

                using (OracleCommand command = new OracleCommand(sql, conexao))
                {
                    command.Parameters.Add(new OracleParameter("tipoFalha", falha.TipoFalha));
                    command.Parameters.Add(new OracleParameter("descricao", falha.Descricao));
                    command.Parameters.Add(new OracleParameter("usuarioRegistro", falha.UsuarioRegistro));

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Falha registrada com sucesso.");

                        LogController logController = new LogController();

                        Log novoLog = new Log
                        {
                            Evento = $"Falha registrada: {falha.TipoFalha} - {falha.Descricao}",
                            Usuario = falha.UsuarioRegistro,
                            DataHora = DateTime.Now
                        };

                        logController.RegistrarLog(novoLog);
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao registrar falha: " + ex.Message);
                    }
                }

                conexao.Close();
            }
            else
            {
                Console.WriteLine("Erro: conexão não foi aberta.");
            }
        }

        // Listar Falhas
        public List<Falha> ListarFalhas()
        {
            List<Falha> listaFalhas = new List<Falha>();

            OracleConnection conexao = new DataBase().ObterConexao();

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                string sql = "SELECT Id, DataHora, TipoFalha, Descricao, UsuarioRegistro FROM FALHASGS ORDER BY DataHora DESC";

                using (OracleCommand command = new OracleCommand(sql, conexao))
                {
                    try
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Falha falha = new Falha
                                {
                                    Id = reader.GetInt32(0),
                                    DataHora = reader.GetDateTime(1),
                                    TipoFalha = reader.GetString(2),
                                    Descricao = reader.GetString(3),
                                    UsuarioRegistro = reader.GetString(4)
                                };

                                listaFalhas.Add(falha);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao listar falhas: " + ex.Message);
                    }
                }

                conexao.Close();
            }
            else
            {
                Console.WriteLine("Erro: conexão não foi aberta.");
            }

            return listaFalhas;
        }

        // Gerar Alerta
        public void GerarAlerta()
        {
            List<Falha> falhas = ListarFalhas();

            if (falhas.Count == 0)
            {
                Console.WriteLine("Nenhuma falha registrada. Não é possível gerar alerta.");
                return;
            }

            Console.WriteLine("===== FALHAS DISPONÍVEIS PARA GERAR ALERTA =====");

            foreach (var falha in falhas)
            {
                Console.WriteLine($"ID: {falha.Id} | Tipo: {falha.TipoFalha} | Data: {falha.DataHora} | Usuário: {falha.UsuarioRegistro}");
            }

            Console.Write("\nDigite o ID da falha para gerar alerta: ");

            int idSelecionado;

            try
            {
                idSelecionado = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entrada inválida. Erro: " + ex.Message);
                return;
            }

            Falha falhaSelecionada = falhas.Find(f => f.Id == idSelecionado);

            if (falhaSelecionada != null)
            {
                Console.WriteLine("\n===== ALERTA GERADO =====");
                Console.WriteLine($"Alerta para Falha ID: {falhaSelecionada.Id}");
                Console.WriteLine($"Tipo de Falha: {falhaSelecionada.TipoFalha}");
                Console.WriteLine($"DataHora: {falhaSelecionada.DataHora}");
                Console.WriteLine($"Usuário que registrou: {falhaSelecionada.UsuarioRegistro}");
                Console.WriteLine("=========================");

                // Registrar log da geração de alerta
                LogController logController = new LogController();

                Log novoLog = new Log
                {
                    Evento = $"Alerta gerado para falha: {falhaSelecionada.TipoFalha} - {falhaSelecionada.Descricao}",
                    Usuario = falhaSelecionada.UsuarioRegistro,
                    DataHora = DateTime.Now
                };

                logController.RegistrarLog(novoLog);
            }
            else
            {
                Console.WriteLine("Falha com ID informado não encontrada.");
            }
        }

    }
}
