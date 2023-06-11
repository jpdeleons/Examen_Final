using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ExamenFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImprimirDatos();
        }
        public void ImprimirDatos()
        {
            List<Alumno> alumnos = new List<Alumno>();
            SqlConnection conexion = new SqlConnection(enlace.CONECCION);
            conexion.Open();
            string consulta = "SELECT Carnet, Nombre, Telefono, Grado FROM Alumno";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    alumnos.Add(new Alumno()
                    {
                        Carnet = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Telefono = reader.GetInt32(2),
                        Grado = reader.GetString(3)
                    });
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection conexion = new SqlConnection(enlace.CONECCION);
            conexion.Open();
            string consulta = "INSERT INTO Alumno (Carnet, Nombre, Telefono, Grado) VALUES (@Carnet, @Nombre, @Telefono, @Grado)";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.Add(new SqlParameter("Carnet", txtCarnet.Text));
            comando.Parameters.Add(new SqlParameter("Nombre", txtNombre.Text));
            comando.Parameters.Add(new SqlParameter("Telefono", txtTelefono.Text));
            comando.Parameters.Add(new SqlParameter("Grado", txtGrado.Text));
            int resultado = comando.ExecuteNonQuery();
            if (resultado > 0) 
            {
                Datos datos = new Datos();
                datos.Closed += Ventana_Closed;
                datos.Show();
            }
        }
        private void Ventana_Closed(object? sender, EventArgs e)
        {
            ImprimirDatos();
        }
    }
}
