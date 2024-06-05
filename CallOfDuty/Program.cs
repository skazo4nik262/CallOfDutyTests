using CallOfDuty;
using System;

string file = "Students.txt";
StudentRepository studentRepository = new StudentRepository(file);
string folder = "dutys";
StudentDuty studentDuty = new StudentDuty(studentRepository, folder);

SelectDuty todayDuty = new SelectDuty(studentDuty);

try
{
    while (todayDuty.CountApproved < 2)
    {
        int index = 1;
        foreach (var student in todayDuty.Students)
            Console.WriteLine($"#{index++} {student.Name} {student.Info}");

        Console.WriteLine("Укажите индекс студента и через пробел знак + или - для подтверждения или отмены участия студента в святом дежурстве");

        var answer = Console.ReadLine();
        var cols = answer.Split();
        if (cols.Length != 2)
            continue;
        if (!int.TryParse(cols[0], out index))
        {
            Console.WriteLine("Неверно указан индекс студента. Укажите число первым в строке");
            continue;
        }
        string action = cols[1];
        if (action != "+" && action != "-")
        {
            Console.WriteLine("Действие должно обозначаться как + или -");
            continue;
        }
        index--;
        if (cols.Length > index && index >= 0)
        {
            if (action == "+")
                todayDuty.Approve(todayDuty.Students[index]);
            else
                todayDuty.RejectAndGetAnotherStudent(todayDuty.Students[index]);
        }
    }
    todayDuty.Save();
    Console.WriteLine("Дежурные сегодня:");
    foreach (var student in todayDuty.Students)
        Console.WriteLine($"{student.Name} {student.Info}");
}
catch (SelectDutyException ex)
{
    Console.WriteLine(ex.Message);
}
catch (StudentDutyException ex)
{
    Console.WriteLine(ex.Message);
}