using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace наследственность
{
    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin("Админ", "adminadmin@example.com", 8000);
            admin.ManageUsers();
            Customer customer = new Customer("Покупатель", "arturzajnutdinov000@gmail.com", 0);
            VipCustomer vipCustomer = new VipCustomer("VIP Покупатель", "vip_arturzajnutdinov000@gmail.com", 0);
            customer.SetBalance(100000);
            vipCustomer.SetBalance(1000000);
            customer.DisplayInfo();
            vipCustomer.DisplayInfo();
            Product prod1 = new Product("Телевизор", 50000, "Электроника");
            Product prod2 = new Product("Футболка", 2000, "Одежда");
            Product prod3 = new Product("Книга", 750, "Книги");
            customer.AddToCart(prod1);
            customer.AddToCart(prod2);
            customer.AddToCart(prod3);
            customer.PlaceOrder();
            vipCustomer.AddToCart(prod1);
            vipCustomer.AddToCart(prod2);
            vipCustomer.AddToCart(prod3);
            vipCustomer.PlaceOrder();
            customer.DisplayInfo();
            vipCustomer.DisplayInfo();
        }
    }

    class User
    {
        public string name;
        public string email;
        public double balance;

        public User(string name, string email, double balance)
        {
            this.name = name;
            this.email = email;
            this.balance = balance;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Имя: {name}, Email: {email}");
        }
    }

    class Customer : User
    {
        public double balance;
        public List<Product> cart = new List<Product>();

        public Customer(string name, string email, double balance) : base(name, email, balance)
        {
            this.balance = balance;
        }

        public void SetBalance(double newBalance)
        {
            balance = newBalance;
        }

        public double GetBalance()
        {
            return balance;
        }

        public void AddToCart(Product product)
        {
            cart.Add(product);
            Console.WriteLine($"Товар {product.GetName()} добавлен в корзину.");
        }

        public void RemoveFromCart(Product product)
        {
            if (cart.Remove(product))
            {
                Console.WriteLine($"Товар {product.GetName()} был удален из корзины.");
            }
            else
            {
                Console.WriteLine($"Товар {product.GetName()} не найден в корзине.");
            }
        }

        public void PlaceOrder()
        {
            Order order = new Order(this, new List<Product>(cart));
            double totalPrice = order.GetTotalPrice();

            if (balance >= totalPrice)
            {
                balance -= totalPrice;
                Console.WriteLine("Заказ оформлен.");
                Console.WriteLine($"Общая стоимость заказа: {totalPrice}");
                cart.Clear();
            }
            else
            {
                Console.WriteLine("Недостаточно средств для оформления заказа.");
            }
        }

        public void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Баланс: {balance}");
        }
    }

    class VipCustomer : Customer
    {
        public double discountRate = 0.10;

        public VipCustomer(string name, string email, double balance) : base(name, email, balance) { }

        public void PlaceOrder()
        {
            Order order = new Order(this, new List<Product>(cart));
            double totalPrice = order.GetTotalPrice();
            totalPrice *= (1 - discountRate);

            if (GetBalance() >= totalPrice)
            {
                SetBalance(GetBalance() - totalPrice);
                Console.WriteLine("VIP Заказ оформлен с учетом скидки.");
                Console.WriteLine($"Общая стоимость заказа с учетом скидки: {totalPrice}");
                cart.Clear();
            }
            else
            {
                Console.WriteLine("Недостаточно средств для оформления VIP заказа.");
            }
        }

        public void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Статус: VIP клиент");
        }
    }

    class Product
    {
        public string name;
        public double price;
        public string category;

        public Product(string name, double price, string category)
        {
            string Name = name;
            this.price = price;
            string Category = category;
        }

        public string GetName()
        {
            return name;
        }

        public double GetPrice()
        {
            return price;
        }
    }

    class Order
    {
        public Customer customer;
        public List<Product> products;

        public Order(Customer customer, List<Product> products)
        {
            this.customer = customer;
            this.products = products;
        }

        public double GetTotalPrice()
        {
            double total = 0;
            foreach (var prod in products)
            {
                total += prod.GetPrice();
            }
            return total;
        }
    }

    class Admin : User
    {
        public Admin(string name, string email, double balance) : base(name, email, balance) { }

        public void ManageUsers()
        {
            Console.WriteLine("Администратор управляет пользователями.");
        }

        public void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Роль: Администратор");
        }
    }
}
