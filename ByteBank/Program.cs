using ByteBank;
using System.Net;
using System.Runtime.CompilerServices;

namespace ByteBank
{
    public class Client
    {
        public string name { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string type;
        public BankAccount account { get; set; }
        public Client()
        {  
            this.name = string.Empty;
            this.email = string.Empty;
            this.cpf = string.Empty;
            this.password = string.Empty;
            this.account = new BankAccount();
        }
    }

    public class ClientsRepository
    {
        public Client[] clients;

        public ClientsRepository()
        {
            this.clients = new Client[1];
            Client userAdmin = new Client();
            userAdmin.cpf = "000.000.000-00";
            userAdmin.password = "admin";
            userAdmin.email = "admin@admin.com";
            userAdmin.name = "Admin";
            userAdmin.type = "Admin";
            userAdmin.account.balance = 0;
            this.clients[0] = userAdmin;
        }

        public void CreateClient(Client client)
        {
            this.clients = this.clients.Append(client).ToArray();
        }

        public Client getClient(string cpf)
        {
            for (int i = 0; i < this.clients.Length; i++)
            {
                if (this.clients[i].cpf == cpf)
                    return this.clients[i];
            }
            return null;
        }

    }

    public class Authentication
    {
        public Client currentUser;
        public bool Login(Client[] clients, string cpf, string password)
        {
            for (int i = 0; i < clients.Length; i++)
            {
                if (clients[i].cpf == cpf && clients[i].password == password)
                {
                    this.currentUser = clients[i];
                    return true;
                }
            }
            this.currentUser = null;
            return false;
        }

        public void Logout()
        {
            this.currentUser = null;
        }
    }

    public class BankAccount
    {
        public double balance { get; set; }

        static Client getClient(Client[] clients, string cpf)
        {
            for (int i = 0; i < clients.Length; i++)
            {
                if (clients[i].cpf == cpf)
                    return clients[i];
            }
            return null;
        }


        public void DepositAmounAtAccount(double amount)
        {
            this.balance += amount;
        }

        public bool WithdrawAmountAtAccount(double amount)
        {
            if (amount <= this.balance)
            {
                this.balance -= amount;
                return true;
            } else
            {
                return false;
            }
        }

        public bool TransferAmountAtAccount(Client receivingClient, double amountToTransfer)
        {
            if (amountToTransfer <= this.balance)
            {
                this.balance -= amountToTransfer;
                receivingClient.account.balance += amountToTransfer;
                return true;
            } else
            {
                return false;
            } 
        }
    }

    public struct LoginFields
    {
        public string cpf;
        public string password;
    }

    public struct TransferFields
    {
        public string cpf;
        public double amount;
    }

    public class UserInterface
    {
        static public void Header(Client client)
        {
            Console.Clear();
            Console.WriteLine(">>> ByteBank <<<");
            Console.WriteLine();
            Console.WriteLine($"Nome: {client.name} | CPF: {client.cpf} | Saldo: {client.account.balance}");
            Console.WriteLine();
        }

        static public int MainMenu(Client client)
        {
            Console.WriteLine("1 - Logout");
            Console.WriteLine("2 - Depositar");
            Console.WriteLine("3 - Sacar");
            Console.WriteLine("4 - Transferir");
            if (client.type == "Admin")
            {
                Console.WriteLine("5 - Cadastrar cliente");
                Console.WriteLine("6 - Listar clientes");
            }
            Console.WriteLine();
            Console.Write("Opção: ");
            return int.Parse(Console.ReadLine());
        }

        static public Client InputClient()
        {
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
            newClient.account.balance = 0;
            return newClient;
        }

