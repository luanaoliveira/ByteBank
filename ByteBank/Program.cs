﻿using System.Runtime.CompilerServices;

namespace ByteBank
{
    public struct Client
    {
        public string name { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public double balance { get; set; }
        public string password { get; set; }
    }
    public class Program {
        static bool Login(Client[] clients)
        {
            Console.WriteLine(">>> Faça seu login <<<");
            Console.WriteLine();
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Senha: ");
            string password = Console.ReadLine();

            for(int i = 0; i < clients.Length; i++)
            {
                if (clients[i].cpf == cpf && clients[i].password == password)
                    return true;
            }

            return false;
        }

        static void Header()
        {
            Console.WriteLine(">>> ByteBank <<<");
            Console.WriteLine();
        }

        static int MainMenu() {
            Console.WriteLine("1 - Cadastrar cliente");
            Console.WriteLine("2 - Listar clientes");
            Console.WriteLine("3 - Ver saldo");
            Console.WriteLine("4 - Depositar na conta de um cliente");
            Console.WriteLine("5 - Sacar da conta de um cliente");
            Console.WriteLine("6 - Transferir entre contas");
            Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();
            Console.Write("Opção: ");
            return int.Parse(Console.ReadLine());
        }

        static Client InputClient()
        {
            Console.WriteLine();
            Console.WriteLine("CADASTRO DE CLIENTE");
            Console.WriteLine();
            Client newClient = new Client();
            Console.Write("Digite o nome: ");
            newClient.name = Console.ReadLine();
            Console.Write("Digite o cpf: ");
            newClient.cpf = Console.ReadLine();
            Console.Write("Digite seu email: ");
            newClient.email = Console.ReadLine();
            Console.Write("Digite sua senha: ");
            newClient.password = Console.ReadLine();
            newClient.balance = 0;
            return newClient;
        }

        static void ListClients (Client[] clients)
        {
            Console.WriteLine("Lista de clientes");
            Console.WriteLine();
            for (int i = 0; i < clients.Length; i++)
            {
                Console.WriteLine($"# {i}");
                Console.WriteLine($"Nome: {clients[i].name}");
                Console.WriteLine($"CPF: {clients[i].cpf}");
                Console.WriteLine($"Email: {clients[i].email}");
                Console.WriteLine("------------------------------");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static void BalanceClient(Client[] clients)
        {
            for (int i = 0; i < clients.Length; i++)
            {

                Console.WriteLine($"Saldo: {clients[i].balance}");
                Console.WriteLine("----");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        public static void Main(string[] args)
        {
            Client[] clients = new Client[0];
            Client userAdmin = new Client();
            userAdmin.cpf = "042.203.515-74";
            userAdmin.password = "admin";
            userAdmin.email = "admin@admin.com";
            userAdmin.name = "Admin";
            userAdmin.balance = 0;

            clients = clients.Append(userAdmin).ToArray();

            int option;
            bool isLogin = false;
            bool isLoginError = false; 

            do
            {
                Console.Clear();
                if(isLoginError)
                {
                    Console.WriteLine();
                    Console.WriteLine("Cpf ou senha inválido, tente novamente!");
                    Console.WriteLine();
                }
                    
                isLogin = Login(clients);
                isLoginError = !isLogin;

            } while (!isLogin);

            do
            {
                Console.Clear();
                Program.Header();
                option = Program.MainMenu();

                switch(option)
                {
                    case 1:
                        Console.Clear();
                        Client newClient = InputClient();
                        clients = clients.Append(newClient).ToArray();
                        break;
                    case 2:
                        Console.Clear();
                        ListClients(clients);
                        break;
                    case 3:
                        Console.Clear();
                        BalanceClient(clients);
                        break;
                }
                

            } while(option != 0);
        }

    }
}