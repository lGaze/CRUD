using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Pokemones : Form
    {
        DBConnection Con;

        public Pokemones(DBConnection C1)
        {
            Con = C1;
            InitializeComponent();
        }


        private void Pokemones_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 Tabla = new Form1();
            Con.SelectQuery("INSERT INTO pokemones VALUES(NULL, '" + textBox1.Text + "' )");
            this.Hide();
            Tabla.Show();

        }
    }
}
