using System;

/// <summary>
/// Представляет музейный экспонат с основными характеристиками.
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

    // Свойства
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

    public bool IsOnDisplay
    {
        get => _isOnDisplay;
        private set => _isOnDisplay = value;
    }

    // Конструктор
   
    public MuseumExhibit(int id, string name, int year, double estimatedValue, bool isOnDisplay)
    {
        this.Id = id;
        this.Name = name;
        this.Year = year;
        this.EstimatedValue = estimatedValue;
        this.IsOnDisplay = isOnDisplay;
    }

    // Перегрузка метода ToString()
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Year: {Year}, Value: ${EstimatedValue:F2}, On Display: {IsOnDisplay}";
    }
}
