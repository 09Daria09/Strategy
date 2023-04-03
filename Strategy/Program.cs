using System;

namespace Strategy
{
    class Travel
    {
        private IStrategy _strategy;
        public DateTime YourTime { get; set; }
        public double YourMoney { get; set; }
        public Travel(IStrategy strategy)
        {
            _strategy = strategy;
        }
        public void SetStrategy(IStrategy strategy)
        {
           _strategy = strategy;
        }
        public void BusinessLogic()
        {
            Console.WriteLine("Сколько времени вы хотите потратить на дорогу? (чч:мм:сс)");
            string input = Console.ReadLine();
            bool isValidTime = DateTime.TryParse(input, out DateTime time); 

            while (!isValidTime) 
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз:");
                input = Console.ReadLine();
                isValidTime = DateTime.TryParse(input, out time);
            }

            YourTime = time;

            Console.WriteLine("Сколько денег вы хотите потратить на дорогу?");
            YourMoney = Convert.ToDouble(Console.ReadLine());


            if (YourMoney < _strategy.Money && YourTime < _strategy.Time)
            {
                Console.WriteLine("У вас недостаточно денег и времени для этой поездки");
                return;
            }
            if(YourMoney < _strategy.Money)
            {
                Console.WriteLine("У вас недостаточно денег");
                return;
            }
            if(YourTime < _strategy.Time)
            {
                Console.WriteLine("Вы не успеете за это время доехать до аэропорта");
                return;
            }
            Console.WriteLine($"Вы доберетесь до аэропорта на {_strategy.ChoiceTransport()}");
        

        }
    }
    public interface IStrategy
    {
        DateTime Time { get; set; }
        double Money { get; set; }
        string ChoiceTransport();
    }
    class Bicycle : IStrategy
    {
        public DateTime Time {get; set;}
        public double Money { get; set;}
        public Bicycle()
        {
            DateTime now = DateTime.Now;
            DateTime timeOnly = new DateTime(now.Year, now.Month, now.Day, 2, 30, 0); 
            Time = timeOnly;
            Money = 0;
        }
        public string ChoiceTransport()
        {
            return "Bicycle";
        }
    }
    class Bus : IStrategy
    {
        public DateTime Time { get; set; }
        public double Money { get; set; }
        public Bus ()
        {
            DateTime now = DateTime.Now;
            DateTime timeOnly = new DateTime(now.Year, now.Month, now.Day, 1, 30, 0);
            Time = timeOnly;
            Money = 50;
        }
        public string ChoiceTransport()
        {
            return "Bus";
        }
    }
    class Taxi : IStrategy
    {
        public DateTime Time { get; set; }
        public double Money { get; set; }
        public Taxi()
        {
            DateTime now = DateTime.Now;
            DateTime timeOnly = new DateTime(now.Year, now.Month, now.Day, 0, 40, 0);
            Time = timeOnly;
            Money = 260;
        }
        public string ChoiceTransport()
        {
            return "Taxi";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IStrategy strategy = new Taxi();
            Travel travel = new Travel(strategy);
            travel.BusinessLogic();
        }
    }
}
