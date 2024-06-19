using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1for_pptx
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Pet[] pets = new Pet[]
            //{
            //    new Pet{Name="Mahesh",Age=40 },
            //    new Pet{Name="Samir",Age=20}
            //};

            #region ALL Linq Method and SQL expresion
            //bool result = pets.All(pet => pet.Name.StartsWith("M"));

            //var result1 = from pet1 in pets
            //             where pet1.Name.IndexOf("m",StringComparison.OrdinalIgnoreCase)>=0
            //             select pet1;

            //foreach ( var pet in result1)
            //{
            //    Console.WriteLine($"Name: {pet.Name}, Age: {pet.Age}");
            //}


            //Console.WriteLine(result);

            List<Person> people = new List<Person>()
            {
                new Person{ Name="Samir",Pets=new Pet[] {new Pet {FullName="Carot",Age=20} } },
                new Person{Name="Mahesh",Pets=new Pet[]{new Pet{FullName="Monkey",Age =10} } },
                new Person{Name="Manoj",Pets=new Pet[] { new Pet  { FullName = "Cat", Age = 5 } } },
            };

            //IEnumerable<Person> result = people.ToList();

            var result = from person in people
                         where person.Pets.All(Pet => Pet.Age >= 5)
                         select new { PersonName = person.Name, PetName = person.Pets };

            foreach (var entry in result)
            {
                Console.WriteLine($"Person: {entry.PersonName}");
                foreach (var pet in entry.PetName)
                {
                    Console.WriteLine($"  Pet Name: {pet.FullName}, Age: {pet.Age}");
                }
            }

            string a = "a";
            string b = "b";
            Console.WriteLine(string.Concat(a, " ", b));
            Console.WriteLine(string.Format("{0} {1}", a, b));
            Console.WriteLine($"{a} {b}");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(a);
            stringBuilder.Append(" ");
            stringBuilder.Append(b);
            Console.WriteLine(stringBuilder);

            string[] strings = { a, b };
            Console.WriteLine(string.Join("", strings));

            List<int> list = new List<int>() { 1, 2, 3, 4 };
            Console.WriteLine(String.Join(", ", list));

            list.ForEach(x => Console.WriteLine(x));
            Console.WriteLine(string.Join(", ", list.Append(5)));
            #endregion
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            var processed = numbers.Where(x => x % 2 == 0).ToList();

            int[] arrays = { 1, 2, 3, 4, 5 };
            var data = arrays.Where(x => x % 2 == 0).OrderByDescending(x => x);
            foreach (var x in data) Console.WriteLine(x);

            List<int> First = new List<int>() { 1, 2, 4 };
            List<int> Last = new List<int>() { 5, 3, 6 };

            var sequence = First.Concat(Last).Where(x => x % 2 == 0).OrderBy(x => x);
            var count = sequence.Count();
            Console.WriteLine("Count");
            Console.WriteLine(count);


            var numbers1 = new List<int>() { 1, 2, 3, 4, 5, 2, 5 };
            var defaultIfEmpty = numbers1.DefaultIfEmpty(0);

            Console.WriteLine("defaultIfEmpty");
            // Print each element in the sequence
            foreach (var number in defaultIfEmpty)
            {
                Console.WriteLine(number);
            }

            var distinct = numbers1.Distinct();
            Console.WriteLine(string.Join(",", distinct));

            HashSet<int> hashset = new HashSet<int>();
            foreach (var number in numbers1)
            {
                hashset.Add(number);
            }

            Console.WriteLine("Hashset Values");
            Console.WriteLine(string.Join(",", hashset));

            var data1 = new List<string> { "apple", "banana", "cherry" };

            IEnumerable<string> result1 = GetFruitsStartingWith(data1, 'x');
            foreach (var fruit in result)
            {
                Console.WriteLine(fruit);
            }

            List<Employee> employees = new List<Employee>()
            {
                new Employee {Id=1,Name="Mahesh",DepartmentId=1 , Salary = 50000},
                new Employee {Id=2,Name="Samir",DepartmentId=2 , Salary = 55000},
                new Employee {Id=3,Name="Sujit" ,DepartmentId=3 , Salary = 60000},
                new Employee {Id=4 ,Name="Rahul" ,DepartmentId =1 , Salary = 70000},
                new Employee {Id=5,Name="Amir" ,DepartmentId=1 , Salary = 80000},
                new Employee {Id=6, Name="Sandeep", DepartmentId=2 , Salary = 90000}
               

            };
            List<Department> departments = new List<Department>()
            {
                new Department{Id=1 ,Name="HR"},
                new Department {Id=2 ,Name="IT"},
                new Department {Id=3 ,Name ="Finace"}
            };
            #region Group by
            var linqdepat = employees.GroupBy(e => departments.First(x => x.Id == e.DepartmentId).Name)
                           .OrderBy(e => e.Key)
                            .Select(q => new {
                                departmentname = q.Key,
                                TotalSalary = q.Sum(e => e.Salary),
                                employees = q.OrderBy(x => x.Name).ToList(),
                            });

            foreach (var group in linqdepat)
            {
                // var departmentname= departments.First(x=>x.Id==group.Key).Name;
                // Console.WriteLine($"Department Name : {group.departmentname}");
                Console.WriteLine($"Department: {group.departmentname}, Total Salary: {group.TotalSalary}");
                foreach (var employee in group.employees)
                {
                    Console.WriteLine($"Epmloyee Name : {employee.Name} ");
                }
                Console.WriteLine("----------------------------------");
            }

            var sqllinq = from employee in employees
                          join department in departments
                          on employee.DepartmentId equals department.Id
                          group employee by department.Name into egroup
                          orderby egroup.Key
                          select new
                          {
                              departname = egroup.Key,
                              TotaSalary = egroup.Sum(e => e.Salary),
                              employees = egroup.OrderBy(x => x.Name).ToList()
                          };

            foreach (var employee in sqllinq)
            {
                Console.WriteLine($"Department: {employee.departname}, Total Salary: {employee.TotaSalary}");
                Console.WriteLine("........................");
                foreach (var result11 in employee.employees)
                {
                    Console.WriteLine($"Employee Name : {result11.Name}");
                }
                Console.WriteLine("----------------------------------");
            }

            #endregion  Group by

            #region Group Join
            var query = departments.GroupJoin(employees,
                        dept => dept.Id,
                        emp => emp.DepartmentId,
                        (depts, emps) => new
                        {
                            departmet = depts.Name,
                            employees = emps.OrderBy(x => x.Name).ToList()
                        }
                        );
                     
            foreach (var dept in query)
            {
                Console.WriteLine($"dept {dept.departmet}");

                foreach(var result12 in dept.employees) 
                    Console.WriteLine($"Employee :{result12.Name}");

                
            }

            var querySql = from depts in departments
                           join emps in employees
                           on depts.Id equals emps.DepartmentId into empGroup
                           select new
                           {
                               department= depts.Name,
                               employees= empGroup
                           };

            Console.WriteLine("By Sql Like Group Join");
            foreach (var dept in querySql) {
                Console.WriteLine($"Depart : {dept.department}");
                foreach (var emps in dept.employees)
                { 
                    Console.WriteLine($"Employee : {emps.Name}");
                }
            }
            #endregion Group Join

            #region Linq Operator
            //Range Operator
            IEnumerable<int> num = Enumerable.Range(1,5).Where(x => x % 2 == 0);

            foreach(var number in num)
            {
                Console.WriteLine($"{number}");
            }

            //Repeat Operator 

            IEnumerable<string> value = Enumerable.Repeat("Mahesh", 2);
            foreach(var str in value) Console.WriteLine(str);

            // Empty  operator
            IEnumerable<int> emptyIntSequence = Enumerable.Empty<int>();
            Console.WriteLine("Count of emptyIntSequence: " + emptyIntSequence.Count());

            object str1 = "MAHESH";
            object str2 = new string("MAHESH".ToCharArray());
            Console.WriteLine(str1 == str2);
            Console.WriteLine(str1.Equals(str2));

            int a1 = 5;
            int b1 = 5;
            Console.WriteLine(a1 == b1);
            Console.WriteLine(a1.Equals(b1));

            //Intersect
         List<Student> students1 = new List<Student>
        {
            new Student { Name = "Alice", Age = 30 },
            new Student { Name = "Bob", Age = 25 },
            new Student { Name = "Charlie", Age = 35 }
        };
            List<Student> students2 = new List<Student>
        {
            new Student { Name = "Bob", Age = 25 },
            new Student { Name = "David", Age = 40 },
            new Student { Name = "Charlie", Age = 35 }
        };
            //for complex type using projection operator 
            var intersectresult=students1.Select(s=> new { s.Name, s.Age }).Intersect(students2.Select(s1 => new { s1.Name, s1.Age }));
            foreach(var s in intersectresult)
            {
                Console.WriteLine($"Name {s.Name} Age : {s.Age}");
            }

            //last method with complex object with projection

            var lastresult= people.Select(x=> new {x.Name }).Last();
            Console.WriteLine($"Last {lastresult.Name}");

            List<int> numberss = new List<int> { 1, 2, 3 };
            

            var skipwhileresult = numberss.SkipWhile(x=>x>1);
            var sumresult = numberss.Sum();
            Console.WriteLine($"Sum :{sumresult}");
            var takeresult = numberss.Take(2);
            foreach (var s in takeresult) Console.WriteLine($"Take Result :{s}");
            foreach (var s in skipwhileresult) { Console.WriteLine(s); }

            // Create a dictionary with Name as key and Age as value
            Dictionary<string, int> ageDictionary = students1.ToDictionary(person => person.Name, person => person.Age);

            // Display the dictionary
            Console.WriteLine("Age Dictionary:");
            foreach (var kvp in ageDictionary)
            {
                Console.WriteLine($"Name: {kvp.Key}, Age: {kvp.Value}");
            }

            //Lookup 

            List<Student> student = new List<Student>
        {
            new Student { Name = "Alice", Age = 30 },
            new Student { Name = "Bob", Age = 25 },
            new Student { Name = "Alice", Age = 35 },
            new Student { Name = "Charlie", Age = 30 }
        };
            ILookup<string,Student> students= student.ToLookup(s => s.Name);
            Console.WriteLine("People Lookup:");
            foreach (var group in students)
            {
                Console.WriteLine($"Name: {group.Key}");
                foreach (var person in group)
                {
                    Console.WriteLine($"- {person.Name}, Age: {person.Age}");
                }
            }

            List<int> numbers12 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Create a lookup grouping odd and even numbers
            ILookup<bool, int> numberLookup = numbers12.ToLookup(num1 => num1 % 2 == 0);

            // Display the groups
            Console.WriteLine("Numbers Lookup:");
            foreach (var group in numberLookup)
            {
                Console.WriteLine(group.Key ? "Even Numbers:" : "Odd Numbers:");
                foreach (var number in group)
                {
                    Console.WriteLine(number);
                }
            }

            List<int> numbersss = new List<int> { 1, 2, 3, 4, 5 };
            List<string> words = new List<string> { "one", "two", "three", "four", "five" };
            //Zip(words, (number, word) => $"{number} - {word}");
            var zippedResult= numbersss.Zip(words,(number,word)=>$"{number} - {word}");
            Console.WriteLine("Zipped sequences:");
            foreach (var item in zippedResult)
            {
                Console.WriteLine(item);
            }
            #endregion Linq Operator
            #region Group Join Practices
            //using linq method
            var groupjoinlinq = departments.GroupJoin(employees,
                               dept => dept.Id,
                               emp => emp.DepartmentId,
                               (depts, emps) => new
                               {
                                   deptname = depts.Name,
                                   Employee = emps.Select(e=>e.Name)
                               }); ;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Group Join Practices");
            foreach (var item in groupjoinlinq)
            {
                Console.WriteLine($"Departmentname : {item.deptname}");
                foreach (var emp in item.Employee)
                {
                    Console.WriteLine($"Employee : {emp}");
                }
                Console.WriteLine();
                Console.WriteLine();

            }

            //group join by sql query

            var groupjoinsqllike = from dept in departments
                                   join emp in employees
                                   on dept.Id equals emp.DepartmentId into egroup
                                   select new
                                   {
                                       deptname=dept.Name,
                                       Employee=egroup.Select(e=>e.Name)
                                   };

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Group Join by sql like");
            foreach(var item in groupjoinsqllike)
            {
                Console.WriteLine($"Depart : {item.deptname}");

                foreach(var emp in item.Employee)
                {
                    Console.WriteLine($"Employee : {emp}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("Inner join with method query and sql query");

            var joinmethodquery = departments.Join(employees,
                                 dept => dept.Id,
                                 emp => emp.DepartmentId,
                                 (depts, emps) => new
                                 {
                                     deptname = depts.Name,
                                     empsname = emps.Name
                                 });


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Join Practices");
            foreach (var resulta in joinmethodquery)
            {
                Console.WriteLine($"Department: {resulta.deptname}, Employee: {resulta.empsname}");
            }

            #region Cross Join 

            Console.WriteLine("Cross  Join Practices");
            var crossjoinmethod = departments.SelectMany(d => employees, (e, d) => new
            {
                deptname=d.Name,
                empsname = e.Name
            });
            foreach (var resulta in crossjoinmethod)
            {
                Console.WriteLine($"Department: {resulta.deptname}, Employee: {resulta.empsname}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Cross join with method using join");

            var crossjoinwithjoin = departments.Join(employees, d => true, e => true, (e1, d1) => new
            {
                deptname = d1.Name,
                empsname = e1.Name
            });
            foreach (var resulta in crossjoinmethod)
            {
                Console.WriteLine($"Department: {resulta.deptname}, Employee: {resulta.empsname}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Cross join with query systax");

            var crossjoinquery = from d in departments
                                 from e in employees
                                 select new
                                 {
                                     deptname = d.Name,
                                     empsname = e.Name
                                 };

            foreach (var resulta in crossjoinmethod)
            {
                Console.WriteLine($"Department: {resulta.deptname}, Employee: {resulta.empsname}");
            }
            #endregion Cross Join 

            #region Left outer join 
            var leftOuterJoinQuery = from dept in departments
                                     join emp in employees on dept.Id equals emp.DepartmentId into empGroup
                                     from emp in empGroup.DefaultIfEmpty()
                                     select new
                                     {
                                         DepartmentName = dept.Name,
                                         EmployeeName = emp?.Name
                                     };
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Left outer join ");
            foreach (var resultt in leftOuterJoinQuery)
            {
                Console.WriteLine($"Department: {resultt.DepartmentName}, Employee: {resultt.EmployeeName ?? "No Employee"}");
            }
            #endregion

            //calculate salary using group join 

            //var calsumgroupjoin = departments.GroupJoin(employees,
            //                     d => d.Id,
            //                     e => e.DepartmentId,
            //                     (de, emp) =>new
            //                     {
            //                         deptname=de.Name,
            //                         totalsalary=emp.Sum(s=>s.Salary),
            //                     });

            var calsumgroupjoin = from d in departments
                                  join e in employees
                                  on d.Id equals e.DepartmentId into empGroup
                                  select new
                                  {
                                      deptname=d.Name,
                                      totalsalary=empGroup.Max(e=>e.Salary)
                                  };

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Depat wise salary sum");

            foreach(var resultt in calsumgroupjoin) {
                Console.WriteLine($"Depart name : {resultt.deptname} Salary :{resultt.totalsalary}");
                    }

            //max salary of employee in list

            var maxsalary =(from d in departments
                           join e in employees
                           on d.Id equals e.DepartmentId
                           orderby e.Salary descending
                           select new
                           {
                               dname=d.Name,
                               ename=e.Name,
                               salary=e.Salary
                           }).FirstOrDefault();

           Console.WriteLine($"Ename {maxsalary.ename} ,Dename {maxsalary.dname},salary {maxsalary.salary}");
            #endregion Group Join Practices
            Console.ReadLine();
        }
        static IEnumerable<string> GetFruitsStartingWith(IEnumerable<string> fruits, char startLetter)
        {
            if (fruits == null || !fruits.Any())
            {
                return Enumerable.Empty<string>();
            }

            return fruits.Where(f => f.StartsWith(startLetter.ToString(), StringComparison.OrdinalIgnoreCase));
        }
        class Pet
        {
            public string FullName { get; set; }
            public int Age { get; set; }
        }

        class Person
        {
            public string Name { get; set; }
            public Pet[] Pets { get; set; }
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DepartmentId { get; set; }
            public decimal Salary { get; set; }
        }

        public class Department
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
