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
        public static void Main(string[] args)
        {
            Client[] clients = new Client[10];
            int option;

            do
            {
                Program.Header();
                option = Program.MainMenu();

                switch(option)
                {
                    case 1: 
                        Client c = InputClient();
                        clients.Append(c);
                        break;
                }
                

            } while(option != 0);
        }

    }
}