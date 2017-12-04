using System.Windows;
using System.Windows.Input;

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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txtTitle.Focus();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnOk_Click(null, null);
        }
    }
}
