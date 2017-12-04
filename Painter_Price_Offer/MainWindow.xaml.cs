using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using Painter_Price_Offer.Models;

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

        /// <summary>
        /// Deffiniálja a mentési útvonalat a registry ben
        /// </summary>
        /// <param name="saveRoute">string</param>
        private void WriteSavePath(string saveRoute)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Painter_Price_Offer");
            rk.SetValue("SavePath", saveRoute);
            rk.Close();

            //RegistryKey rk = Registry.CurrentUser.CreateSubKey("SpaceRiderLocalUser");
            //rk.SetValue("LocalUserLastEarned", lblTotal.Content);
            //int a = Convert.ToInt32(rk.GetValue("LocalUserMaxEarned"));
            //if (Convert.ToInt32(rk.GetValue("LocalUserMaxEarned")) < Convert.ToInt32(rk.GetValue("LocalUserLastEarned")))
            //    rk.SetValue("LocalUserMaxEarned", lblTotal.Content);
            //rk.Close();
        }

        private string ReadSavePath()
        {
            string ret = "";
            try
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Painter_Price_Offer");
                ret = rk.GetValue("SavePath").ToString();
                rk.Close();
            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message, "Hiba a beolvasásnál", MessageBoxButton.OK, MessageBoxImage.Error);
                ret = "";
            }

            return ret;
        }

        public MainWindow()
        {
            InitializeComponent();

            //Munkafolyamat adattábla generálása
            tableWorkflow.Columns.Add("Megnevezés", typeof(string));
            tableWorkflow.Columns.Add("Mennyiség", typeof(string));
            tableWorkflow.Columns.Add("Egységár", typeof(string));
            tableWorkflow.Columns.Add("Fmm2", typeof(string));
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
            txtLocation.Text = "2881 Ászár Báthory István utca 11";
            txtTelNumber.Text = "+3620/42 45 121";
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
                    int c;
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
                            if(!string.IsNullOrEmpty(numbers[4].ToString()) && int.TryParse(numbers[4].ToString(), out c))
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
                    int c;
                    int sum = 0;

                    if (!string.IsNullOrEmpty(dr[1].ToString()) && !string.IsNullOrEmpty(dr[2].ToString()) && int.TryParse(dr[1].ToString(), out a) && int.TryParse(dr[2].ToString(), out b))
                    {
                        tableConsumption.Rows[tableConsumption.Rows.IndexOf(dr)].SetField<string>(tableConsumption.Columns[3], (a * b).ToString());
                    }

                    if (!int.TryParse(dr[1].ToString(), out a) || !int.TryParse(dr[2].ToString(), out a))
                    {
                        tableConsumption.Rows[tableConsumption.Rows.IndexOf(dr)].SetField<string>(tableConsumption.Columns[3], "Hibás adat!");
                    }
                    else
                    {
                        foreach (DataRow numbers in tableConsumption.Rows)
                        {
                            if (!string.IsNullOrEmpty(numbers[3].ToString()) && int.TryParse(numbers[3].ToString(), out c))
                                sum += Convert.ToInt32(numbers[3]);
                            lblMaterial.Content = sum;
                        }
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
            else
                Window.Activate();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        { //Új dokumentum menü
            DialogResult result = System.Windows.Forms.MessageBox.Show("Minden adat törölve lesz", "Új dokumentum", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                txtTitle.Focus();
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
                Window.Activate();
            }
            else
                Window.Activate();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog opener = new FolderBrowserDialog();
            opener.ShowDialog();

            if (!string.IsNullOrEmpty(opener.SelectedPath))
                WriteSavePath(opener.SelectedPath);
            else
                WriteSavePath("$");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            grdConsumption_LostFocus(null, null);
            txtTitle.Focus();
            DataToPDF data = null; //adatfeldolgozás
            OwnerModel owner = new OwnerModel(); //Tulajdonos
            CustommerModel customer = new CustommerModel(); //Megrendelő
            List<WorkflowModel> workflow = new List<WorkflowModel>(); //Munka
            List<ConsuptionModel> consuption = new List<ConsuptionModel>(); //Fogyasztás, anyag
            string savePath = ""; //Mentés helye
            int wSum = 0; //Munkadíj össz
            int cSum = 0; //Anyagdíj össz
            int totalSum = 0; //Végösszeg

            //Tulajdonos adatai
            owner._title = txtTitle.Text;
            owner._name = txtName.Text;
            owner._location = txtLocation.Text;
            owner._phoneNumber = txtTelNumber.Text;
            owner._email = txtEmail.Text;

            //Megrendelő adatai
            customer._isActive = Convert.ToBoolean(cbCustomer.IsChecked);
            customer._name = txtCustomerName.Text;
            customer._location = txtCustomerLocation.Text;
            customer._phoneNumber = txtCustomerPhoneNumber.Text;
            customer._email = txtCustomerEmail.Text;

            //Munkadíj adatok
            foreach(DataRow dr in tableWorkflow.Rows)
            {
                workflow.Add(new WorkflowModel()
                {
                    _Megnevezés = dr["Megnevezés"].ToString(),
                    _Mennyiség = dr["Mennyiség"].ToString(),
                    _Egységár = dr["Egységár"].ToString(),
                    _Fm_m2 = dr["fmm2"].ToString(),
                    _Munkadíj = dr["Munkadíj"].ToString()

            });
                                
            }

            //Anyagdíj adatok
            foreach (DataRow dr in tableConsumption.Rows)
            {
                consuption.Add(new ConsuptionModel()
                {
                    _Megnevezés = dr["Megnevezés"].ToString(),
                    _Mennyiség = dr["Mennyiség"].ToString(),
                    _Egységár = dr["Egységár"].ToString(),
                    _Anyagdíj = dr["Anyagdíj"].ToString()
                });
            }

            //Mentés helyének lekérése
            savePath = ReadSavePath();

            //Összegek
            if(!(tableWorkflow.Rows.Count == 0))
            {
                wSum = int.Parse(lblWork.Content.ToString());
            }
            else
            {
                lblWork.Content = "0";
            }
            if (!(tableConsumption.Rows.Count == 0))
            {
                cSum = int.Parse(lblMaterial.Content.ToString());
            }
            else
            {
                lblMaterial.Content = "0";
            }
            totalSum = 0;

            //Adatok átadása nyomtatásra
            data = new DataToPDF(owner, customer, workflow, consuption, savePath, wSum, cSum, totalSum);

            //Adatok nyomtatása
            data.Print();
        }

    }
}