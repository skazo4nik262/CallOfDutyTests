namespace CallOfDuty.Tests
{
    public class Huina_tests
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

        }
        [Test]
        public void OsvobojdenieVsehStudentovOtJertvi()
        {
            foreach (var i in db.Students)
            {
                Assert.IsFalse(model.studentStatus.GetValueOrDefault(i));
            }
        }
    }
}