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


namespace SimpleGradingSystem
{
    public partial class homePage : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public homePage()
        {
            InitializeComponent();
            readDataBase();
            studentFormPanel.Visible = false;
            homePanel.Visible = true;
            fillMenuStrip.Visible = false;
            Size  = new Size(402, 439);
            homePanel.Dock = DockStyle.Fill;
            gradesPanel.Visible = false;
            readDBSubject();
        }





        #region HELPER METHODS

        /// <summary>
        /// OPEN SQL CONNECTION
        /// </summary>  
        private void openSqlConnection()
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentFormDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
        }

        /// <summary>
        /// CLOSE SQL CONNECTION
        /// </summary>
        private void closeSqlConnection()
        {

            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "StudentData");
            studentDataGridView.DataSource = ds.Tables[0];
            con.Close();
        }
        /// <summary>
        /// Confirmation Box
        /// </summary>
        private void confirmationMessageBox(String message)
        {
           MessageBox.Show("Successfully " + message);
           idTextBox.Text = "";
           lastNameTextBox.Text = "";
           firstNameTextBox.Text = "";
           miTextBox.Text = "";
           birthDateTimePicker.Text = "01-01-2000";
           courseComboBox.Text = "";
           readDataBase();
        }

        #endregion HELPER METHODS

        #region CRUD OPERATIONS

        /// <summary>
        /// READ DATABASE
        /// </summary>
        public void readDataBase()
        {
            openSqlConnection();
            cmd = new SqlCommand("Select * from StudentData");
            closeSqlConnection();
        }

        /// <summary>
        /// CREATE NEW STUDENTS
        /// </summary>
        private void insertSqlCommand(String message)
        {
            openSqlConnection();
            cmd = new SqlCommand(message);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@Lastname", lastNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Firstname", firstNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Mi", miTextBox.Text);
            cmd.Parameters.AddWithValue("@Birthdate", birthDateTimePicker.Text);
            cmd.Parameters.AddWithValue("@Course", courseComboBox.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Search Data
        /// </summary>
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            openSqlConnection();

            if (choicesComboBox.Text == "LastName")
                cmd = new SqlCommand("Select * from StudentData Where Lastname like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "FirstName")
                cmd = new SqlCommand("Select * from StudentData Where Firstname like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "ID")
                cmd = new SqlCommand("Select * from StudentData Where Id like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "Course")
                cmd = new SqlCommand("Select * from StudentData Where Course like '%' + '" + searchTextBox.Text + "' + '%' ");
            else if (choicesComboBox.Text == "BirthDate")
                cmd = new SqlCommand("Select * from StudentData Where Birthdate like '%' + '" + searchTextBox.Text + "' + '%' ");

            closeSqlConnection();
        }

        #endregion  CRUD OPERATIONS

        #region CLICKABLE BUTTON
        /// <summary>
        /// HOME 
        /// </summary>
        private void studentFormButton_Click(object sender, EventArgs e)
        {
            homePanel.Visible = false;
            studentFormPanel.Visible = true;
            Size = new Size(982, 500);
            studentFormPanel.Dock = DockStyle.Fill;
            this.Text = "Student Form";
            fillMenuStrip.Visible = true;
     
        }

        /// <summary>
        /// INSERT NEW DATA
        /// </summary>
        private void insertButton_Click(object sender, EventArgs e)
        {
            insertSqlCommand("Insert Into StudentData values(@Id,@Lastname,@Firstname,@Mi,@Birthdate,@Course)");
            confirmationMessageBox("Inserted");
        }

        /// <summary>
        /// UPDATE DATA
        /// </summary>
        private void updateButton_Click(object sender, EventArgs e)
        {
            openSqlConnection();
            SqlCommand cmd = new SqlCommand("Update StudentData SET Lastname = @Lastname ,Firstname = @Firstname, Mi = @Mi, Birthdate = @Birthdate,Course = @Course WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@Lastname", lastNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Firstname", firstNameTextBox.Text);
            cmd.Parameters.AddWithValue("@Mi", miTextBox.Text);
            cmd.Parameters.AddWithValue("@Birthdate", birthDateTimePicker.Text);
            cmd.Parameters.AddWithValue("@Course", courseComboBox.Text);
            cmd.ExecuteNonQuery();
            con.Close();


            confirmationMessageBox("Updated!");
        }

        /// <summary>
        /// DELETE BUTTON
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete DB", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                openSqlConnection();
                SqlCommand cmd = new SqlCommand("DELETE StudentData Where Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", idTextBox.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                confirmationMessageBox("Deleted");
            }
        }


        /// <summary>
        /// Appear Grades Panel
        /// </summary>
        private void gradesFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Grades Form";
            Size = new Size(1019, 400);
            gradesPanel.Dock = DockStyle.Fill;
            gradesPanel.Visible = true;
            studentFormPanel.Visible = false;

        }

        /// <summary>
        /// Appear Student Panels
        /// </summary
        private void studentFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size = new Size(982, 500);
            studentFormPanel.Dock = DockStyle.Fill;
            studentFormPanel.Visible = true;
            gradesPanel.Visible = false;
        }

        /// <summary>
        /// Exit Button
        /// </summary>
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }



        /// <summary>
        /// Calculate Grades
        /// </summary>
        private void calculateButton_Click(object sender, EventArgs e)
        {

            float grade = 0;
            try
            {
                float prelim = (float)Convert.ToDouble(prelimTextBox.Text);
                float midterm = (float)Convert.ToDouble(midTermTextBox.Text);
                float semi = (float)Convert.ToDouble(semiTextBox.Text);
                float final = (float)Convert.ToDouble(finalTextBox.Text);
                grade = (prelim + midterm + semi + final) / 4;
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid Input! Must be numbers", "Warning Message", buttons, MessageBoxIcon.Warning);
            }

            if (grade >= 3.0)
                status.Text = "Failed";
            else if (grade < 3.0)
                status.Text = "Passed";

            finalGradeResultTextBox.Text = grade.ToString("0.00");

        }

        #endregion CLICKABLE BUTTON


        #region FEATURES
        /// <summary>
        /// FROM CELL CLICK 
        /// </summary>
        private void studentDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (studentDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                studentDataGridView.CurrentRow.Selected = true;
                idTextBox.Text = studentDataGridView.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                lastNameTextBox.Text = studentDataGridView.Rows[e.RowIndex].Cells["Lastname"].FormattedValue.ToString();
                firstNameTextBox.Text = studentDataGridView.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString();
                miTextBox.Text = studentDataGridView.Rows[e.RowIndex].Cells["Mi"].FormattedValue.ToString();
                
                //Formatting DatePicker
                string format = studentDataGridView.Rows[e.RowIndex].Cells["Birthdate"].FormattedValue.ToString();
                DateTime date = DateTime.ParseExact(format, "MM-dd-yyyy", null);
                birthDateTimePicker.Text = date.ToString();

                courseComboBox.Text = studentDataGridView.Rows[e.RowIndex].Cells["Course"].FormattedValue.ToString();
            }
        }
       
        /// <summary>
        /// PlaceHolder SearchBox
        /// </summary>
        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
                searchTextBox.Text = "Search";

            readDataBase();
            searchTextBox.ForeColor = Color.DarkGray;
        }

        /// <summary>
        /// SearchTextBox Null
        /// </summary>
        private void searchTextBox_Enter_1(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Search")
                searchTextBox.Text = null;
            searchTextBox.ForeColor = Color.Black;


        }


        /// <summary>
        /// Dark Mode
        /// </summary>
        private void darkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGray;
            studentFormPanel.BackColor = Color.DarkGray;
            fillMenuStrip.BackColor = Color.DarkGray;
           
        }

        /// <summary>
        /// White Mode
        /// </summary>
        private void whiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            studentFormPanel.BackColor = Color.WhiteSmoke;
            fillMenuStrip.BackColor = Color.WhiteSmoke;
        }


        /// <summary>
        /// Grade Form Button
        /// </summary>
        private void gradeFormButton_Click(object sender, EventArgs e)
        {
            studentFormPanel.Visible = false;
            fillMenuStrip.Visible = true;
            homePanel.Visible = false;
            gradesPanel.Visible = true;
            this.Text = "Grades Form";
            Size = new Size(1019, 400);
            gradesPanel.Dock = DockStyle.Fill;

        }

        /// <summary>
        /// Read SubjectDB in Grades Form
        /// </summary>
        public void readDBSubject()
        {
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SubjectEntry;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            cmd = new SqlCommand("Select * from Subject");
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Subject");
            subjectDataGridView.DataSource = ds.Tables[0];
            con.Close();

        }

        #endregion FEATURES




    }
}
