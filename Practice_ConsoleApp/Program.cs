using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ConsoleApp.Models;
using ConsoleApp.Repositories;
using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;

class Program{
        public readonly IStudentRepository _studentrepository;
        
        public Program(IStudentRepository studentrepository){
            _studentrepository = studentrepository;
        }

        public static void Main(String[] args){
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IStudentRepository, StudentService>()
            .AddSingleton<IExecuteStoredProcedure, ExecuteStoredProcedure>() 
            .AddSingleton<Program>() 
            .BuildServiceProvider();
 
        var program = serviceProvider.GetService<Program>();
        program?.Run();  
        }

        public void Run(){

            int num = 0;

            while(num != 5){

                Console.WriteLine("Enter 1 for get all students");
                Console.WriteLine("Enter 2 for get one student");
                Console.WriteLine("Enter 3 for upsert one student");
                Console.WriteLine("Enter 4 for delete one student");
                Console.WriteLine("Enter anything else to exit");

                num = Convert.ToInt32(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        _studentrepository.GetAllStudents();
                        break;
                    
                    case 2:
                        Console.WriteLine("Enter the Id of the student that you want details of : ");
                        int get_stud_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(get_stud_id);
                        _studentrepository.GetOneStudent(get_stud_id);
                        break;

                    case 3:
                        Console.WriteLine("Enter the Id of student : ");
                        int new_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the First Name of student : ");
                        String ?new_first_name = Console.ReadLine();
                        Console.WriteLine("Enter the Last Name of student : ");
                        String ?new_last_name = Console.ReadLine();
                        Console.WriteLine("Enter the Email of student : ");
                        String ?new_email = Console.ReadLine();
                        Console.WriteLine("Enter the Address of student : ");
                        String ?new_address = Console.ReadLine();
                        Console.WriteLine("Enter the Mobile number of student : ");
                        String ?new_mobile = Console.ReadLine();
                        Console.WriteLine("Has student already passed ? (y/n) : ");
                        bool new_has_passed = Console.ReadLine() == "y";
                        Console.ReadLine();

                        _studentrepository.UpsertStudent(new_id, new_first_name, new_last_name, new_email, new_address, new_mobile, new_has_passed);
                        break;

                    case 4:
                        Console.WriteLine("Enter the Id of student which you want to delete : ");
                        int delete_id = Convert.ToInt32(Console.ReadLine());
                        _studentrepository.DeleteOneStudent(delete_id);
                        break;

                    default:
                        num = 5;
                        break;
                }
            }
            return;
        }
}

