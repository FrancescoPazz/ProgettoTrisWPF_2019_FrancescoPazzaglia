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
        Button[] bottoniGriglia = new Button[9];
        bool pareggio = false;
        int turno = 0;
        int cont = -1;
        string segno = string.Empty;
        Random rnd = new Random();
        string[] coloriGriglia = { "Default", "Giallo", "Verde", "Arancione", "Rosa" };
        Socket connessione;
        bool connesso = false;
        bool fineConnessione = false;
        List<string> listaIndirizziIP = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            AssegnaBottoniGriglia();
            for (int i = 0; i < coloriGriglia.Length; i++)
            {
                cmbSceltaColoreGriglia.Items.Add(coloriGriglia[i]);
            }
            tbkTestoModalita.Text = "Gioco del TRIS con comunicazione tramite UDP!";
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
            txtInserimentoIP.IsEnabled = true;
            txtInserimentoPorta.IsEnabled = true;
        }
        private void btnGriglia_Click(object sender, RoutedEventArgs e)
        {
            txtTurni.Text = "";
            string ipAddress = txtInserimentoIP.Text;
            int port = int.Parse(txtInserimentoPorta.Text);
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
            SocketSend(IPAddress.Parse(ipAddress), port, InviaCampoDiGioco());
            DisabilitaGriglia();
        }
        private void txtInserimentoIP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtInserimentoIP.Text == "Inserire IP dell'altro giocatore")
            {
                txtInserimentoIP.Text = string.Empty;
                txtInserimentoIP.FontSize = 34;
            }
        }
        private void txtInserimentoPorta_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtInserimentoPorta.Text == "Inserire la porta dell'altro giocatore")
            {
                txtInserimentoPorta.Text = string.Empty;
                txtInserimentoPorta.FontSize = 34;
            }
        }
        bool ipGiaScelto = false;
        private void txtInserimentoIPePorta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtInserimentoIP != null && txtInserimentoPorta != null && btnCreaSocket != null)
            {
                for (int i = 0; i < listaIndirizziIP.Count; i++)
                {
                    if (txtInserimentoIP.Text == listaIndirizziIP[i])
                    {
                        tbkAttesa.Visibility = Visibility.Visible;
                        tbkAttesa.Text = "Indirizzo IP già utilizzato in precedenza";
                        ipGiaScelto = true;
                        break;
                    }
                    else
                    {
                        tbkAttesa.Visibility = Visibility.Hidden;
                        ipGiaScelto = false;
                    }
                }
                if (IPAddress.TryParse(txtInserimentoIP.Text, out IPAddress s) && int.TryParse(txtInserimentoPorta.Text, out int n) && !ipGiaScelto)
                    btnCreaSocket.IsEnabled = true;
                else
                    btnCreaSocket.IsEnabled = false;
            }
        }
        private void btnCreaSocket_Click(object sender, RoutedEventArgs e)
        {
            cont = 0;
            //metodo che prende indirizzo locale della macchina
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            //Inserisco l'indirizzo locale per fare automaticamente il collegamento.
            IPEndPoint sourceSocket = new IPEndPoint(IPAddress.Parse(localIP), 56000);
            Thread receive = new Thread(new ParameterizedThreadStart(SocketReceive));
            receive.Start(sourceSocket);
            SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RQCN");
            btnCreaSocket.IsEnabled = false;
            fineConnessione = false;
            cont = 0;
            txtInserimentoIP.IsEnabled = false;
            txtInserimentoPorta.IsEnabled = false;
            listaIndirizziIP.Add(txtInserimentoIP.Text);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult risultato = MessageBox.Show("Sicuro di voler chiudere la finestra?", "ATTENZIONE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (risultato == MessageBoxResult.Yes && connesso)
            {
                SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "TRMN");
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
            SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RQOK");
            tbkAttesa.Visibility = Visibility.Visible;
            tbkAttesa.Text = "In attesa dell'avversario. . .";
            btnOKMod1.IsEnabled = false;
        }
        private void btnIndietro_Click(object sender, RoutedEventArgs e)
        {
            string messaggio = "Sicuro di voler chiudere la connessione?\n\rSuccessivamente non potrai più connetterti allo stesso indirizzo IP";
            if (cont == -1)
                messaggio = "Sicuro di voler tornare al menu?";
            if (MessageBox.Show(messaggio, "ATTENZIONE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                fineConnessione = true;
                if (IPAddress.TryParse(txtInserimentoIP.Text, out IPAddress s) && int.TryParse(txtInserimentoPorta.Text, out int n))
                {
                    SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "TRMN");
                    //connessione.Shutdown(SocketShutdown.Both);
                    //connessione.Close();
                }
                txtInserimentoIP.Text = "Inserire IP dell'altro giocatore";
                txtInserimentoIP.FontSize = 17;
                txtInserimentoPorta.Text = "Inserire la porta dell'altro giocatore";
                txtInserimentoPorta.FontSize = 17;
                ScomparsaGriglia();
                SvuotaGriglia();
                ScomparsaMod1();
                AbilitaGriglia();
                txtTurni.Text = "";
                RendiVisibileMenu();
                pareggio = false;
                btnCreaSocket.IsEnabled = false;
                btnOKMod1.IsEnabled = false;
                tbkAttesa.Text = string.Empty;
                cont = -1;
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Riniziare la partita?", "AVVISO", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SvuotaGriglia();
                AbilitaGriglia();
                SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RST");
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
            tbkRichiestaNickname.Visibility = Visibility.Visible;
            btnOKMod1.Visibility = Visibility.Visible;
            txtInserimentoIP.Visibility = Visibility.Visible;
            txtInserimentoPorta.Visibility = Visibility.Visible;
            btnCreaSocket.Visibility = Visibility.Visible;
        }
        private void ScomparsaMod1()
        {
            btnCreaSocket.Visibility = Visibility.Hidden;
            txtInserimentoIP.Visibility = Visibility.Hidden;
            txtInserimentoPorta.Visibility = Visibility.Hidden;
            tbkRichiestaNickname.Visibility = Visibility.Hidden;
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
        int contBind = 0;
        public async void SocketReceive(object socketsource)
        {
            IPEndPoint ipendp = (IPEndPoint)socketsource;
            if(contBind == 0)
            {
                connessione = new Socket(ipendp.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                connessione.Bind(ipendp);
                contBind++;
            }
            Byte[] byteRicevuti = new Byte[256];
            string messaggio;
            int nBytes = 0;
            await Task.Run(() =>
            {
                while (!fineConnessione)
                {
                    if (connessione.Available > 0)
                    {
                        messaggio = string.Empty;
                        nBytes = connessione.Receive(byteRicevuti, byteRicevuti.Length, 0);
                        messaggio += Encoding.ASCII.GetString(byteRicevuti, 0, nBytes);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            AggiornaGrigliaOrName(messaggio);
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
                        SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RQCN");
                    if (cont == 1)
                    {
                        turno = SceltaTurno();
                        SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), turno.ToString()+"RQCN");
                    }
                    if (cont == 2)
                    {
                        segno = SceltaSegno();
                        SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), segno.ToString()+"RQCN");
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
                        SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RQCN");
                    }
                }
                if (cont == 4)
                {
                    btnOKMod1.IsEnabled = true;
                    btnCreaSocket.IsEnabled = false;
                    SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RQCN");
                }
                cont++;
            }
            else if (messaggio == "TRMN")
            {
                fineConnessione = true;
                if (IPAddress.TryParse(txtInserimentoIP.Text, out IPAddress s) && int.TryParse(txtInserimentoPorta.Text, out int n))
                {
                    SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "TRMN");
                    //connessione.Shutdown(SocketShutdown.Both);
                    //connessione.Close();
                }
                txtInserimentoIP.Text = "Inserire IP dell'altro giocatore";
                txtInserimentoIP.FontSize = 17;
                txtInserimentoPorta.Text = "Inserire la porta dell'altro giocatore";
                txtInserimentoPorta.FontSize = 17;
                ScomparsaGriglia();
                SvuotaGriglia();
                ScomparsaMod1();
                AbilitaGriglia();
                txtTurni.Text = "";
                RendiVisibileMenu();
                pareggio = false;
                btnCreaSocket.IsEnabled = false;
                btnOKMod1.IsEnabled = false;
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
                    SocketSend(IPAddress.Parse(txtInserimentoIP.Text), int.Parse(txtInserimentoPorta.Text), "RQOK");
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