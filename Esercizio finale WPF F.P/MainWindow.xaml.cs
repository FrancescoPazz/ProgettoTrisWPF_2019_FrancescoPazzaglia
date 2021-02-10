using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Esercizio_finale_WPF_F.P
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string inserimento;
        private void txtBoxInserimentoVoto_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ok;
            double voto = 0;
            inserimento = txtBoxInserimentoVoto.Text.Replace('.',',');
            if (!inserimento.EndsWith(",") && !inserimento.EndsWith(",0") && !inserimento.StartsWith("0"))
                ok = double.TryParse(inserimento, out voto);
            else
                ok = false;
            if (ok)
                if (voto % 0.5 == 0)
                    if (voto >= 1 && voto <= 10)
                        btnCarica.IsEnabled = true;
                    else
                        btnCarica.IsEnabled = false;
                else
                    btnCarica.IsEnabled = false;
            else
                btnCarica.IsEnabled = false;
        }
        private void btnCarica_Click(object sender, RoutedEventArgs e)
        {
            double tempLstVoto;
            int tempLstVolte;
            bool votoTrovato = false;
            txtStampaSuccessfull.Text = string.Empty;
            lstVoto.Items.Add(inserimento);
            btnStampa.IsEnabled = true;
            btnReset.IsEnabled = true;
            for (int i = 0; i < lstVoto.Items.Count - 1; i++)
                if (lstVoto.Items[i].ToString() == inserimento)
                {
                    lstVolte.Items[i] = (int)lstVolte.Items[i] + 1;
                    lstVoto.Items.Remove(inserimento);
                    votoTrovato = true;
                    break;
                }
            if (!votoTrovato)
                lstVolte.Items.Add(1);
            double[] vectVoti = new double[lstVoto.Items.Count];
            int[] vectVolte = new int[lstVolte.Items.Count];
            for (int i = 0; i < lstVoto.Items.Count; i++)
                vectVoti[i] = double.Parse(lstVoto.Items[i].ToString());
            for (int i = 0; i < lstVolte.Items.Count; i++)
                vectVolte[i] = int.Parse(lstVolte.Items[i].ToString());
            lstVoto.Items.Clear();
            lstVolte.Items.Clear();
            for (int i = 0; i < vectVoti.Length-1; i++)
                for (int j = 0; j < vectVoti.Length - 1; j++)
                    if (vectVoti[j] > vectVoti[j + 1])
                    {
                        tempLstVoto = vectVoti[j];
                        vectVoti[j] = vectVoti[j + 1];
                        vectVoti[j + 1] = tempLstVoto;
                        tempLstVolte = vectVolte[j];
                        vectVolte[j] = vectVolte[j + 1];
                        vectVolte[j + 1] = tempLstVolte;
                    }
            for (int i = 0; i < vectVoti.Length; i++)
                lstVoto.Items.Add(vectVoti[i]);
            for (int i = 0; i < vectVolte.Length; i++)
                lstVolte.Items.Add(vectVolte[i]);
            txtBoxInserimentoVoto.Clear();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lstVoto.Items.Clear();
            lstVolte.Items.Clear();
            txtStampaSuccessfull.Text = string.Empty;
            btnStampa.IsEnabled = false;
            btnReset.IsEnabled = false;
        }
        private void btnStampa_Click(object sender, RoutedEventArgs e)
        {
            btnStampa.IsEnabled = false;
            StreamWriter scriviFile = new StreamWriter("RiepilogoVoti.txt");
            txtStampaSuccessfull.Text = "Scrittura avvenuta con successo!";
            scriviFile.WriteLine("Voto:" + "Numero volte:".PadLeft(20));
            for (int i = 0; i < lstVoto.Items.Count; i++)
                scriviFile.WriteLine($"{lstVoto.Items[i]}" + $"{lstVolte.Items[i]}".PadLeft(20));
            scriviFile.Close();
        }
    }
}
