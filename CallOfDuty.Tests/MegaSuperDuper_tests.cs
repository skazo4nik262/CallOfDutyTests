namespace CallOfDuty.Tests
{
    public class MegaSuperDuper_tests
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
        [Test]
        public void RedactStudentaKotorogoNet()
        {
            Random rnd = new Random();
            string search;
            string simbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int a = simbols.Length;
            for (int i = 0; i < 11; i++)
            {
                search = simbols[rnd.Next(a)].ToString();
            }
            // доделать
        }
    }
}