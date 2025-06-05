using System;
using Controller;
using Model;

namespace View
{
    public class MenuPrincipal
    {
        public void Exibir()
        {
            int opcao = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("===== MENU PRINCIPAL =====");
                Console.WriteLine("1 - Registrar Falha");
                Console.WriteLine("2 - Gerar Alerta de Falha Cadastrada");
                Console.WriteLine("3 - Consultar Logs de Eventos");
                Console.WriteLine("4 - Gerar Relatório de Status");
                Console.WriteLine("5 - Simular Recuperação de Sistema (Teste de Conectividade)");
                Console.WriteLine("6 - Sair");
                Console.Write("Escolha uma opção: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: entrada inválida. Digite um número.");
                    Console.WriteLine("Detalhe: " + ex.Message);
                    Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        RegistrarFalha();
                        break;
                    case 2:
                        GerarAlerta();
                        break;
                    case 3:
                        ConsultarLogs();
                        break;
                    case 4:
                        GerarRelatorioStatus();
                        break;
                    case 5:
                        SimularRecuperacaoSistema();
                        break;
                    case 6:
                        Console.WriteLine("Encerrando o sistema...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                if (opcao != 6)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }

            } while (opcao != 6);
        }

        // Opção 1 - Registrar Falha
        private void RegistrarFalha()
        {
            Console.Clear();
            Console.WriteLine("===== REGISTRAR FALHA =====");

            Console.Write("Tipo de Falha: ");
            string tipoFalha = Console.ReadLine();

            Console.Write("Descrição da Falha: ");
            string descricao = Console.ReadLine();

            Console.Write("Usuário que está registrando: ");
            string usuarioRegistro = Console.ReadLine();

            Falha falha = new Falha
            {
                TipoFalha = tipoFalha,
                Descricao = descricao,
                UsuarioRegistro = usuarioRegistro,
                DataHora = DateTime.Now
            };

            FalhaController falhaController = new FalhaController();
            falhaController.RegistrarFalha(falha);
        }

        // Opção 2 - Gerar Alerta
        private void GerarAlerta()
        {
            Console.Clear();
            Console.WriteLine("===== GERAR ALERTA DE FALHA =====");

            FalhaController falhaController = new FalhaController();
            falhaController.GerarAlerta();
        }

        // Opção 3 - Consultar Logs
        private void ConsultarLogs()
        {
            Console.Clear();
            Console.WriteLine("===== CONSULTAR LOGS DE EVENTOS =====");

            LogController logController = new LogController();
            var logs = logController.ListarLogs();

            if (logs.Count == 0)
            {
                Console.WriteLine("Nenhum log registrado.");
            }
            else
            {
                foreach (var log in logs)
                {
                    Console.WriteLine($"ID: {log.Id} | Evento: {log.Evento} | Data: {log.DataHora} | Usuário: {log.Usuario}");
                }
            }
        }

        // Opção 4 - Gerar Relatório de Status
        private void GerarRelatorioStatus()
        {
            Console.Clear();
            Console.WriteLine("===== GERAR RELATÓRIO DE STATUS =====");

            RelatorioController relatorioController = new RelatorioController();
            relatorioController.GerarRelatorioStatus();
        }

        // Opção 5 - Simular Recuperação de Sistema
        private void SimularRecuperacaoSistema()
        {
            Console.Clear();
            Console.WriteLine("===== SIMULAR RECUPERAÇÃO DE SISTEMA =====");

            RelatorioController relatorioController = new RelatorioController();
            relatorioController.SimularRecuperacaoSistema();
        }

    }
}
