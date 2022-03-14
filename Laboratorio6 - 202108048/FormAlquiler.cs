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
    public partial class FormAlquiler : Form
    {
        List<Aquiler> alquileres = new List<Aquiler>();
        List<Vehículo> vehiculos = new List<Vehículo>();
        List<Cliente> clientes = new List<Cliente>();

        public FormAlquiler()
        {
            InitializeComponent();
        }

        private void Guardar()
        {
            FileStream stream = new FileStream("Alquileres.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter writer = new StreamWriter(stream);

            foreach (var alquiler in alquileres)
            {
                writer.WriteLine(alquiler.Nit);
                writer.WriteLine(alquiler.Placa);
                writer.WriteLine(alquiler.FechaAlquiler);
                writer.WriteLine(alquiler.FechaDevolucion);
                writer.WriteLine(alquiler.Kilometros);

            }


            writer.Close();

        }

        private void LeerAlquileres(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Aquiler alquiler = new Aquiler();
                alquiler.Nit = reader.ReadLine();
                alquiler.placa = reader.ReadLine();
                alquiler.FechaAlquiler = Convert.ToDateTime(reader.ReadLine());
                alquiler.FechaDevolucion = Convert.ToDateTime(reader.ReadLine());
                alquiler.Kilometros = Convert.ToInt32(reader.ReadLine());


                alquileres.Add(alquiler);

            }

            reader.Close();

        }

        private void FormAlquiler_Load(object sender, EventArgs e)
        {
            if (File.Exists("Vehiculos.txt"))
            {
                FileStream stream = new FileStream("Vehiculos.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() > -1)
                {
                    Vehículo vehiculo = new Vehículo();
                    vehiculo.placa = reader.ReadLine();
                    vehiculo.marca = reader.ReadLine();
                    vehiculo.modelo = reader.ReadLine();
                    vehiculo.color = reader.ReadLine();
                    vehiculo.preciokm = reader.ReadLine();

                    vehiculos.Add(vehiculo);
                }
                reader.Close();
            }

            LeerAlquileres("Alquileres.txt");
   
            comboBox1.ValueMember = "placa";
            comboBox1.DataSource = null;
            comboBox1.DataSource = vehiculos;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Aquiler alquiler = new Aquiler();
            alquiler.Nit = textBox1.Text;
            alquiler.Placa = comboBox1.SelectedValue.ToString();
            alquiler.FechaAlquiler = dateTimePicker1.Value;
            alquiler.FechaDevolucion = dateTimePicker2.Value;
            alquiler.Kilometros = Convert.ToInt32(textBox5.Text);

            alquileres.Add(alquiler);
            Guardar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

            dataGridView1.DataSource = alquileres;
            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
         
            int valMax = alquileres.Max(x => x.Kilometros);
            textBox6.Text = valMax.ToString();
        }
    }
}
