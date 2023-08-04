using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

[Serializable]
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public readonly int RollNumber;
    public string Grade { get; set; }

    private static int rollNumberCounter = 1;

    public Student(string name, int age, string grade)
    {
        Name = name;
        Age = age;
        RollNumber = rollNumberCounter++;
        Grade = grade;
    }
}

public class StudentList<T> where T : Student
{
    private List<T> students;

    public StudentList()
    {
        students = new List<T>();
    }

    public void AddStudent(T student)
    {
        students.Add(student);
    }


    public T GetStudentByName(string name)
    {
        return students.FirstOrDefault(s => s.Name == name);
    }

    public T GetStudentByRoleNumber(int rollNumber)
    {
        return students.FirstOrDefault(s => s.RollNumber == rollNumber);
    }

    public List<T> GetAllStudents()
    {
        return students;
    }
}

public class Program
{
    private const string DataFileName = "all-students.json";

    static void Main()
    {
        StudentList<Student> studentList = new StudentList<Student>();
        LoadStudentsFromFile(studentList);

        Console.WriteLine(@"
------------------------------------------
|                    Welcome!             |
|                                         |
------------------------------------------
");


        while (true)
        {
            int choice;
            do
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Display all students");
                Console.WriteLine("2. Search student by name");
                Console.WriteLine("3. Search student by roll number");
                Console.WriteLine("4. Add new student");
                Console.WriteLine("5. Exit");

            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5);

            switch (choice)
            {
                case 1:
                    DisplayAllStudents(studentList);

                    break;
                case 2:
                    SearchStudentByName(studentList);
                    break;
                case 3:
                    SearchStudentById(studentList);
                    break;
                case 4:
                    AddNewStudent(studentList);
                    break;
                case 5:
                    SaveStudentsToFile(studentList);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }


    private static void AddNewStudent(StudentList<Student> studentList)
    {
        Console.WriteLine("Enter student name:");
        string name = Console.ReadLine();
        int age;
        do
        {
            Console.WriteLine("Enter student age:");

        } while (!int.TryParse(Console.ReadLine(), out age) || age < 1);

        Console.WriteLine("Enter student grade:");
        string grade = Console.ReadLine();


        Student newStudent = new Student(name, age, grade);
        studentList.AddStudent(newStudent);
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Student added successfully.");
        Console.WriteLine("---------------------------------");
    }

    private static void SearchStudentByName(StudentList<Student> studentList)
    {
        Console.WriteLine("Enter student name to search:");
        string nameToSearch = Console.ReadLine();

        Student student = studentList.GetStudentByName(nameToSearch);
        if (student != null)
        {
            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Roll Number: {student.RollNumber}, Grade: {student.Grade}");
            Console.WriteLine("----------------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("There is no student with this name");
            Console.WriteLine("-----------------------------------");
        }
    }

    private static void SearchStudentById(StudentList<Student> studentList)
    {
      
        int idToSearch;
        do
        {
    Console.WriteLine("Enter student ID (Roll Number) to search:");

        } while (!int.TryParse(Console.ReadLine(), out idToSearch) || idToSearch < 1);


        Student student = studentList.GetStudentByRoleNumber(idToSearch);
        if (student != null)
        {
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Roll Number: {student.RollNumber}, Grade: {student.Grade}");
            Console.WriteLine("----------------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("There is no student with this role number.");
        }
    }

    private static void DisplayAllStudents(StudentList<Student> studentList)
    {
        Console.WriteLine("List of all students:");
        var allStudents = studentList.GetAllStudents();

        foreach (var student in allStudents)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Roll Number: {student.RollNumber}, Grade: {student.Grade}");
       
        }
        Console.WriteLine("-----------------------------------------------------------------------------------");
    }

    private static void LoadStudentsFromFile(StudentList<Student> studentList)
    {
        if (File.Exists(DataFileName))
        {
            string jsonData = File.ReadAllText(DataFileName);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonData);

            if (students != null && students.Any())
            {
                foreach (var student in students)
                {
                    studentList.AddStudent(student);
                }
            }
        }
    }

    private static void SaveStudentsToFile(StudentList<Student> studentList)
    {
        var allStudents = studentList.GetAllStudents();
        string jsonData = JsonSerializer.Serialize(allStudents);
        File.WriteAllText(DataFileName, jsonData);
    }
}
