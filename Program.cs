using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypożyczalnia
{
    internal class Program
    {

        class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime LicenseDate { get; set; }

            public Customer(int id, string name, DateTime licenseDate)
            {
                Id = id;
                Name = name;
                LicenseDate = licenseDate;
            }
        }

        class Car
        {
            public int Id { get; set; }
            public string BrandModel { get; set; }
            public string Segment { get; set; }
            public string FuelType { get; set; }
            public decimal PricePerDay { get; set; }
            public bool IsAvailable { get; set; }

            public Car(int id, string brandModel, string segment, string fuelType, decimal pricePerDay, bool isAvailable)
            {
                Id = id;
                BrandModel = brandModel;
                Segment = segment;
                FuelType = fuelType;
                PricePerDay = pricePerDay;
                IsAvailable = isAvailable;
            }
        }

        static List<Customer> customers = new List<Customer>();
        static List<Car> cars = new List<Car>();

        static void Main(string[] args)
        {
            InitializeData();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Wyświetl listę klientów");
                Console.WriteLine("2. Wyświetl listę samochodów");
                Console.WriteLine("3. Wypożycz samochód");
                Console.WriteLine("4. Wyjdź");
                Console.Write("Wybierz opcję: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        DisplayCustomers();
                        break;
                    case "2":
                        DisplayCars();
                        break;
                    case "3":
                        RentCar();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Niepoprawna opcja. Spróbuj ponownie.");
                        break;
                }
            }
        }

        static void InitializeData()
        {
            customers.Add(new Customer(1, "Jan Nowak", new DateTime(2021, 3, 4)));
            customers.Add(new Customer(2, "Agnieszka Kowalska", new DateTime(1999, 1, 15)));
            customers.Add(new Customer(3, "Robert Lewandowski", new DateTime(2010, 12, 18)));
            customers.Add(new Customer(4, "Zofia Plucińska", new DateTime(2020, 4, 29)));
            customers.Add(new Customer(5, "Grzegorz Braun", new DateTime(2015, 7, 12)));

            cars.Add(new Car(1, "Škoda Citigo", "mini", "benzyna", 70, true));
            cars.Add(new Car(2, "Toyota Aygo", "mini", "benzyna", 90, true));
            cars.Add(new Car(3, "Fiat 500", "mini", "elektryczny", 110, true));
            cars.Add(new Car(4, "Ford Focus", "kompakt", "diesel", 160, true));
            cars.Add(new Car(5, "Kia Ceed", "kompakt", "benzyna", 150, true));
            cars.Add(new Car(6, "Volkswagen Golf", "kompakt", "benzyna", 160, true));
            cars.Add(new Car(7, "Hyundai Kona Electric", "kompakt", "elektryczny", 180, true));
            cars.Add(new Car(8, "Audi A6 Allroad", "premium", "diesel", 290, true));
            cars.Add(new Car(9, "Mercedes E270 AMG", "premium", "benzyna", 320, true));
            cars.Add(new Car(10, "Tesla Model S", "premium", "elektryczny", 350, true));
        }

        static void DisplayCustomers()
        {
            Console.WriteLine("Lista klientów:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id}, Imię i nazwisko: {customer.Name}, Data uzyskania prawa jazdy: {customer.LicenseDate.ToShortDateString()}");
            }
            Console.WriteLine();
        }

        static void DisplayCars()
        {
            Console.WriteLine("Lista samochodów:");
            foreach (var car in cars)
            {
                string status = car.IsAvailable ? "Dostępny" : "Niedostępny";
                Console.WriteLine($"Id: {car.Id}, Marka i model: {car.BrandModel}, Segment: {car.Segment}, Rodzaj paliwa: {car.FuelType}, Cena za dobę: {car.PricePerDay} PLN, Status: {status}");
            }
            Console.WriteLine();
        }

        static void RentCar()
        {
            Console.WriteLine("Podaj Id klienta:");
            int customerId = int.Parse(Console.ReadLine());
            Customer customer = customers.Find(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("Klient o podanym Id nie istnieje.");
                return;
            }

            Console.WriteLine("Podaj preferencje wypożyczenia:");
            Console.Write("Segment samochodu (mini, kompakt, premium): ");
            string segment = Console.ReadLine();
            Console.Write("Rodzaj paliwa (benzyna, diesel, elektryczny): ");
            string fuelType = Console.ReadLine();
            Console.Write("Okres wypożyczenia (ilość dni): ");
            int days = int.Parse(Console.ReadLine());

            Car selectedCar = cars.Find(c => c.Segment == segment && c.FuelType == fuelType && c.IsAvailable);
            if (selectedCar != null)
            {
                selectedCar.IsAvailable = false;
                DateTime rentalDate = DateTime.Now;
                DateTime returnDate = rentalDate.AddDays(days);
                decimal totalPrice = selectedCar.PricePerDay * days;
                Console.WriteLine($"Data wypożyczenia: {rentalDate.ToShortDateString()}, Imię i nazwisko Klienta: {customer.Name}, Data zwrotu pojazdu: {returnDate.ToShortDateString()}, Marka i model pojazdu: {selectedCar.BrandModel}, Całkowita cena za wypożyczenie: {totalPrice} PLN");
            }
            else
            {
                Console.WriteLine("Brak dostępnych samochodów spełniających kryteria.");
            }
        }
    }

}

