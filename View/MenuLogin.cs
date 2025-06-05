using System;
using Controller;
using Model;

namespace View
{
    public class MenuLogin
    {
        public void Exibir()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== LOGIN NO SISTEMA =====");
                Console.Write("Nome de usuário: ");
                string nomeUsuario = Console.ReadLine();

                Console.Write("Senha: ");
                string senha = Console.ReadLine();

                UserController userController = new UserController();

                User usuarioLogado = userController.ValidarLogin(nomeUsuario, senha);

                if (usuarioLogado != null)
                {
                    Console.WriteLine($"\nBem-vindo, {usuarioLogado.NomeCompleto}!");

                    // Chama o MenuPrincipal
                    MenuPrincipal menuPrincipal = new MenuPrincipal();
                    menuPrincipal.Exibir();
                }
                else
                {
                    Console.WriteLine("\nLogin inválido. Tente novamente.");
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        }
    }
}
