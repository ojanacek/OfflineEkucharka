using System.Windows;
using System.Windows.Controls;

namespace OfflineKucharka.Controls
{
    public partial class DetailReceptu : UserControl
    {
        public Recept VybranyRecept
        {
            get { return (Recept)GetValue(VybranyReceptProperty); }
            set { SetValue(VybranyReceptProperty, value); }
        }        
        public static readonly DependencyProperty VybranyReceptProperty = DependencyProperty.Register("VybranyRecept", typeof(Recept), typeof(DetailReceptu));
        
        public DetailReceptu(Recept recept)
        {
            VybranyRecept = recept;
            InitializeComponent();
        }
    }
}
