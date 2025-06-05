Sistema de Registro de Falhas e Controle de Status

O sistema tem como objetivo registrar e acompanhar falhas de energia, incidentes cibernéticos e problemas de infraestrutura em ambientes corporativos. Ele permite o controle de falhas, geração de alertas, consulta de logs e geração de relatórios, com total rastreabilidade dos eventos.

Finalidade do Sistema

- Registrar falhas operacionais em sistemas e infraestrutura
- Gerar alertas para equipes técnicas
- Consultar histórico de logs de eventos
- Gerar relatórios de status consolidados
- Simular recuperação do sistema após falhas (teste de conectividade)
- Prover rastreabilidade e controle para ambientes críticos

Instruções de Execução

1. Clone este repositório:
2. Abra a solução no Visual Studio.
3. Configure o projeto UI como projeto de inicialização.
4. Confira se os dados da conexão com o banco estão corretos.
5. Execute o projeto (F5).
6. Teste o sistema através da interface em modo console.

Dependências
- .NET 8.0
- Oracle.ManagedDataAccess (Oracle Data Provider for .NET)
  - Instalado via NuGet no projeto Data
- Banco de Dados Oracle

Estrutura de Pastas

ProjetoGS
├── Controller
    └──Controller
       └── UserController.cs
       └── FalhaController.cs
       └── LogController.cs
       └── RelatorioController.cs
├── Data
    └──Data
       └── DataBase.cs
├── Model
    └── Model
        └── User.cs
        └── Falha.cs
        └── Log.cs
├── View
    └── View
        └── MenuLogin.cs
        └── MenuPrincipal.cs
├── UI
   └── Program.cs

