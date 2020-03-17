using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace LINQ___primer
{

    class Program
    {
        /*
         * Student: Stefan Zivic S21
         * DZ01
         * Korisceni linkovi:
         *  https://www.youtube.com/watch?v=yQB4HGmuwY8
         *  https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-console?view=vs-2019
         *  
         */

        static void Main(string[] args)
        {
            DataStudentDataContext dsdc = new DataStudentDataContext();

            Random rdn = new Random();
            int rdn_id = rdn.Next(1, 100);

            int option = 0;

            Console.WriteLine("----------------");
            Console.WriteLine("1) Insert");
            Console.WriteLine("2) Read all");
            Console.WriteLine("3) Update");
            Console.WriteLine("4) Delete");
            Console.WriteLine("----------------");

            Console.WriteLine("Odaberite opciju: ");
            option = Convert.ToInt32(Console.ReadLine());

            if ( option == 1 || option == 2 || option == 3 || option == 4 || option == 5 )
            {
                switch (option)
                {
                    // CREATE
                    case 1:

                        Console.Write("Ime: ");
                        string FirstName = Console.ReadLine();
                        
                        Console.Write("Prezime: ");
                        string LastName = Console.ReadLine();

                        Console.Write("Email: ");
                        string Email = Console.ReadLine();

                        Console.Write("Broj indexa: ");
                        string BrojInexa = Console.ReadLine();

                        Student newStudent = new Student
                        {
                            id = rdn_id,
                            name = FirstName,
                            lname = LastName,
                            email = Email,
                            brIndexa = BrojInexa
                        };

                        dsdc.Students.InsertOnSubmit(newStudent);

                        try
                        {
                            dsdc.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            // TRY AGAIN
                            dsdc.SubmitChanges();
                        }
                        break;
                    // SELECT
                    case 2:

                        var stdName = from std in dsdc.Students
                                      select std.name;

                        foreach (string n in stdName)
                        {
                            Console.WriteLine("{0}", n);
                        }
                        break;
                    // UPDATE
                    case 3:
                        // Ima greske
                        /*
                         *  Primer: S21
                         */
                        Console.Write("Upisite broj indexa: ");
                        string brInx = Console.ReadLine();

                        Console.Write("Ime: ");
                        string ime = Console.ReadLine();

                        var query = from std in dsdc.Students
                                         where std.brIndexa == brInx
                                         select std;

                        foreach (Student std in query)
                        {
                            std.name = ime;
                        }

                        try
                        {
                            dsdc.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                    // DELETE
                    case 4:
                        // Ima greske
                        /*
                         *  Primer: S21
                         */
                        Console.Write("Upisite broj indexa: ");
                        string brindex = Console.ReadLine();

                        var deleteStudent = from std in dsdc.Students
                                     where std.brIndexa == brindex
                                     select std;

                        foreach (var student in deleteStudent)
                        {
                            dsdc.Students.DeleteOnSubmit(student);
                        }


                        try
                        {
                            dsdc.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            // Provide for exceptions.
                        }
                        break;
                }
                Console.WriteLine("Radi");
            } 
            else
            {
                Console.WriteLine("Izaberite broj od 1 do 5");
            }
        }

    }
}
