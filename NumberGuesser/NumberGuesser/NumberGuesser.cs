using System;

namespace NumberGuesser
{
    public class NumberGuesser
    {
        private readonly string _user;
        private static readonly string[] Phrases =
        {   "{0}, не разочаровывай меня.",
            "Ну и зашквар... соберись, {0}!",
            "{0}, ты точно понял задание?" ,
            "Потрачено, {0}!",
            "Забей, {0}, это бесполезно.",
            "{0}, ты болван!",
            "{0], как ты в НГУ поступил?"
        };

        public NumberGuesser(string user)
        {
            _user = user;
        }

        public void Run()
        {
            var history = new int[1000];
            var random = new Random();
            var guessNumber = random.Next(101);
            var attempts = 0;

            Console.WriteLine("{0}, угадай число от 0 до 100!", _user);
            var start = DateTime.Now;

            while (true)
            {

                var input = Console.ReadLine();
                if (input == "q")
                {
                    Console.WriteLine("Пока, {0}.", _user);
                    break;
                }
                try
                {
                    var userNumber = int.Parse(input);
                    attempts++;
                    history[(attempts - 1) % history.Length] = userNumber;
                    if (userNumber == guessNumber)
                    {

                        Console.WriteLine("Oкей, {0}, ты победил. Это было число {1}", _user, guessNumber);
                        Console.WriteLine("Время - {0} мин\nКол-во попыток - {1}", (DateTime.Now - start).TotalMinutes, attempts);
                        for (int i = 0; i < attempts; i++)
                        {
                            Console.WriteLine("{0} {1}", history[i], history[i] < guessNumber ? "меньше" : history[i] > guessNumber ? "больше" : "успех");
                        }
                        break;
                    }
                    if (attempts % 4 == 0)
                    {
                        Console.WriteLine(Phrases[random.Next(Phrases.Length)], _user);
                    }
                    Console.WriteLine("Число {0} {1} загаданного", userNumber, userNumber < guessNumber ? "меньше" : "больше");

                }
                catch
                {
                    Console.WriteLine("Введи число или нажми 'q' для выхода!");
                }
            }
        }
    }
}