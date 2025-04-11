using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace Lingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString = "Server=localhost;Database=language_hub;Uid=root;Password=;SslMode=None";

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool Belepes(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT `Id` FROM `users` WHERE `UserName` = @username AND `Password`= @password";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            UsersDatas.Id = dr.GetInt32(0);
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt! " + ex.ToString());
                return false;
            }


        }

        private bool VanEIlyenFelhasznalo(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT COUNT(*) FROM `users` WHERE `UserName` = @username";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@username", username);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Hiba történt: " + e.Message);
                return false;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Belepes(UserNameTxT.Text, PasswordTxT.Password))
                {
                    MessageBox.Show("Bejelentkezve!");
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
        }

        public static class UsersDatas
        {
            public static int Id { get; set; }
            public static int CategoryId { get; set; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow form2 = new MainWindow();
            form2.ShowDialog();
        }
    }
}
