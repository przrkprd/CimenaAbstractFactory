using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimenaAbstractFactory
{

    // Интерфейс звуковой дорожки.
    // Все звуковые дорожки должны иметь название языка.
    interface IAudio
    {
        string GetLanguage();
    }

    // Интерфейс субтитров.
    // Все файлы субтитров также имеют язык.
    interface ISubtitles
    {
        string GetLanguage();
    }

    // Класс русской звуковой дорожки.
    class RussianAudio : IAudio
    {
        public string GetLanguage()
        {
            return "Русский";
        }
    }

    // Класс английской звуковой дорожки.
    class EnglishAudio : IAudio
    {
        public string GetLanguage()
        {
            return "Английский";
        }
    }

    // Класс русских субтитров.
    class RussianSubtitles : ISubtitles
    {
        public string GetLanguage()
        {
            return "Русский";
        }
    }

    // Класс английских субтитров.
    class EnglishSubtitles : ISubtitles
    {
        public string GetLanguage()
        {
            return "Английский";
        }
    }

    // Абстрактная фабрика.
    // Она определяет, какие звуковую дорожку и субтитры должна создавать конкретная фабрика.
    interface IMovieFactory
    {
        IAudio CreateAudio();
        ISubtitles CreateSubtitles();
    }

    // Фабрика для русского языка.
    // Создаёт русскую дорожку и русские субтитры.
    class RussianFactory : IMovieFactory
    {
        public IAudio CreateAudio()
        {
            return new RussianAudio();
        }

        public ISubtitles CreateSubtitles()
        {
            return new RussianSubtitles();
        }
    }

    // Фабрика для английского языка.
    // Создаёт английскую дорожку и английские субтитры.
    class EnglishFactory : IMovieFactory
    {
        public IAudio CreateAudio()
        {
            return new EnglishAudio();
        }

        public ISubtitles CreateSubtitles()
        {
            return new EnglishSubtitles();
        }
    }

    // Класс фильма.
    // Хранит название фильма и выбранные компоненты.
    class Movie
    {
        public string Title { get; set; }
        public IAudio Audio { get; set; }
        public ISubtitles Subtitles { get; set; }

        public Movie(string title, IAudio audio, ISubtitles subtitles)
        {
            Title = title;
            Audio = audio;
            Subtitles = subtitles;
        }

        // Вывод информации о заказанном фильме.
        public void ShowInfo()
        {
            Console.WriteLine("\nЗаказ оформлен!");
            Console.WriteLine("Фильм: " + Title);
            Console.WriteLine("Язык звука: " + Audio.GetLanguage());
            Console.WriteLine("Язык субтитров: " + Subtitles.GetLanguage());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Список доступных фильмов.
            string[] films =
            {
            "Фильм 1",
            "Фильм 2",
            "Фильм 3"
        };
                        
            // Предлагаем пользователю выбрать фильм.
            for (int i = 0; i < films.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + films[i]);
            }

            Console.Write("Выберите фильм: ");
            int filmNumber = Convert.ToInt32(Console.ReadLine());

            // Проверяем правильность выбора фильма.
            if (filmNumber < 1 || filmNumber > films.Length)
            {
                Console.WriteLine("Такого фильма нет.");
                return;
            }

            Console.WriteLine("\nВыберите язык:");
            Console.WriteLine("1. Русский");
            Console.WriteLine("2. Английский");

            Console.Write("Ваш выбор: ");
            int languageNumber = Convert.ToInt32(Console.ReadLine());

            // Выбираем фабрику в зависимости от языка.
            IMovieFactory factory;

            if (languageNumber == 1)
            {
                factory = new RussianFactory();
            }
            else if (languageNumber == 2)
            {
                factory = new EnglishFactory();
            }
            else
            {
                Console.WriteLine("Неверный выбор языка.");
                return;
            }

            // Создаём звуковую дорожку и субтитры
            // с помощью выбранной фабрики.
            IAudio audio = factory.CreateAudio();
            ISubtitles subtitles = factory.CreateSubtitles();

            // Создаём фильм с выбранными компонентами.
            Movie movie = new Movie(
                films[filmNumber - 1],
                audio,
                subtitles
            );

            // Показываем информацию о заказе.
            movie.ShowInfo();

            Console.ReadKey();
        }
    }
}
