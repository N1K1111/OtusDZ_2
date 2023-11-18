using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WebClient
{
    static class Program
    {
        const string server_adress = "https://localhost:7249";
        private static HttpClient client = new()
        {
            BaseAddress = new Uri(server_adress)
        };
        static Task Main(string[] args)
        {
            while (true)
            {
             
                Console.WriteLine("Введите ID клиента: ");
                string id = Console.ReadLine();

                Console.WriteLine("Введите 1 если нужно запросить пользователя с текущим ID");
                Console.WriteLine("Введите 2 если нужно создать нового пользователя со случайными данными");

                int i = int.Parse(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        {
                            var customer = GetCustomerById(id);
                            if (customer != null)
                            {
                                Console.WriteLine($"ID пользователя: {customer.Id}");
                                Console.WriteLine($"Имя и фамилия клиента: {customer.FirstName} {customer.LastName}");
                            }
                        }
                        break;
                    case 2:
                        {
                            var customer = RandomCustomer();
                            customer.Id = Convert.ToInt64(id);
                            AddCustomer(customer);
                        }
                    break;
                    default:
                        break;
                }
            }
        }

        private static void AddCustomer(CustomerCreateRequest request)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
            "application/json");

            using HttpResponseMessage response = client.PostAsync(
                "customers",
                jsonContent).Result;

            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine("Статус код: " + response.StatusCode);

        }
        private static Customer GetCustomerById(string id)
        {
            using var client = new HttpClient();

            var result = client.GetAsync(Program.server_adress + $"/customers/{id}").Result;

            Console.WriteLine("Статус код: " + result.StatusCode);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Пользователь успешно получен с сервера!");

                var res = result.Content.ReadAsStringAsync().Result;
                var customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(res);
                return customer;
            }

            return null;
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            string[] firstNames = new string[5] { "Никита", "Петр", "Пётр", "Андрей", "Сергей" };
            string[] lastNames = new string[5] { "Иванов", "Смирнов", "Сидоров", "Лебедев", "Орлов" };

            Random r = new Random();

            var createCustomerRequest = new CustomerCreateRequest(
            firstNames[r.Next(0, 5)],
            lastNames[r.Next(0, 5)]
            );

            return createCustomerRequest;
        }
    }
}
