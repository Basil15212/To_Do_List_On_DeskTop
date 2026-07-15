using System;
using System.Collections.Generic;
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

            person1.FirstName = "Khaled";
            person1.LastName = "Faisal";
            person1.Address = "!2_Df3";
            person1.Phone = "012873663";
            person1.Email = "K99@Gmail.com";
            person1.DateOfBirth = DateTime.Now;

           if(person1.Save())
            {
                Console.WriteLine("Saved Successfuly");
            }
        }

        static void Main(string[] args)
        {
            testaddnew();
            Console.ReadKey();
        }
    }
}
