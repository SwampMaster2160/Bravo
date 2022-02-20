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
            // Init employee list
            List<Employee> employees = new List<Employee>();
            // Print starting info
            Console.WriteLine("==========Employee Information==========");
            Console.Write("Functions:\n1: Add employee.\n2: Insert employee.\n3: Update employee.\n4: Delete employee.\n5: Search employee.\n6: Display employee.\n7: Exit.\n");
            // Enter running loop
            bool running = true;
            while (running)
            {
                // Get function number
                Console.Write("Please enter function number: ");
                char function = Console.ReadKey().KeyChar;
                Console.WriteLine();
                // Run the function
                switch (function)
                {
                    case '1':
                    case 'a':
                        Add(employees);
                        break;
                    case '2':
                    case 'i':
                        Insert(employees);
                        break;
                    case '3':
                    case 'u':
                        Update(employees);
                        break;
                    case '4':
                    case 'd':
                        Delete(employees);
                        break;
                    case '5':
                    case 's':
                        Search(employees);
                        break;
                    case '6':
                    case 'v':
                        Display(employees);
                        break;
                    case '7':
                    case 'e':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid function.");
                        break;
                }
            }
        }

        // Get a valid list index from the console.
        // If isPickLocation is set then the function will get an index for picking/replaceing/deleting an employee, else it will get one for inserting at the location.
        static int GetValidListIndex(List<Employee> employees, bool isPickLocation)
        {
            try
            {
                int count = employees.Count;
                int returnVal = int.Parse(Console.ReadLine());
                if (isPickLocation)
                {
                    if (count == 0)
                    {
                        Console.WriteLine("Error: employee list is empty.");
                        return -1;
                    }
                    count -= 1;
                }
                if (returnVal < 0)
                {
                    Console.WriteLine("Error: index is negitive, it must be at least 0.");
                    return -1;
                }
                if (count < returnVal)
                {
                    Console.WriteLine("Error: index is too large, must be at most " + count + ".");
                    return -1;
                }
                return returnVal;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: invalid number entered.");
                return -1;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: number entered is outside of valid range.");
                return -1;
            }
        }

        static void Add(List<Employee> employees)
        {
            // Get names
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            // Add to list
            employees.Add(new Employee(firstName, lastName));
        }

        static void Insert(List<Employee> employees)
        {
            // Get location
            Console.Write("Enter the insertion location: ");
            int insertLocation = GetValidListIndex(employees, false);
            if (insertLocation == -1) return;
            // Get names
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            // Insert
            employees.Insert(insertLocation, new Employee(firstName, lastName));
        }

        static void Update(List<Employee> employees)
        {
            // Get location
            Console.Write("Enter the insertion location: ");
            int location = GetValidListIndex(employees, true);
            if (location == -1) return;
            // Get name
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            // Update
            employees[location] = new Employee(firstName, lastName);
        }

        static void Delete(List<Employee> employees)
        {
            // Get location
            Console.Write("Enter the employee ID to delete: ");
            int id = GetValidListIndex(employees, true);
            if (id == -1) return;
            // Delete
            employees.RemoveAt(id);
        }

        static void Search(List<Employee> employees)
        {
            // Get name
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            // Loop thrugh all names
            for (int x = 0; x < employees.Count; x++)
            {
                // Print out a employee is their name is matches the search name
                if (employees[x].FirstName == firstName)
                {
                    Console.WriteLine(x + ": " + employees[x].FirstName + " " + employees[x].LastName + ".");
                }
            }
        }

        static void Display(List<Employee> employees)
        {
            // If the employee list is empty then say it is empty
            if (employees.Count == 0)
            {
                Console.WriteLine("Employee list is empty.");
                return;
            }
            // Else print out all employees
            else
            {
                Console.WriteLine("Employees:");
                for (int x = 0; x < employees.Count; x++)
                {
                    Employee employee = employees[x];
                    Console.WriteLine(x + ": " + employee.FirstName + " " + employee.LastName + ".");
                }
            }
        }
    }
}
