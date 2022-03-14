using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Laboratorio6___202108048
{
    public partial class Form1 : Form
    {
        List<Vehículo> vehículos = new List<Vehículo>();
        List<Cliente> clientes = new List<Cliente>();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Leer(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Vehículo auto = new Vehículo();
                auto.placa = reader.ReadLine();
                auto.marca = reader.ReadLine();
                auto.modelo =reader.ReadLine();
                auto.color = reader.ReadLine();
                auto.preciokm = reader.ReadLine();

               vehículos.Add(auto);

            }

            reader.Close();

        }

        private void LeerClientes(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Cliente cliente = new Cliente();
                cliente.nit = reader.ReadLine();
                cliente.nombre = reader.ReadLine();
                cliente.direccion = reader.ReadLine();
  
                clientes.Add(cliente);

            }

            reader.Close();

        }
        private void GuardarClientes(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var c in clientes)
            {
                writer.WriteLine(c.nit);
                writer.WriteLine(c.nombre);
                writer.WriteLine(c.direccion);

            }

            writer.Close();
        }
        private void Guardar (string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var vehiculo in vehículos)
            {
                writer.WriteLine(vehiculo.placa);
                writer.WriteLine(vehiculo.marca);
                writer.WriteLine(vehiculo.modelo);
                writer.WriteLine(vehiculo.color);
                writer.WriteLine(vehiculo.preciokm);
            }
       
            writer.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            bool existe = vehículos.Exists(v => v.placa == textBox1.Text);

            if (existe)
            {
                MessageBox.Show("Esa placa ya existe", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                Vehículo auto = new Vehículo();
                auto.placa = textBox1.Text;
                auto.marca = textBox2.Text;
                auto.modelo = textBox3.Text;
                auto.color = textBox4.Text;
                auto.preciokm = textBox5.Text;

                vehículos.Add(auto);
                Guardar("Vehiculos.txt");
            }
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            Leer("Vehiculos.txt");
            LeerClientes("Clientes.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vehículos = vehículos.OrderBy(x => x.preciokm).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

            dataGridView1.DataSource = vehículos;
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.nit = textBox6.Text;
            cliente.nombre = textBox7.Text;
            cliente.direccion = textBox8.Text;

            clientes.Add(cliente);
            GuardarClientes("Clientes.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
     

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();

            dataGridView2.DataSource = clientes;
            dataGridView2.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormAlquiler F1 = new
            FormAlquiler();
            F1.Show();
        }
    }
}
