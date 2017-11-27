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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

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
            tableConsumption.Columns.Add("Anyagdíj", typeof(string));
            grdConsumption.ItemsSource = tableConsumption.DefaultView;

            //Munkafolyamat adattábla legördülőlista feltöltése
            Unital.ItemsSource = cbUnit;

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
        {//Munkadíj számolása

            foreach (DataRow dr in tableWorkflow.Rows)
            {
                try
                {
                    int a;
                    int b;
                    int sum = 0;
                    if (!string.IsNullOrEmpty(dr[1].ToString()) && !string.IsNullOrEmpty(dr[2].ToString()) && int.TryParse(dr[1].ToString(), out a) && int.TryParse(dr[2].ToString(), out b))
                    {
                        tableWorkflow.Rows[tableWorkflow.Rows.IndexOf(dr)].SetField<string>(tableWorkflow.Columns[4], (Convert.ToInt32(dr[1].ToString()) * Convert.ToInt32(dr[2].ToString())).ToString());
                    }
                    if (!int.TryParse(dr[1].ToString(), out a) || !int.TryParse(dr[2].ToString(), out a))
                    {
                        tableWorkflow.Rows[tableWorkflow.Rows.IndexOf(dr)].SetField<string>(tableWorkflow.Columns[4], "Hibás adat!");

                        //CELLASZINEZÉS
                        //Style cellStyle = new Style(typeof(DataGridCell));
                        //cellStyle = new Style(typeof(DataGridCell));
                        //cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.Red));
                        //Mennyiseg_Column.CellStyle = cellStyle;

                    }
                    else
                    {

                        foreach (DataRow numbers in tableWorkflow.Rows)
                        {
                            sum += Convert.ToInt32(numbers[4]);
                            lblWork.Content = sum;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        private void grdConsumption_LostFocus(object sender, RoutedEventArgs e)
        {//Anyagdíj számolása
            foreach (DataRow dr in tableConsumption.Rows)
            {
                try
                {
                    int a;
                    int b;

                    if (!string.IsNullOrEmpty(dr[1].ToString()) && !string.IsNullOrEmpty(dr[2].ToString()) && int.TryParse(dr[1].ToString(), out a) && int.TryParse(dr[2].ToString(), out b))
                    {
                        tableConsumption.Rows[tableConsumption.Rows.IndexOf(dr)].SetField<string>(tableConsumption.Columns[3], (a * b).ToString());
                    }

                    if (!int.TryParse(dr[1].ToString(), out a) || !int.TryParse(dr[2].ToString(), out a))
                    {
                        tableConsumption.Rows[tableConsumption.Rows.IndexOf(dr)].SetField<string>(tableConsumption.Columns[3], "Hibás adat!");
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        { //Kilépés menü
            // MessageBox.Show("Biztos hogy kilépsz?", "Kilépés", MessageBoxButton.YesNo, MessageBoxImage.Information)
            DialogResult result = System.Windows.Forms.MessageBox.Show("Biztos hogy kilépsz?", "Kilépés", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        { //Új dokumentum menü
            DialogResult result = System.Windows.Forms.MessageBox.Show("Minden adat törölve lesz", "Új dokumentum", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //ALAPÉRTELMEZETT ADATOK
                txtTitle.Text = "MUNKALAP - ÁRAJÁNLAT";
                txtName.Text = "Malasits Gyula";
                txtLocation.Text = "2881 Ászár Báthory István utca 6";
                txtTelNumber.Text = "0620/33 91 562";
                txtEmail.Text = "adrige@vipmail.hu";
                txtCustomerEmail.Text = "";
                txtCustomerLocation.Text = "";
                txtCustomerName.Text = "";
                txtCustomerPhoneNumber.Text = "";
                tableWorkflow.Clear();
                tableConsumption.Clear();
                cbCustomer.IsChecked = false;
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //RegistryKey rk = Registry.CurrentUser.CreateSubKey("SpaceRiderLocalUser");
            //rk.SetValue("LocalUserLastEarned", lblTotal.Content);
            //int a = Convert.ToInt32(rk.GetValue("LocalUserMaxEarned"));
            //if (Convert.ToInt32(rk.GetValue("LocalUserMaxEarned")) < Convert.ToInt32(rk.GetValue("LocalUserLastEarned")))
            //    rk.SetValue("LocalUserMaxEarned", lblTotal.Content);
            //rk.Close();
            string savePath = "";

            FolderBrowserDialog opener = new FolderBrowserDialog();
            opener.ShowDialog();

            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Painter_Price_Offer");
            savePath = opener.SelectedPath;
            rk.SetValue("MentesHelye", savePath);
            rk.Close();
        }
    }
}
