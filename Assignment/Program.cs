using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            studentFormPanel.Visible = false;
            gradesPanel.Visible = false;

        }


        //Submit Button
        private void Sumbit_button_Click_1(object sender, EventArgs e)
        {

            //ID_TextBox Exception
            try
            {
                if (String.IsNullOrWhiteSpace(ID_TextBox.Text))
                    MessageBox.Show("Please Fill the ID Texbox", "ID", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                else
                    long.Parse(ID_TextBox.Text);
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input!  ID Number must be numeric", "Warning Message", buttons, MessageBoxIcon.Warning);

            }


            //First_Name Exception
            try
            {

                if (String.IsNullOrWhiteSpace(FirstName_TextBox.Text))
                    MessageBox.Show("Please Fill the FirstName Texbox", "FirstName", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (int.TryParse(FirstName_TextBox.Text, out _))
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input!  FirstName  must be string", "Warning Message", buttons, MessageBoxIcon.Warning);
            }


            //LastName Exception
            try
            {

                if (String.IsNullOrWhiteSpace(FirstName_TextBox.Text))
                    MessageBox.Show("Please Fill the LastName Texbox", "LastName", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (int.TryParse(LastName_TextBox.Text, out _))
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input!  LastName  must be string", "Warning Message", buttons, MessageBoxIcon.Warning);
            }

            //QuizScore
            try
            {
                int value = int.Parse(quiz_textbox.Text);
                if (value <= 74)
                    resulting.Text = "Poor";
                else if (value >= 75 && value <= 80)
                    resulting.Text = "Fair";
                else if (value >= 81 && value <= 90)
                    resulting.Text = "Good";
                else if (value >= 91)
                    resulting.Text = "Excellent";
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input! Quiz Score must be numeric", "Warning Message", buttons, MessageBoxIcon.Warning);
            }


        }
        public enum remark
        {
            Poor = 1,
            Fair = 2,
            Good = 3,
            Excellent = 4
        }

        public struct Student
        {
            public int id;
            public string firstName;
            public string lastName;
            public int quizScore;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            studentFormPanel.Visible = true;
            gradesPanel.Visible = false;
        }

     
        private void gradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            studentFormPanel.Visible = false;
            gradesPanel.Visible = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            menuStrip1.BackColor = Color.White;
            studentFormPanel.BackColor = Color.White;
            Remarks.BackColor = Color.White;
        

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.DimGray;
            menuStrip1.BackColor = Color.DimGray;
            studentFormPanel.BackColor = Color.DimGray;
            Remarks.BackColor = Color.DimGray;
      
        }



        //Insert
        private void SaveButton_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyFirstDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            cmd = new SqlCommand("Insert Into Students values(@StudId,@Lastname,@Firstname,@MI,@Course)");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@StudId", IdTextBox.Text);
            cmd.Parameters.AddWithValue("@Lastname", lastNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Firstname", firstNameTextBox.Text);
            cmd.Parameters.AddWithValue("@MI", MiTextBox.Text);
            cmd.Parameters.AddWithValue("@Course", courseTextBox.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            viewData();
        }

        void viewData()
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyFirstDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            cmd = new SqlCommand("Select *  from Students");
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Students");

            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            viewData();
        }

        private void gradesPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        //gradePanel - WhiteMode
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            menuStrip1.BackColor = Color.White;
            studentFormPanel.BackColor = Color.White;
            Remarks.BackColor = Color.White;
        }

        private void gradesDarkButton_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.DimGray;
            menuStrip1.BackColor = Color.DimGray;
            studentFormPanel.BackColor = Color.DimGray;
            Remarks.BackColor = Color.DimGray;

        }
    }
}