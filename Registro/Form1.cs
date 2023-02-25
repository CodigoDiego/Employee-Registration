using Logic;
using System.Drawing;
using static Logic.Workers;
using LinqToDB.Mapping;
using System.Xml.Linq;
using Logic.Library;
using Data;
using LinqToDB.Data;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;

namespace Registro
{
    public partial class Form1 : Form
    {
        // INSTANCE OF THE CLASS WORKER WHICH INHERITED FROM THE PROFILE PIC ONE
        Dconnect Dconn= new Dconnect();
        SqlConnection sqlConn = new SqlConnection();
        SqlCommand cmd= new SqlCommand();

        public Workers worker = new Workers();
        public Methods method=new Methods();
        public Workers.lWorkers lworkers;
        Image defaultImage;
        public Form1()
        {
            InitializeComponent();
            sqlConn = Dconn.GetConnection();

            var listTextBox = new List<TextBox>();
            listTextBox.Add(tb_ID);
            listTextBox.Add(tb_Name);
            listTextBox.Add(tb_lName);
            listTextBox.Add(tb_mAddress);
            var listLabel = new List<Label>();
            Object[] objetos =  {pb_profImg, dgv };


            lworkers = new Workers.lWorkers(listTextBox, objetos);

        }
        bool mouseDown;
        private Point offset;


        private void button2_Click(object sender, EventArgs e)
        {
            //Form2 SecondForm = new Form2();
            //SecondForm.Show();
            method.ValidateTb(tb_ID,errorProvider1,"An ID",false);
            method.ValidateTb(tb_Name, errorProvider1, "Your name",false);
            method.ValidateTb(tb_lName, errorProvider1, "Your last name",false);
            method.ValidateTb(tb_mAddress, errorProvider1, "An e-mail",true);

            if (tb_ID.Text!="" & tb_Name.Text!= "" & tb_lName.Text!="" & tb_mAddress.Text!="")
            {
                byte[] imgArr=worker.ImageToByte(pb_profImg.Image);
                //method.toDb(tb_ID.Text,tb_Name.Text,tb_lName.Text,tb_mAddress.Text);

                sqlConn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) from employeesreg where nid like ('"+tb_ID.Text+ "') OR email like ('"+tb_mAddress+"')", sqlConn);
                int userCount = (int)cmd.ExecuteScalar();
                if (userCount == 0) 
                    {
                    cmd = new SqlCommand("insert into employeesreg(nid, name, lname, email,image)values('" + tb_ID.Text + "','" + tb_Name.Text + "','" + tb_lName.Text + "','" + tb_mAddress.Text + "',@image)", sqlConn);
                    SqlParameter imageDataParameter = new SqlParameter("@image", SqlDbType.VarBinary, -1);
                    imageDataParameter.Value = imgArr;
                    cmd.Parameters.Add(imageDataParameter);
                    
                    
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                    //MessageBox.Show("Registration succesful");
                }
                else { MessageBox.Show("The user already exists");sqlConn.Close(); }
                    
            }
            clearTb();
            pb_profImg.Image= defaultImage;
            method.LoadDgv(sqlConn, dgv);
        }
        private void clearTb()
        {
            tb_ID.Clear();
            tb_Name.Clear();
            tb_lName.Clear();
            tb_mAddress.Clear();
            tb_srch.Clear();
            pb_profImg.Image = defaultImage;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sqlConn.Open();
            method.Delete(tb_ID,sqlConn,dgv);
            sqlConn.Close();
            clearTb();
            method.LoadDgv(sqlConn, dgv);

        }

        #region CLOSE MAXIMAZE AND MINIMIZE BUTTONS
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.WindowState==FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
            this.WindowState = FormWindowState.Maximized;
            }
        }   ////Maximize w/Custom button and take back to normal size and pos

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }   //Minimize w/Custom button

        private void btn_cls_Click(object sender, EventArgs e)
        {
            this.Close();
        }   //Close w/Custom button
        #endregion


        //REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD
        #region MOVEMENT OF BORDERLESS FORM
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown==true)
            {
            Point currentPos = PointToScreen(e.Location);
            Location = new Point(currentPos.X - offset.X, currentPos.Y - offset.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown= false;  
        }
        //END of the voids
        #endregion

        private void pb_profImg_Click(object sender, EventArgs e)
        {
            worker.CargarImagen(pb_profImg); //On click event the inherited function is called
        } //Call of the foreign function to change the image on click

        private void pb_profImg_MouseHover(object sender, EventArgs e)
        {
            tt1.Show("Click to upload or change your image",pb_profImg);
        } //Displays a hint when hovering over the image


        //REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD - REGION AHEAD
        #region TEXTBOXES EVENTS


        //ID TEXTBOX EVENTS
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (tb_ID.Text==null)
            {

            }
            else
            {

            }
        }

        private void tb_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            method.numericTextbox(e);
        }


        //NAME TEXTBOX EVENTS
        private void tb_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            method.textkeypress(e);
        }

        //LAST NAME TEXTBOX EVENTS
        private void tb_lName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_lName_KeyPress(object sender, KeyPressEventArgs e)
        {
            method.textkeypress(e);
        }

        //MAIL ADDRESS TEXTBOX EVENTS

        private void tb_mAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_mAddress_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion

        private void btn_cls_MouseHover(object sender, EventArgs e)
        {
            btn_cls.BackColor = Color.Red;
        }

        private void btn_cls_MouseLeave(object sender, EventArgs e)
        {
            btn_cls.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image defaultImage = pb_profImg.Image;
            method.LoadDgv(sqlConn,dgv);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_ID.Text=dgv.SelectedCells[0].Value.ToString();
            tb_Name.Text = dgv.SelectedCells[1].Value.ToString();
            tb_lName.Text = dgv.SelectedCells[2].Value.ToString();
            tb_mAddress.Text = dgv.SelectedCells[3].Value.ToString();
            sqlConn.Open();
            method.LoadImg(tb_ID,sqlConn, pb_profImg);
            sqlConn.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tb_srch_TextChanged(object sender, EventArgs e)
        {
            sqlConn.Open();
            method.FilterStd(dgv, tb_srch, sqlConn);
            sqlConn.Close();
        }

        private void srch_btn_Click(object sender, EventArgs e)
        {
            sqlConn.Open();
            method.FilterStd(dgv,tb_srch,sqlConn);
            sqlConn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            method.LoadDgv(sqlConn, dgv);
        }

        private void ref_lbl_Click(object sender, EventArgs e)
        {
            method.LoadDgv(sqlConn, dgv);
            clearTb();
        }
    }
}