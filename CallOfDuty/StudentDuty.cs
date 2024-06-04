
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CallOfDuty
{
    public class StudentDuty
    {
        private StudentRepository db;

        public StudentDuty(StudentRepository db)
        {
            this.db = db;
        }

        public StudentDuty(StudentRepository db, string folder) : this(db)
        {
            this.folder = folder;
        }

        Random rnd = new Random();
        private string folder;

        public List<Student> GetRandomStudents(int count)
        {
            if (db.Students.Count < count)
                throw new StudentDutyException("Нужно больше студентов");

            List<Student> result = new List<Student>();
            while (result.Count < count)
            {
                Student student = db.Students[rnd.Next(0, db.Students.Count)];
                if (!result.Contains(student))
                    result.Add(student);
            }
            return result;
        }

        public int GetDutyCount(Student student)
        {
            List<DateTime> dutys = null;
            using (var fs = File.OpenRead(Path.Combine(Environment.CurrentDirectory, folder,  $"{student.Info}.json")))
                dutys = JsonSerializer.Deserialize<List<DateTime>>(fs);
            return dutys.Count;
        }
    }
}