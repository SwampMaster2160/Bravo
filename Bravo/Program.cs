using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravo
{
    class Employee
    {
        public string FirstName = "";
        public string LastName = "";
        public string Email = "";
        public string PhoneNumber = "";
        public string Address = "";

        public Employee(string firstName, string lastName, string email, string phoneNumber, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Init employee list
            List<Employee> employees = LoadData();
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
            SaveData(employees);
        }

        static void SaveData(List<Employee> employees)
        {
            // Open file
            System.IO.StreamWriter file = File.CreateText(@"database.db");
            foreach (Employee employee in employees)
            {
                // Write the names
                file.WriteLine(employee.FirstName);
                file.WriteLine(employee.LastName);
                // Write the optional feilds if they are not blank
                if (0 < employee.Email.Count()) file.WriteLine("-email\n" + employee.Email);
                if (0 < employee.PhoneNumber.Count()) file.WriteLine("-phone\n" + employee.PhoneNumber);
                if (0 < employee.Address.Count()) file.WriteLine("-address\n" + employee.Address);
                // Write the person end symbol
                file.WriteLine("---");
            }
            // Close file
            file.Close();
        }

        static List<Employee> LoadData()
        {
            // Create a blank list if there is no file
            if (!File.Exists(@"database.db")) return new List<Employee>();
            // Otherwise open file
            List<Employee> list = new List<Employee>();
            System.IO.StreamReader file = File.OpenText(@"database.db");

            // Init vars
            string firstName = "";
            string lastName = "";
            string email = "";
            string phoneNumber = "";
            string address = "";
            string varName = "";
            uint lineOfPerson = 1;
            string line;
            // For each line in the file
            while((line = file.ReadLine()) != null)
            {
                // If person end symbol then create person object
                if (line == "---")
                {
                    list.Add(new Employee(firstName, lastName, email, phoneNumber, address));
                    lineOfPerson = 0;
                    firstName = "";
                    lastName = "";
                    email = "";
                    phoneNumber = "";
                    address = "";
                    varName = "";
                }
                // Read data
                if (lineOfPerson == 1) firstName = line;
                else if (lineOfPerson == 2) lastName = line;
                else if (lineOfPerson % 2 == 1) varName = line;
                else if (lineOfPerson % 2 == 0)
                {
                    if (varName == "-email") email = line;
                    if (varName == "-phone") phoneNumber = line;
                    if (varName == "-address") address = line;
                }
                lineOfPerson++;
            }

            // Close file
            file.Close();
            return list;
        }

        // Get a valid list index from the console.
        // If isPickLocation is set then the function will get an index for picking/replaceing/deleting an employee, else it will get one for inserting at the location.
        static int GetValidListIndex(List<Employee> employees, bool isPickLocation)
        {
            try
            {
                // Get the count from the console
                int count = employees.Count;
                // Get the length of the list
                int returnVal = int.Parse(Console.ReadLine());
                // If isPickLocation is set then adjust the length
                if (isPickLocation)
                {
                    if (count == 0)
                    {
                        Console.WriteLine("Error: employee list is empty.");
                        return -1;
                    }
                    count -= 1;
                }
                // Boundry check
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
            // If the user does not enter a valid number eg. "v67t57v7eb4v"
            catch (FormatException)
            {
                Console.WriteLine("Error: invalid number entered.");
                return -1;
            }
            // If the number is outside what can fit inside an int
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
            // Get optional stuff
            Console.Write("Enter email address (optional): ");
            string email = Console.ReadLine();
            Console.Write("Enter mobile phone number (optional): ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter home address (optional): ");
            string address = Console.ReadLine();
            // Check for empty required feilds
            if (firstName.Count() == 0)
            {
                System.Console.WriteLine("Error: first name may not be blank.");
                return;
            }
            if (lastName.Count() == 0)
            {
                System.Console.WriteLine("Error: last name may not be blank.");
                return;
            }
            // Add to list
            employees.Add(new Employee(firstName, lastName, email, phoneNumber, address));
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
            // Get optional stuff
            Console.Write("Enter email address (optional): ");
            string email = Console.ReadLine();
            Console.Write("Enter mobile phone number (optional): ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter home address (optional): ");
            string address = Console.ReadLine();
            // Check for empty required feilds
            if (firstName.Count() == 0)
            {
                System.Console.WriteLine("Error: first name may not be blank.");
                return;
            }
            if (lastName.Count() == 0)
            {
                System.Console.WriteLine("Error: last name may not be blank.");
                return;
            }
            // Insert
            employees.Insert(insertLocation, new Employee(firstName, lastName, email, phoneNumber, address));
        }

        static void Update(List<Employee> employees)
        {
            // Get location
            Console.Write("Enter the employee ID: ");
            int location = GetValidListIndex(employees, true);
            if (location == -1) return;
            // Get name
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            // Get optional stuff
            Console.Write("Enter email address (optional): ");
            string email = Console.ReadLine();
            Console.Write("Enter mobile phone number (optional): ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter home address (optional): ");
            string address = Console.ReadLine();
            // Update
            Employee employee = employees[location];
            if (0 < firstName.Count()) employee.FirstName = firstName;
            if (0 < lastName.Count()) employee.LastName = lastName;
            if (0 < email.Count()) employee.Email = email;
            if (0 < phoneNumber.Count()) employee.PhoneNumber = phoneNumber;
            if (0 < address.Count()) employee.Address = address;
            // Check for removal
            if (email == "remove") employee.Email = "";
            if (phoneNumber == "remove") employee.PhoneNumber = "";
            if (address == "remove") employee.Address = "";
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
                    Console.Write(x + ": " + employee.FirstName + " " + employee.LastName);
                    if (0 < employee.Email.Count()) Console.Write(", Email: " + employee.Email);
                    if (0 < employee.PhoneNumber.Count()) Console.Write(", Ph: " + employee.PhoneNumber);
                    if (0 < employee.Address.Count()) Console.Write(", Address: " + employee.Address);
                    Console.WriteLine(".");
                }
            }
        }
    }
}
