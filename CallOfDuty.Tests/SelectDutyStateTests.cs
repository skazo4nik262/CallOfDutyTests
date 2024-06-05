using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CallOfDuty.Tests
{
    public class SelectDutyStateTests
    {
        SelectDuty model;
        StudentRepository db;
        StudentDuty studentDuty;
        [SetUp]
        public void Setup()
        {
            string file = "testStudents.txt";
            db = new StudentRepository(file);
            string folder = "test_dutys";
            studentDuty = new StudentDuty(db, folder);
            model = new SelectDuty(studentDuty);

            RemoveJsonTestFile(folder, "test4.json");
            RemoveJsonTestFile(folder, "test5.json");
        }

        private static void RemoveJsonTestFile(string folder, string name)
        {
            string path = Path.Combine(Environment.CurrentDirectory, folder, name);
            if (File.Exists(path))
                File.Delete(path);
        }

        [Test]
        public void SelectDuty_Take2StudentsWithLessDutyCount()
        {
            var stud1 = model.Students.FirstOrDefault(s => s.Info == "test2");
            var stud2 = model.Students.FirstOrDefault(s => s.Info == "test3");

            Assert.IsNotNull(stud1);
            Assert.IsNotNull(stud2);
        }

        [Test]
        public void SelectDuty_CanApproveStudentDuty()
        {
            var stud1 = model.Students.FirstOrDefault(s => s.Info == "test2");
            model.Approve(stud1);
            Assert.That(model.CountApproved, Is.EqualTo(1));
        }

        [Test]
        public void SelectDuty_CanApprove2StudentDuty()
        {
            foreach (var stud in model.Students)
                model.Approve(stud);
            Assert.That(model.CountApproved, Is.EqualTo(2));
        }

        [Test]
        public void SelectDuty_CanRejectStudentDuty()
        {
            var stud1 = model.Students.FirstOrDefault(s => s.Info == "test2");
            model.RejectAndGetAnotherStudent(stud1);
            Assert.That(model.CountApproved, Is.EqualTo(0));
        }

        [Test]
        public void SelectDuty_AfterRejectGetAnotherOneStudent()
        {
            var stud1 = model.Students.FirstOrDefault(s => s.Info == "test2");
            var stud2 = model.Students.FirstOrDefault(s => s.Info == "test3");
            var stud3 = model.RejectAndGetAnotherStudent(stud2);
            Assert.IsNotNull(stud3);
            Assert.That(stud3.Info, Is.EqualTo("test1"));
        }

        [Test]
        public void SelectDuty_ExceptionOnSaveIfApprovedLessThen2()
        {
            TestDelegate testDelegate = new TestDelegate(() => model.Save());
            Assert.Catch(typeof(SelectDutyException), testDelegate);
        }

        [Test]
        public void SelectDuty_SaveCreateJsonForAllApprovedStudents()
        {
            string file = "testStudents5.txt";
            db = new StudentRepository(file);
            string folder = "test_dutys";
            studentDuty = new StudentDuty(db, folder);
            model = new SelectDuty(studentDuty);

            foreach (var stud in model.Students)
                model.Approve(stud);
            model.Save();

            foreach (var stud in model.Students)
            {
                Assert.That(studentDuty.GetDutyCount(stud), Is.EqualTo(1));
            }
        }
    }
}
