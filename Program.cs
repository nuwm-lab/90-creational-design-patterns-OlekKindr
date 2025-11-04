using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 
    
    // Клас автомобіля (Product)
    public class Car
    {
        public string BodyType { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string TrimLevel { get; set; }

        public void DisplayInfo()
        {
            Console.WriteLine("=== Інформація про автомобіль ===");
            Console.WriteLine($"Тип кузова: {BodyType}");
            Console.WriteLine($"Двигун: {Engine}");
            Console.WriteLine($"Колір: {Color}");
            Console.WriteLine($"Комплектація: {TrimLevel}");
            Console.WriteLine("================================\n");
        }
    }

    // Інтерфейс будівельника (Builder Interface)
    public interface ICarBuilder
    {
        ICarBuilder SetBodyType(string bodyType);
        ICarBuilder SetEngine(string engine);
        ICarBuilder SetColor(string color);
        ICarBuilder SetTrimLevel(string trimLevel);
        Car Build();
    }

    // Конкретний будівельник (Concrete Builder)
    public class CarBuilder : ICarBuilder
    {
        private Car _car;

        public CarBuilder()
        {
            _car = new Car();
        }

        public ICarBuilder SetBodyType(string bodyType)
        {
            _car.BodyType = bodyType;
            return this;
        }

        public ICarBuilder SetEngine(string engine)
        {
            _car.Engine = engine;
            return this;
        }

        public ICarBuilder SetColor(string color)
        {
            _car.Color = color;
            return this;
        }

        public ICarBuilder SetTrimLevel(string trimLevel)
        {
            _car.TrimLevel = trimLevel;
            return this;
        }

        public Car Build()
        {
            Car result = _car;
            _car = new Car(); // Скидаємо для наступного автомобіля
            return result;
        }
    }

    // Директор (Director) - опціонально, для створення популярних конфігурацій
    public class CarDirector
    {
        private ICarBuilder _builder;

        public CarDirector(ICarBuilder builder)
        {
            _builder = builder;
        }

        public Car BuildSportsCar()
        {
            return _builder
                .SetBodyType("Купе")
                .SetEngine("V8 4.0L Twin-Turbo")
                .SetColor("Червоний")
                .SetTrimLevel("Sport Premium")
                .Build();
        }

        public Car BuildFamilyCar()
        {
            return _builder
                .SetBodyType("Універсал")
                .SetEngine("2.0L Hybrid")
                .SetColor("Сірий")
                .SetTrimLevel("Comfort")
                .Build();
        }

        public Car BuildLuxuryCar()
        {
            return _builder
                .SetBodyType("Седан")
                .SetEngine("V6 3.0L")
                .SetColor("Чорний")
                .SetTrimLevel("Luxury Plus")
                .Build();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрація патерну Builder (Будівельник) ===\n");

            // Спосіб 1: Використання будівельника напряму
            Console.WriteLine("1. Створення автомобіля за допомогою Builder:\n");
            
            ICarBuilder builder = new CarBuilder();
            Car customCar = builder
                .SetBodyType("Хетчбек")
                .SetEngine("1.5L Turbo")
                .SetColor("Синій")
                .SetTrimLevel("Standart Plus")
                .Build();
            
            customCar.DisplayInfo();

            // Спосіб 2: Використання Director для створення популярних конфігурацій
            Console.WriteLine("2. Створення автомобілів за допомогою Director:\n");
            
            CarDirector director = new CarDirector(new CarBuilder());

            Console.WriteLine("Спортивний автомобіль:");
            Car sportsCar = director.BuildSportsCar();
            sportsCar.DisplayInfo();

            Console.WriteLine("Сімейний автомобіль:");
            Car familyCar = director.BuildFamilyCar();
            familyCar.DisplayInfo();

            Console.WriteLine("Люксовий автомобіль:");
            Car luxuryCar = director.BuildLuxuryCar();
            luxuryCar.DisplayInfo();

            // Спосіб 3: Покрокове створення
            Console.WriteLine("3. Покрокове створення автомобіля:\n");
            
            ICarBuilder stepBuilder = new CarBuilder();
            stepBuilder.SetBodyType("Кросовер");
            stepBuilder.SetEngine("2.5L AWD");
            stepBuilder.SetColor("Білий");
            stepBuilder.SetTrimLevel("Adventure");
            Car stepCar = stepBuilder.Build();
            
            stepCar.DisplayInfo();

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
