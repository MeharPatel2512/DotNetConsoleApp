namespace ConsoleApp.Models
{
     public class Student{
        public int Id {get; set;}
        public String ?First_Name {get; set;}
        public String ?Last_Name {get; set;}
        public String ?Email {get; set;}
        public String ?Address {get; set;}
        public String ?Mobile {get; set;}
        public bool has_passed {get; set;}

        public Student(){}

        public Student(int Id, String ?First_Name, String ?Last_Name, String ?Email, String ?Address, String ?Mobile, bool has_passed){
            this.Id = Id;
            this.First_Name = First_Name;
            this.Last_Name = Last_Name;
            this.Email = Email;
            this.Address = Address;
            this.Mobile = Mobile;
            this.has_passed = has_passed;
        }
    }
}
