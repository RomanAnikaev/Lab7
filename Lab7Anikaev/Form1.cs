using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace Lab7Anikaev
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null; //клас для підключення БД
        //Завдяки цьому класу відбуваються всі операції з БД

        public Form1()
        {
            InitializeComponent();
        }

        //Insert Lecturer
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Lecturers] (Name, Surname, Birthday) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', '{textBox3.Text}')", 
                sqlConnection);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Lab7DB"].ConnectionString);
            //Відкриваємо підключення до баз даних
            sqlConnection.Open();
            //Перевіряємо підключення
            if (sqlConnection.State != ConnectionState.Open) { MessageBox.Show("Нема підключення до БД!"); }
        }

        //Insert Item
        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Items] (Item_name, Year_of_study, Num_of_groups) VALUES (N'{textBox4.Text}', '{textBox5.Text}', '{textBox6.Text}')",
                sqlConnection);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        //Update Lecturer
        private void button3_Click(object sender, EventArgs e)
        {
            string str1 = listBox1.SelectedItem.ToString();

                SqlCommand command = new SqlCommand(
                    $"UPDATE [Lecturers] SET {str1} = N'{textBox8.Text}' WHERE {str1} = N'{textBox7.Text}'",
                    sqlConnection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
     
        }

        //Update Item
        private void button4_Click(object sender, EventArgs e)
        {
            string str2 = listBox2.SelectedItem.ToString();
            if (str2 == "Item_name")
            {
                SqlCommand command = new SqlCommand(
                    $"UPDATE [Items] SET {str2} = N'{textBox10.Text}' WHERE {str2} = N'{textBox9.Text}'",
                    sqlConnection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
            }
            else
            {
                SqlCommand command = new SqlCommand(
                   $"UPDATE [Items] SET {str2} = '{Convert.ToInt32(textBox10.Text)}' WHERE {str2} = '{Convert.ToInt32(textBox9.Text)}'",
                   sqlConnection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
            }
        }

        //Output value Lecturers
        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
               $"SELECT * FROM [Lecturers]",
               sqlConnection);
            //обьектное представление БД
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        //Output value Items
        private void button6_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
               $"SELECT * FROM [Items]",
               sqlConnection);
            //обьектное представление БД
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        //Сonnect by Lecturer_id
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int indf = Convert.ToInt32(textBox11.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(
                   $"SELECT * FROM Lecturers L JOIN Lecturer_Item LI ON L.Lecturer_id = LI.Lecturer_id JOIN Items I ON LI.Item_id = I.Item_id  WHERE L.Lecturer_id = {indf}",
                   sqlConnection);
                //обьектное представление БД
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch 
            {
                MessageBox.Show("Введіть ідентифікатор Викладача!");
            }
        }

        //Сonnect between Lecturer and Item
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int indf1 = Convert.ToInt32(textBox12.Text);
                int indf2 = Convert.ToInt32(textBox13.Text);
                SqlCommand command1 = new SqlCommand(
                $"INSERT INTO [Lecturer_Item] VALUES ('{indf1}', '{indf2}')",
                sqlConnection);
                MessageBox.Show(command1.ExecuteNonQuery().ToString());
            }
            catch
            {
                MessageBox.Show("Невірно введені дані, або такий звязок вже існує!");
            }
        }
    }
}
