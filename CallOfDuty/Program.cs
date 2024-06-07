using CallOfDuty;
using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace CallOfDuty
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Для выбора дежурного напишите: V \nДля редактирования студента напишите: R");
            string a = Console.ReadLine();
            switch (a)
            {
                case "V":
                    ViborJertv();
                    break;
                case "R":
                case "К":
                    RedactStudent();
                    break;
                case "М":
                    ViborJertv();
                    break;
                

            }
            Console.ReadLine();

        }

        private static void RedactStudent()
        {
            string file = "Students.txt";
            StudentRepository studentRepository = new StudentRepository(file);
            string folder = "dutys";
            StudentDuty studentDuty = new StudentDuty(studentRepository, folder);
            SelectDuty todayDuty = new SelectDuty(studentDuty);

            Console.WriteLine("Введите имя и фамилию студента");
            string fio = Console.ReadLine();
            fio = fio.Replace(" ", ";");
            //Console.WriteLine(fio);
            //int id = 0;

            List<string> Students = new();
            foreach (Student a in studentRepository.Students)
                Students.Add(a.Name +";"+ a.Info);
            if (Students.Contains(fio))
            {
                Console.WriteLine("Студент найден");
                Console.WriteLine("Введите измененное имя и фамилию");
                string newfio = Console.ReadLine();
                newfio = newfio.Replace(" ", ";");
                if (Students.Contains(fio))
                {
                    foreach (var zov in Students)
                    {
                        zov.Replace(fio, newfio);
                        int id = Students.IndexOf(zov);
                        Students[id] = zov;
                    }

                }

                File.WriteAllLines(file, Students);
            }
            /*foreach (Student a in studentRepository.Students)
            {
                string b = a.Name + a.Info;
                if (fio == b )
                {
                    id = studentRepository.Students.IndexOf(a);
                    Console.WriteLine("Студент найден");
                    break;
                }
            }*/

            /*Console.WriteLine("Введите измененное имя");
            studentRepository.Students[id].Name = Console.ReadLine();
            Console.WriteLine("Введите измененную фамилию");
            studentRepository.Students[id].Info = Console.ReadLine();
            List<string> Students = new();
            foreach (var z in studentRepository.Students)
            {
                Students.Add(z.Name + ";" + z.Info + ";");
            }
            File.WriteAllLines(file, Students);*/

        }

        private static void ViborJertv()
        {
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
        }
    }
}