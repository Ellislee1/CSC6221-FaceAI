using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FacialAI.Azure
{
    class DataBase
    {
        SqlConnectionStringBuilder builder;
        public DataBase()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = ConfigurationManager.AppSettings.Get("DBS_SOURCE");
            builder.UserID = ConfigurationManager.AppSettings.Get("USERID");
            builder.Password = ConfigurationManager.AppSettings.Get("PASSWORD");
            builder.InitialCatalog = ConfigurationManager.AppSettings.Get("INITIAL");
        }

        public bool Insert(string query)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteReader();
                        success = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            return success;
        }

        public SqlDataReader read(string query)
        {
            SqlDataReader reader = null;
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        reader = command.ExecuteReader();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
                
            }
            return reader;

        }
    }
}
