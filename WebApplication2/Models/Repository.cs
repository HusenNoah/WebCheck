using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.ViewModels;

namespace WebApplication2.Models
{
    public class Repository
    {
        public string dbconn = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        internal void CreatePerson(Customers cus)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert Into Customers Values (@cn,@cp,@ca,getdate())";
                cmd.Parameters.AddWithValue("@cn", cus.CustomerName);
                cmd.Parameters.AddWithValue("@cp", cus.CustomerPhone);
                cmd.Parameters.AddWithValue("@ca", cus.CustomerAdres);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal void CreateTimeTable(TimeTable time)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO Timetable VALUES 
                            (@name,@sat,@sun,@mon,@tue,@wed,@thu,Fri,getdate())" ;
                cmd.Parameters.AddWithValue("@name", time.Name);
                cmd.Parameters.AddWithValue("@sat", time.Saturday );
                cmd.Parameters.AddWithValue("@sun", time.Sunday);
                cmd.Parameters.AddWithValue("@mon", time.Monday);
                cmd.Parameters.AddWithValue("@tue", time.Tuesday);
                cmd.Parameters.AddWithValue("@wed", time.Wednesday);
                cmd.Parameters.AddWithValue("@thu", time.Thursday);
                cmd.Parameters.AddWithValue("@fri", time.Friday);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal List<TimeTable> ListTimeTable()
        {
               using (var conn = new SqlConnection(dbconn))
            using (var cmd= conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Timetable";
                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<TimeTable>();
                while (reader.Read())
                {
                    TimeTable time = new TimeTable();
                    time.Id = (int)reader["Id"];
                    time.Name = reader["Name"] as string;
                    if (reader["Tuesday"] != DBNull.Value)
                    {
                        time.Saturday = (bool)reader["Saturday"];

                        time.Sunday = (bool)reader["Sunday"];
                        time.Monday = (bool)reader["Monday"];
                        time.Tuesday = (bool)reader["Tuesday"];
                        time.Wednesday = (bool)reader["Wednesday"];
                        time.Thursday = (bool)reader["Thursday"];
                        time.Friday = (bool)reader["Friday"];
                    }  
                    list.Add(time);

                }
                return list;
            }
        }

        internal Login Authenticate(string username, string password,bool? Active=null)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"SELECT *FROM Users WHERE Username=@username AND Password=@password And Active=@active" ;
                command.Parameters.AddWithValue("@active", Active = true);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                conn.Open();
                var reader = command.ExecuteReader();
                var login = (Login)null;
                if (reader.Read())
                {
                    login = new Login();
                    login.Username = reader["Username"] as string;
                    login.Password = reader["Password"] as string;
                    
                    if (reader["CurrentPermision"] != DBNull.Value)
                    {
                        login.CurrentPermissions = (Login.Permissions)reader["CurrentPermision"];
                    }
                    


                }
                return login;
            }
        }

        internal Customers GetCustomerExist(string customerName)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select CustomerName From Customers Where CustomerName=@Customer";
                cmd.Parameters.AddWithValue("@Customer", customerName);
                conn.Open();
                var reader = cmd.ExecuteReader();
                var cus = (Customers)null;
                if (reader.Read())
                {
                    cus = new Customers();
                    cus.CustomerName = reader["CustomerName"] as string;

                }
                return cus;
            }
        }

        internal List<Customers> ListPersons()
        {
            using (var conn = new SqlConnection(dbconn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from CustomerS ";
                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<Customers>();
                while (reader.Read())
                {
                    Customers cus = new Customers();
                    cus.Id = (int)reader["Id"];
                    cus.CustomerName = reader["CustomerName"] as string;
                    cus.CustomerAdres = reader["cUSTOMERaddres"] as string;
                    cus.CustomerPhone = reader["CustomerPhone"] as string;
                    cus.Date = (DateTime)reader["Date"];
                    list.Add(cus);

                }
                return list;

            }
        }

        internal void UpdatePersons(Customers cus)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"UPDATE Customers SET CustomerName=@cn,CustomerAddres=@ca,CustomerPhone=@cp WHERE Id=@id";
                command.Parameters.AddWithValue("@cn", cus.CustomerName);
                command.Parameters.AddWithValue("@ca", cus.CustomerAdres);
                command.Parameters.AddWithValue("@cp", cus.CustomerPhone);
                command.Parameters.AddWithValue("@id", cus.Id);

                conn.Open();
                command.ExecuteNonQuery();

            }
        }

        internal void DeletePerson(Customers cus)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"DELETE FROM Customers WHERE Id=@id";
                command.Parameters.AddWithValue("@id", cus.Id);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        internal Customers GetUpdate(string id)
        {
            using (var conn = new SqlConnection(dbconn))
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"Select *from CustomerS Where Id=@id";
                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                var reader = command.ExecuteReader();
                var cus = (Customers)null;
                if (reader.Read())
                {
                    cus = new Customers();
                    cus.Id = (int)reader["Id"];
                    cus.CustomerName = reader["CustomerName"] as string;
                    cus.CustomerAdres = reader["CustomerAddres"] as string;
                    cus.CustomerPhone = reader["CustomerPhone"] as string;


                }
                return cus;

            }
        }
    }
}