        static public void ListClients(Client[] clients)
        {
            Console.WriteLine("TODOS CLIENTES DO SISTEMA");
            Console.WriteLine();
            for (int i = 0; i < clients.Length; i++)
            {
                Console.WriteLine($"# {i}");
                Console.WriteLine($"Nome: {clients[i].name}");
                Console.WriteLine($"CPF: {clients[i].cpf}");
                Console.WriteLine($"Email: {clients[i].email}");
                Console.WriteLine($"Saldo: {clients[i].account.balance}");
                Console.WriteLine("------------------------------");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static public void ShowLoginErrorMsg()
        {
            Console.WriteLine();
            Console.WriteLine("Cpf ou senha inválido, tente novamente!");
            Console.WriteLine();
        }

        static public LoginFields InputLogin()
        {
            LoginFields loginFields;
            Console.WriteLine(">>> Faça seu login <<<");
            Console.WriteLine();
            Console.Write("CPF: ");
            loginFields.cpf = Console.ReadLine();
            Console.Write("Senha: ");
            loginFields.password = Console.ReadLine();

            return loginFields;
        }

        static public double WithdrawInputAmount()
        {
            Console.WriteLine("SAQUE");
            Console.WriteLine();
            Console.Write("Valor: ");
            double amountToWithdraw = double.Parse(Console.ReadLine());
            Console.WriteLine();
            return amountToWithdraw;
        }

        static public double DepositInputAmount ()
        {
            Console.WriteLine("DEPÓSITO");
            Console.WriteLine();
            Console.Write("Valor: ");
            double amountToDeposit = double.Parse(Console.ReadLine());
            Console.WriteLine();
            return amountToDeposit;
        }

        static public TransferFields InputTransfer ()
        {
            Console.WriteLine("TRANSFERÊNCIA");
            Console.WriteLine();
            TransferFields transferFields = new TransferFields();
            Console.Write("CPF do destinatário: ");
            transferFields.cpf = Console.ReadLine();
            Console.Write("Valor: ");
            transferFields.amount = double.Parse(Console.ReadLine());

            return transferFields;
        }

        static public void TransferSuccessMsg()
        {
            Console.WriteLine();
            Console.WriteLine("Transferência realizada com sucesso!");
            Console.WriteLine();
        }

        static public void TransferErrorMsg()
        {
            Console.WriteLine();
            Console.WriteLine("Saldo insulficiente. Tente um valor menor.");
            Console.WriteLine();
        }

        static public void WithdrawErrorMsg()
        {
            Console.WriteLine();
            Console.WriteLine("Saldo insulficiente. Tente um valor menor.");
            Console.WriteLine();
        }
    }

    public class Program {

        public static void Main(string[] args)
        {
            ClientsRepository repository = new ClientsRepository();   
            Authentication authentication = new Authentication();
            int option;
            bool isLoginError = false;

            do
            {
                while (authentication.currentUser == null)
                {
                    Console.Clear();
                    if (isLoginError)
                    {
                        UserInterface.ShowLoginErrorMsg(); 
                    }
                    
                    LoginFields loginFields = UserInterface.InputLogin();
                    authentication.Login(repository.clients, loginFields.cpf, loginFields.password);
                    isLoginError = authentication.currentUser == null;
                } ;

                UserInterface.Header(authentication.currentUser);
                option = UserInterface.MainMenu(authentication.currentUser);

                switch(option)
                {
                    case 1:
                        Console.Clear();
                        authentication.Logout();
                        break;
                    case 2:
                        UserInterface.Header(authentication.currentUser);
                        double amountToDeposit = UserInterface.DepositInputAmount();
                        authentication.currentUser.account.DepositAmounAtAccount(amountToDeposit);
                        break;
                    case 3:
                        UserInterface.Header(authentication.currentUser);
                        double amountToWithdraw = UserInterface.WithdrawInputAmount();
                        bool isValidWithdrawAmount = authentication
                            .currentUser
                            .account
                            .WithdrawAmountAtAccount(amountToWithdraw);
                        
                        if (!isValidWithdrawAmount)
                        {
                            UserInterface.WithdrawErrorMsg();
                            Console.ReadKey();
                        }
                        
                        break;
                    case 4:
                        UserInterface.Header(authentication.currentUser);
                        TransferFields transferFields = UserInterface.InputTransfer();

                        Client receivingClient = repository.getClient(transferFields.cpf);

                        if(receivingClient == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Conta inexistente!");
                            Console.ReadKey();
                            break;
                        }
                        
                        bool isTransferValidAmount = 
                            authentication
                            .currentUser
                            .account
                            .TransferAmountAtAccount(receivingClient, transferFields.amount);

                        if (isTransferValidAmount)
                        {
                            UserInterface.TransferSuccessMsg();
                        } else
                        {
                            UserInterface.TransferErrorMsg();
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        UserInterface.Header(authentication.currentUser);
                        Client newClient = UserInterface.InputClient();
                        repository.CreateClient(newClient);
                        break;
                    case 6:
                        UserInterface.Header(authentication.currentUser);
                        UserInterface.ListClients(repository.clients);
                        break;
                }
                

            } while(option != 0);
        }

    }
}