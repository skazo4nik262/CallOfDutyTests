namespace CallOfDuty
{
    public class SelectDuty
    {
        private StudentDuty studentDuty;

        public SelectDuty(StudentDuty studentDuty)
        {
            this.studentDuty = studentDuty;
            Students = studentDuty.GetRandomStudents(2);
            foreach (Student student in Students)
                Reject(student);
        }

        Dictionary<Student, bool> studentStatus = new Dictionary<Student, bool>();
        public List<Student> Students { get; set; } = new();
        public int CountApproved { get => studentStatus.Values.Where(s => s).Count(); }
        

        public void Approve(Student student)
        {
            SetStudentStatus(student, true);
        }

        public Student RejectAndGetAnotherStudent(Student student)
        {
            Reject(student);
            return GetAnotherStudent(student);
        }

        private void Reject(Student student)
        {
            SetStudentStatus(student, false);
        }

        private Student GetAnotherStudent(Student student)
        {
            Students.Remove(student);
            Student stud = studentDuty.GetRandomStudents(1, studentStatus.Keys).First();
            Students.Add(stud);
            return stud;
        }

        private void SetStudentStatus(Student student, bool status)
        {
            if (!studentStatus.ContainsKey(student))
                studentStatus.Add(student, status);
            studentStatus[student] = status;
        }

        public void Save()
        {
            if (CountApproved < 2)
                throw new SelectDutyException("Нужно назначить больше дежурных");

            foreach (var student in Students)
                studentDuty.AddNewDuty(student, DateTime.Today);
        }
    }
}