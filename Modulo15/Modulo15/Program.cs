using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Modulo15.Entities;


namespace Modulo15
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();

        }
            static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            try
            {
                List<Employee> employees = new List<Employee>();
                using (StreamReader sr = File.OpenText(path))
                {

                    while (!sr.EndOfStream)
                    {
                        string[] vect = sr.ReadLine().Split(',');
                        string name = vect[0];
                        string email = vect[1];
                        double salary = double.Parse(vect[2], CultureInfo.InvariantCulture);
                        employees.Add(new Employee(name, email, salary));
                    }
                }
                /*foreach (Employee emp in employees)
                {
                    Console.WriteLine(emp.Name + ", " + emp.Email + ", " + emp.Salary);
                }*/
                Console.Write("Enter salary: ");
                double minSalary = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);

                var emp_email = from employee in employees
                                where employee.Salary > minSalary
                                orderby employee.Email select employee.Email;
                Print("Email of people whose salary is more than " + minSalary + ": ", emp_email);

                var m_salary = employees.Where(emp => emp.Name[0] == 'M')
                    .Sum(emp => emp.Salary).ToString("F2", CultureInfo.InvariantCulture);

                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + m_salary);

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}