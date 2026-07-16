using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Business_Layer;

namespace traingLogicConsoleApp
{
    internal class Program
    {

        public static void testaddnew()
        {
            clsPerson person1 = new clsPerson();

            person1.FirstName = "Mohammed";
            person1.LastName = "Saleh";
            person1.Address = "we##Df3";
            person1.Phone = "0120000663";
            person1.Email = "Moh@Gmail.com";
            person1.DateOfBirth = DateTime.Now;

           if(person1.Save())
            {
                Console.WriteLine("Saved Successfuly");
            }
        }

        public static void testFind(int ID)
        {
            clsPerson Person1 = clsPerson.Find(ID);

            if(Person1 != null)
            {
                Console.WriteLine(Person1.FirstName);
                Console.WriteLine(Person1.LastName);
            }
            else
            {
                Console.WriteLine("Coudnt Find a Person with ID [" + ID+"] .");
            }
        }

        public static void testUpdatePersonByID(int ID)
        {
            clsPerson person = clsPerson.Find(ID);
            if(person != null)
            {
                person.FirstName = "Ahmed";
                person.LastName = "Omer";
                person.Email = "Ahmed@Gmail.com";
                if(person.Save())
                {
                    Console.WriteLine("Updated Successfuly");
                }
            }
            else
            {
                Console.WriteLine("Updating Faild");
            }
        }


        public static void testDeletePersonByID(int id)
        {
            if(clsPerson.DeletePersonByID(id))
            {
                Console.WriteLine("Deleted Successfully");
            }else
            {
                Console.WriteLine("Deleting Faild");
            }
        }

        public static void testGetAll()
        {
            DataTable dataTable =new DataTable();
            dataTable = clsPerson.GetAllPersons();

            foreach(DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["PersonID"]} ,     { row["FirstName"]} ,       { row["LastName"]}");
            }
        }
        static void Main(string[] args)
        {
           


            Console.ReadKey();
        }
    }
}
