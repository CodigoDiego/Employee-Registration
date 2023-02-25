using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace Registro
{
    public partial class Form2 : Form
    {
        private Workers.lWorkers lworkers;
        public Form2()
        {
            InitializeComponent();
            var listTextBox = new List<TextBox>();
            listTextBox.Add(tb_ID);
            listTextBox.Add(tbName);
            listTextBox.Add(tb_lName);
            listTextBox.Add(tb_mAddress);
            var listLabel = new List<Label>();
            //lworkers =new Workers.lWorkers(listTextBox);
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {this.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tb_lName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_cls_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
