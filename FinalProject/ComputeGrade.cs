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