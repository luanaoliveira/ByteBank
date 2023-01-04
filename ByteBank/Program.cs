using System.Runtime.CompilerServices;

namespace ByteBank
{
    public struct Client
    {
        public string name { get; set; }
        public string cpf { get; set; }
        public double balance { get; set; }
    }
    public class Program {

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
            newClient.balance = 0;
            return newClient;
        }

        static void ListClients (Client[] clients)
        {
            Console.WriteLine("Lista de clientes");
            Console.WriteLine();
            for (int i = 0; i < clients.Length; i++)
            {
                Console.WriteLine($"Index {i}");
                Console.WriteLine($"Nome: {clients[i].name}");
                Console.WriteLine($"CPF: {clients[i].cpf}");
                Console.WriteLine("----");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        public static void Main(string[] args)
        {
            Client[] clients = new Client[0];
            int option;

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
                }
                

            } while(option != 0);
        }

    }
}