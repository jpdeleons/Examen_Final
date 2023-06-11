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
using System.Windows.Shapes;

namespace ExamenFinal
{
    /// <summary>
    /// Lógica de interacción para Datos.xaml
    /// </summary>
    public partial class Datos : Window
    {
        public Datos()
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
            grilla.ItemsSource = alumnos;
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int carnet = ((Alumno)grilla.SelectedItem).Carnet;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas a punto de borrar al alumno", "Confirmacion de borrado", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult.HasFlag(System.Windows.MessageBoxResult.Yes))
            {
                SqlConnection conexion = new SqlConnection(enlace.CONECCION);
                conexion.Open();
                string consulta = "DELETE From Alumno WHERE Carnet=@Carnet";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.Add(new SqlParameter("Carnet", carnet));

                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    ImprimirDatos();
                }
            }
        }
    }
}
