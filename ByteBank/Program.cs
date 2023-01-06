using System.Net;
using System.Runtime.CompilerServices;

namespace ByteBank
{
    public class Client
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
            Console.WriteLine("7 - Logout");
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

    static Client getClient(Client[] clients, string cpf)
        {
            for (int i = 0; i < clients.Length; i++)
            {
                if (clients[i].cpf == cpf)
                    return clients[i];
            }
            return null;
        }

    static void DepositAmounAtAccount(Client[] clients)
        {
            Console.Write("Digite o CPF da conta: ");
            string cpf = Console.ReadLine();
            Client client = getClient(clients, cpf);
            
            Console.Write("Digite o valor a ser depositado: ");
            double amount = double.Parse(Console.ReadLine());
            client.balance += amount;
        }

        static void WithdrawAmountAtAccount(Client[] clients)
        {
            Console.Write("Digite o CPF da conta: ");
            string cpf = Console.ReadLine();
            Client client = getClient(clients, cpf);

            Console.Write("Digite o valor que você deseja sacar: ");
            double amount = double.Parse(Console.ReadLine());
            client.balance -= amount;
        }

        static void TransferAmountAtAccount(Client[] clients)
        {
            Client client1, client2;
            double amount;
            bool isValidAmount = true;
            do
            {
                Console.Clear();
                if (!isValidAmount)
                {
                    Console.WriteLine("Saldo insulficiente. Tente um valor menor.");
                    Console.WriteLine();
                }

                Console.Write("Digite o CPF da conta: ");
                string cpf1 = Console.ReadLine();
                client1 = getClient(clients, cpf1);

                Console.Write("Digite o valor que você deseja transferir: ");
                amount = double.Parse(Console.ReadLine());

                Console.Write("Digite o CPF da conta para a qual deseja transferir: ");
                string cpf2 = Console.ReadLine();
                client2 = getClient(clients, cpf2);
                
                isValidAmount = amount <= client1.balance;

                
            } while (!isValidAmount);

            client1.balance -= amount;
            client2.balance += amount;
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
                Console.WriteLine($"Saldo: {clients[i].balance}");
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
                    case 4: 
                        Console.Clear();
                        DepositAmounAtAccount(clients);
                        break;
                    case 5:
                        Console.Clear();
                        WithdrawAmountAtAccount(clients);
                        break;

                    case 6:
                        Console.Clear();
                        TransferAmountAtAccount(clients);
                        break;
                }
                

            } while(option != 0);
        }

    }
}