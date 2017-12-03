using System;
using System.Collections.Generic;
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

namespace Painter_Price_Offer
{
    /// <summary>
    /// Interaction logic for Title.xaml
    /// </summary>
    public partial class Title : Window
    {
        public string _title { get; set; }

        public Title()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            _title = txtTitle.Text;
            this.Close();
        }
    }
}
