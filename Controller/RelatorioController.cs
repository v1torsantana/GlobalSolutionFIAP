using Data;
using Model;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Controller
{
    public class RelatorioController
    {
        // Gerar Relatório de Status do Sistema
        public void GerarRelatorioStatus()
        {
            OracleConnection conexao = new DataBase().ObterConexao();

            if (conexao.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    // Total de Falhas
                    string sqlFalhas = "SELECT COUNT(*) FROM FALHASGS";
                    OracleCommand cmdFalhas = new OracleCommand(sqlFalhas, conexao);
                    int totalFalhas = Convert.ToInt32(cmdFalhas.ExecuteScalar());

                    // Total de Logs
                    string sqlLogs = "SELECT COUNT(*) FROM LOGSGS";
                    OracleCommand cmdLogs = new OracleCommand(sqlLogs, conexao);
                    int totalLogs = Convert.ToInt32(cmdLogs.ExecuteScalar());

                    // Última Falha
                    string sqlUltimaFalha = "SELECT MAX(DataHora) FROM FALHASGS";
                    OracleCommand cmdUltimaFalha = new OracleCommand(sqlUltimaFalha, conexao);
                    object ultimaFalhaObj = cmdUltimaFalha.ExecuteScalar();
                    string ultimaFalha = (ultimaFalhaObj == DBNull.Value) ? "Nenhuma falha registrada" : Convert.ToDateTime(ultimaFalhaObj).ToString();

                    // Último Log
                    string sqlUltimoLog = "SELECT MAX(DataHora) FROM LOGSGS";
                    OracleCommand cmdUltimoLog = new OracleCommand(sqlUltimoLog, conexao);
                    object ultimoLogObj = cmdUltimoLog.ExecuteScalar();
                    string ultimoLog = (ultimoLogObj == DBNull.Value) ? "Nenhum log registrado" : Convert.ToDateTime(ultimoLogObj).ToString();

                    // Exibir Relatório
                    Console.WriteLine("===== RELATÓRIO DE STATUS =====");
                    Console.WriteLine($"Total de Falhas Registradas: {totalFalhas}");
                    Console.WriteLine($"Total de Logs Registrados: {totalLogs}");
                    Console.WriteLine($"Data/Hora da Última Falha: {ultimaFalha}");
                    Console.WriteLine($"Data/Hora do Último Log: {ultimoLog}");
                    Console.WriteLine("===============================");

                    
                    LogController logController = new LogController();

                    Log novoLog = new Log
                    {
                        Evento = "Relatório de status gerado",
                        Usuario = "Sistema", 
                        DataHora = DateTime.Now
                    };

                    logController.RegistrarLog(novoLog);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao gerar relatório: " + ex.Message);
                }

                conexao.Close();
            }
            else
            {
                Console.WriteLine("Erro: conexão não foi aberta.");
            }
        }
        public void SimularRecuperacaoSistema()
        {
            Console.Clear();
            Console.WriteLine("===== SIMULAR RECUPERAÇÃO DE SISTEMA =====");

            try
            {
                OracleConnection conexao = new DataBase().ObterConexao();

                if (conexao.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Conectividade com banco de dados restabelecida.");
                    conexao.Close();

                    // Registrar log da recuperação concluída
                    LogController logController = new LogController();

                    Log novoLog = new Log
                    {
                        Evento = "Recuperação de sistema concluída com sucesso",
                        Usuario = "Sistema",
                        DataHora = DateTime.Now
                    };

                    logController.RegistrarLog(novoLog);
                }
                else
                {
                    Console.WriteLine("Falha ao restabelecer comunicação com banco de dados.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao tentar se conectar ao banco: " + ex.Message);
            }
        }


    }
}
