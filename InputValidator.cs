using System;

/// <summary>
/// Содержит методы валидации и безопасного ввода данных.
/// </summary>
public static class InputValidator
{
    public static int ReadInt(string prompt)
    {
        int value;
        do
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine($"Введите целое число!");
        } while (true);
    }

    public static double ReadDouble(string prompt)
    {
        double value;
        do
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine($"Введите вещественное число!");
        } while (true);
    }

    public static string ReadNonEmptyString(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(input)) return input;

            Console.WriteLine("Поле не может быть пустым!");
        } while (true);
    }

    public static bool ReadBool(string prompt)
    {
        do
        {
            Console.Write(prompt + " (1 —да, 0 — нет): ");
            string input = Console.ReadLine();
            if (input == "1") return true;
            if (input == "0") return false;

            Console.WriteLine("Введите 1 или 0!");
        } while (true);
    }
}
