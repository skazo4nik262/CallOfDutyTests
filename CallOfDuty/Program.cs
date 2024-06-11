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
                case "М":
                    ProgramBase.ViborJertv();
                    break;
                case "R":
                case "К":
                    Console.WriteLine("Введите имя и фамилию студента");
                    string fio = Console.ReadLine();
                    ProgramBase.RedactStudent(fio);
                    break;
                

            }
            
            Console.ReadLine();

        }
    }
}