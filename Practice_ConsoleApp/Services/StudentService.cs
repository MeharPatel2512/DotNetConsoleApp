using System.Data;
using ConsoleApp.Models;
using ConsoleApp.Repositories;
using Microsoft.Data.SqlClient;

namespace ConsoleApp.Services
{
    public class StudentService : IStudentRepository{

        private readonly IExecuteStoredProcedure _executeStroedProcedure;

        public StudentService(IExecuteStoredProcedure executeStroedProcedure)
        {
            _executeStroedProcedure = executeStroedProcedure;
        }

        public void GetAllStudents(){
            var dataset = _executeStroedProcedure.CallStoredProcedure("ConsoleApp.get_all_students", null);

            List<Student> resultList = [];
            if(dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0){
                DataTable dt = dataset.Tables[0];
                foreach (DataRow row in dt.Rows){
                    resultList.Add(new Student(
                        row.Table.Columns.Contains("Id") ? row.Field<int>("Id") : 0,
                        row.Table.Columns.Contains("First_Name") ? row.Field<String>("First_Name") : null,
                        row.Table.Columns.Contains("Last_Name") ? row.Field<String>("Last_Name") : null,
                        row.Table.Columns.Contains("Email") ? row.Field<String>("Email") : null,
                        row.Table.Columns.Contains("Address") ? row.Field<String>("Address") : null,
                        row.Table.Columns.Contains("Mobile") ? row.Field<String>("Mobile") : null,
                        row.Table.Columns.Contains("has_passed") && row.Field<bool>("has_passed")
                    ));
                    }
                    foreach (var student in resultList){
                        Console.WriteLine($"Id : {student.Id}");
                        Console.WriteLine($"First Name : {student.First_Name}");
                        Console.WriteLine($"Last Name : {student.Last_Name}");
                        Console.WriteLine($"Email : {student.Email}");
                        Console.WriteLine($"Mobile : {student.Mobile}");
                        Console.WriteLine($"Address : {student.Address}");
                        Console.WriteLine($"Has already passed? : {student.has_passed}");
                    }
            }
            else{
                Console.WriteLine("No Data Found!!");
            }
        }
        public void GetOneStudent(int Id){
            
            if (Id <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.");
            }
            var parameter = new Dictionary<String, Object>{
                {"@Id", Id}
            };
            var dataset = _executeStroedProcedure.CallStoredProcedure("ConsoleApp.get_one_student", parameter);

            Student student = new Student();
            if(dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0){
                var dt = dataset.Tables[0].Rows[0];
                    student.Id = dt.Field<int>("Id");
                    student.First_Name = dt.Field<String>("First_Name");
                    student.Last_Name = dt.Field<String>("Last_Name");
                    student.Email = dt.Field<String>("Email");
                    student.Address = dt.Field<String>("Address");
                    student.Mobile = dt.Field<String>("Mobile");
                    student.has_passed = dt.Field<bool>("has_passed");
                Console.WriteLine($"Id : {student.Id}");
                Console.WriteLine($"First Name : {student.First_Name}");
                Console.WriteLine($"Last Name : {student.Last_Name}");
                Console.WriteLine($"Email : {student.Email}");
                Console.WriteLine($"Mobile : {student.Mobile}");
                Console.WriteLine($"Address : {student.Address}");
                Console.WriteLine($"Has already passed? : {student.has_passed}");
            }            
            else{
                Console.WriteLine("No data found with that Id!");
            }
        }

        public void UpsertStudent(int Id, String ?First_Name, String? Last_Name, String ?Email, String ?Address, String? Mobile, bool has_passed){
            
            var parameter = new Dictionary<String, Object>{
                {"@Id", Id},
                {"@First_Name", First_Name ?? (object)DBNull.Value},
                {"@Last_Name", Last_Name ?? (object)DBNull.Value},
                {"@Email", Email ?? (object)DBNull.Value},
                {"@Address", Address ?? (object)DBNull.Value},
                {"@Mobile", Mobile ?? (object)DBNull.Value},
                {"@has_passed", has_passed}
            };

            try{
                // Console.WriteLine("Hi");
                _executeStroedProcedure.CallStoredProcedure("ConsoleApp.upsert_student", parameter);
            }
            catch(SqlException sqlex){
                throw new Exception("Something wrong with the SQL connection!!" + sqlex);
            }
            catch(Exception exception){
                throw new Exception("An unexpected error occurred!!" + exception);
            }
            finally{
                Console.WriteLine("Student Inserted Successfully!!");
            }
        }

        public void DeleteOneStudent(int Id){
            
            var parameter = new Dictionary<String, Object>{
                {"@Id", Id}
            };

            try{
                var dataset = _executeStroedProcedure.CallStoredProcedure("ConsoleApp.delete_one_student", parameter);
                if(dataset.Tables.Count <= 0){
                    Console.WriteLine("No recoreds Deleted!!");
                }
            }
            catch(SqlException sqlex){
                throw new Exception("Something wrong with the SQL connection!!" + sqlex);
            }
            catch(Exception exception){
                throw new Exception("An unexpected error occurred!!" + exception);
            }
            finally{
                Console.WriteLine("Student Deleted Successfully!!");
            }
        }
    }
}