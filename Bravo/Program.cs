using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravo
{
    class Employee
    {
        public string FirstName;
        public string LastName;

        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            Console.WriteLine("==========Employee Information==========");
            Console.Write("Functions:\n1: Add employee.\n2: Insert employee.\n3: Update employee.\n4: Delete employee.\n5: Search employee.\n6: Display employee.\n");
            while (true)
            {
                Console.Write("Please enter function number: ");
                char function = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (function)
                {
                    case '1':
                        Add(employees);
                        break;
                    case '2':
                        Insert(employees);
                        break;
                    case '3':
                        Update(employees);
                        break;
                    case '6':
                        Display(employees);
                        break;
                    default:
                        Console.WriteLine("Invalid function.");
                        break;
                }
            }
        }

        static void Add(List<Employee> employees)
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            employees.Add(new Employee(firstName, lastName));
        }

        static void Insert(List<Employee> employees)
        {
            Console.Write("Enter the insertion location: ");
            int insertLocation = int.Parse(Console.ReadLine());
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            employees.Insert(insertLocation, new Employee(firstName, lastName));
        }

        static void Update(List<Employee> employees)
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter the insertion location: ");
            int location = int.Parse(Console.ReadLine());
            employees[location] = new Employee(firstName, lastName);
        }

        static void Display(List<Employee> employees)
        {
            Console.WriteLine("Employees:");
            for (int x = 0; x < employees.Count(); x++)
            {
                Employee employee = employees[x];
                Console.WriteLine(x + ": " + employee.FirstName + " " + employee.LastName + ".");
            }
        }
    }
}
