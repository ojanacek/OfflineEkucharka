using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace OfflineKucharka.Controls
{
    public partial class SeznamReceptu : UserControl
    {
        public event Action<string> KliknutiNaObrazek;

        public SeznamReceptu()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Image obrazek = (Image)sender;            
            KliknutiNaObrazek(obrazek.Tag.ToString());
        }
    }
}
