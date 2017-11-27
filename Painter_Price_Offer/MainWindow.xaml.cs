using System;
using System.Collections.Generic;
using System.Data;
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

namespace Painter_Price_Offer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable tableWorkflow = new DataTable(); //Adattábla a munkadíjhoz
        DataTable tableConsumption = new DataTable(); //Adattábla anyagszükséglethez
        List<string> cbUnit = new List<string>() //Lista a mértékegységhez
        {
            "m2", "fm"
        };

        public MainWindow()
        {
            InitializeComponent();
            
            //Munkafolyamat adattábla generálása
            tableWorkflow.Columns.Add("Megnevezés", typeof(string));
            tableWorkflow.Columns.Add("Mennyiség", typeof(string));
            tableWorkflow.Columns.Add("Egységár", typeof(string));
            tableWorkflow.Columns.Add("Fm / m2", typeof(string));
            tableWorkflow.Columns.Add("Munkadíj", typeof(string));
            grdWorkflow.ItemsSource = tableWorkflow.DefaultView;

            //Anyagszükséglet adattábla generálása
            tableConsumption.Columns.Add("Megnevezés", typeof(string));
            tableConsumption.Columns.Add("Mennyiség", typeof(string));
            tableConsumption.Columns.Add("Egységár", typeof(string));
            tableConsumption.Columns.Add("Fm / m2", typeof(string));
            tableConsumption.Columns.Add("Anyagdíj", typeof(string));
            grdConsumption.ItemsSource = tableConsumption.DefaultView;

            //Munkafolyamat adattábla legördülőlista feltöltése
            Unital.ItemsSource = cbUnit;
            Unital2.ItemsSource = cbUnit;

            //Alap textboxok feltöltése adatokkal
            txtTitle.Text = "MUNKALAP - ÁRAJÁNLAT";
            txtName.Text = "Malasits Gyula";
            txtLocation.Text = "2881 Ászár Báthory István utca 6";
            txtTelNumber.Text = "0620/33 91 562";
            txtEmail.Text = "adrige@vipmail.hu";
        }

        //ESEMÉNYEK

        private void cbCustomer_Checked(object sender, RoutedEventArgs e)
        {
                txtCustomerName.IsEnabled = true;
                txtCustomerPhoneNumber.IsEnabled = true;
                txtCustomerLocation.IsEnabled = true;
                txtCustomerEmail.IsEnabled = true;
        }

        private void cbCustomer_Unchecked(object sender, RoutedEventArgs e)
        {
            txtCustomerName.IsEnabled = false;
            txtCustomerPhoneNumber.IsEnabled = false;
            txtCustomerLocation.IsEnabled = false;
            txtCustomerEmail.IsEnabled = false;
        }


        private void grdWorkflow_LostFocus(object sender, RoutedEventArgs e)
        {
            Style cellStyle = new Style(typeof(DataGridCell));
            foreach (DataRow dr in tableWorkflow.Rows)
            {
                try
                {
                    int a;
                    int b;
                    if (!string.IsNullOrEmpty(dr[1].ToString()) && !string.IsNullOrEmpty(dr[2].ToString()) && int.TryParse(dr[1].ToString(), out a) && int.TryParse(dr[2].ToString(), out b))
                    {
                        tableWorkflow.Rows[tableWorkflow.Rows.IndexOf(dr)].SetField<string>(tableWorkflow.Columns[4], (Convert.ToInt32(dr[1].ToString()) * Convert.ToInt32(dr[2].ToString())).ToString());
                    }
                    if (!int.TryParse(dr[1].ToString(), out a))
                    {
                        cellStyle = new Style(typeof(DataGridCell));
                        cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.Red));
                        Mennyiseg_Column.CellStyle = cellStyle;
                    }
                    else
                    {
                        cellStyle = new Style(typeof(DataGridCell));
                        cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.White));
                        Mennyiseg_Column.CellStyle = cellStyle;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grdConsumption_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in tableConsumption.Rows)
            {
                if (dr[1].ToString() != "" && dr[2].ToString() != "")
                {
                    tableConsumption.Rows[tableConsumption.Rows.IndexOf(dr)].SetField<string>(tableConsumption.Columns[4], (Convert.ToInt32(dr[1].ToString()) * Convert.ToInt32(dr[2].ToString())).ToString());
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void grdWorkflow_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //Klikkelés
        }
    }
}
