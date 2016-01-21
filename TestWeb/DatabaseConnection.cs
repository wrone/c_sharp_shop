using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_kursa
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader dataReader;

        public DatabaseConnection(string host, string port, string dbName, string user, string password)
        {
            string str = "Server=" + host + ";Port=" + port + ";Database=" + dbName + ";Uid=" + user + ";Pwd=" + password + ";";
            connection = new MySqlConnection(str);
            connection.Open();

            cmd = connection.CreateCommand();
            dataReader = null;
        }

        public void WriteData(string query)
        {
            try
            {
                cmd.CommandText = query; //"INSERT INTO Users(ID, Name, Lastname) VALUES(2, 'da', 'net')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> ReadData(string query)
        {
            //string text = "";
            List<string> list = new List<string>();
            cmd.CommandText = query;
            dataReader = cmd.ExecuteReader();

            int count = dataReader.FieldCount;
            while (dataReader.Read())
            {
                for (int i = 0; i < count; i++)
                {
                    //text += dataReader.GetValue(i) + " ";
                    list.Add(dataReader.GetValue(i).ToString());
                }
            }

            dataReader.Close();

            return list;
        }

        public void CloseConnection()
        {
            dataReader.Close();
            connection.Close();
        }

    }
}

// DATA TO GRID N SHIT

//private void button4_Click(object sender, EventArgs e)
//{
//    try
//    {
//        string MyConnection2 = "datasource=localhost;port=3307;username=root;password=root";
//        //Display query
//        string Query = "select * from student.studentinfo;";
//        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
//        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
//        //  MyConn2.Open();
//        //For offline connection we weill use  MySqlDataAdapter class.
//        MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
//        MyAdapter.SelectCommand = MyCommand2;
//        DataTable dTable = new DataTable();
//        MyAdapter.Fill(dTable);
//        dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
//        // MyConn2.Close();
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }
//}