using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

namespace AdoNet1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Nametxtblock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox txtbox = sender as TextBox;

            txtbox.Text = "";
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            

            using (var conn = new SqlConnection())
            {                
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();

                string query = @"insert into Books(Id, [Name],Pages,YearPress,Id_Themes,Id_Category,Id_Author,Id_Press,Comment,Quantity)
                   values(@MyId,@Name,@Pages,@YearPress,@Id_Themes,@Id_Category,@Id_Author,@Id_Press,@Comment,@Quantity)";

                int id = random.Next(600, 1500);

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@MyId";
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = id;

                var name= Nametxtblock.Text;
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@Name";
                param2.SqlDbType = SqlDbType.NChar;
                param2.Value = name;

                var pages= Int32.Parse(Pagetxtblock.Text);
                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@Pages";
                param3.SqlDbType = SqlDbType.Int;
                param3.Value = pages;

                var year= Int32.Parse(YearPresstxtblock.Text);
                SqlParameter param4 = new SqlParameter();
                param4.ParameterName = "@YearPress";
                param4.SqlDbType = SqlDbType.Int;
                param4.Value = year;

                SqlParameter param5 = new SqlParameter();
                param5.ParameterName = "@Id_Themes";
                param5.SqlDbType = SqlDbType.Int;
                param5.Value = 3;

                SqlParameter param6 = new SqlParameter();
                param6.ParameterName = "@Id_Category";
                param6.SqlDbType = SqlDbType.Int;
                param6.Value = 6;

                SqlParameter param7 = new SqlParameter();
                param7.ParameterName = "@Id_Author";
                param7.SqlDbType = SqlDbType.Int;
                param7.Value = 2;

                SqlParameter param8 = new SqlParameter();
                param8.ParameterName = "@Id_Press";
                param8.SqlDbType = SqlDbType.Int;
                param8.Value = 5;

                var comment= Commenttxtblock.Text;
                SqlParameter param9 = new SqlParameter();
                param9.ParameterName = "@Comment";
                param9.SqlDbType = SqlDbType.NVarChar;
                param9.Value = comment; 

                var quantity= Int32.Parse(Quantitytxtblock.Text);
                SqlParameter param10 = new SqlParameter();
                param10.ParameterName = "@Quantity";
                param10.SqlDbType = SqlDbType.Int;
                param10.Value = quantity;

                var command = new SqlCommand(query, conn);
                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
                command.Parameters.Add(param4);
                command.Parameters.Add(param5);
                command.Parameters.Add(param6);
                command.Parameters.Add(param7);
                command.Parameters.Add(param8);
                command.Parameters.Add(param9);
                command.Parameters.Add(param10);

                var result = command.ExecuteNonQuery();

                MessageBox.Show("Book was added...");
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();

                string query = @"delete from Books where Books.Id= @lala";
                              

                var id = Int32.Parse(idTxtBlock.Text);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@lala";
                param.SqlDbType = SqlDbType.Int;
                param.Value = id;

                var command = new SqlCommand(query, conn);
                
                command.Parameters.Add(param);

                
                var result = command.ExecuteNonQuery();

                if (result != 0)
                {
                    MessageBox.Show("Book was deleted...");
                }
                else
                {
                    MessageBox.Show("Id is used or not found. Select another Id...");
                }
            }
        }
        //kukuli 
        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();
                string query = @"select * from Books";
               

                var command = new SqlCommand(query, conn);
                using (var reader = command.ExecuteReader())
                {
                    ListBox.Items.Add("Id | Name     | Pages     | Year Press    | Comment     | Quantity ");
                    while (reader.Read())
                    {
                        ListBox.Items.Add(reader.GetValue(0) + " | " + reader.GetValue(1) + " | " + reader.GetValue(2) + " | " + reader.GetValue(3) + " | " + reader.GetValue(8) + " | " + reader.GetValue(9));
                    }
                }

            }
        }
    }
}
