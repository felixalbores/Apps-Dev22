//Team alpha
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Sumbit_button_Click(object sender, EventArgs e)
        {
            int value = int.Parse(quiz_textbox.Text);

            if (value == (int)remark.Poor)
                resulting.Text = "Poor";
            else if (value == (int)remark.Fair)
                resulting.Text = "Fair";
            else if (value == (int)remark.Good)
                resulting.Text = "Good";
            else if (value == (int)remark.Excellent)
                resulting.Text = "Excellent";
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
    }
}
