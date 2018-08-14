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
    public partial class Form1 : Form
    {
        DBConnection C1;
        bool entrenadores;
        bool pokemones;
        bool especies;

        public Form1()
        {
            entrenadores = false;
            pokemones = false;
            especies = false;
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            C1 = new DBConnection("138.68.20.16", "vflores_pfinal", "vflores", "1234567890");

            if (C1.Connect(ref errorMsg))
            {
                
            }
            else
            {
                MessageBox.Show("Conection Failure: " + errorMsg);
                Close();
            }
        }

        private void UpdateTable()
        {
            
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            if (entrenadores)
            {
                DataTable entrenadores = C1.SelectQuery("Select * from entrenadores");
                dataGridView1.DataSource = entrenadores;
                
            }
            if(pokemones)
            {
                DataTable pokemones = C1.SelectQuery("Select * from pokemones");
                dataGridView1.DataSource = pokemones;
            }
            if(especies)
            {
                DataTable especies = C1.SelectQuery("Select * from especies");
                dataGridView1.DataSource = especies;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(entrenadores)
            {
                Entrenadores addEntrenadores = new Entrenadores(C1);
                this.Hide();
                addEntrenadores.Show();
            }
            if(pokemones)
            {
                Pokemones addPokemones = new Pokemones(C1);
                this.Hide();
                addPokemones.Show();
            }
            if(especies)
            {
                Especies addEspecies = new Especies(C1);
                this.Hide();
                addEspecies.Show();
            }
        }


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable entrenadores = dataGridView1.DataSource as DataTable;

            string query = "UPDATE " + "entrenadores" + " SET "
               + entrenadores.Columns[e.ColumnIndex].ColumnName + " = '"
               + entrenadores.Rows[e.RowIndex][e.ColumnIndex]
               + "' WHERE id = " + entrenadores.Rows[e.RowIndex][0];
            bool success = C1.ExecuteQuery(query);
            if (!success) MessageBox.Show("Error desconocido en el query");
            UpdateTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            entrenadores = true;
            pokemones = false;
            especies = false;
            UpdateTable();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            entrenadores = false;
            pokemones = true;
            especies = false;
            UpdateTable();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            entrenadores = false;
            pokemones = false;
            especies = true;
            UpdateTable();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var senderGrid = sender as DataGridView;
            if(entrenadores)
            {
              C1.ExecuteQuery("Delete from entrenadores where id =" + e.Row.Cells[0].Value.ToString());
              UpdateTable();
            }

            if (pokemones)
            {
                C1.ExecuteQuery("Delete from pokemones where id =" + e.Row.Cells[0].Value.ToString());
                UpdateTable();
            }

            if (especies)
            {
                C1.ExecuteQuery("Delete from especies where id =" + e.Row.Cells[0].Value.ToString());
                UpdateTable();
            }

            e.Cancel = true;
        }
    }
}
