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

namespace StudentDB
{
    public partial class StudentForm : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public StudentForm()
        {
            InitializeComponent();
            viewData();
    
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            fillUpForm();
        }

        private void searchTextBox_TextChanged_1(object sender, EventArgs e)
        {


            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();

            if (choicesComboBox.Text == "LastName")
                cmd = new SqlCommand("Select * from Student Where Lastname like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "FirstName")
                cmd = new SqlCommand("Select * from Student Where Firstname like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "ID")
                cmd = new SqlCommand("Select * from Student Where Id like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "QuizScore")
                cmd = new SqlCommand("Select * from Student Where Quizscore like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "Remarks")
                cmd = new SqlCommand("Select * from Student Where Remarks like '%' + '" + searchTextBox.Text + "' + '%' ");


            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Student");
            studentGridView.DataSource = ds.Tables[0];
            con.Close();

        }

        void viewData()
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            cmd = new SqlCommand("Select *  from Student");
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Student");

            studentGridView.DataSource = ds.Tables[0];
            con.Close();
        }

        private void darkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            studentPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        }

        private void lightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            studentPanel.BackColor = System.Drawing.SystemColors.HighlightText;
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if(searchTextBox.Text == "Search")
                searchTextBox.Text = null;
            searchTextBox.ForeColor = Color.Black;
            

        }
        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
                searchTextBox.Text = "Search";

            viewData();
            searchTextBox.ForeColor = Color.DarkGray;
        }
        
        private bool fillUpForm()
        {
            //ID_TextBox Exception
            try
            {
                if (String.IsNullOrWhiteSpace(idTextBox.Text))
                    MessageBox.Show("Please Fill the ID Texbox", "ID", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                else
                    long.Parse(idTextBox.Text);
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input!  ID Number must be numeric", "Warning Message", buttons, MessageBoxIcon.Warning);
                return false;
            }

            //First_Name Exception
            try
            {

                if (String.IsNullOrWhiteSpace(firstNameTextBox.Text))
                    MessageBox.Show("Please Fill the FirstName Texbox", "FirstName", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (int.TryParse(firstNameTextBox.Text, out _))
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input!  FirstName  must be string", "Warning Message", buttons, MessageBoxIcon.Warning);
                return false;
            }

            
            //LastName Exception
            try
            {

                if (String.IsNullOrWhiteSpace(lastNameTextBox.Text))
                    MessageBox.Show("Please Fill the LastName Texbox", "LastName", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (int.TryParse(lastNameTextBox.Text, out _))
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input!  LastName  must be string", "Warning Message", buttons, MessageBoxIcon.Warning);
                return false;
            }

            //QuizScore Exception
            try
            {
                int value = int.Parse(quizScoreTextBox.Text);
                if (value <= 74)
                    resultTextBox.Text = "Poor";
                else if (value >= 75 && value <= 80)
                    resultTextBox.Text = "Fair";
                else if (value >= 81 && value <= 90)
                    resultTextBox.Text = "Good";
                else if (value >= 91)
                    resultTextBox.Text = "Excellent";
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input! Quiz Score must be numeric", "Warning Message", buttons, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            if (fillUpForm()) {
                   jointSqlCommand("Insert Into Student values(@Id,@Lastname,@Firstname,@Quizscore,@Remarks)");
                   confirmationMessageBox("Inserted");
            }
            else
                MessageBox.Show("Please Fill out all the necessary Fields");

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
              con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
              con.Open();
             SqlCommand cmd = new SqlCommand("Update Student SET Lastname = @lastname ,Firstname = @firstname, QuizScore = @Quizscore, Remarks = @Remarks WHERE Id = @Id", con);
             cmd.Parameters.AddWithValue("@Id", idTextBox.Text);
             cmd.Parameters.AddWithValue("@Lastname", lastNameTextBox.Text);
             cmd.Parameters.AddWithValue("@Firstname", firstNameTextBox.Text);
             cmd.Parameters.AddWithValue("@Quizscore", quizScoreTextBox.Text);
             cmd.Parameters.AddWithValue("@Remarks", resultTextBox.Text);
             cmd.ExecuteNonQuery();
             con.Close();

            confirmationMessageBox("Updated!");
        }

        private void confirmationMessageBox(String message)
        {
            MessageBox.Show("Successfully " + message);
            idTextBox.Text = "";
            lastNameTextBox.Text = "";
            firstNameTextBox.Text = "";
            quizScoreTextBox.Text = "";
            resultTextBox.Text = "";
            viewData();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE Student Where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", idTextBox.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            confirmationMessageBox("Deleted");

        }

        private void jointSqlCommand(String message)
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            cmd = new SqlCommand(message);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@Lastname", lastNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Firstname", firstNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Quizscore", quizScoreTextBox.Text);
            cmd.Parameters.AddWithValue("@Remarks", resultTextBox.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
