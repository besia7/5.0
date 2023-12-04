using System;
using System.Collections.Generic;
using System.Linq;

class Товар
{
    public string Назва { get; set; }
    public decimal Ціна { get; set; }
    public string Опис { get; set; }
    public string Категорія { get; set; }
    public int Рейтинг { get; set; }
}

class Користувач
{
    public string Логін { get; set; }
    public string Пароль { get; set; }
    public List<string> ІсторіяПокупок { get; set; }

    public Користувач(string логін, string пароль)
    {
        Логін = логін;
        Пароль = пароль;
        ІсторіяПокупок = new List<string>();
    }
}

class Замовлення
{
    public List<Товар> Товари { get; set; }
    public int Кількість { get; set; }
    public decimal ЗагальнаВартість { get; set; }
    public string Статус { get; set; }
}

interface ISearchable
{
    List<Товар> ЗнайтиТовари(string критерій, object значення);
}

class Магазин : ISearchable
{
    public List<Товар> Товари { get; private set; }
    public List<Користувач> Користувачі { get; private set; }
    public List<Замовлення> Замовлення { get; private set; }

    public Магазин()
    {
        Товари = new List<Товар>
        {
            new Товар { Назва = "Ноутбук", Ціна = 1200, Опис = "Потужний ноутбук", Категорія = "Електроніка", Рейтинг = 5 },
            new Товар { Назва = "Смартфон", Ціна = 500, Опис = "Сучасний смартфон", Категорія = "Електроніка", Рейтинг = 4 },
            new Товар { Назва = "Книга", Ціна = 20, Опис = "Класична книга", Категорія = "Книги", Рейтинг = 4 }
        };

        Користувачі = new List<Користувач>
        {
            new Користувач("user1", "password1"),
            new Користувач("user2", "password2")
        };

        Замовлення = new List<Замовлення>();
    }

    public void ДодатиТовар(Товар товар)
    {
        Товари.Add(товар);
    }

    public void ДодатиКористувача(Користувач користувач)
    {
        Користувачі.Add(користувач);
    }

    public void ДодатиЗамовлення(Користувач користувач, List<Товар> товари, int кількість)
    {
        Замовлення замовлення = new Замовлення
        {
            Товари = товари,
            Кількість = кількість,
            ЗагальнаВартість = товари.Sum(товар => товар.Ціна) * кількість,
            Статус = "В обробці"
        };

        користувач.ІсторіяПокупок.Add($"Замовлення #{Замовлення.Count + 1}");
        Замовлення.Add(замовлення);
    }

    public List<Товар> ЗнайтиТовари(string критерій, object значення)
    {
        switch (критерій)
        {
            case "ціна":
                return Товари.Where(товар => товар.Ціна <= (decimal)значення).ToList();
            case "категорія":
                return Товари.Where(товар => товар.Категорія == (string)значення).ToList();
            case "рейтинг":
                return Товари.Where(товар => товар.Рейтинг >= (int)значення).ToList();
            default:
                return new List<Товар>();
        }
    }

    public void ПоказатиВесьАсортимент()
    {
        Console.WriteLine("Асортимент товарів:");
        foreach (var товар in Товари)
        {
            Console.WriteLine($"Назва: {товар.Назва}, Ціна: {товар.Ціна}, Категорія: {товар.Категорія}, Рейтинг: {товар.Рейтинг}");
        }
    }
}

class Програма
{
    static void Main()
    {
        Магазин магазин = new Магазин();

        магазин.ПоказатиВесьАсортимент();

        // Додати користувача і замовлення для прикладу
        Користувач користувач = магазин.Користувачі.FirstOrDefault();
        if (користувач != null)
        {
            магазин.ДодатиЗамовлення(користувач, магазин.Товари, 2);
            Console.WriteLine($"\nІсторія покупок користувача {користувач.Логін}:");
            foreach (var історія in користувач.ІсторіяПокупок)
            {
                Console.WriteLine(історія);
            }
        }

        Console.ReadLine(); // Затримка, щоб консольне вікно не закрилося відразу
    }
}
