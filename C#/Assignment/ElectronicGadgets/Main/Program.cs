using ElectronicGadgets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicGadgets.DAO;


namespace ElectronicGadgets.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== TechShop Menu =====");
                Console.WriteLine("1. Customer Registration");
                Console.WriteLine("2. Product Catalog Management");
                Console.WriteLine("3. Placing Customer Orders");
                Console.WriteLine("4. Tracking Order Status");
                Console.WriteLine("5. Inventory Management");
                Console.WriteLine("6. Sales Reporting");
                Console.WriteLine("7. Customer Account Updates");
                Console.WriteLine("8. Payment Processing");
                Console.WriteLine("9. Product Search and Recommendations");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterCustomer();
                        break;
                    case "2":
                        ManageProductCatalog();
                        break;
                    case "3":
                        PlaceOrder();
                        break;
                    case "4":
                        TrackOrderStatus();
                        break;
                    case "5":
                        ManageInventory();
                        break;
                    case "6":
                        GenerateSalesReport();
                        break;
                    case "7":
                        UpdateCustomerAccount();
                        break;
                    case "8":
                        ProcessPayment();
                        break;
                    case "9":
                        SearchProducts();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Try again.");
                        break;
                }
            }
        }

        static void RegisterCustomer()
        {
            Console.WriteLine("--- Register Customer ---");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();

            Customers customer = new Customers(0, firstName, lastName, email, phone, address);
            ICustomerDao dao = new CustomerDao();
            dao.RegisterCustomer(customer);
        }

        static void ManageProductCatalog()
        {
            Console.WriteLine("--- Manage Product Catalog ---");
            Console.Write("Product Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Products product = new Products(0, name, description, price);
            IProductDao dao = new ProductDao();
            dao.AddProduct(product);
        }

        static void PlaceOrder()
        {
            Console.WriteLine("--- Place Order ---");
            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            IOrderDao dao = new OrderDao();
            dao.PlaceOrder(customerId, productId, quantity);
        }

        static void TrackOrderStatus()
        {
            Console.WriteLine("--- Track Order Status ---");
            Console.Write("Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            IOrderDao dao = new OrderDao();
            dao.GetOrderStatus(orderId);
        }

        static void ManageInventory()
        {
            Console.WriteLine("--- Manage Inventory ---");
            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Quantity to Add: ");
            int qty = int.Parse(Console.ReadLine());

            IInventoryDao dao = new InventoryDao();
            dao.AddToInventory(productId, qty);
        }

        static void GenerateSalesReport()
        {
            Console.WriteLine("--- Generate Sales Report ---");
            ISalesReportDao dao = new SalesReportDao();
            dao.GenerateReport();
        }

        static void UpdateCustomerAccount()
        {
            Console.WriteLine("--- Update Customer Account ---");
            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("New Email: ");
            string email = Console.ReadLine();
            Console.Write("New Phone: ");
            string phone = Console.ReadLine();
            Console.Write("New Address: ");
            string address = Console.ReadLine();

            ICustomerDao dao = new CustomerDao();
            dao.UpdateCustomerInfo(customerId, email, phone, address);
        }

        static void ProcessPayment()
        {
            Console.WriteLine("--- Process Payment ---");
            Console.Write("Order ID: ");
            int orderId = int.Parse(Console.ReadLine());
            Console.Write("Payment Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Payment Method (e.g., Card, UPI): ");
            string method = Console.ReadLine();

            IPaymentDao dao = new PaymentDao();
            dao.ProcessPayment(orderId, amount, method);
        }

        static void SearchProducts()
        {
            Console.WriteLine("--- Search Products ---");
            Console.Write("Enter keyword: ");
            string keyword = Console.ReadLine();

            IProductDao dao = new ProductDao();
            dao.SearchProducts(keyword);
        }
    }
}
