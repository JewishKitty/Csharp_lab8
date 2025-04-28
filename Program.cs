using System;

/// <summary>
/// Главный класс программы для управления базой данных музейных экспонатов
/// </summary>
class Program
{
    /// <summary>
    /// Имя файла, в котором сохраняется база данных экспонатов
    /// </summary>
    private const string FileName = "exhibits.bin";

    /// <summary>
    /// Точка входа в программу
    /// Загружает базу данных и предоставляет меню для взаимодействия с коллекцией экспонатов
    /// </summary>
    static void Main()
    {
        var listOfExhibits = MuseumManager.Load(FileName);

        while (true)
        {
            // Если список пуст, выводим сообщение
            if (!listOfExhibits.Any())
                Console.WriteLine("База данных пуста. Добавьте хотя бы один экспонат.");

            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Просмотр базы");
            Console.WriteLine("2. Добавить экспонат");
            Console.WriteLine("3. Удалить экспонат");
            Console.WriteLine("4. Запросы");
            Console.WriteLine("0. Выход");

            switch (InputValidator.ReadNonEmptyString("Введите номер операции: "))
            {
                case "1":
                    MuseumManager.View(listOfExhibits);
                    break;

                case "2":
                    try
                    {
                        var exhibit = MuseumManager.CreateExhibit(listOfExhibits);
                        MuseumManager.Add(listOfExhibits, exhibit);
                        MuseumManager.Save(listOfExhibits, FileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка сохранения в файл: " + ex.Message);
                    }
                    break;

                case "3":
                    try
                    {
                            int id = InputValidator.ReadInt("Введите Id: ");
                            MuseumManager.Delete(listOfExhibits, id);
                            MuseumManager.Save(listOfExhibits, FileName);
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка сохранения в файл: " + ex.Message);
                    }
                    break;

                case "4":
                    MuseumManager.RunQueries(listOfExhibits);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Недопустимая операция!");
                    break;
            }
        }
    }
}
