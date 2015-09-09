using OfflineKucharka.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OfflineKucharka
{
    public partial class MainWindow : Window
    {
        private readonly List<Recept> vsechnyRecepty;
        private readonly SeznamReceptu seznamReceptu = new SeznamReceptu();

        public ListCollectionView FiltrovaneRecepty { get { return (ListCollectionView)GetValue(FiltrovaneReceptyProperty); } set { SetValue(FiltrovaneReceptyProperty, value); } }
        public static readonly DependencyProperty FiltrovaneReceptyProperty = DependencyProperty.Register("FiltrovaneRecepty", typeof(ListCollectionView), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            vsechnyRecepty = PraceSeSoubory.NacistRecepty().ToList();
            FiltrovaneRecepty = new ListCollectionView(vsechnyRecepty);
            ZobrazSeznamReceptu();
            seznamReceptu.KliknutiNaObrazek += SeznamReceptu_KliknutiNaObrazek;
        }

        private void SeznamReceptu_KliknutiNaObrazek(string nazevReceptu)
        {            
            Recept vybranyRecept = vsechnyRecepty.FirstOrDefault(r => r.Nazev == nazevReceptu);
            PromennaPlocha.Content = new DetailReceptu(vybranyRecept);
        }

        private void VsechnyRecepty_Click(object sender, RoutedEventArgs e)
        {
            FiltrovaneRecepty = new ListCollectionView(vsechnyRecepty);
            ZobrazSeznamReceptu();
        }

        private void ZobrazSeznamReceptu()
        {
            PromennaPlocha.Content = seznamReceptu;
        }

        private void hledanyVyraz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FiltrovaneRecepty.Filter = o => (o as Recept).Nazev.IndexOf(hledanyVyraz.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                ZobrazSeznamReceptu();
                hledanyVyraz.Text = "";
            }
        }
    }
}
