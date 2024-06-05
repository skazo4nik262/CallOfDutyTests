namespace CallOfDuty.Tests
{
    public class ModelTests
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

            Assert.That(propName.PropertyType, Is.EqualTo(typeof(string)));
            Assert.That(propInfo.PropertyType, Is.EqualTo(typeof(string)));
        }

        [Test]
        public void Duty_HasListDateProperty()
        {
            Duty studentDuty = new Duty();

            Type type = studentDuty.GetType();
            var prop = type.GetProperty("Dates");

            Assert.IsNotNull(prop);
            Assert.That(prop.PropertyType, Is.EqualTo(typeof(List<DateTime>)));
        }

        [Test]
        public void SelectDuty_HasPropertyStudents()
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_dutys";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            SelectDuty model = new SelectDuty(studentDuty);
            Type type = model.GetType();
            var prop = type.GetProperty("Students");

            Assert.IsNotNull(prop);
            Assert.That(prop.PropertyType, Is.EqualTo(typeof(List<Student>)));
        }


    }
}