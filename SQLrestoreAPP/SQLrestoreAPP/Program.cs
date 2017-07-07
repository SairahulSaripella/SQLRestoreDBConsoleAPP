using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLrestoreAPP
{
    /*
     *      REQUIREMENTS FOR RUNNING: The input must include a valid sql server address and the restore file must be located on disk
     * 
     * */


    class Program
    {
        static void Main(string[] args)
        {
            // Gather all the data from user to run the queries

            System.Console.WriteLine("Console SQL Server Restore App \n");
            System.Console.WriteLine("========================================== \n \n ");

            System.Console.WriteLine("Enter the server name you want to connect to. \n");
            string server = Console.ReadLine();


            System.Console.WriteLine("Enter the servers username for connection. \n");
            string user = Console.ReadLine();


            System.Console.WriteLine("Now enter password to connect to server. \n");
            string pass = Console.ReadLine();

            System.Console.WriteLine("Now enter the database name you wish to restore. \n");
            string db = Console.ReadLine();



            System.Console.WriteLine("Lastly, enter the restore file that is on the server to restore the database. Enter the path and filename \n");
            string file = Console.ReadLine();


            runRestore(server, user, pass, file, db);


        }


        public static void runRestore(string server, string username, string password, string filename, string database)
        {

            string SQLcommand = $"Server={server}; Database=master; User ID={username}; Password={password};";

            try
            {



                System.Console.WriteLine(" \n NOW IN RUN RESTORE FUNCTION. NEW CONNECTION STARTING ");

                // establish connection with server
                SqlConnection connR = new SqlConnection(SQLcommand);
                connR.Open();


                System.Console.WriteLine(" \n CONNECTED TO MASTER ");

                // Kicks out all users in the database to allow for a restore to be run
                string BATCH = $"ALTER DATABASE {database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE {database} FROM DISK = '{filename}' WITH REPLACE; ALTER DATABASE {database} SET MULTI_USER;";

                SqlCommand cmd3 = new SqlCommand(BATCH, connR);

                cmd3.ExecuteNonQuery();



                System.Console.WriteLine(" \n Completed Restore \n");

                // close connection
                connR.Close();
            }
            catch (Exception e)
            {
                // print the error to the user if there is one
                Console.WriteLine(e.Message);
            }

        }
    }
}
