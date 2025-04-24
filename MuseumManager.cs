using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// ������������� ������ ��� ������ � ���������� �������� ����������.
/// </summary>
public static class MuseumManager
{
    // �������� ������ ��������� ���������
    public static MuseumExhibit CreateExhibit(List<MuseumExhibit> listOfExhibits)
    {
        int id;
        do
        {
            id = InputValidator.ReadInt("������� ID (����������): ");
            if (listOfExhibits.Any(e => e.Id == id))
                Console.WriteLine("�������� � ����� ID ��� ����������!");
            else
                break;
        } while (true);

        string name = InputValidator.ReadNonEmptyString("������� ��������: ");
        int year = InputValidator.ReadInt("������� ��� ��������: ");
        double value = InputValidator.ReadDouble("������� ��������� ���������: ");
        bool onDisplay = InputValidator.ReadBool("��������� �� �������� � ����������");

        return new MuseumExhibit(id, name, year, value, onDisplay);
    }

    // �������������� �� � ������ �������� ����������
    public static List<MuseumExhibit> Load(string filename)
    {
        if (!File.Exists(filename)) return new List<MuseumExhibit>();
        using var fs = new FileStream(filename, FileMode.Open);
        var bf = new BinaryFormatter();
        return (List<MuseumExhibit>) bf.Deserialize(fs);
    }

    //���������� �������� ������ ���������� � ����
    public static void Save(List<MuseumExhibit> listOfExhibits, string filename)
    {
        using var fs = new FileStream(filename, FileMode.Create);
        var bf = new BinaryFormatter();
        bf.Serialize(fs, listOfExhibits);
    }

    // �������� ����������� ��
    public static void View(List<MuseumExhibit> listOfExhibits)
    {
        listOfExhibits.ForEach(e => Console.WriteLine(e));
    }

    // ���������� ��������� ��������� � ������
    public static void Add(List<MuseumExhibit> listOfExhibits, MuseumExhibit exhibit)
    {
        listOfExhibits.Add(exhibit);
    }

    // �������� �������� �� �����
    public static void Delete(List<MuseumExhibit> listOfExhibits, int id)
    {
        var exhibitToRemove = listOfExhibits.FirstOrDefault(e => e.Id == id);
        if (exhibitToRemove != null) listOfExhibits.Remove(exhibitToRemove);
        else Console.WriteLine("ID �� ��������. �� ���� ������� �� �����.");
    }

    // ��������� ���������� � ��������� ���������� ������ ��������
    public static IEnumerable<MuseumExhibit> GetExpensiveExhibits(List<MuseumExhibit> listOfExhibits, double minValue) =>
        listOfExhibits.Where(e => e.EstimatedValue >= minValue);

    
    // ��������� ���������� � ����� �������� ������ ���������
    public static IEnumerable<MuseumExhibit> GetOldExhibits(List<MuseumExhibit> listOfExhibits, int beforeYear) =>
        listOfExhibits.Where(e => e.Year < beforeYear);

    // ������� ���������� ������������ ����������
    public static int CountOnDisplay(List<MuseumExhibit> listOfExhibits) =>
        listOfExhibits.Count(e => e.IsOnDisplay);

    // ������� ������� ���� ����������
    public static double AverageValue(List<MuseumExhibit> listOfExhibits) =>
        listOfExhibits.Average(e => e.EstimatedValue);

    // ���� ��������
    public static void RunQueries(List<MuseumExhibit> listOfExhibits)
    {
        Console.WriteLine("1. ��������� ������ �������� �����");
        Console.WriteLine("2. ��������� �� ��������� ����");
        Console.WriteLine("3. ���-�� ���������� �� ����������");
        Console.WriteLine("4. ������� ��������� ����������");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("����������� ���������: ");
                double min = double.Parse(Console.ReadLine());
                var exspensiveList = MuseumManager.GetExpensiveExhibits(listOfExhibits, min);
                if (exspensiveList.Count() == 0)
                {
                    Console.WriteLine("��� ���������� ������ ��������� ���������.");
                }
                foreach (var e in exspensiveList) Console.WriteLine(e);
                break;
            case "2":
                Console.Write("�� ������ ����: ");
                int year = int.Parse(Console.ReadLine());
                var oldList = MuseumManager.GetOldExhibits(listOfExhibits, year);
                if (oldList.Count() == 0)
                {
                    Console.WriteLine("��� ���������� ������ ��������� ����.");
                }
                foreach (var e in oldList) Console.WriteLine(e);
                break;
            case "3":
                Console.WriteLine("��������� ���������� �� ����������: " + MuseumManager.CountOnDisplay(listOfExhibits));
                break;
            case "4":
                if (listOfExhibits.Count() == 0)
                {
                    Console.WriteLine("��� ���������� ��� �������� ������� ���������.");
                }
                else Console.WriteLine("������� ���������: $" + MuseumManager.AverageValue(listOfExhibits).ToString("F2"));
                break;
            default:
                Console.WriteLine("������������ ��������!");
                break;
        }
    }
}
