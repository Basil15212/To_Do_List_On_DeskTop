using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_Data_Layer
{
    public class clsUserData
    {

        public static bool GetUserInfoByID(int UserID, ref int PersonID, ref string UserName, ref string UserPassword, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(DataSittings.Connection);
            string Query = @"Select * from Users where UserID =@UserID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["AccUserName"];
                    UserPassword = (string)reader["PasswordHash"];
                    IsActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); IsFound = false; }
            finally { conn.Close(); }
            return IsFound;
        }

        public static int AddNewUser(int personID, string UserName, string UserPassword ,bool isactive)
        {
            int UserID = -1;
            SqlConnection con = new SqlConnection(DataSittings.Connection);
            string query = @"INSERT INTO Users (PersonID, AccUserName, PasswordHash, IsActive)
                     VALUES (@PersonID, @UserName, @Password, @IsActive);
                     SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID" , personID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", UserPassword);
            cmd.Parameters.AddWithValue("@IsActive", isactive);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString() ,out int InsertedID))
                {
                    UserID = InsertedID;
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message);} finally { con.Close(); }
            return UserID;
        }

        public static bool UpdateUser(int PersonId ,int UserID ,string UserName ,string Password ,bool IsActive)
        {
            int AffectedRows = 0;
            SqlConnection con = new SqlConnection(DataSittings.Connection);
            string query = @"Update Users 
                            set PersonID =@PersonID ,
                                UserID = @UserID ,
                                 AccUserName= @AccUserName ,
                                PasswordHash =@PasswordHash,
                                IsActive = @IsActive 
                                
                                where PersonID = @PersonID;";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonId);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@AccUserName", UserName);
            cmd.Parameters.AddWithValue("@PasswordHash", Password);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }

            return (AffectedRows > 0);
        }

        public static bool DeleteUserWithID(int UserId)
        {
            int AffectedRows = 0;
            SqlConnection con = new SqlConnection( DataSittings.Connection);
            string query = @"delete from Users where UserId =@UserID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId" , UserId);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }

            return (AffectedRows > 0);
        }

        public static DataTable GetAllUsers()
        {
            DataTable dtUsers = new DataTable();
            SqlConnection con = new SqlConnection(DataSittings.Connection);
            string query = @"Select Users.UserID,Persons.PersonID ,Persons.FirstName , Persons.LastName, Persons.DateOfBirth,
                                Persons.Address ,USers.AccUserName 
                                from Users inner join Persons on Persons.PersonID = USers.UserID;";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dtUsers.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return dtUsers;
        }

        public static bool IsUserExist(int UserID)
        {
            bool isfound = false;
            SqlConnection con = new SqlConnection(DataSittings.Connection);
            string query = "select * from Users where UserID = @UserID";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId" , UserID);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    isfound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isfound= false;
                Console.WriteLine(ex.Message);
            }
            finally{ con.Close(); }
            return isfound;
        }


    }
}
