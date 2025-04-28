using System;

/// <summary>
/// Представляет музейный экспонат с основными характеристиками
/// </summary>
[Serializable]
public class MuseumExhibit
{
    // Поля
    private int _id;
    private string _name;
    private int _year;
    private double _estimatedValue;
    private bool _isOnDisplay;

    //Свойства
    /// <summary>
    /// Уникальный дентификатор экспоната
    /// </summary>
    public int Id
    {
        get => _id;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("ID должен быть положительным числом.");
            _id = value;
        }
    }

    /// <summary>
    /// Название экспонат
    /// </summary>
    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название экспоната не может быть пустым.");
            _name = value.Trim();
        }
    }

    /// <summary>
    /// Год создания экспоната
    /// </summary>
    public int Year
    {
        get => _year;
        private set
        {
            if (value > DateTime.Now.Year)
                throw new ArgumentException("Год создания экспоната не может превышать текущий год.");
            _year = value;
        }
    }

    /// <summary>
    /// Оценочная стоимость экспоната
    /// </summary>
    public double EstimatedValue
    {
        get => _estimatedValue;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Оценочная стоимость не может быть отрицательной.");
            _estimatedValue = value;
        }
    }

    /// <summary>
    /// Находится ли экспонат в экспозиции
    /// </summary>
    public bool IsOnDisplay
    {
        get => _isOnDisplay;
        private set => _isOnDisplay = value;
    }

    //Конструктор
    /// <summary>
    /// Инициализирует новый экземпляр класса MuseumExhibit с заданными характеристиками
    /// </summary>
    /// <param name="id">Уникальный идентификатор экспоната</param>
    /// <param name="name">Название экспоната</param>
    /// <param name="year">Год создания экспоната</param>
    /// <param name="estimatedValue">Оценочная стоимость экспоната</param>
    /// <param name="isOnDisplay">Находится ли экспонат в экспозиции</param>
    public MuseumExhibit(int id, string name, int year, double estimatedValue, bool isOnDisplay)
    {
        this.Id = id;
        this.Name = name;
        this.Year = year;
        this.EstimatedValue = estimatedValue;
        this.IsOnDisplay = isOnDisplay;
    }

    /// <summary>
    /// Возвращает строковое представление экземпляра класса MuseumExhibit
    /// </summary>
    /// <returns>Строка с описанием всех свойств экспоната</returns>
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Year: {Year}, Value: ${EstimatedValue:F2}, On Display: {IsOnDisplay}";
    }
}
