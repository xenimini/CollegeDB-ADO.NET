using CollegeConsole.Data;
using CollegeConsole.Models;

string connectionString = @"Server=localhost\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

var studentRepo = new StudentRepository(connectionString);
var groupRepo = new GroupRepository(connectionString);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Меню ===");
    Console.WriteLine("1. Показать всех студентов");
    Console.WriteLine("2. Добавить студента");
    Console.WriteLine("3. Показать все группы");
    Console.WriteLine("4. Добавить группу");
    Console.WriteLine("5. Найти студента по имени/фамилии");
    Console.WriteLine("0. Выход");
    Console.Write("Выбери пункт: ");

    string? choice = Console.ReadLine();

    if (choice == "1")
    {
        var students = studentRepo.GetAll();
        Console.WriteLine("\n--- Список студентов ---");
        if (students.Count == 0)
            Console.WriteLine("Студентов нет");
        else
            foreach (var s in students)
                Console.WriteLine(s);
    }
    else if (choice == "2")
    {
        Console.Write("Имя: ");
        string? firstName = Console.ReadLine();

        Console.Write("Фамилия: ");
        string? lastName = Console.ReadLine();

        Console.Write("Возраст: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Неверный возраст");
            Console.ReadKey();
            continue;
        }

        Console.Write("ID группы (или Enter): ");
        string? groupInput = Console.ReadLine();
        int? groupId = string.IsNullOrWhiteSpace(groupInput) ? null : int.Parse(groupInput);

        var student = new Student
        {
            FirstName = firstName ?? "",
            LastName = lastName ?? "",
            Age = age,
            GroupId = groupId
        };

        int newId = studentRepo.CreateStudent(student);
        Console.WriteLine($"Студент добавлен! Его Id = {newId}");
    }
    else if (choice == "3")
    {
        var groups = groupRepo.GetAll();
        Console.WriteLine("\n--- Список групп ---");
        if (groups.Count == 0)
            Console.WriteLine("Групп нет");
        else
            foreach (var g in groups)
                Console.WriteLine(g);
    }
    else if (choice == "4")
    {
        Console.Write("Название группы: ");
        string? name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            groupRepo.Add(name);
            Console.WriteLine("Группа добавлена!");
        }
    }
    else if (choice == "5")
    {
        Console.Write("Введи имя или фамилию: ");
        string? name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            var found = studentRepo.FindAllByName(name);
            Console.WriteLine("\n--- Результаты поиска ---");
            if (found.Count == 0)
                Console.WriteLine("Ничего не найдено");
            else
                foreach (var s in found)
                    Console.WriteLine(s);
        }
    }
    else if (choice == "0")
        break;
    else
        Console.WriteLine("Неверный выбор");

    Console.WriteLine("\nНажми любую клавишу...");
    Console.ReadKey();
}