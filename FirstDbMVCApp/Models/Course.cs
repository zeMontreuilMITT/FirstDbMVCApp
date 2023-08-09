namespace FirstDbMVCApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HashSet<Student> EnrolledStudents = new HashSet<Student>();
    }
}
