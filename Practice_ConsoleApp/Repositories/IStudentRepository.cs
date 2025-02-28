namespace ConsoleApp.Repositories
{
    public interface IStudentRepository{
        public void GetAllStudents();
        public void GetOneStudent(int Id);
        public void UpsertStudent(int Id, String ?First_Name, String? Last_Name, String ?Email, String ?Address, String? Mobile, bool has_passed);
        public void DeleteOneStudent(int Id);
    }
}