using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DictionariesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionaries app = new Dictionaries();
            app.Start();
        }
    }
    // Класс, представляющий элемент словаря
    public class DictionaryEntry
    {
        public string Word { get; set; }
        public List<string> Translations { get; set; }

        public DictionaryEntry(string word, List<string> translations)
        {
            Word = word;
            Translations = translations;
        }
    }

    // Класс, представляющий словарь
    public class Dictionary
    {
        public string Name { get; set; }
        public string LanguageFrom { get; set; }
        public string LanguageTo { get; set; }
        public List<DictionaryEntry> Entries { get; set; }

        public Dictionary(string name, string languageFrom, string languageTo)
        {
            Name = name;
            LanguageFrom = languageFrom;
            LanguageTo = languageTo;
            Entries = new List<DictionaryEntry>();
        }

        // Добавление элемента в словарь
        public void Add(string word, List<string> translations)
        {
            Entries.Add(new DictionaryEntry(word, translations));
        }

        // Замена элемента в словаре
        public void Replace(string oldWord, string newWord, List<string> newTranslations)
        {
            DictionaryEntry entry = Entries.Find(e => e.Word == oldWord);
            if (entry != null)
            {
                entry.Word = newWord;
                entry.Translations = newTranslations;
            }
        }

        // Удаление элемента из словаря
        public void Remove(string word)
        {
            Entries.RemoveAll(e => e.Word == word);
        }

        // Получение перевода слова
        public List<string> Translation(string word)
        {
            DictionaryEntry entry = Entries.Find(e => e.Word == word);
            if (entry != null)
            {
                return entry.Translations;
            }
            return null;
        }

        // Экспорт словаря в файл
        public void Export(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"{LanguageFrom} -> {LanguageTo}");
                foreach (DictionaryEntry e in Entries)
                {
                    sw.WriteLine($"{e.Word} - {string.Join(", ", e.Translations)}");
                }
            }
        }
    }

    // Класс, представляющий приложение
    public class Dictionaries
    {
        private List<Dictionary> _dictionaries;

        public Dictionaries()
        {
            _dictionaries = new List<Dictionary>();
        }

        // Создание словаря
        public void Create(string name, string languageFrom, string languageTo)
        {
            _dictionaries.Add(new Dictionary(name, languageFrom, languageTo));
        }

        // Добавление элемента в словарь
        public void Add(string dictionaryName, string word, List<string> translations)
        {
            Dictionary dictionary = _dictionaries.Find(d => d.Name == dictionaryName);
            if (dictionary != null)
            {
                dictionary.Add(word, translations);
            }
            else
            {
                Console.WriteLine($"Словарь {dictionaryName} не найдено");
            }
        }

        // Замена элемента в словаре
        public void Replace(string dictionaryName, string oldWord, string newWord, List<string> newTranslations)
        {
            Dictionary dictionary = _dictionaries.Find(d => d.Name == dictionaryName);
            if (dictionary != null)
            {
                dictionary.Replace(oldWord, newWord, newTranslations);
            }
            else
            {
                Console.WriteLine($"Словарь {dictionaryName} не найдено");
            }
        }

        // Удаление элемента из словаря
        public void Remove(string dictionaryName, string word)
        {
            Dictionary dictionary = _dictionaries.Find(d => d.Name == dictionaryName);
            if (dictionary != null)
            {
                dictionary.Remove(word);
            }
            else
            {
                Console.WriteLine($"Словарь {dictionaryName} не найдено");
            }
        }

        // Получение перевода слова
        public void Translation(string dictionaryName, string word)
        {
            Dictionary dictionary = _dictionaries.Find(d => d.Name == dictionaryName);
            if (dictionary != null)
            {
                List<string> translations = dictionary.Translation(word);
                if (translations != null)
                {
                    Console.WriteLine($"Переводы {word}: {string.Join(", ", translations)}");
                }
                else
                {
                    Console.WriteLine($"Слово {word} не найдено в словаре {dictionaryName}");
                }
            }
            else
            {
                Console.WriteLine($"Словарь {dictionaryName} не найдено");
            }
        }

        // Экспорт словаря в файл
        public void Export(string dictionaryName, string fileName)
        {
            Dictionary dictionary = _dictionaries.Find(d => d.Name == dictionaryName);
            if (dictionary != null)
            {
                dictionary.Export(fileName);
            }
            else
            {
                Console.WriteLine($"Словарь {dictionaryName} не найдено");
            }
        }

        // Вывод списка словарей
        public void ListDictionaries()
        {
            foreach (Dictionary d in _dictionaries)
            {
                Console.WriteLine($"Название: {d.Name}, Язык: {d.LanguageFrom} -> {d.LanguageTo}, Записи: {d.Entries.Count}");
            }
        }

        // Вывод меню
        public void Menu()
        {
            Console.WriteLine("1. Создать словарь");
            Console.WriteLine("2. Добавить запись в словарь");
            Console.WriteLine("3. Заменить запись в словаре");
            Console.WriteLine("4. Удалить запись из словаря");
            Console.WriteLine("5. Получить перевод");
            Console.WriteLine("6. Экспорт словаря в файл");
            Console.WriteLine("7. Список словарей");
            Console.WriteLine("0. Выход");
        }

        // Запуск приложения
        public void Start()
        {
            while (true)
            {
                Menu();
                Console.Write("Введите команду: ");
                int command = int.Parse(Console.ReadLine());
                switch (command)
                {
                    case 1:
                        Console.Write("Введите название словаря: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите язык из: ");
                        string languageFrom = Console.ReadLine();
                        Console.Write("Введите язык в: ");
                        string languageTo = Console.ReadLine();
                        Create(name, languageFrom, languageTo);
                        Console.WriteLine($"Словарь {name} Создан");
                        break;
                    case 2:
                        Console.Write("Введите название словаря: ");
                        string dictionaryName = Console.ReadLine();
                        Console.Write("Введите слово: ");
                        string word = Console.ReadLine();
                        Console.Write("Введите переводы, разделенные командой:");
                        string[] translationArray = Console.ReadLine().Split();
                        List<string> translations = new List<string>(translationArray);
                        Add(dictionaryName, word, translations);
                        Console.WriteLine($"Слово {word} добавлено в словарь {dictionaryName}");
                        break;
                    case 3:
                        Console.Write("Введите название словаря: ");
                        dictionaryName = Console.ReadLine();
                        Console.Write("Введите старое слово: ");
                        string oldWord = Console.ReadLine();
                        Console.Write("Введите новое слово: ");
                        string newWord = Console.ReadLine();
                        Console.Write("Введите новые переводы: ");
                        translationArray = Console.ReadLine().Split();
                        translations = new List<string>(translationArray);
                        Replace(dictionaryName, oldWord, newWord, translations);
                        Console.WriteLine($"Слово {oldWord} заменён на {newWord} в словаре {dictionaryName}");
                        break;
                    case 4:
                        Console.Write("Введите название словаря: ");
                        dictionaryName = Console.ReadLine();
                        Console.Write("Введите слово: ");
                        word = Console.ReadLine();
                        Remove(dictionaryName, word);
                        Console.WriteLine($"Слово {word} удалено из словаря {dictionaryName}");
                        break;
                    case 5:
                        Console.Write("Введите название словаря: ");
                        dictionaryName = Console.ReadLine();
                        Console.Write("Введите слово: ");
                        word = Console.ReadLine();
                        Translation(dictionaryName, word);
                        break;
                    case 6:
                        Console.Write("Введите название словаря: ");
                        dictionaryName = Console.ReadLine();
                        Console.Write("Введите имя файла: ");
                        string fileName = Console.ReadLine();
                        Export(dictionaryName, fileName);
                        Console.WriteLine($"Словарь { dictionaryName} экспортировано в файл {fileName}");
                        break;
                    case 7:
                        ListDictionaries();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
