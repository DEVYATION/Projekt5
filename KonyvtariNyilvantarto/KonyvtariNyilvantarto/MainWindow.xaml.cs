using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;

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

        public Kolcsonzok()
        {

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
        bool mentetlen = false;
        bool kolcsonzoIrSzamOK = false;

        public MainWindow()
        {
            InitializeComponent();
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
            Konyvek.DataContext = konyvek;
            Kolcsonzok.DataContext = kolcsonzok;
            Kolcsonzesek.DataContext = kolcsonzesek;
        }

        private void MindenMenteseButton_Click(object sender, RoutedEventArgs e)
        {
            MindenMentese();
        }

        private void Konyvek_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            konyvek[Konyvek.SelectedIndex] = (Konyvek)Konyvek.SelectedItem;
            KonyvekMenteseButton.IsEnabled = true;
            MindenMenteseButton.IsEnabled = true;
            mentetlen = true;
        }

        private void Konyvek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Konyvek.SelectedItems.Count != 0)
            {
                KonyvTorleseGomb.IsEnabled = true;
            }
            else
            {
                KonyvTorleseGomb.IsEnabled = false;
            }
        }

        private void KonyvSzerzoBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KonyvHozzaadasaEngedely();
        }

        private void KonyvCimBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KonyvHozzaadasaEngedely();
        }

        private void KonyvKiadasEveBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KonyvHozzaadasaEngedely();
        }

        private void KonyvKiadoBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KonyvHozzaadasaEngedely();
        }

        public void KonyvHozzaadasaEngedely()
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
            Konyvek.DataContext = null;
            konyvek.Add(new Konyvek() { KonyvID = konyvek[konyvek.Count - 1].KonyvID + 1, KonyvSzerzo = KonyvSzerzoBox.Text, KonyvCime = KonyvCimBox.Text, KonyvKiadasEve = KonyvKiadasEveBox.Text, KonyvKiado = KonyvKiadoBox.Text, KonyvKolcsonozheto = (bool)KonyvKolcsonozhetoCheck.IsChecked });
            Konyvek.DataContext = konyvek;
            KonyvSzerzoBox.Text = "";
            KonyvCimBox.Text = "";
            KonyvKiadasEveBox.Text = "";
            KonyvKiadoBox.Text = "";
            KonyvKolcsonozhetoCheck.IsChecked = true;
            KonyvHozzaadasaButton.IsEnabled = false;
            KonyvHozzaadasaMegseGomb.IsEnabled = false;
            KonyvekMenteseButton.IsEnabled = true;
            MindenMenteseButton.IsEnabled = true;
            mentetlen = true;

        }

        private void KonyvHozzaadasaMegseGomb_Click(object sender, RoutedEventArgs e)
        {
            KonyvSzerzoBox.Text = "";
            KonyvCimBox.Text = "";
            KonyvKiadasEveBox.Text = "";
            KonyvKiadoBox.Text = "";
            KonyvKolcsonozhetoCheck.IsChecked = true;
            KonyvHozzaadasaButton.IsEnabled = false;
            KonyvHozzaadasaMegseGomb.IsEnabled = false;
        }

        private void KonyvTorleseGomb_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Ezzel kitörli a kijelölt könyvet a listából. Ezt csak ebben az esetben tegye meg, ha a könyvet véglegesen eltávolították a könyvtárból.", "Figyelem!", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (mbr == MessageBoxResult.OK)
            {
                foreach (var item in Konyvek.SelectedItems)
                {
                    konyvek.Remove((Konyvek)item);
                }
                Konyvek.DataContext = null;
                Konyvek.DataContext = konyvek;
                KonyvekMenteseButton.IsEnabled = true;
                MindenMenteseButton.IsEnabled = true;
                mentetlen = true;
            }
        }

        private void KonyvekMenteseButton_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("./konyvek.txt");
            foreach (var item in konyvek)
            {
                sw.WriteLine(item.KonyvID + ";" + item.KonyvSzerzo + ";" + item.KonyvCime + ";" + item.KonyvKiadasEve + ";" + item.KonyvKiado + ";" + item.KonyvKolcsonozheto);
            }
            sw.Close();
            sw.Dispose();
            KonyvekMenteseButton.IsEnabled = false;
            MindenMenteseButton.IsEnabled = false;
            mentetlen = false;
        }

        private void Kolcsonzok_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            kolcsonzok[Kolcsonzok.SelectedIndex] = (Kolcsonzok)Kolcsonzok.SelectedItem;
            KolcsonzokMenteseButton.IsEnabled = true;
            MindenMenteseButton.IsEnabled = true;
            mentetlen = true;
        }

        private void Kolcsonzok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kolcsonzok.SelectedItems.Count != 0)
            {
                KolcsonzoTorleseButton.IsEnabled = true;
            }
            else
            {
                KolcsonzoTorleseButton.IsEnabled = false;
            }
        }

        private void KolcsonzoNevBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KolcsonzoHozzaadasaEngedely();
        }

        private void KolcsonzoSzuletesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KolcsonzoHozzaadasaEngedely();
        }

        private void KolcsonzoIranyitoszamBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KolcsonzoIranyitoszamBox.Text != "")
            {
                if (int.TryParse(KolcsonzoIranyitoszamBox.Text, out _))
                {
                    kolcsonzoIrSzamOK = true;
                }
                else
                {
                    kolcsonzoIrSzamOK = false;
                }
            }
            KolcsonzoHozzaadasaEngedely();
        }

        private void KolcsonzoTelepulesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KolcsonzoHozzaadasaEngedely();
        }

        private void KolcsonzoUtcaBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KolcsonzoHozzaadasaEngedely();
        }

        public void KolcsonzoHozzaadasaEngedely()
        {
            if (KolcsonzoNevBox.Text != "" && KolcsonzoSzuletesBox.Text != "" && KolcsonzoIranyitoszamBox.Text != "" && kolcsonzoIrSzamOK && KolcsonzoTelepulesBox.Text != "" && KolcsonzoUtcaBox.Text != "")
            {
                KolcsonzoFelvetelButton.IsEnabled = true;
            }
            else
            {
                KolcsonzoFelvetelButton.IsEnabled = false;
            }
            if (!(KolcsonzoNevBox.Text == "" && KolcsonzoSzuletesBox.Text == "" && KolcsonzoIranyitoszamBox.Text == "" && KolcsonzoTelepulesBox.Text == "" && KolcsonzoUtcaBox.Text == ""))
            {
                KolcsonzoFelvetelMegseButton.IsEnabled = true;
            }
            else
            {
                KolcsonzoFelvetelMegseButton.IsEnabled = false;
            }
        }

        private void KolcsonzoFelvetelButton_Click(object sender, RoutedEventArgs e)
        {
            Kolcsonzok.DataContext = null;
            kolcsonzok.Add(new Kolcsonzok() { OlvasoID = kolcsonzok[kolcsonzok.Count - 1].OlvasoID + 1, OlvasoNev = KolcsonzoNevBox.Text, OlvasoSzulDatum = KolcsonzoSzuletesBox.Text, OlvasoIranyitoSzam = KolcsonzoIranyitoszamBox.Text, OlvasoTelepules = KolcsonzoTelepulesBox.Text, OlvasoUtcaHazszam = KolcsonzoUtcaBox.Text });
            Kolcsonzok.DataContext = kolcsonzok;
            KolcsonzoNevBox.Text = "";
            KolcsonzoSzuletesBox.Text = "";
            KolcsonzoIranyitoszamBox.Text = "";
            KolcsonzoTelepulesBox.Text = "";
            KolcsonzoUtcaBox.Text = "";
            KolcsonzoFelvetelButton.IsEnabled = false;
            KolcsonzoFelvetelMegseButton.IsEnabled = false;
            KolcsonzokMenteseButton.IsEnabled = true;
            MindenMenteseButton.IsEnabled = true;
            mentetlen = true;
        }

        private void KolcsonzoFelvetelMegseButton_Click(object sender, RoutedEventArgs e)
        {
            KolcsonzoNevBox.Text = "";
            KolcsonzoSzuletesBox.Text = "";
            KolcsonzoIranyitoszamBox.Text = "";
            KolcsonzoTelepulesBox.Text = "";
            KolcsonzoUtcaBox.Text = ""; 
            KolcsonzoFelvetelButton.IsEnabled = false;
            KolcsonzoFelvetelMegseButton.IsEnabled = false;
        }

        private void KolcsonzoTorleseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Ezzel eltávolítja a kijelölt kölcsönzőt a listából. Ezt csak ebben az esetben tegye meg, ha a kölcsönző véglegesen kilépett a kölcsönzői programból.", "Figyelem!", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (mbr == MessageBoxResult.OK)
            {
                foreach (var item in Kolcsonzok.SelectedItems)
                {
                    kolcsonzok.Remove((Kolcsonzok)item);
                }
                Kolcsonzok.DataContext = null;
                Kolcsonzok.DataContext = kolcsonzok;
                KolcsonzokMenteseButton.IsEnabled = true;
                MindenMenteseButton.IsEnabled = true;
                mentetlen = true;
            }
        }

        private void KolcsonzoKolcsonzeseiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KolcsonzokMenteseButton_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("./tagok.txt");
            foreach (var item in kolcsonzok)
            {
                sw.WriteLine(item.OlvasoID + ";" + item.OlvasoNev + ";" + item.OlvasoSzulDatum + ";" + item.OlvasoIranyitoSzam + ";" + item.OlvasoTelepules + ";" + item.OlvasoUtcaHazszam);
            }
            sw.Close();
            sw.Dispose();
            KolcsonzokMenteseButton.IsEnabled = false;
            MindenMenteseButton.IsEnabled = false;
            mentetlen = false;
        }

        private void Kolcsonzesek_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void Kolcsonzesek_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            if (mentetlen == true)
            {
                MessageBoxResult mbr = MessageBox.Show("Nem mentette el a munkáját. Szeretné menteni a munkáját?", "Figyelem!", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
                if (mbr == MessageBoxResult.Yes)
                {
                    MindenMentese();
                }
                else if (mbr == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        public void MindenMentese()
        {

        }
    }
}
