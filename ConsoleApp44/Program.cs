using System;
using System.Collections.Generic;

namespace StudentManagementSystem
{
    public enum Grade
    {
        Fail = 1,
        Good = 2,
        VeryGood = 3,
        Excellent = 4
    }

    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Governorate { get; set; }
        public double FirstTestScore { get; set; }
        public double SecondTestScore { get; set; }
        public Grade Grade { get; set; }
        public double Average { get; set; }

        public void CalculateAverageAndGrade()
        {
            Average = (FirstTestScore + SecondTestScore) / 2.0;

            if (Average < 50)
                Grade = Grade.Fail;
            else if (Average >= 50 && Average < 65)
                Grade = Grade.Good;
            else if (Average >= 65 && Average < 80)
                Grade = Grade.VeryGood;
            else
                Grade = Grade.Excellent;
        }

        public void DisplayStudent()
        {
            Console.WriteLine($"Student ID: {Id}");
            Console.WriteLine($"Full Name: {FullName}");
            Console.WriteLine($"Governorate: {Governorate}");
            Console.WriteLine($"First Test Score: {FirstTestScore}");
            Console.WriteLine($"Second Test Score: {SecondTestScore}");
            Console.WriteLine($"Average: {Average:F2}");
            Console.WriteLine($"Grade: {Grade}");
            Console.WriteLine("----------------------------------------");
        }
    }

    public class Node
    {
        public Student Data { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(Student student)
        {
            Data = student;
            Next = null;
            Previous = null;
        }
    }

    public class DoublyLinkedList
    {
        private Node head;
        private Node tail;
        private int count;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void AddLast(Student student)
        {
            Node newNode = new Node(student);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            count++;
        }

        public void AddFirst(Student student)
        {
            Node newNode = new Node(student);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            count++;
        }

        public void SortByName()
        {
            if (head == null || head.Next == null)
                return;

            bool swapped;
            do
            {
                swapped = false;
                Node current = head;

                while (current.Next != null)
                {
                    if (string.Compare(current.Data.FullName, current.Next.Data.FullName) > 0)
                    {
                        SwapNodes(current, current.Next);
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
        }

        public void SortByAverage()
        {
            if (head == null || head.Next == null)
                return;

            bool swapped;
            do
            {
                swapped = false;
                Node current = head;

       
while (current.Next != null)
                {
                    if (current.Data.Average > current.Next.Data.Average)
                    {
                        SwapNodes(current, current.Next);
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
        }

        private void SwapNodes(Node node1, Node node2)
        {
            Student temp = node1.Data;
            node1.Data = node2.Data;
            node2.Data = temp;
        }

        public List<Student> SearchByScore(double targetScore, Node current = null, List<Student> results = null)
        {
            if (results == null)
            {
                results = new List<Student>();
                current = head;
            }

            if (current == null)
                return results;

            if (current.Data.FirstTestScore == targetScore || current.Data.SecondTestScore == targetScore)
            {
                results.Add(current.Data);
            }

            return SearchByScore(targetScore, current.Next, results);
        }

        public void DisplayAllStudents()
        {
            if (head == null)
            {
                Console.WriteLine("No students in the list.");
                return;
            }

            Console.WriteLine("\n========== All Students List ==========");
            Node current = head;
            int studentNumber = 1;

            while (current != null)
            {
                Console.WriteLine($"\nStudent Number {studentNumber}:");
                current.Data.DisplayStudent();
                current = current.Next;
                studentNumber++;
            }
        }

        public int Count()
        {
            return count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("     Algorithms Course Student System     ");
            Console.WriteLine("==========================================");

            DoublyLinkedList studentList = new DoublyLinkedList();

            Console.WriteLine("\n--- Student Data Entry Phase ---");
            Console.WriteLine("Please enter data for 5 students:");

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\n--- Student {i} Data ---");
                Student student = InputStudentData(i);
                studentList.AddLast(student);
            }

            bool exit = false;

            while (!exit)
            {
                DisplayMainMenu();
                Console.Write("Choose an option: ", 1, 6);
                int choice = Convert.ToInt32(Console.ReadLine()); 

                switch (choice)
                {
                    case 1:
                        studentList.DisplayAllStudents();
                        break;

                    case 2:
                        SortStudents(studentList);
                        break;

                    case 3:
                        SearchStudentsByScore(studentList);
                        break;

                    case 4:
                        AddNewStudent(studentList);
                        break;

                    case 5:
                        AddFiveMoreStudents(studentList);
                        break;

                    case 6:
                        exit = true;
                        Console.WriteLine("\nThank you for using the system. Goodbye!");
                        break;
                }
            }
        }

        static Student InputStudentData(int id)
{
            Student student = new Student();
        student.Id = id;
            
            Console.Write("Enter student full name: ");
            student.FullName = Console.ReadLine();
            
            Console.Write("Enter governorate: ");
            student.Governorate = Console.ReadLine();
            
            student.FirstTestScore = GetDoubleInput("Enter first test score (0-100): ", 0, 100);
        student.SecondTestScore = GetDoubleInput("Enter second test score (0-100): ", 0, 100);

        student.CalculateAverageAndGrade();
            
            Console.WriteLine($"Calculated average: {student.Average:F2}");
            Console.WriteLine($"Grade: {student.Grade}");
            
            return student;
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n========== Main Menu ==========");
            Console.WriteLine("1. Display all students");
            Console.WriteLine("2. Sort students");
            Console.WriteLine("3. Search students by score");
            Console.WriteLine("4. Add new student");
            Console.WriteLine("5. Add 5 more students");
            Console.WriteLine("6. Exit");
            Console.WriteLine("===============================");
        }

        static void SortStudents(DoublyLinkedList studentList)
        {
            if (studentList.Count() == 0)
            {
                Console.WriteLine("No students to sort.");
                return;
            }

            Console.WriteLine("\n--- Sorting Options ---");
            Console.WriteLine("1. Sort by name (A to Z)");
            Console.WriteLine("2. Sort by average (lowest to highest)");
            int sortChoice = GetIntegerInput("Choose sorting method: ", 1, 2);

            if (sortChoice == 1)
            {
                studentList.SortByName();
                Console.WriteLine("Students sorted by name successfully.");
            }
            else
            {
                studentList.SortByAverage();
                Console.WriteLine("Students sorted by average successfully.");
            }

            studentList.DisplayAllStudents();
        }

        static void SearchStudentsByScore(DoublyLinkedList studentList)
        {
            if (studentList.Count() == 0)
            {
                Console.WriteLine("No students to search.");
                return;
            }

            double targetScore = GetDoubleInput("Enter the score to search for: ", 0, 100);

            List<Student> results = studentList.SearchByScore(targetScore);

            if (results.Count == 0)
            {
                Console.WriteLine($"No students found with score {targetScore}");
            }
            else
            {
                Console.WriteLine($"\nFound {results.Count} student(s) with score {targetScore}:");
                Console.WriteLine("==========================================");

                foreach (var student in results)
                {
                    student.DisplayStudent();
                }
            }
        }

        static void AddNewStudent(DoublyLinkedList studentList)
        {
            Console.WriteLine("\n--- Add New Student ---");
            Console.WriteLine("1. Add at beginning of list");
            Console.WriteLine("2. Add at end of list");
            int positionChoice = GetIntegerInput("Choose position: ", 1, 2);

            Student student = InputStudentData(studentList.Count() + 1);

            if (positionChoice == 1)
            {
                studentList.AddFirst(student);
                Console.WriteLine("Student added at beginning of list successfully.");
            }
            else
            {
                studentList.AddLast(student);
                Console.WriteLine("Student added at end of list successfully.");
            }
        }
static void AddFiveMoreStudents(DoublyLinkedList studentList)
    {
        Console.WriteLine("\n--- Add 5 More Students ---");

        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"\n--- Additional Student {i} Data ---");
            Student student = InputStudentData(studentList.Count() + 1);
            studentList.AddLast(student);
        }

        Console.WriteLine("5 additional students added successfully.");
    }

    static int GetIntegerInput(string message, int min, int max)
    {
        int value;
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Error! Please enter a number between {min} and {max}");
        }
    }

    static double GetDoubleInput(string message, double min, double max)
    {
        double value;
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Error! Please enter a number between {min} and {max}");
        }
    }
}
}
