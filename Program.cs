using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos

    /// <summary>
    /// Тип кузова автомобіля
    /// </summary>
    public enum BodyType
    {
        Sedan,      // Седан
        Coupe,      // Купе
        Hatchback,  // Хетчбек
        Wagon,      // Універсал
        SUV         // Позашляховик/Кросовер
    }

    /// <summary>
    /// Колір автомобіля
    /// </summary>
    public enum CarColor
    {
        Red,    // Червоний
        Blue,   // Синій
        Black,  // Чорний
        White,  // Білий
        Gray,   // Сірий
        Silver  // Сріблястий
    }

    /// <summary>
    /// Рівень комплектації
    /// </summary>
    public enum TrimLevel
    {
        Base,           // Базова
        Standard,       // Стандартна
        StandardPlus,   // Стандарт Плюс
        Comfort,        // Комфорт
        SportPremium,   // Спорт Преміум
        LuxuryPlus,     // Люкс Плюс
        Adventure       // Пригодницька
    }

    /// <summary>
    /// Клас автомобіля (Product) - незмінний об'єкт після створення
    /// </summary>
    public record Car
    {
        public BodyType BodyType { get; init; }
        public required string Engine { get; init; }
        public CarColor Color { get; init; }
        public TrimLevel TrimLevel { get; init; }

        /// <summary>
        /// Перевизначений метод ToString для виведення інформації про автомобіль
        /// </summary>
        public override string ToString()
        {
            return $"=== Інформація про автомобіль ===\n" +
                   $"Тип кузова: {GetBodyTypeDisplayName(BodyType)}\n" +
                   $"Двигун: {Engine}\n" +
                   $"Колір: {GetColorDisplayName(Color)}\n" +
                   $"Комплектація: {GetTrimLevelDisplayName(TrimLevel)}\n" +
                   $"================================\n";
        }

        private static string GetBodyTypeDisplayName(BodyType type) => type switch
        {
            BodyType.Sedan => "Седан",
            BodyType.Coupe => "Купе",
            BodyType.Hatchback => "Хетчбек",
            BodyType.Wagon => "Універсал",
            BodyType.SUV => "Позашляховик",
            _ => type.ToString()
        };

        private static string GetColorDisplayName(CarColor color) => color switch
        {
            CarColor.Red => "Червоний",
            CarColor.Blue => "Синій",
            CarColor.Black => "Чорний",
            CarColor.White => "Білий",
            CarColor.Gray => "Сірий",
            CarColor.Silver => "Сріблястий",
            _ => color.ToString()
        };

        private static string GetTrimLevelDisplayName(TrimLevel level) => level switch
        {
            TrimLevel.Base => "Базова",
            TrimLevel.Standard => "Стандарт",
            TrimLevel.StandardPlus => "Стандарт Плюс",
            TrimLevel.Comfort => "Комфорт",
            TrimLevel.SportPremium => "Спорт Преміум",
            TrimLevel.LuxuryPlus => "Люкс Плюс",
            TrimLevel.Adventure => "Пригодницька",
            _ => level.ToString()
        };
    }

    /// <summary>
    /// Інтерфейс будівельника (Builder Interface)
    /// </summary>
    public interface ICarBuilder
    {
        /// <summary>
        /// Встановлює тип кузова автомобіля
        /// </summary>
        ICarBuilder SetBodyType(BodyType bodyType);

        /// <summary>
        /// Встановлює тип двигуна
        /// </summary>
        ICarBuilder SetEngine(string engine);

        /// <summary>
        /// Встановлює колір автомобіля
        /// </summary>
        ICarBuilder SetColor(CarColor color);

        /// <summary>
        /// Встановлює рівень комплектації
        /// </summary>
        ICarBuilder SetTrimLevel(TrimLevel trimLevel);

        /// <summary>
        /// Створює та повертає готовий автомобіль
        /// </summary>
        Car Build();

        /// <summary>
        /// Скидає стан будівельника для створення нового автомобіля
        /// </summary>
        ICarBuilder Reset();
    }

    /// <summary>
    /// Конкретний будівельник (Concrete Builder)
    /// </summary>
    public class CarBuilder : ICarBuilder
    {
        private BodyType? _bodyType;
        private string? _engine;
        private CarColor? _color;
        private TrimLevel? _trimLevel;

        public ICarBuilder SetBodyType(BodyType bodyType)
        {
            _bodyType = bodyType;
            return this;
        }

        public ICarBuilder SetEngine(string engine)
        {
            if (string.IsNullOrWhiteSpace(engine))
                throw new ArgumentException("Двигун не може бути null або порожнім", nameof(engine));
            
            _engine = engine;
            return this;
        }

        public ICarBuilder SetColor(CarColor color)
        {
            _color = color;
            return this;
        }

        public ICarBuilder SetTrimLevel(TrimLevel trimLevel)
        {
            _trimLevel = trimLevel;
            return this;
        }

        /// <summary>
        /// Створює автомобіль з валідацією обов'язкових полів
        /// </summary>
        public Car Build()
        {
            // Додаткова перевірка (на випадок прямого доступу до полів)
            if (string.IsNullOrWhiteSpace(_engine))
            {
                throw new InvalidOperationException("Двигун є обов'язковим параметром. Використайте SetEngine().");
            }

            var car = new Car
            {
                BodyType = _bodyType ?? BodyType.Sedan,        // За замовчуванням - Седан
                Engine = _engine,
                Color = _color ?? CarColor.Black,              // За замовчуванням - Чорний
                TrimLevel = _trimLevel ?? TrimLevel.Standard   // За замовчуванням - Стандарт
            };

            Reset(); // Автоматичне скидання після створення
            return car;
        }

        /// <summary>
        /// Скидає стан будівельника
        /// </summary>
        public ICarBuilder Reset()
        {
            _bodyType = null;
            _engine = null;
            _color = null;
            _trimLevel = null;
            return this;
        }
    }

    /// <summary>
    /// Директор (Director) - для створення популярних конфігурацій
    /// </summary>
    public class CarDirector
    {
        private readonly ICarBuilder _builder;

        // Константи для популярних конфігурацій
        private const string SportEngineSpec = "V8 4.0L Twin-Turbo";
        private const string FamilyEngineSpec = "2.0L Hybrid";
        private const string LuxuryEngineSpec = "V6 3.0L";

        /// <summary>
        /// Конструктор з перевіркою на null
        /// </summary>
        public CarDirector(ICarBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// Створює спортивний автомобіль
        /// </summary>
        public Car BuildSportsCar()
        {
            return _builder
                .SetBodyType(BodyType.Coupe)
                .SetEngine(SportEngineSpec)
                .SetColor(CarColor.Red)
                .SetTrimLevel(TrimLevel.SportPremium)
                .Build();
        }

        /// <summary>
        /// Створює сімейний автомобіль
        /// </summary>
        public Car BuildFamilyCar()
        {
            return _builder
                .SetBodyType(BodyType.Wagon)
                .SetEngine(FamilyEngineSpec)
                .SetColor(CarColor.Gray)
                .SetTrimLevel(TrimLevel.Comfort)
                .Build();
        }

        /// <summary>
        /// Створює люксовий автомобіль
        /// </summary>
        public Car BuildLuxuryCar()
        {
            return _builder
                .SetBodyType(BodyType.Sedan)
                .SetEngine(LuxuryEngineSpec)
                .SetColor(CarColor.Black)
                .SetTrimLevel(TrimLevel.LuxuryPlus)
                .Build();
        }
    }

    /// <summary>
    /// Головний клас програми
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрація патерну Builder (Будівельник) ===\n");

            // Спосіб 1: Використання будівельника напряму
            Console.WriteLine("1. Створення автомобіля за допомогою Builder:\n");

            var builder = new CarBuilder();
            var customCar = builder
                .SetBodyType(BodyType.Hatchback)
                .SetEngine("1.5L Turbo")
                .SetColor(CarColor.Blue)
                .SetTrimLevel(TrimLevel.StandardPlus)
                .Build();

            Console.WriteLine(customCar);

            // Спосіб 2: Використання Director для створення популярних конфігурацій
            Console.WriteLine("2. Створення автомобілів за допомогою Director:\n");

            var director = new CarDirector(new CarBuilder());

            Console.WriteLine("Спортивний автомобіль:");
            var sportsCar = director.BuildSportsCar();
            Console.WriteLine(sportsCar);

            Console.WriteLine("Сімейний автомобіль:");
            var familyCar = director.BuildFamilyCar();
            Console.WriteLine(familyCar);

            Console.WriteLine("Люксовий автомобіль:");
            var luxuryCar = director.BuildLuxuryCar();
            Console.WriteLine(luxuryCar);

            // Спосіб 3: Покрокове створення
            Console.WriteLine("3. Покрокове створення автомобіля:\n");

            var stepBuilder = new CarBuilder();
            stepBuilder.SetBodyType(BodyType.SUV);
            stepBuilder.SetEngine("2.5L AWD");
            stepBuilder.SetColor(CarColor.White);
            stepBuilder.SetTrimLevel(TrimLevel.Adventure);
            var stepCar = stepBuilder.Build();

            Console.WriteLine(stepCar);

            // Спосіб 4: Демонстрація валідації
            Console.WriteLine("4. Демонстрація валідації:\n");

            // 4a: Спроба створити без двигуна
            try
            {
                var invalidBuilder = new CarBuilder();
                invalidBuilder
                    .SetBodyType(BodyType.Sedan)
                    .SetColor(CarColor.Black)
                    .Build();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"❌ Помилка (без двигуна): {ex.Message}");
            }

            // 4b: Спроба встановити порожній двигун
            try
            {
                var emptyEngineBuilder = new CarBuilder();
                emptyEngineBuilder.SetEngine("   "); // Тільки пробіли
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Помилка (порожній двигун): {ex.Message}");
            }

            // Спосіб 5: Демонстрація Reset() та значень за замовчуванням
            Console.WriteLine("\n5. Використання Reset() і значень за замовчуванням:\n");

            var reuseBuilder = new CarBuilder();
            reuseBuilder
                .SetEngine("2.0L Turbo")
                .Reset() // Скидаємо все
                .SetEngine("3.5L V6"); // Встановлюємо тільки двигун
            
            var defaultCar = reuseBuilder.Build();
            Console.WriteLine("Автомобіль зі значеннями за замовчуванням (тільки двигун вказано):");
            Console.WriteLine(defaultCar);

            // Спосіб 6: Флюїдний API з Reset
            Console.WriteLine("6. Флюїдний API з Reset:\n");

            var fluentCar = new CarBuilder()
                .Reset()
                .SetBodyType(BodyType.Coupe)
                .SetEngine("Electric 400kW")
                .SetColor(CarColor.Silver)
                .SetTrimLevel(TrimLevel.LuxuryPlus)
                .Build();

            Console.WriteLine(fluentCar);

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
