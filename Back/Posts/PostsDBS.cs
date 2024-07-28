﻿using InventoryEyeBack.Posts;
using InventoryEyeBack.Users;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryEyeBack.Posts
{
    public class PostsDBS
    {

        public PostsDBS() { }
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

        //-------------Insert Post-------------//
        public int InsertPostDBS(PostsModel post)
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

            cmd = CreateInsertPostWithStoredProcedure("SP_InEye_InsertPost", con, post); // create the command

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

        private SqlCommand CreateInsertPostWithStoredProcedure(String spName, SqlConnection con, PostsModel post)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@userId", post.UserId);
            cmd.Parameters.AddWithValue("@createAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@editedAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@name", post.ProductName);
            cmd.Parameters.AddWithValue("@content", post.Content);
            cmd.Parameters.AddWithValue("@image", post.Image);
            cmd.Parameters.AddWithValue("@tags", post.Tags);
            cmd.Parameters.AddWithValue("@category", post.Category);
            cmd.Parameters.AddWithValue("@pickUpFromUser", post.PickUpFromUser);
            cmd.Parameters.AddWithValue("@pickUpLat", post.PickUpLat);
            cmd.Parameters.AddWithValue("@picUpLng", post.PicUpLng);
            cmd.Parameters.AddWithValue("@pickUpAddress", post.PickUpAddress);
            return cmd;
        }

        //-------------Update Post-------------//
        public int UpdatePostDBS(PostsModel post)
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

            cmd = CreateUpdatetPostWithStoredProcedure("SP_InEye_UpdatePost", con, post); // create the command

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
        private SqlCommand CreateUpdatetPostWithStoredProcedure(String spName, SqlConnection con, PostsModel post)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@userId", post.UserId);
            cmd.Parameters.AddWithValue("@createAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@editedAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@name", post.ProductName);
            cmd.Parameters.AddWithValue("@content", post.Content);
            cmd.Parameters.AddWithValue("@image", post.Image);
            cmd.Parameters.AddWithValue("@tags", post.Tags);
            cmd.Parameters.AddWithValue("@category", post.Category);
            cmd.Parameters.AddWithValue("@pickUpFromUser", post.PickUpFromUser);
            cmd.Parameters.AddWithValue("@pickUpLat", post.PickUpLat);
            cmd.Parameters.AddWithValue("@picUpLng", post.PicUpLng);
            cmd.Parameters.AddWithValue("@pickUpAddress", post.PickUpAddress);

            return cmd;
        }

        //-------------Delete Post -------------//
        public int DeletePostDBS(int postId)
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

            cmd = CreateDeletePostWithStoredProcedure("SP_InEye_DeletePost", con, postId); // create the command

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
        private SqlCommand CreateDeletePostWithStoredProcedure(String spName, SqlConnection con, int postId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@id", postId);

            return cmd;
        }

        //-------------Read all Posts -------------//
        public List<PostsModel> ReadAllPostsDBS()
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
            List<PostsModel> posts = new List<PostsModel>();

            // create a Command with the connection to use, name of stored procedure and its parameters
            cmd = buildReadAllPostsStoredProcedureCommand(con, "SP_InEye_ReadAllPosts");

            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                PostsModel p = new PostsModel();

                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());

                posts.Add(p);


            }

            if (con != null)
            {
                // close the db connection
                con.Close();
            }

            return posts;
        }
        private SqlCommand buildReadAllPostsStoredProcedureCommand(SqlConnection con, String spName)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;

        }

        //-------------Read Post by post id -------------//
        public PostsModel ReadPostByPostIdDBS(int id)
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


            PostsModel p = new PostsModel();

            // create a Command with the connection to use, name of stored procedure and its parameters
            cmd = buildReadPostByIdStoredProcedureCommand(con, "SP_InEye_ReadPostsByPostId", id);

            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {


                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());


            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }

            return p;
        }
        private SqlCommand buildReadPostByIdStoredProcedureCommand(SqlConnection con, String spName, int id)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }

        //-------------Read Post by category -------------//
        public List<PostsModel> ReadPostByCategoryDBS(int category)
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
            List<PostsModel> posts = new List<PostsModel>();

            // create a Command with the connection to use, name of stored procedure and its parameters
            cmd = buildReadPostByCategoryStoredProcedureCommand(con, "SP_InEye_ReadPostsByCategory", category);

            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                PostsModel p = new PostsModel();

                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());

                posts.Add(p);


            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }

            return posts;
        }
        private SqlCommand buildReadPostByCategoryStoredProcedureCommand(SqlConnection con, String spName, int category)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@categoryId", category);
            return cmd;
        }

        //-------------Read Post by userId -------------//
        public List<PostsModel> ReadPostByUserIdDBS(int userId)
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
            List<PostsModel> posts = new List<PostsModel>();

            // create a Command with the connection to use, name of stored procedure and its parameters
            cmd = buildReadPostByUserIdStoredProcedureCommand(con, "SP_InEye_ReadPostsByUserId", userId);

            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                PostsModel p = new PostsModel();

                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());

                posts.Add(p);


            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }

            return posts;
        }
        private SqlCommand buildReadPostByUserIdStoredProcedureCommand(SqlConnection con, String spName, int userId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@userId", userId);
            return cmd;
        }

        //-------------Search Posts by FullName -------------//
        public List<PostsModel> SearchPostsByFullNameDBS(string fullName)

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
            List<PostsModel> posts = new List<PostsModel>();
            // create a Command with the connection to use, name of stored procedure and its parameters

            cmd = buildSearchPostByFullNameStoredProcedureCommand(con, "SP_InEye_SearchPostByFullName", fullName);

            // call the stored procedure (using the cmd) and get results to DataReader

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record

            while (dataReader.Read())

            {

                PostsModel p = new PostsModel();
                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());
                posts.Add(p);
            }

            if (con != null)

            {
                // close the db connection

                con.Close();
            }
            return posts;
        }
        private SqlCommand buildSearchPostByFullNameStoredProcedureCommand(SqlConnection con, string spName, string fullName)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = spName; // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
            cmd.Parameters.AddWithValue("@fullName", fullName);
            return cmd;
        }

        //-------------Search Posts by Content -------------//

        public List<PostsModel> SearchPostsByContentDBS(string content)
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
            List<PostsModel> posts = new List<PostsModel>();
            // create a Command with the connection to use, name of stored procedure and its parameters

            cmd = buildSearchPostByContentStoredProcedureCommand(con, "SP_InEye_SearchPostByContent", content);
            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                PostsModel p = new PostsModel();
                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());
                posts.Add(p);
            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            return posts;
        }
        private SqlCommand buildSearchPostByContentStoredProcedureCommand(SqlConnection con, string spName, string content)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = spName; // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
            cmd.Parameters.AddWithValue("@content", content);
            return cmd;

        }


        //-------------Search Posts by Tags -------------//
        public List<PostsModel> SearchPostsByTagsDBS(string tags)
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
            List<PostsModel> posts = new List<PostsModel>();
            // create a Command with the connection to use, name of stored procedure and its parameters

            cmd = buildSearchPostByTagsStoredProcedureCommand(con, "SP_InEye_SearchPostByTags", tags);
            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                PostsModel p = new PostsModel();
                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());
                posts.Add(p);
            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            return posts;
        }

        private SqlCommand buildSearchPostByTagsStoredProcedureCommand(SqlConnection con, string spName, string tags)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = spName; // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
            cmd.Parameters.AddWithValue("@tags", tags);
            return cmd;
        }


        //-------------Search Posts by Product Name -------------//
        public List<PostsModel> SearchPostsByProductNameDBS(string name)
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
            List<PostsModel> posts = new List<PostsModel>();
            // create a Command with the connection to use, name of stored procedure and its parameters

            cmd = buildSearchPostByProductNameStoredProcedureCommand(con, "SP_InEye_SearchPostByProductName", name);
            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {
                PostsModel p = new PostsModel();
                p.PostId = Convert.ToInt32(dataReader["Id"].ToString());
                p.UserId = Convert.ToInt32(dataReader["UserId"].ToString());
                p.UserName = dataReader["FullName"].ToString();
                p.UserImage = dataReader["Expr1"].ToString();
                p.Image = dataReader["Image"].ToString();
                p.CreateAt = Convert.ToDateTime(dataReader["CreateAt"].ToString());
                p.EditedAt = Convert.ToDateTime(dataReader["EditedAt"].ToString());
                p.ProductName = dataReader["Name"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.Tags = dataReader["Tags"].ToString();
                p.Category = Convert.ToInt32(dataReader["Category"].ToString());
                p.CategoryDesc = dataReader["CategoryDesc"].ToString();
                p.PickUpAddress = dataReader["PickUpAddress"].ToString();
                p.Score = Convert.ToInt32(dataReader["Score"].ToString());
                posts.Add(p);
            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            return posts;
        }

        private SqlCommand buildSearchPostByProductNameStoredProcedureCommand(SqlConnection con, string spName, string name)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = spName; // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
            cmd.Parameters.AddWithValue("@name", name);
            return cmd;
        }
    }
}