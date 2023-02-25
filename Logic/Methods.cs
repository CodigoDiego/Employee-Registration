using Data;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.IO;
using System.Drawing;

namespace Logic.Library
{
    public class Methods
    {
        //Dconnect connClass = new Dconnect();
        //SqlConnection sqlConn = new SqlConnection();
        //SqlCommand cmd = new SqlCommand();
        string controlName { get; set; }
        ErrorProvider errProv { get; set; }
        TextBox tb { get; set; }
        bool isMail = false;
        SqlConnection sqlConn = new SqlConnection(@"Data Source=DSKTP-DGUV\DGUEV;Database=employeesreg;Integrated Security=True;Encrypt=False;TrustServerCertificate = False;Trusted_Connection=True;Enlist=False;");
        SqlCommand cmd = new SqlCommand();
        public Methods()
        {

        }

        public void ValidateTb(TextBox tbF, ErrorProvider errPr, string conName, bool ismail) //This void validates if the textboxes in the Form1 are empty
        {
            this.tb = tbF;
            this.errProv = errPr;
            this.controlName = conName;
            this.isMail = ismail;
            if (string.IsNullOrEmpty(tb.Text) && isMail == false)
            {
                errProv.SetError(tb, $"{controlName} is needed for the registration process");
            }
            else if (isMail == true)
            {
                bool isvalid = new EmailAddressAttribute().IsValid(tb.Text);
                if (isvalid == false)
                {
                    errProv.SetError(tb, $"Invalid e-mail address");
                }
            }
            else
            {
                errProv.SetError(tb, string.Empty);
            }

        }

        public void textkeypress(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }
        public void numericTextbox(KeyPressEventArgs e) {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }
        public void LoadDgv(SqlConnection sqlconn, DataGridView dgv)
        {
            SqlDataAdapter adpt = new SqlDataAdapter("SELECT nid, name, lname,email FROM employeesreg", sqlconn);
            DataTable dt = new DataTable();
            DataTable filtered = new DataTable();
            filtered.Columns.Add("ID");
            filtered.Columns.Add("Name");
            filtered.Columns.Add("Last Name");
            filtered.Columns.Add("Email");
            adpt.Fill(dt);
            dgv.DataSource = dt;
        }
        public void FilterStd(DataGridView dgv, TextBox fltr, SqlConnection sqlconn) 
        { 
            string query = @"select nid, name, lname,email from employeesreg where name =('" + fltr.Text + "') OR lname =('" + fltr.Text + "') OR nid =('" + fltr.Text + "');";
            SqlDataAdapter adpt = new SqlDataAdapter(query, sqlConn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adpt);
            DataTable dt = new DataTable();
            DataTable filtered = new DataTable();
            filtered.Columns.Add("ID");
            filtered.Columns.Add("Name");
            filtered.Columns.Add("Last Name");
            filtered.Columns.Add("Email");
            adpt.Fill(dt);
            dgv.DataSource = dt;
        }
        public void Delete(TextBox tb, SqlConnection sqlconn, DataGridView dgv) 
        {
            if (tb.Text == "")
            {

            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show("Do you want to delete the selected enty?", "Delete entry", buttons);
                if (result == DialogResult.Yes)
                {
                    DataSet ds = new DataSet();

                    // Create a new SqlDataAdapter with a parameterized DELETE statement
                    SqlDataAdapter adpt = new SqlDataAdapter("DELETE FROM employeesreg WHERE nid = @nid", sqlConn);
                    adpt.SelectCommand.Parameters.AddWithValue("@nid", tb.Text);

                    // Create a SqlCommandBuilder to automatically generate insert, update, and delete commands
                    SqlCommandBuilder builder = new SqlCommandBuilder(adpt);

                    // Fill the DataSet with data from the database
                    adpt.Fill(ds, "employeesreg");

                    // Delete the appropriate row(s) from the DataTable in the DataSet
                    DataTable table = ds.Tables["employeesreg"];
                    if (table != null)
                    {

                        DataRow[] rows = table.Select("nid = " + tb.Text);
                        foreach (DataRow row in rows)
                        {
                            row.Delete();
                        }

                        // Update the database with changes made to the DataSet
                        adpt.Update(ds, "employeesreg");
                    }
                }
            } 
        }
        public void LoadImg(TextBox tb, SqlConnection sqlConn,PictureBox pcb) 
        {
            SqlCommand cmd = new SqlCommand("select image from employeesreg where nid = @nid",sqlConn);
            cmd.Parameters.AddWithValue("@nid",tb.Text);
            byte[] imgData = (byte[])cmd.ExecuteScalar();

            MemoryStream stream = new MemoryStream(imgData);
            Image image = Image.FromStream(stream);
            pcb.Image = image;
        }
    }
}
