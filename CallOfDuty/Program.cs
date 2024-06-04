using CallOfDuty;
using System.Text.Json;
string file = "Students.txt";
StudentRepository studentRepository = new StudentRepository(file);
StudentDuty studentDuty = new StudentDuty(studentRepository);
var victims = studentDuty.GetRandomStudents(2);

foreach (var v in victims)
{
    Console.WriteLine($"{v.Name}\t{v.Info}");
}


List<DateTime> dateTimes = new List<DateTime>();
dateTimes.Add(new DateTime(2024, 5, 2));
dateTimes.Add(new DateTime(2024, 1, 3));
dateTimes.Add(new DateTime(2024, 2, 4));

var str = JsonSerializer.Serialize(dateTimes);
File.WriteAllText("test", str);