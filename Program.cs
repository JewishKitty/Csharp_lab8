class Program
{
    // ��� �������� ������� ��������� ����� ������������ ���� � ������ ��������� � � ����������� ��� ������ ����� ����������� � ���
    const string FileName = "exhibits.bin"; 

   
    static void Main()
    {
         
        var listOfExhibits = MuseumManager.Load(FileName);

        while (true)
        {
            // ���� ������ ���� ������� ���������
            if (!listOfExhibits.Any()) Console.WriteLine("���� ������ �����. �������� ���� �� ���� ��������.");

            Console.WriteLine("\n����:");
            Console.WriteLine("1. �������� ����");
            Console.WriteLine("2. �������� ��������");
            Console.WriteLine("3. ������� ��������");
            Console.WriteLine("4. �������");
            Console.WriteLine("0. �����");

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
                        Console.WriteLine("������ ���������� � ����: " + ex.Message);
                    }
                    break;

                case "3":
                    Console.Write("������� ID: ");
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
                        Console.WriteLine("������ ���������� � ����: " + ex.Message);
                    }
                    break;

                case "4":
                    MuseumManager.RunQueries(listOfExhibits);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("������������ ��������!");
                    break;
            }
        }
    }



}
