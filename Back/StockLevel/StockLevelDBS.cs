﻿using InventoryEyeBack.Posts;
using InventoryEyeBack.Stores;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryEyeBack.StockLevel
{
    public class StockLevelDBS
    {
        public StockLevelDBS() { }
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("MySqlConnectionString");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //-------------Insert StockLevel-------------//
        public int InsertStockLevelDBS(StockLevelModel stock)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("MySqlConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateInsertStockLevelWithStoredProcedure("SP_InEye_InsertStockLevel", con, stock); // create the command

            try
            {
                // 0 - failure, 1 - success
                cmd.ExecuteScalar(); // execute the command
                return 1;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private SqlCommand CreateInsertStockLevelWithStoredProcedure(String spName, SqlConnection con, StockLevelModel stock)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@stockDesc",stock.StockDesc);
          

            return cmd;
        }


        //-------------Update StockLevel-------------//
        public int UpdateStockLevelDBS(StockLevelModel stock)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("MySqlConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUpdateStockLevelWithStoredProcedure("SP_InEye_UpdateStockLevel", con, stock); // create the command

            try
            {
                // 0 - failure, 1 - success
                cmd.ExecuteScalar(); // execute the command
                return 1;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private SqlCommand CreateUpdateStockLevelWithStoredProcedure(String spName, SqlConnection con, StockLevelModel stock)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@id", stock.StockId);
            cmd.Parameters.AddWithValue("@stockDesc", stock.StockDesc);

            return cmd;
        }

        //-------------Read all StockLevel -------------//
        public List<StockLevelModel> ReadAllStockLevelDBS()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("MySqlConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // creat users list
            List<StockLevelModel> stockes = new List<StockLevelModel>();

            // create a Command with the connection to use, name of stored procedure and its parameters
            cmd = buildReadAllStockLevelProcedureCommand(con, "SP_InEye_ReadAllStockLevel");

            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                StockLevelModel s = new StockLevelModel();

                s.StockId = Convert.ToInt32(dataReader["Id"].ToString());
                s.StockDesc = dataReader["StockDesc"].ToString();

                stockes.Add(s);

            }

            if (con != null)
            {
                // close the db connection
                con.Close();
            }

            return stockes;
        }
        private SqlCommand buildReadAllStockLevelProcedureCommand(SqlConnection con, String spName)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;

        }

        //-------------Delete StockLevel -------------//

        public int DeleteStockLevelDBS(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("MySqlConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateDeleteStockLevelWithStoredProcedure("SP_InEye_DeleteStorckLevel", con, id); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private SqlCommand CreateDeleteStockLevelWithStoredProcedure(String spName, SqlConnection con, int id)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@id", id);

            return cmd;
        }
    }
}    

