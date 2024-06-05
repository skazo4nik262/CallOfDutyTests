using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallOfDuty.Tests
{
    public class StudentDutyTests
    {
        [SetUp]
        public void Setup()
        {
            string folder = "test_folder";
            string path = Path.Combine(Environment.CurrentDirectory, folder);

            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void StudentDuty_CanPickRandomStudent(int count)
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
        public void StudentDuty_ThrowExceptionOnPickRandomStudent_CountNotEnough()
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);

            TestDelegate testDelegate = new TestDelegate(() => studentDuty.GetRandomStudents(4));
            Assert.Catch(typeof(StudentDutyException), testDelegate);
        }

        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void StudentDuty_StudentsHaveSomeApprovedDutyAfterInit(int studIndex, int dutyCount)
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_dutys";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            Student student = db.Students[studIndex];
            int studentDutyCount = studentDuty.GetDutyCount(student);

            Assert.That(dutyCount, Is.EqualTo(studentDutyCount));
        }

        [Test]
        public void StudentDuty_FileWithDutyCreatesForNewStudent()
        {
            string file = "testStudents4.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_dutys";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            Student student = db.Students[3];
            int studentDutyCount = studentDuty.GetDutyCount(student);

            Assert.That(studentDutyCount, Is.EqualTo(0));
        }

        [Test]
        public void StudentDuty_CreateFolderForDutyIfNotExist()
        {
            string file = "testStudents4.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_folder";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            string path = Path.Combine(Environment.CurrentDirectory, folder);
            Assert.That(Directory.Exists(path), Is.True);
        }
    }
}
