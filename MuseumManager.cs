using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Предоставляет методы для работы с коллекцией музейных экспонатов
/// </summary>
public static class MuseumManager
{
    /// <summary>
    /// Создаёт новый музейный экспонат на основе пользовательского ввода
    /// </summary>
    /// <param name="listOfExhibits">Список существующих музейных экспонатов для проверки уникальности Id</param>
    /// <returns>Экземпляр класса MuseumExhibit</returns>
    public static MuseumExhibit CreateExhibit(List<MuseumExhibit> listOfExhibits)
    {
        int id;
        // Цикл проверки ID на уникальность
        do
        {
            id = InputValidator.ReadInt("Введите ID (уникальный): ");
            var exhibit = from e in listOfExhibits
                          where e.Id == id
                          select e;

            if (exhibit.Any())
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

    /// <summary>
    /// Загружает список музейных экспонатов из бинарного файла
    /// </summary>
    /// <param name="filename">Путь к файлу</param>
    /// <returns>Список музейных экспонатов</returns>
    public static List<MuseumExhibit> Load(string filename)
    {
        if (!File.Exists(filename)) return new List<MuseumExhibit>();
        using var fs = new FileStream(filename, FileMode.Open);
        var bf = new BinaryFormatter();
        return (List<MuseumExhibit>)bf.Deserialize(fs);
    }

    /// <summary>
    /// Сохраняет список музейных экспонатов в бинарный файл
    /// </summary>
    /// <param name="listOfExhibits">Список экспонатов для сохранения</param>
    /// <param name="filename">Путь к файлу</param>
    public static void Save(List<MuseumExhibit> listOfExhibits, string filename)
    {
        using var fs = new FileStream(filename, FileMode.Create);
        var bf = new BinaryFormatter();
        bf.Serialize(fs, listOfExhibits);
    }

    /// <summary>
    /// Выводит содержимое списка музейных экспонатов на экран
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    public static void View(List<MuseumExhibit> listOfExhibits)
    {
        Console.WriteLine("Список экспонатов:");
        foreach (var exhibit in listOfExhibits)
            Console.WriteLine(exhibit);
    }

    /// <summary>
    /// Добавляет новый музейный экспонат в список
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    /// <param name="exhibit">Экспонат для добавления</param>
    public static void Add(List<MuseumExhibit> listOfExhibits, MuseumExhibit exhibit)
    {
        listOfExhibits.Add(exhibit);
    }

    /// <summary>
    /// Удаляет музейный экспонат по его идентификатору
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    /// <param name="id">Идентификатор экспоната для удаления</param>
    public static void Delete(List<MuseumExhibit> listOfExhibits, int id)
    {
        var exhibit = from e in listOfExhibits
                      where e.Id == id
                      select e;

        if (exhibit.Any())
        {
            listOfExhibits.RemoveAll(e => e.Id == id);
            Console.WriteLine("Экспонат удалён.");
        }
        else
        {
            Console.WriteLine("Экспонат с таким ID не найден.");
        }
    }

    /// <summary>
    /// Возвращает коллекцию музейных экспонатов с оценочной стоимостью выше заданной
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    /// <param name="minValue">Минимальная оценочная стоимость</param>
    /// <returns>Коллекция экспонатов дороже заданной стоимости</returns>
    public static IEnumerable<MuseumExhibit> GetExpensiveExhibits(List<MuseumExhibit> listOfExhibits, double minValue)
    {
        var listOfExpensiveExhibits = from e in listOfExhibits
                                      where e.EstimatedValue > minValue
                                      select e;
        return listOfExpensiveExhibits;
    }

    /// <summary>
    /// Возвращает коллекцию музейных экспонатов, созданных до указанного года
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    /// <param name="beforeYear">Год, до которого должны быть созданы экспонаты</param>
    /// <returns>Коллекция экспонатов, созданных до указанного года</returns>
    public static IEnumerable<MuseumExhibit> GetOldExhibits(List<MuseumExhibit> listOfExhibits, int beforeYear)
    {
        var listOfOldExhibits = from e in listOfExhibits
                                where e.Year < beforeYear
                                select e;
        return listOfOldExhibits;
    }

    /// <summary>
    /// Подсчитывает количество экспонатов, находящихся в экспозиции
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    /// <returns>Количество экспонатов на экспозиции</returns>
    public static int CountOnDisplay(List<MuseumExhibit> listOfExhibits)
    {
        var listOfExhibitsOnDisplay = from e in listOfExhibits
                                      where e.IsOnDisplay
                                      select e;
        return listOfExhibitsOnDisplay.Count();
    }

    /// <summary>
    /// Вычисляет среднюю оценочную стоимость музейных экспонатов
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    /// <returns>Средняя стоимость экспонатов</returns>
    public static double AverageValue(List<MuseumExhibit> listOfExhibits)
    {
        var totalValue = (from e in listOfExhibits
                          select e.EstimatedValue).Sum();
        var averageValue = totalValue / listOfExhibits.Count;
        return averageValue;
    }

    /// <summary>
    /// Выводит меню запросов и выполняет выбранный пользователем запрос
    /// </summary>
    /// <param name="listOfExhibits">Список музейных экспонатов</param>
    public static void RunQueries(List<MuseumExhibit> listOfExhibits)
    {
        Console.WriteLine("1. Экспонаты дороже заданной суммы");
        Console.WriteLine("2. Экспонаты до заданного года");
        Console.WriteLine("3. Кол-во экспонатов на экспозиции");
        Console.WriteLine("4. Средняя стоимость экспонатов");

        switch (InputValidator.ReadNonEmptyString("Введите номер операции: "))
        {
            case "1":
                double min = InputValidator.ReadDouble("Минимальная стоимость: ");
                var expensiveList = MuseumManager.GetExpensiveExhibits(listOfExhibits, min);
                if (!expensiveList.Any())
                {
                    Console.WriteLine("Нет экспонатов дороже указанной стоимости.");
                }
                foreach (var e in expensiveList) Console.WriteLine(e);
                break;
            case "2":
                int year = InputValidator.ReadInt("До какого года: ");
                var oldList = MuseumManager.GetOldExhibits(listOfExhibits, year);
                if (!oldList.Any())
                {
                    Console.WriteLine("Нет экспонатов старее указанной даты.");
                }
                foreach (var e in oldList) Console.WriteLine(e);
                break;
            case "3":
                Console.WriteLine("Доступных экспонатов на экспозиции: " + MuseumManager.CountOnDisplay(listOfExhibits));
                break;
            case "4":
                if (!listOfExhibits.Any())
                {
                    Console.WriteLine("Нет экспонатов для подсчёта средней стоимости.");
                }
                else
                {
                    Console.WriteLine("Средняя стоимость: $" + MuseumManager.AverageValue(listOfExhibits).ToString("F2"));
                }
                break;
            default:
                Console.WriteLine("Недопустимая операция!");
                break;
        }
    }
}
