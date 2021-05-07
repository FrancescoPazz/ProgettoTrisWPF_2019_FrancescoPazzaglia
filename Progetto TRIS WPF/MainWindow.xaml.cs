﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Progetto_TRIS_WPF
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        //VARIABILI
        //
        Button[] bottoniGriglia = new Button[9];
        bool pareggio = false;
        int turno = 0;
        int cont = 0;
        string segno = string.Empty;
        Random rnd = new Random();
        string[] coloriGriglia = { "Default", "Giallo", "Verde", "Arancione", "Rosa" };
        Socket connessione;
        bool connesso = false;
        bool fineConnessione = false;
        NetworkStream stream;
        TcpClient client;
        StreamReader ascolta;
        string rispostaServer = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            AssegnaBottoniGriglia();
            for (int i = 0; i < coloriGriglia.Length; i++)
            {
                cmbSceltaColoreGriglia.Items.Add(coloriGriglia[i]);
            }
            tbkTestoModalita.Text = "Puntare il mouse sopra di un bottone delle tre modalità per scoprire come funzionano!";
        }
        //
        //INIZIO CON SCELTA MODALITA'
        //
        private void btnModalita1_Click(object sender, RoutedEventArgs e)
        {
            ScomparsaMenu();
            RendiVisibileMod1();
            btnIndietro.Content = "Torna al menù iniziale";
            btnIndietro.Visibility = Visibility.Visible;
            btnIndietro.IsEnabled = true;
            client = new TcpClient("127.0.0.1", 56000);
            stream = client.GetStream();
            Thread receive = new Thread(new ParameterizedThreadStart(SocketReceive));
            receive.Start();
            Invia("RQCN");
            fineConnessione = false;
        }
        private void btnGriglia_Click(object sender, RoutedEventArgs e)
        {
            txtTurni.Text = "";
            txtTurni.Text = $"E' il turno dell'avversario";
            switch (((Button)sender).Name.Substring(3, 1))
            {
                case "1":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "2":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "3":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "4":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "5":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "6":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "7":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "8":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
                case "9":
                    {
                        InserimentoSegniConTurni(sender);
                        GiocateMod1(sender);
                        break;
                    }
            }
            AssegnaBottoniGriglia();
            Invia(InviaCampoDiGioco());
            DisabilitaGriglia();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult risultato = MessageBox.Show("Sicuro di voler chiudere la finestra?", "ATTENZIONE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (risultato == MessageBoxResult.Yes && connesso)
            {
                Invia("TRMN");
                connesso = false;
            }
            else if(risultato == MessageBoxResult.No)
                e.Cancel = true;
        }
        //
        //BOTTONI FUNZIONALI
        //
        private void btnOKMod1_Click(object sender, RoutedEventArgs e)
        {
            cont = 0;
            Invia("RQOK");
            tbkAttesa.Visibility = Visibility.Visible;
            tbkAttesa.Text = "In attesa dell'avversario. . .";
            btnOKMod1.IsEnabled = false;
        }
        private void btnIndietro_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Sicuro di voler chiudere la connessione?", "ATTENZIONE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                fineConnessione = true;
                Invia("TRMN");
                stream.Close();
                client.Close();
                ScomparsaGriglia();
                SvuotaGriglia();
                ScomparsaMod1();
                AbilitaGriglia();
                txtTurni.Text = string.Empty;
                RendiVisibileMenu();
                pareggio = false;
                btnOKMod1.IsEnabled = false;
                tbkAttesa.Text = string.Empty;
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Riniziare la partita?", "AVVISO", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SvuotaGriglia();
                AbilitaGriglia();
                Invia("RST");
                pareggio = false;
                if (turno == 1)
                {
                    txtTurni.Text = $"Sei stato sorteggiato per primo!";
                    AbilitaGriglia();
                }
                else
                {
                    txtTurni.Text = $"L'avversario stato sorteggiato per primo!";
                    DisabilitaGriglia();
                }
                ScomparsaMod1();
                RendiVisibileGriglia();
                btnReset.IsEnabled = false;
            }
        }
        private void cmbSceltaColoreGriglia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBox)sender).SelectedItem)
            {
                case "Giallo":
                    {
                        CambiaColoreGriglia(Colors.Yellow);
                        break;
                    }
                case "Verde":
                    {
                        CambiaColoreGriglia(Colors.GreenYellow);
                        break;
                    }
                case "Arancione":
                    {
                        CambiaColoreGriglia(Colors.LightSalmon);
                        break;
                    }
                case "Rosa":
                    {
                        CambiaColoreGriglia(Colors.LightPink);
                        break;
                    }
                case "Default":
                    {
                        CambiaColoreGriglia(Colors.White);
                        break;
                    }
            }
        }
        //
        //METODI DI COMPARSA O SCOMPARSA E GESTIONI
        //
        private string InviaCampoDiGioco()
        {
            string messaggio = string.Empty;
            for (int i = 0; i < bottoniGriglia.Length; i++)
            {
                if (bottoniGriglia[i] != null)
                    if (bottoniGriglia[i].Content == null)
                        messaggio += "null,";
                    else
                        messaggio += bottoniGriglia[i].Content + ",";
            }
            messaggio += $"{turno},{segno}";
            return messaggio;
        }
        private void AssegnaBottoniGriglia()
        {
            bottoniGriglia[0] = btn1mat;
            bottoniGriglia[1] = btn2mat;
            bottoniGriglia[2] = btn3mat;
            bottoniGriglia[3] = btn4mat;
            bottoniGriglia[4] = btn5mat;
            bottoniGriglia[5] = btn6mat;
            bottoniGriglia[6] = btn7mat;
            bottoniGriglia[7] = btn8mat;
            bottoniGriglia[8] = btn9mat;
        }
        private void ScomparsaMenu()
        {
            btnModalita1.Visibility = Visibility.Hidden;
            tbkSceltaMod.Visibility = Visibility.Hidden;
            tbkTitolo.Visibility = Visibility.Hidden;
            tbkSceltaColoreGriglia.Visibility = Visibility.Hidden;
            cmbSceltaColoreGriglia.Visibility = Visibility.Hidden;
            tbkTestoModalita.Visibility = Visibility.Hidden;
            btnModalita1.IsEnabled = false;
        }
        private void RendiVisibileMenu()
        {
            btnModalita1.Visibility = Visibility.Visible;
            tbkSceltaMod.Visibility = Visibility.Visible;
            tbkTitolo.Visibility = Visibility.Visible;
            tbkSceltaColoreGriglia.Visibility = Visibility.Visible;
            cmbSceltaColoreGriglia.Visibility = Visibility.Visible;
            tbkTestoModalita.Visibility = Visibility.Visible;
            btnModalita1.IsEnabled = true;
        }
        private void RendiVisibileGriglia()
        {
            btn1mat.Visibility = Visibility.Visible;
            btn2mat.Visibility = Visibility.Visible;
            btn3mat.Visibility = Visibility.Visible;
            btn4mat.Visibility = Visibility.Visible;
            btn5mat.Visibility = Visibility.Visible;
            btn6mat.Visibility = Visibility.Visible;
            btn7mat.Visibility = Visibility.Visible;
            btn8mat.Visibility = Visibility.Visible;
            btn9mat.Visibility = Visibility.Visible;
            txtTurni.Visibility = Visibility.Visible;
            btnReset.Visibility = Visibility.Visible;
        }
        private void SvuotaGriglia()
        {
            btn1mat.Content = null;
            btn2mat.Content = null;
            btn3mat.Content = null;
            btn4mat.Content = null;
            btn5mat.Content = null;
            btn6mat.Content = null;
            btn7mat.Content = null;
            btn8mat.Content = null;
            btn9mat.Content = null;
        }
        private void ScomparsaGriglia()
        {
            btn1mat.Visibility = Visibility.Hidden;
            btn2mat.Visibility = Visibility.Hidden;
            btn3mat.Visibility = Visibility.Hidden;
            btn4mat.Visibility = Visibility.Hidden;
            btn5mat.Visibility = Visibility.Hidden;
            btn6mat.Visibility = Visibility.Hidden;
            btn7mat.Visibility = Visibility.Hidden;
            btn8mat.Visibility = Visibility.Hidden;
            btn9mat.Visibility = Visibility.Hidden;
            txtTurni.Visibility = Visibility.Hidden;
            btnIndietro.Visibility = Visibility.Hidden;
            btnReset.Visibility = Visibility.Hidden;
        }
        private void CambiaColoreGriglia(Color colore)
        {
            btn1mat.Background = new SolidColorBrush(colore);
            btn2mat.Background = new SolidColorBrush(colore);
            btn3mat.Background = new SolidColorBrush(colore);
            btn4mat.Background = new SolidColorBrush(colore);
            btn5mat.Background = new SolidColorBrush(colore);
            btn6mat.Background = new SolidColorBrush(colore);
            btn7mat.Background = new SolidColorBrush(colore);
            btn8mat.Background = new SolidColorBrush(colore);
            btn9mat.Background = new SolidColorBrush(colore);
        }
        private void RendiVisibileMod1()
        {
            tbkAttesa.Visibility = Visibility.Visible;
            btnOKMod1.Visibility = Visibility.Visible;
        }
        private void ScomparsaMod1()
        {
            tbkAttesa.Visibility = Visibility.Hidden;
            btnOKMod1.Visibility = Visibility.Hidden;
        }
        private void AbilitaGriglia()
        {
            for (int i = 0; i < bottoniGriglia.Length; i++)
                if (bottoniGriglia[i] != null)
                    if (bottoniGriglia[i].Content == null)
                        bottoniGriglia[i].IsHitTestVisible = true;
        }
        private void DisabilitaGriglia()
        {
            btn1mat.IsHitTestVisible = false;
            btn2mat.IsHitTestVisible = false;
            btn3mat.IsHitTestVisible = false;
            btn4mat.IsHitTestVisible = false;
            btn5mat.IsHitTestVisible = false;
            btn6mat.IsHitTestVisible = false;
            btn7mat.IsHitTestVisible = false;
            btn8mat.IsHitTestVisible = false;
            btn9mat.IsHitTestVisible = false;
        }
        //
        //METODI PER I CONTROLLI
        //
        private void InserimentoSegniConTurni(object sender)
        {
            if (segno == "X")
            {
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Red);
                ((Button)sender).Content = "X";
            }
            else if (segno == "O")
            {
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Blue);
                ((Button)sender).Content = "O";
            }
            ((Button)sender).IsHitTestVisible = false;
        }
        private int SceltaTurno()
        {
            return rnd.Next(1, 3);
        }
        private string SceltaSegno()
        {
            int n = rnd.Next(1, 3);
            if (n == 1)
            {
                return "X";
            }
            else
            {
                return "O";
            }
        }
        private bool TrovaTris(string segno)
        {
            if (((string)btn1mat.Content == segno && (string)btn2mat.Content == segno && (string)btn3mat.Content == segno) || ((string)btn4mat.Content == segno && (string)btn5mat.Content == segno && (string)btn6mat.Content == segno) || ((string)btn7mat.Content == segno && (string)btn8mat.Content == segno && (string)btn9mat.Content == segno))
            {
                DisabilitaGriglia();
                return true;
            }
            if (((string)btn1mat.Content == segno && (string)btn4mat.Content == segno && (string)btn7mat.Content == segno) || ((string)btn2mat.Content == segno && (string)btn5mat.Content == segno && (string)btn8mat.Content == segno) || ((string)btn3mat.Content == segno && (string)btn6mat.Content == segno && (string)btn9mat.Content == segno))
            {
                DisabilitaGriglia();
                return true;
            }
            if (((string)btn1mat.Content == segno && (string)btn5mat.Content == segno && (string)btn9mat.Content == segno) || ((string)btn3mat.Content == segno && (string)btn5mat.Content == segno && (string)btn7mat.Content == segno))
            {
                DisabilitaGriglia();
                return true;
            }
            return false;
        }
        private void ControlloPareggio()
        {
            if (btn1mat.Content != null && btn2mat.Content != null && btn3mat.Content != null && btn4mat.Content != null && btn5mat.Content != null && btn6mat.Content != null && btn7mat.Content != null && btn8mat.Content != null && btn9mat.Content != null)
            {
                txtTurni.Text = "PAREGGIO";
                pareggio = true;
                btnReset.IsEnabled = true;
            }
        }
        private void GiocateMod1(object sender)
        {
            if (TrovaTris("X"))
            {
                if (segno == "X")
                    txtTurni.Text = $"Hai vinto!";
                else
                    txtTurni.Text = $"Hai perso!";
                btnReset.IsEnabled = true;
            }
            else if (TrovaTris("O"))
            {
                if (segno == "O")
                    txtTurni.Text = $"Hai vinto!";
                else
                    txtTurni.Text = $"Hai perso!";
                btnReset.IsEnabled = true;
            }
            else
                ControlloPareggio();
        }
        public async void SocketReceive(object p)
        {
            Byte[] byteRicevuti = new Byte[256];
            ascolta = new StreamReader(stream);
            await Task.Run(() =>
            {
                while (!fineConnessione)
                {
                    if (stream.DataAvailable)
                    {
                        rispostaServer = ascolta.ReadLine();
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            AggiornaGrigliaOrName(rispostaServer);
                        }));
                    }
                }
            });
        }
        public void Invia(string message)
        {
            Byte[] bytesended = Encoding.ASCII.GetBytes(message);
            stream = client.GetStream();
            stream.Write(bytesended, 0, bytesended.Length);
        }
        public void AggiornaGrigliaOrName(string messaggio)
        {
            //Invio tabella di gioco
            if (messaggio.Contains(','))
            {
                string[] griglia = messaggio.Split(',');
                for (int i = 0; i < bottoniGriglia.Length; i++)
                {
                    if (griglia[i] != "null" || int.TryParse(griglia[i], out int n))
                    {
                        bottoniGriglia[i].Content = griglia[i];
                        if (griglia[i] == "X")
                            bottoniGriglia[i].Foreground = new SolidColorBrush(Colors.Red);
                        else if (griglia[i] == "O")
                            bottoniGriglia[i].Foreground = new SolidColorBrush(Colors.Blue);
                        bottoniGriglia[i].IsHitTestVisible = false;
                    }
                }
                AbilitaGriglia();
                txtTurni.Text = "E' il tuo turno";
                if (TrovaTris("X"))
                {
                    if (segno == "X")
                        txtTurni.Text = $"Hai vinto!";
                    else
                        txtTurni.Text = $"Hai perso!";
                    btnReset.IsEnabled = true;
                }
                else if (TrovaTris("O"))
                {
                    if (segno == "O")
                        txtTurni.Text = $"Hai vinto!";
                    else
                        txtTurni.Text = $"Hai perso!";
                    btnReset.IsEnabled = true;
                }
                else
                {
                    ControlloPareggio();
                }
            }
            //Sincronizzazione iniziale del turno e del segno, tramite dei messaggi di riscontro.
            else if (messaggio.Contains("RQCN"))
            {
                if (!connesso || cont <= 3)
                {
                    connesso = true;
                    if (cont == 0)
                        Invia("RQCN");
                    if (cont == 1)
                    {
                        turno = SceltaTurno();
                        Invia(turno.ToString()+"RQCN");
                    }
                    if (cont == 2)
                    {
                        segno = SceltaSegno();
                        Invia(segno.ToString()+"RQCN");
                        if(int.TryParse(messaggio[0].ToString(), out int s))
                        {
                            if (int.Parse(messaggio[0].ToString()) == 1)
                                turno = 2;
                            else if (int.Parse(messaggio[0].ToString()) == 2)
                                turno = 1;
                        }
                    }
                    if(cont == 3)
                    {
                        if (messaggio[0] == 'O')
                            segno = "X";
                        else if (messaggio[0] == 'X')
                            segno = "O";
                        Invia("RQCN");
                    }
                }
                if (cont == 4)
                {
                    btnOKMod1.IsEnabled = true;
                    Invia("RQCN");
                }
                cont++;
            }
            else if (messaggio == "TRMN")
            {
                fineConnessione = true;
                Invia("TRMN");
                stream.Close();
                client.Close();
                ScomparsaGriglia();
                SvuotaGriglia();
                ScomparsaMod1();
                AbilitaGriglia();
                txtTurni.Text = "";
                RendiVisibileMenu();
                pareggio = false;
                btnOKMod1.IsEnabled = false;
                tbkAttesa.Text = string.Empty;
            }
            else if (messaggio == "RST")
            {
                SvuotaGriglia();
                AbilitaGriglia();
                pareggio = false;
                if (turno == 1)
                {
                    txtTurni.Text = $"Sei stato sorteggiato per primo!";
                    AbilitaGriglia();
                }
                else
                {
                    txtTurni.Text = $"L'avversario stato sorteggiato per primo!";
                    DisabilitaGriglia();
                }
                ScomparsaMod1();
                RendiVisibileGriglia();
                btnReset.IsEnabled = false;
            }
            else if(messaggio == "RQOK")
            {
                if(cont == 0)
                {
                    Invia("RQOK");
                    if (turno == 1)
                    {
                        txtTurni.Text = $"Sei stato sorteggiato per primo!";
                        AbilitaGriglia();
                    }
                    else
                    {
                        txtTurni.Text = $"L'avversario stato sorteggiato per primo!";
                        DisabilitaGriglia();
                    }
                    ScomparsaMod1();
                    RendiVisibileGriglia();
                    btnIndietro.Content = "Torna al menù e chiudi socket";
                    tbkAttesa.Visibility = Visibility.Hidden;
                    cont++;
                }
            }
        }
    }
}