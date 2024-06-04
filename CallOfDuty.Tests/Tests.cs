namespace CallOfDuty.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Student_HasNameInfoCountDutyProperties()
        {
            Student student = new Student();

            Type type = student.GetType();
            var propName = type.GetProperty("Name");
            var propInfo = type.GetProperty("Info");

            Assert.IsNotNull(propName);
            Assert.IsNotNull(propInfo);

            Assert.That(propName.PropertyType.Name, Is.EqualTo("String"));
            Assert.That(propInfo.PropertyType.Name, Is.EqualTo("String"));
        }

        [Test]
        public void StudentRepository_StudentListNotNullAfterInit()
        {
            StudentRepository db = new StudentRepository();

            Assert.IsNotNull(db.Students);
            Assert.That(db.Students, Is.TypeOf<List<Student>>());
        }

        List<Student> GetTestStudents()
        {
            return new List<Student> {
                new Student{ Name = "Валера", Info = "test1" },
                new Student{ Name = "Серега", Info = "test2" },
                new Student{ Name = "Валера", Info = "test3" }
            };
        }

        [Test]
        public void StudentRepository_LoadStudentsFromFileAfterInit()
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            var students = GetTestStudents();

            Assert.That(db.Students.Count, Is.EqualTo(students.Count));
            for (int i = 0; i < students.Count; i++)
            {
                Assert.That(db.Students[i].Name, Is.EqualTo(students[i].Name));
                Assert.That(db.Students[i].Info, Is.EqualTo(students[i].Info));
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void StudentRepository_CanPickRandomStudent(int count)
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);

            List<Student> students = studentDuty.GetRandomStudents(count);

            Assert.IsNotNull(students);
            Assert.That(students.Count, Is.EqualTo(count));
            Assert.That(students.Count, Is.EqualTo(students.Distinct().Count()));
        }

        [Test]
        public void StudentRepository_ThrowExceptionOnPickRandomStudent_CountNotEnough()
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);

            TestDelegate testDelegate = new TestDelegate(() => studentDuty.GetRandomStudents(4));
            Assert.Catch(typeof(StudentDutyException), testDelegate);
        }

        [Test]
        public void Duty_HasListDateProperty()
        {
            Duty studentDuty = new Duty();

            Type type = studentDuty.GetType();
            var prop = type.GetProperty("Dates");

            Assert.IsNotNull(prop);
            //Assert.That(prop.PropertyType, Is.TypeOf<List<DateTime>>()));
        }

        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void StudentRepository_StudentsHaveSomeApprovedDutyAfterInit(int studIndex, int dutyCount)
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_dutys";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            Student student = db.Students[studIndex];
            int studentDutyCount = studentDuty.GetDutyCount(student);

            Assert.That(dutyCount, Is.EqualTo(studentDutyCount));
        }
    }
}