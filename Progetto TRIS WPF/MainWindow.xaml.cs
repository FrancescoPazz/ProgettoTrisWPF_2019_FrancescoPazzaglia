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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net.Sockets;
using System.Net;

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
        string utente1, utente2;
        bool ok1, ok2;
        Button[] bottoniGriglia = new Button[9];
        static int cont = -1;
        int turno = 0;
        Random rnd = new Random();
        string[] coloriGriglia = { "Default", "Giallo", "Verde", "Arancione", "Rosa" };
        public MainWindow()
        {
            InitializeComponent();
            rtbnSceltaPartenzaUtente1.IsChecked = false;
            rtbnSceltaPartenzaUtente2.IsChecked = false;
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
            btnIndietro.Visibility = Visibility.Visible;
            btnIndietro.IsEnabled = true;
        }
        //
        //CONTROLLO MODALITA
        //
        private void txtInserimentoUtente1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInserimentoUtente1.Text))
                ok1 = true;
            else
                ok1 = false;
        }
        private void txtInserimentoUtente2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInserimentoUtente2.Text))
                ok2 = true;
            else
                ok2 = false;
        }

        //
        //FINE CONTROLLO MODALITA
        //
        //
        //EVENTI
        //
        private void btnGriglia_Click(object sender, RoutedEventArgs e)
        {
            txtTurni.Text = "";
            string ipAddress = txtInserimentoIP.Text;
            int port = int.Parse(txtInserimentoPorta.Text);
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
            assegnaBottoniGriglia();
            SocketSend(IPAddress.Parse(ipAddress), port, InviaCampoDiGioco());
        }
        private void txtInserimentoIP_GotFocus(object sender, RoutedEventArgs e)
        {
            txtInserimentoIP.Text = string.Empty;
            txtInserimentoIP.FontSize = 31;
        }
        private void txtInserimentoPorta_GotFocus(object sender, RoutedEventArgs e)
        {
            txtInserimentoPorta.Text = string.Empty;
            txtInserimentoPorta.FontSize = 31;
        }
        private void txtInserimentoIPePorta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtInserimentoIP != null && txtInserimentoPorta != null && btnCreaSocket != null)
            {
                string ipAddress = txtInserimentoIP.Text;
                string port = txtInserimentoPorta.Text;
                int countDot = 0;
                for (int i = 0; i < ipAddress.Length; i++)
                {
                    if (ipAddress[i] == '.')
                        countDot++;
                }
                if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrEmpty(port) && int.TryParse(port, out int n) && countDot == 3)
                    btnCreaSocket.IsEnabled = true;
                else
                    btnCreaSocket.IsEnabled = false;
            }
        }
        private void btnCreaSocket_Click(object sender, RoutedEventArgs e)
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            IPEndPoint sourceSocket = new IPEndPoint(IPAddress.Parse(localIP), 56000);
            Thread receive = new Thread(new ParameterizedThreadStart(SocketReceive));
            receive.Start(sourceSocket);
            if (ok1 && ok2)
                btnOKMod1.IsEnabled = true;
            else
                btnOKMod1.IsEnabled = false;
        }
        //
        //BOTTONI FUNZIONALI
        //
        private void btnOKMod1_Click(object sender, RoutedEventArgs e)
        {
            utente1 = txtInserimentoUtente1.Text;
            utente2 = txtInserimentoUtente2.Text;
            turno = SceltaTurno();
            ScomparsaMod1();
            RendiVisibileGriglia();
        }
        private void btnIndietro_Click(object sender, RoutedEventArgs e)
        {
            txtInserimentoUtente1.Text = "Utente1";
            txtInserimentoUtente2.Text = "Utente2";
            txtInserimentoIP.Text = "Inserire IP dell'altro giocatore";
            txtInserimentoIP.FontSize = 17;
            txtInserimentoPorta.Text = "Inserire la porta dell'altro giocatore";
            txtInserimentoPorta.FontSize = 17;
            rtbnSceltaPartenzaUtente1.IsChecked = false;
            rtbnSceltaPartenzaUtente2.IsChecked = false;
            ScomparsaGriglia();
            SvuotaGriglia();
            ScomparsaMod1();
            AbilitaGriglia();
            txtTurni.Text = "";
            RendiVisibileMenu();
            cont = -1;
            pareggio = false;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SvuotaGriglia();
            AbilitaGriglia();
            cont = -1;
            pareggio = false;
            turno = SceltaTurno();
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
                if(bottoniGriglia[i] != null)
                    if (bottoniGriglia[i].Content == null)
                        messaggio += "null,";
                    else
                        messaggio += bottoniGriglia[i].Content + ",";
            }
            messaggio += turno;
            return messaggio;
        }
        private void assegnaBottoniGriglia()
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
            tbkRichiestaNickname.Visibility = Visibility.Visible;
            btnOKMod1.Visibility = Visibility.Visible;
            txtInserimentoUtente1.Visibility = Visibility.Visible;
            txtInserimentoIP.Visibility = Visibility.Visible;
            txtInserimentoPorta.Visibility = Visibility.Visible;
            btnCreaSocket.Visibility = Visibility.Visible;
            txtInserimentoUtente2.Visibility = Visibility.Visible;
            txtSceltaSegnoO.Visibility = Visibility.Visible;
            txtSceltaSegnoX.Visibility = Visibility.Visible;
            txtPartenza.Visibility = Visibility.Visible;
            rtbnSceltaPartenzaUtente1.Visibility = Visibility.Visible;
            rtbnSceltaPartenzaUtente2.Visibility = Visibility.Visible;
        }
        private void ScomparsaMod1()
        {
            btnCreaSocket.Visibility = Visibility.Hidden;
            txtInserimentoIP.Visibility = Visibility.Hidden;
            txtInserimentoPorta.Visibility = Visibility.Hidden;
            tbkRichiestaNickname.Visibility = Visibility.Hidden;
            btnOKMod1.Visibility = Visibility.Hidden;
            txtInserimentoUtente1.Visibility = Visibility.Hidden;
            txtInserimentoUtente2.Visibility = Visibility.Hidden;
            txtSceltaSegnoO.Visibility = Visibility.Hidden;
            txtSceltaSegnoX.Visibility = Visibility.Hidden;
            txtPartenza.Visibility = Visibility.Hidden;
            rtbnSceltaPartenzaUtente1.Visibility = Visibility.Hidden;
            rtbnSceltaPartenzaUtente2.Visibility = Visibility.Hidden;
        }
        private void AbilitaGriglia()
        {
            btn1mat.IsHitTestVisible = true;
            btn2mat.IsHitTestVisible = true;
            btn3mat.IsHitTestVisible = true;
            btn4mat.IsHitTestVisible = true;
            btn5mat.IsHitTestVisible = true;
            btn6mat.IsHitTestVisible = true;
            btn7mat.IsHitTestVisible = true;
            btn8mat.IsHitTestVisible = true;
            btn9mat.IsHitTestVisible = true;
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
            if (turno == 1)
            {
                txtTurni.Text = $"{utente2} è il tuo turno!";
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Red);
                ((Button)sender).Content = "X";
                turno = 2;
            }
            else
            {
                txtTurni.Text = $"{utente1} è il tuo turno!";
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Blue);
                ((Button)sender).Content = "O";
                turno = 1;
            }
            ((Button)sender).IsHitTestVisible = false;
        }
        private int SceltaTurno()
        {
            if (rtbnSceltaPartenzaUtente1.IsChecked == false && rtbnSceltaPartenzaUtente2.IsChecked == false)
            {
                turno = rnd.Next(1, 3);
                if (turno == 1)
                    txtTurni.Text = $"{utente1} sei stato sorteggiato per primo";
                else if (turno == 2)
                    txtTurni.Text = $"{utente2} sei stato sorteggiato per primo";
            }
            else
            {
                if (rtbnSceltaPartenzaUtente1.IsChecked == true)
                {
                    turno = 1;
                    txtTurni.Text = $"{utente1} il primo turno è il tuo";
                }
                else if (rtbnSceltaPartenzaUtente2.IsChecked == true)
                {
                    turno = 2;
                    txtTurni.Text = $"{utente2} il primo turno è il tuo";
                }
            }
            return turno;
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
        bool pareggio = false;
        private void ControlloPareggio()
        {
            if (btn1mat.Content != null && btn2mat.Content != null && btn3mat.Content != null && btn4mat.Content != null && btn5mat.Content != null && btn6mat.Content != null && btn7mat.Content != null && btn8mat.Content != null && btn9mat.Content != null)
            {
                txtTurni.Text = "PAREGGIO";
                pareggio = true;
            }
        }
        private void GiocateMod1(object sender)
        {
            if (TrovaTris((string)((Button)sender).Content))
            {
                if ((string)((Button)sender).Content == "X")
                    txtTurni.Text = $"{utente1} HAI VINTOOO";
                else
                    txtTurni.Text = $"{utente2} HAI VINTOOO";
            }
            else
                ControlloPareggio();
        }
        public async void SocketReceive(object socketsource)
        {
            IPEndPoint ipendp = (IPEndPoint)socketsource;
            Socket t = new Socket(ipendp.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            t.Bind(ipendp);
            Byte[] byteRicevuti = new Byte[256];
            string messaggio;
            int nBytes = 0;
            await Task.Run(() =>
            {
                while (true)
                {
                    if (t.Available > 0)
                    {
                        messaggio = string.Empty;
                        nBytes = t.Receive(byteRicevuti, byteRicevuti.Length, 0);
                        messaggio += Encoding.ASCII.GetString(byteRicevuti, 0, nBytes);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            AggiornaGriglia(messaggio);
                        }));

                    }
                }
            });
        }
        public void SocketSend(IPAddress destination, int destinationPort, string message)
        {
            Byte[] bytesended = Encoding.ASCII.GetBytes(message);
            Socket s = new Socket(destination.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint remote_endpoint = new IPEndPoint(destination, destinationPort);
            s.SendTo(bytesended, remote_endpoint);
        }
        public void AggiornaGriglia(string messaggio)
        {
            string[] griglia = messaggio.Split(',');
            for (int i = 0; i < bottoniGriglia.Length; i++)
            {
                if (griglia[i] != "null" || int.TryParse(griglia[i], out int n))
                    bottoniGriglia[i].Content = griglia[i];
            }
            turno = int.Parse(griglia[griglia.Length - 1]);
        }
    }
}
