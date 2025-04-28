using System;

/// <summary>
/// Содержит методы для безопасного ввода и валидации данных
/// </summary>
public static class InputValidator
{
    /// <summary>
    /// Считывает целое число из консоли с валидацией ввода
    /// </summary>
    /// <param name="prompt">Сообщение для пользователя</param>
    /// <returns>Корректно введённое целое число</returns>
    public static int ReadInt(string prompt)
    {
        int value;
        do
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Введите целое число!");
        } while (true);
    }

    /// <summary>
    /// Считывает вещественное число из консоли с валидацией ввода
    /// </summary>
    /// <param name="prompt">Сообщение для пользователя</param>
    /// <returns>Корректно введённое вещественное число</returns>
    public static double ReadDouble(string prompt)
    {
        double value;
        do
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Введите вещественное число!");
        } while (true);
    }

    /// <summary>
    /// Считывает непустую строку из консоли
    /// </summary>
    /// <param name="prompt">Сообщение для пользователя</param>
    /// <returns>Введённая строка без незначащих пробелов пробелов</returns>
    public static string ReadNonEmptyString(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(input)) return input;

            Console.WriteLine("Поле не может быть пустым!");
        } while (true);
    }

    /// <summary>
    /// Преобразует введённое значение(1 или 0) в булевое значение(true и false соответсвенно)
    /// </summary>
    /// <param name="prompt">Сообщение для пользователя</param>
    /// <returns>Булевое значение на основе ввода</returns>
    public static bool ReadBool(string prompt)
    {
        do
        {
            Console.Write(prompt + " (1 — Да, 0 — Нет): ");
            string input = Console.ReadLine();
            if (input == "1") return true;
            if (input == "0") return false;

            Console.WriteLine("Введите 1 или 0!");
        } while (true);
    }
}
