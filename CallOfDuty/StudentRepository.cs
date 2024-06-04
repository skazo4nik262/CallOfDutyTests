
namespace CallOfDuty
{
    public class StudentRepository
    {
        public List<Student> Students { get; set; }

        public StudentRepository()
        {
            Students = new List<Student>();
        }

        public StudentRepository(string file)
        {
            var lines = File.ReadAllLines(file);
            Students = new List<Student>(lines.Length);
            foreach (var line in lines)
            {
                var cols = line.Split(';');
                Students.Add(new Student { Name = cols[0], Info = cols[1] });
            }
        }        
    }
}