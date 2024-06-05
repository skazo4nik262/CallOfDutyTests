
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
            string path = Path.Combine(Environment.CurrentDirectory, folder);
            Directory.CreateDirectory(path);
        }

        Random rnd = new Random();
        private string folder = "";

        public List<Student> GetRandomStudents(int count, Dictionary<Student, bool>.KeyCollection except = null)
        {
            var selectFrom = db.Students;
            if (except != null) 
                selectFrom = selectFrom.Except(except).ToList();

            if (selectFrom.Count < count)
                throw new StudentDutyException("Нужно больше студентов");

            var list = selectFrom.
                Select(s=>(student: s, count: GetDutyCount(s))).
                OrderBy(s=>s.count).
                ToList();
            int min = list[0].count;
            List<Student> takeFrom = new List<Student>();
            foreach (var stud in list)
            {
                if (stud.count == min)                    
                {
                    takeFrom.Add(stud.student);
                }
                else if(takeFrom.Count < count)
                {
                    takeFrom.Add(stud.student);
                    min = stud.count;
                }
            }

            List<Student> result = new List<Student>();
            while (result.Count < count)
            {
                Student student = takeFrom[rnd.Next(0, takeFrom.Count)];
                if (!result.Contains(student))
                    result.Add(student);
            }
            return result;
        }

        public int GetDutyCount(Student student)
        {
            string path = Path.Combine(Environment.CurrentDirectory, folder, $"{student.Info}.json");
            if (!File.Exists(path))
                return 0;
            List<DateTime> dutys = null;
            using (var fs = File.OpenRead(path))
                dutys = JsonSerializer.Deserialize<List<DateTime>>(fs);
            return dutys.Count;
        }

        internal void AddNewDuty(Student student, DateTime today)
        {
            List<DateTime> dutys = null;
            string path = Path.Combine(Environment.CurrentDirectory, folder, $"{student.Info}.json");
            if (!File.Exists(path))
                dutys = new List<DateTime>();
            else
                using (var fs = File.OpenRead(path))
                    dutys = JsonSerializer.Deserialize<List<DateTime>>(fs);
            
            dutys.Add(today);

            using (var fs = File.Create(path))
                JsonSerializer.Serialize(fs, dutys);
        }
    }
}