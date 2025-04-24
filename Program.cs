class Program
{
    // При создании первого экспоната будет созадаваться файл с данным названием и в последствии вся работа будет происходить с ним
    const string FileName = "exhibits.bin"; 

   
    static void Main()
    {
         
        var listOfExhibits = MuseumManager.Load(FileName);

        while (true)
        {
            // Если список пуст выводим сообщение
            if (!listOfExhibits.Any()) Console.WriteLine("База данных пуста. Добавьте хотя бы один экспонат.");

            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Просмотр базы");
            Console.WriteLine("2. Добавить экспонат");
            Console.WriteLine("3. Удалить экспонат");
            Console.WriteLine("4. Запросы");
            Console.WriteLine("0. Выход");

            switch (Console.ReadLine())
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
                    Console.Write("Введите ID: ");
                    try 
                    { 
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            MuseumManager.Delete(listOfExhibits, id);
                            MuseumManager.Save(listOfExhibits, FileName);
                        }
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
