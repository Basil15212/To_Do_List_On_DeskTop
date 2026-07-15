using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace To_Do_List_Data_Layer
{
    public class clsPersonData
    {

        public static int AddNewPerson(string FirstName ,string LastName , string Email ,string Address ,string Phone ,DateTime DateOfBirth)
        {

            int NewID = -1;
            SqlConnection connection = new SqlConnection(DataSittings.Connection);
            string query = @"insert into Persons (FirstName ,LastName ,Email, Address ,Phone ,DateOfBirth )
                            values 
                            (@FirstName , @LastName ,@Email ,@Address ,@Phone ,@DateOfBirth);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand comannd  =new SqlCommand(query, connection);

            comannd.Parameters.AddWithValue("@FirstName", FirstName);
            comannd.Parameters.AddWithValue("@LastName", LastName);
            if(Email != "")
            {
                comannd.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                comannd.Parameters.AddWithValue("@Email", System.DBNull.Value);
            }

            if ( Address!= "")
            {
                comannd.Parameters.AddWithValue("@Address", Address);
            }
            else
            {
                comannd.Parameters.AddWithValue("@Address", System.DBNull.Value);
            }

            if (Phone != "")
            {
                comannd.Parameters.AddWithValue("@Phone", Phone);
            }
            else
            {
                comannd.Parameters.AddWithValue("@Phone", System.DBNull.Value);
            }

            if (DateOfBirth != null)
            {
                comannd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            }
            else
            {
                comannd.Parameters.AddWithValue("@DateOfBirth", System.DBNull.Value);
            }



            
         
            try
            {
                connection.Open();
                object result = comannd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    NewID = InsertedID;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }



            return NewID;
        }

    }
}
