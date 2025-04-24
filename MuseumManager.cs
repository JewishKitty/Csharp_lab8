using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Предоставляет методы для работы с коллекцией музейных экспонатов.
/// </summary>
public static class MuseumManager
{
    // Создание нового музейного экспоната
    public static MuseumExhibit CreateExhibit(List<MuseumExhibit> listOfExhibits)
    {
        int id;
        do
        {
            id = InputValidator.ReadInt("Введите ID (уникальный): ");
            if (listOfExhibits.Any(e => e.Id == id))
                Console.WriteLine("Экспонат с таким ID уже существует!");
            else
                break;
        } while (true);

        string name = InputValidator.ReadNonEmptyString("Введите название: ");
        int year = InputValidator.ReadInt("Введите год создания: ");
        double value = InputValidator.ReadDouble("Введите оценочную стоимость: ");
        bool onDisplay = InputValidator.ReadBool("Находится ли экспонат в экспозиции");

        return new MuseumExhibit(id, name, year, value, onDisplay);
    }

    // Десериализация БД в список музейных экспонатов
    public static List<MuseumExhibit> Load(string filename)
    {
        if (!File.Exists(filename)) return new List<MuseumExhibit>();
        using var fs = new FileStream(filename, FileMode.Open);
        var bf = new BinaryFormatter();
        return (List<MuseumExhibit>) bf.Deserialize(fs);
    }

    // Сохранение текущего списка экспонатов в файл
    public static void Save(List<MuseumExhibit> listOfExhibits, string filename)
    {
        using var fs = new FileStream(filename, FileMode.Create);
        var bf = new BinaryFormatter();
        bf.Serialize(fs, listOfExhibits);
    }

    // Просмотр содержимого БД
    public static void View(List<MuseumExhibit> listOfExhibits)
    {
        listOfExhibits.ForEach(e => Console.WriteLine(e));
    }

    // Добавление музейного экспоната в список
    public static void Add(List<MuseumExhibit> listOfExhibits, MuseumExhibit exhibit)
    {
        listOfExhibits.Add(exhibit);
    }

    // Удаление элемента по ключу
    public static void Delete(List<MuseumExhibit> listOfExhibits, int id)
    {
        var exhibitToRemove = listOfExhibits.FirstOrDefault(e => e.Id == id);
        if (exhibitToRemove != null) listOfExhibits.Remove(exhibitToRemove);
        else Console.WriteLine("ID не встречен. Ни один элемент не удалён.");
    }

    // Получение экспонатов с оценочной стоимостью больше заданной
    public static IEnumerable<MuseumExhibit> GetExpensiveExhibits(List<MuseumExhibit> listOfExhibits, double minValue) =>
        listOfExhibits.Where(e => e.EstimatedValue >= minValue);

    
    // Получение экспонатов с годом создания раньше заданного
    public static IEnumerable<MuseumExhibit> GetOldExhibits(List<MuseumExhibit> listOfExhibits, int beforeYear) =>
        listOfExhibits.Where(e => e.Year < beforeYear);

    // Подсчёт количества выставляемых экспонатов
    public static int CountOnDisplay(List<MuseumExhibit> listOfExhibits) =>
        listOfExhibits.Count(e => e.IsOnDisplay);

    // Подсчёт средней цены экспонатов
    public static double AverageValue(List<MuseumExhibit> listOfExhibits) =>
        listOfExhibits.Average(e => e.EstimatedValue);

    // Меню запросов
    public static void RunQueries(List<MuseumExhibit> listOfExhibits)
    {
        Console.WriteLine("1. Экспонаты дороже заданной суммы");
        Console.WriteLine("2. Экспонаты до заданного года");
        Console.WriteLine("3. Кол-во экспонатов на экспозиции");
        Console.WriteLine("4. Средняя стоимость экспонатов");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Минимальная стоимость: ");
                double min = double.Parse(Console.ReadLine());
                var exspensiveList = MuseumManager.GetExpensiveExhibits(listOfExhibits, min);
                if (exspensiveList.Count() == 0)
                {
                    Console.WriteLine("Нет экспонатов дороже указанной стоимости.");
                }
                foreach (var e in exspensiveList) Console.WriteLine(e);
                break;
            case "2":
                Console.Write("До какого года: ");
                int year = int.Parse(Console.ReadLine());
                var oldList = MuseumManager.GetOldExhibits(listOfExhibits, year);
                if (oldList.Count() == 0)
                {
                    Console.WriteLine("Нет экспонатов старее указанной даты.");
                }
                foreach (var e in oldList) Console.WriteLine(e);
                break;
            case "3":
                Console.WriteLine("Доступных экспонатов на экспозиции: " + MuseumManager.CountOnDisplay(listOfExhibits));
                break;
            case "4":
                if (listOfExhibits.Count() == 0)
                {
                    Console.WriteLine("Нет экспонатов для подсчёта средней стоимости.");
                }
                else Console.WriteLine("Средняя стоимость: $" + MuseumManager.AverageValue(listOfExhibits).ToString("F2"));
                break;
            default:
                Console.WriteLine("Недопустимая операция!");
                break;
        }
    }
}
