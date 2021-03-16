using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace KonyvtariNyilvantarto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    class Konyvek
    {
        public int KonyvID { get; set; }
        public string KonyvSzerzo { get; set; }
        public string KonyvCime { get; set; }
        public string KonyvKiadasEve { get; set; }
        public string KonyvKiado { get; set; }
        public bool KonyvKolcsonozheto { get; set; }

        public Konyvek(string sor)
        {
            string[] resz = sor.Split(';');
            KonyvID = Convert.ToInt32(resz[0]);
            KonyvSzerzo = resz[1];
            KonyvCime = resz[2];
            KonyvKiadasEve = resz[3];
            KonyvKiado = resz[4];
            if (resz[5] == "True")
            {
                KonyvKolcsonozheto = true;
            }
            else
            {
                KonyvKolcsonozheto = false;
            }
        }

        public Konyvek()
        {
        }
    }

    class Kolcsonzok
    {
        public int OlvasoID { get; set; }
        public string OlvasoNev { get; set; }
        public string OlvasoSzulDatum { get; set; }
        public string OlvasoIranyitoSzam { get; set; }
        public string OlvasoTelepules { get; set; }
        public string OlvasoUtcaHazszam { get; set; }

        public Kolcsonzok(string sor)
        {
            string[] resz = sor.Split(';');
            OlvasoID = Convert.ToInt32(resz[0]);
            OlvasoNev = resz[1];
            OlvasoSzulDatum = resz[2];
            OlvasoIranyitoSzam = resz[3];
            OlvasoTelepules = resz[4];
            OlvasoUtcaHazszam = resz[5];
        }
    }

    class Kolcsonzesek
    {
        public int KolcsonzesID { get; set; }
        public int OlvasoID { get; set; }
        public int KonyvID { get; set; }
        public string KolcsonzesDatuma { get; set; }
        public string VisszavetelDatuma { get; set; }

        public Kolcsonzesek(string sor)
        {
            string[] resz = sor.Split(';');
            KolcsonzesID = Convert.ToInt32(resz[0]);
            OlvasoID = Convert.ToInt32(resz[1]);
            KonyvID = Convert.ToInt32(resz[2]);
            KolcsonzesDatuma = resz[3];
            VisszavetelDatuma = resz[4];
        }
    }

    public partial class MainWindow : Window
    {
        List<Konyvek> konyvek = new List<Konyvek>();
        List<Kolcsonzok> kolcsonzok = new List<Kolcsonzok>();
        List<Kolcsonzesek> kolcsonzesek = new List<Kolcsonzesek>();

        public MainWindow()
        {
            InitializeComponent();
            Konyvek.DataContext = konyvek;
            Kolcsonzok.DataContext = kolcsonzok;
            Kolcsonzesek.DataContext = kolcsonzesek;

            foreach (var item in File.ReadAllLines("konyvek.txt"))
            {
                konyvek.Add(new Konyvek(item));
            }
            foreach (var item in File.ReadAllLines("tagok.txt"))
            {
                kolcsonzok.Add(new Kolcsonzok(item));
            }
            foreach (var item in File.ReadAllLines("kolcsonzesek.txt"))
            {
                kolcsonzesek.Add(new Kolcsonzesek(item));
            }
        }

        private void Konyvek_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MindenMenteseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Konyvek_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void KonyvSzerzoBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(KonyvSzerzoBox.Text == "" && KonyvCimBox.Text == "" && KonyvKiadasEveBox.Text == "" && KonyvKiadoBox.Text == ""))
            {
                KonyvHozzaadasaButton.IsEnabled = true;
                KonyvHozzaadasaMegseGomb.IsEnabled = true;
            }
            else
            {
                KonyvHozzaadasaButton.IsEnabled = false;
                KonyvHozzaadasaMegseGomb.IsEnabled = false;
            }
        }

        private void KonyvCimBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(KonyvSzerzoBox.Text == "" && KonyvCimBox.Text == "" && KonyvKiadasEveBox.Text == "" && KonyvKiadoBox.Text == ""))
            {
                KonyvHozzaadasaButton.IsEnabled = true;
                KonyvHozzaadasaMegseGomb.IsEnabled = true;
            }
            else
            {
                KonyvHozzaadasaButton.IsEnabled = false;
                KonyvHozzaadasaMegseGomb.IsEnabled = false;
            }
        }

        private void KonyvKiadasEveBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(KonyvSzerzoBox.Text == "" && KonyvCimBox.Text == "" && KonyvKiadasEveBox.Text == "" && KonyvKiadoBox.Text == ""))
            {
                KonyvHozzaadasaButton.IsEnabled = true;
                KonyvHozzaadasaMegseGomb.IsEnabled = true;
            }
            else
            {
                KonyvHozzaadasaButton.IsEnabled = false;
                KonyvHozzaadasaMegseGomb.IsEnabled = false;
            }
        }

        private void KonyvKiadoBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(KonyvSzerzoBox.Text == "" && KonyvCimBox.Text == "" && KonyvKiadasEveBox.Text == "" && KonyvKiadoBox.Text == ""))
            {
                KonyvHozzaadasaButton.IsEnabled = true;
                KonyvHozzaadasaMegseGomb.IsEnabled = true;
            }
            else
            {
                KonyvHozzaadasaButton.IsEnabled = false;
                KonyvHozzaadasaMegseGomb.IsEnabled = false;
            }
        }

        private void KonyvHozzaadasaButton_Click(object sender, RoutedEventArgs e)
        {
            konyvek.Add(new Konyvek() { KonyvID = konyvek[konyvek.Count - 1].KonyvID + 1, KonyvSzerzo = KonyvSzerzoBox.Text, KonyvCime = KonyvCimBox.Text, KonyvKiadasEve = KonyvKiadasEveBox.Text, KonyvKiado = KonyvKiadoBox.Text, KonyvKolcsonozheto = (bool)KonyvKolcsonozhetoCheck.IsChecked });
        }

        private void KonyvHozzaadasaMegseGomb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KonyvTorleseGomb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KonyvekMenteseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Kolcsonzok_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Kolcsonzok_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void KolcsonzoNevBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzoSzuletesBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzoIranyitoszamBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzoTelepulesBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzoUtcaBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzoFelvetelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzoFelvetelMegseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzoTorleseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzoKolcsonzeseiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzokMenteseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Kolcsonzesek_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Kolcsonzesek_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void KolcsonzesKonyvIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzesKolcsonzoIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzesFelveteleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzesFelveteleMegseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzesTorleseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzesekMenteseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KonyvekRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CimRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SzerzoRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void KonyvekKeresesBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KolcsonzokRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzokKeresesBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KeresesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
