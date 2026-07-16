using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace To_Do_List_Data_Layer
{
    public class clsPersonData
    {

        public static bool GetPersonInfoByID( int ID ,ref string FirstName ,ref string LastName,ref string Email ,
                                                ref string Address ,ref string Phone ,ref  DateTime DateOfBirth)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataSittings.Connection);

            string query = @" select * from Persons where PersonID =@PersonID";
            SqlCommand comand = new SqlCommand(query, connection);

            comand.Parameters.AddWithValue("@PersonID" , ID);

            try
            {
                connection.Open();
                SqlDataReader reader = comand.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "NoEmail";
                    }
                    if (reader["Address"] != DBNull.Value)
                    {
                        Address = (string)reader["Address"];
                    }
                    else
                    {
                        Email = "NoAddress";
                    }

                    if (reader["Phone"] != DBNull.Value)
                    {
                        Phone = (string)reader["Phone"];
                    }
                    else
                    {
                        Phone = "NoPhoneNumber";
                    }

                    if (reader["DateOfBirth"] != DBNull.Value)
                    {
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                    }
                    else
                    {
                        DateOfBirth = DateTime.Now;
                    }

                    reader.Close();

                }
                else
                {
                    isFound = false;
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



            return isFound;

        }

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

        public static bool UpdatePerson(int ID ,string FName ,string LName ,string Email ,string Address ,string Phone ,DateTime DateOFBirth)
        {
            int afectedRows = 0;

            SqlConnection connection = new SqlConnection(DataSittings.Connection);
            string query = @"Update Persons 
                            set FirstName =@FirstName ,
                                LastName = @LastName ,
                                Email = @Email ,
                                Phone =@Phone,
                                Address = @Address , 
                                DateOfBirth = @DateOfBirth
                                where PersonID = @PersonID;";

            SqlCommand comand = new SqlCommand(query, connection);

            comand.Parameters.AddWithValue("@PersonID", ID);
            comand.Parameters.AddWithValue("@FirstName", FName);
            comand.Parameters.AddWithValue("@LastNAme", LName);

            if(Email != null)
                comand.Parameters.AddWithValue("@Email", Email);
            else
                comand.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if(Phone!="")
                comand.Parameters.AddWithValue("@Phone", Phone);
            else
                comand.Parameters.AddWithValue("@Phone", System.DBNull.Value);

            if(DateOFBirth != null)
                comand.Parameters.AddWithValue("@DateOfBirth", DateOFBirth);
            else
                comand.Parameters.AddWithValue("@DateOfBirth", System.DBNull.Value);

            if(Address != null)
                comand.Parameters.AddWithValue("@Address", Address);
            else
                comand.Parameters.AddWithValue("@Address", System.DBNull.Value);


            try
            {
                connection.Open();
                afectedRows = comand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return (afectedRows > 0);


        }

        public static bool DeletePerson(int ID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(DataSittings.Connection);
            string query = @"delete from Persons where PersonID =@PersonID";

            SqlCommand cmd = new SqlCommand(query, connection); 

            cmd.Parameters.AddWithValue("@PersonID" , ID);

            try
            {
                connection.Open();
                affectedRows = cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return (affectedRows > 0);
        }

        public static DataTable GetAllPersons()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DataSittings.Connection);
            string query = "select * from Persons";

            SqlCommand cmd = new SqlCommand(@query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool IsExist(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataSittings.Connection);
            string query = @"select * from Persons where PersonID =@PersonID ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    isFound = true;
                }
                reader.Close();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}
