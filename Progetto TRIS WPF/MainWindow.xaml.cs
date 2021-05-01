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
        static int modalita;
        string utente1, utente2;
        bool ok1, ok2;
        Button[] bottoniGriglia = new Button[9];
        static int cont = -1;
        int turno = 0;
        Random rnd = new Random();
        string segnoUtenteMod2;
        int numeroSceltoBot;
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
            modalita = 1;
            ScomparsaMenu();
            RendiVisibileMod1();
            btnIndietro.Visibility = Visibility.Visible;
            btnIndietro.IsEnabled = true;
        }
        private void btnModalita2_Click(object sender, RoutedEventArgs e)
        {
            modalita = 2;
            ScomparsaMenu();
            btnIndietro.Visibility = Visibility.Visible;
            btnIndietro.IsEnabled = true;
            RendiVisibileMod2();
        }

        private void btnModalita3_Click(object sender, RoutedEventArgs e)
        {
            modalita = 3;
            ScomparsaMenu();
            btnIndietro.Visibility = Visibility.Visible;
            btnReset.IsEnabled = false;
            btnIndietro.IsEnabled = true;
            RendiVisibileMod1();
        }
        //
        //CONTROLLO MODALITA(non so perchè ho provato a fare il controllo con un metodo static void ma non funzionava)
        //
        private void txtInserimentoUtente1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (modalita == 1)
            {
                if (!string.IsNullOrWhiteSpace(txtInserimentoUtente1.Text))
                    ok1 = true;
                else
                    ok1 = false;
                if (ok1 && ok2)
                    btnOKMod1.IsEnabled = true;
                else
                    btnOKMod1.IsEnabled = false;
            }
        }
        private void txtInserimentoUtente2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (modalita == 1)
            {
                if (!string.IsNullOrWhiteSpace(txtInserimentoUtente2.Text))
                    ok2 = true;
                else
                    ok2 = false;
                if (ok1 && ok2)
                    btnOKMod1.IsEnabled = true;
                else
                    btnOKMod1.IsEnabled = false;
            }
        }
        private void rtbnSceltaXMod2_Checked(object sender, RoutedEventArgs e)
        {
            btnOKMod1.IsEnabled = true;
        }

        private void rtbnSceltaOMod2_Checked(object sender, RoutedEventArgs e)
        {
            btnOKMod1.IsEnabled = true;
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
            switch (modalita)
            {
                //
                //SCELTA MODALITA'
                //
                case 1:
                    {
                        //
                        //PRIMA MODALITA' BOTTONI
                        //
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
                        break;
                    }
                case 2:
                    {
                        //
                        //SECONDA MODALITA' BOTTONI
                        //
                        assegnaBottoniGriglia();
                        switch (((Button)sender).Name.Substring(3, 1))
                        {
                            case "1":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "2":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "3":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "4":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "5":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "6":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "7":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "8":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                            case "9":
                                {
                                    GiocateMod2(sender);
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        //
        //BOTTONI FUNZIONALI
        //
        private void btnOKMod1_Click(object sender, RoutedEventArgs e)
        {
            if (modalita == 1 || modalita == 3)
            {
                utente1 = txtInserimentoUtente1.Text;
                utente2 = txtInserimentoUtente2.Text;
                turno = SceltaTurno();
                ScomparsaMod1();
                RendiVisibileGriglia();
                if(modalita == 3)
                {
                    GiocateMod3();
                    btnIndietro.IsEnabled = false;
                }
            }
            else if (modalita == 2)
            {
                turno = SceltaTurno();
                ScomparsaMod2();
                RendiVisibileGriglia();
                if (rtbnSceltaOMod2.IsChecked == true)
                    segnoUtenteMod2 = "O";
                else if (rtbnSceltaXMod2.IsChecked == true)
                    segnoUtenteMod2 = "X";
            }

        }
        private void btnIndietro_Click(object sender, RoutedEventArgs e)
        {
            txtInserimentoUtente1.Text = "Utente1";
            txtInserimentoUtente2.Text = "Utente2";
            rtbnSceltaPartenzaUtente1.IsChecked = false;
            rtbnSceltaPartenzaUtente2.IsChecked = false;
            rtbnSceltaXMod2.IsChecked = false;
            rtbnSceltaOMod2.IsChecked = false;
            rtbnSceltaPartenzaUtenteMod2.IsChecked = false;
            rtbnSceltaPartenzaCPUMod2.IsChecked = false;
            ScomparsaGriglia();
            SvuotaGriglia();
            ScomparsaMod1();
            ScomparsaMod2();
            AbilitaGriglia();
            txtTurni.Text = "";
            RendiVisibileMenu();
            cont = -1;
            pareggio = false;
            if(modalita==3)
                btnIndietro.IsEnabled = false;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SvuotaGriglia();
            AbilitaGriglia();
            cont = -1;
            pareggio = false;
            if (modalita == 1 || modalita == 2)
            {
                turno = SceltaTurno();
            }
            else
            {
                turno = SceltaTurno();
                btnReset.IsEnabled = false;
                btnIndietro.IsEnabled = false;
                GiocateMod3();
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
        private void btnModalita1_MouseEnter(object sender, MouseEventArgs e)
        {
            tbkTestoModalita.Text = "La prima modalità è quella classica dove ci sono due utenti che si sfidano.";
        }
        private void btnModalita2_MouseEnter(object sender, MouseEventArgs e)
        {
            tbkTestoModalita.Text = "La seconda modalità è utente contro la CPU che è in grado di prevenire le tue mosse e gestire anche le proprie";
        }
        private void btnModalita3_MouseEnter(object sender, MouseEventArgs e)
        {
            tbkTestoModalita.Text = "La terza modalità è una modalità spettatore dove si possono vedere le partite di due CPU l'una contro l'altra";
        }
        private void btnModalita_MouseLeave(object sender, MouseEventArgs e)
        {
            tbkTestoModalita.Text = "Puntare il mouse sopra di un bottone delle tre modalità per scoprire come funzionano!";
        }
        //
        //METODI DI COMPARSA O SCOMPARSA E GESTIONI
        //
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
            btnModalita2.Visibility = Visibility.Hidden;
            btnModalita3.Visibility = Visibility.Hidden;
            tbkSceltaMod.Visibility = Visibility.Hidden;
            tbkTitolo.Visibility = Visibility.Hidden;
            tbkSceltaColoreGriglia.Visibility = Visibility.Hidden;
            cmbSceltaColoreGriglia.Visibility = Visibility.Hidden;
            tbkTestoModalita.Visibility = Visibility.Hidden;
            btnModalita1.IsEnabled = false;
            btnModalita2.IsEnabled = false;
            btnModalita3.IsEnabled = false;
        }
        private void RendiVisibileMenu()
        {
            btnModalita1.Visibility = Visibility.Visible;
            btnModalita2.Visibility = Visibility.Visible;
            btnModalita3.Visibility = Visibility.Visible;
            tbkSceltaMod.Visibility = Visibility.Visible;
            tbkTitolo.Visibility = Visibility.Visible;
            tbkSceltaColoreGriglia.Visibility = Visibility.Visible;
            cmbSceltaColoreGriglia.Visibility = Visibility.Visible;
            tbkTestoModalita.Visibility = Visibility.Visible;
            btnModalita1.IsEnabled = true;
            btnModalita2.IsEnabled = true;
            btnModalita3.IsEnabled = true;
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
            txtInserimentoUtente2.Visibility = Visibility.Visible;
            txtSceltaSegnoO.Visibility = Visibility.Visible;
            txtSceltaSegnoX.Visibility = Visibility.Visible;
            txtPartenza.Visibility = Visibility.Visible;
            rtbnSceltaPartenzaUtente1.Visibility = Visibility.Visible;
            rtbnSceltaPartenzaUtente2.Visibility = Visibility.Visible;
            btnOKMod1.IsEnabled = true;
        }
        private void RendiVisibileMod2()
        {
            btnOKMod1.Visibility = Visibility.Visible;
            txtSceltaSegnoO.Visibility = Visibility.Visible;
            txtSceltaSegnoX.Visibility = Visibility.Visible;
            txtPartenza.Visibility = Visibility.Visible;
            rtbnSceltaPartenzaUtenteMod2.Visibility = Visibility.Visible;
            rtbnSceltaPartenzaCPUMod2.Visibility = Visibility.Visible;
            rtbnSceltaXMod2.Visibility = Visibility.Visible;
            rtbnSceltaOMod2.Visibility = Visibility.Visible;
            tbkRichiestaSegnoMod2.Visibility = Visibility.Visible;
            btnOKMod1.IsEnabled = false;
        }
        private void ScomparsaMod1()
        {
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
        private void ScomparsaMod2()
        {
            btnOKMod1.Visibility = Visibility.Hidden;
            txtSceltaSegnoO.Visibility = Visibility.Hidden;
            txtSceltaSegnoX.Visibility = Visibility.Hidden;
            txtPartenza.Visibility = Visibility.Hidden;
            rtbnSceltaPartenzaUtenteMod2.Visibility = Visibility.Hidden;
            rtbnSceltaPartenzaCPUMod2.Visibility = Visibility.Hidden;
            rtbnSceltaXMod2.Visibility = Visibility.Hidden;
            rtbnSceltaOMod2.Visibility = Visibility.Hidden;
            tbkRichiestaSegnoMod2.Visibility = Visibility.Hidden;
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
            if (modalita == 1 || modalita == 3)
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
            }
            else if (modalita == 2)
            {
                assegnaBottoniGriglia();
                if (rtbnSceltaPartenzaUtenteMod2.IsChecked == false && rtbnSceltaPartenzaCPUMod2.IsChecked == false)
                {
                    turno = rnd.Next(1, 3);
                    if (turno == 1)
                        txtTurni.Text = $"Sei stato sorteggiato per primo";
                    if (turno == 2)
                    {
                        numeroSceltoBot = rnd.Next(9);
                        if (rtbnSceltaXMod2.IsChecked == true)
                            bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Blue);
                        else if (rtbnSceltaOMod2.IsChecked == true)
                            bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Red);
                        bottoniGriglia[numeroSceltoBot].Content = "CPU";
                        bottoniGriglia[numeroSceltoBot].IsHitTestVisible = false;
                        txtTurni.Text = $"La CPU è stata sorteggiata per prima per prima";
                    }
                }
                else
                {
                    if (rtbnSceltaPartenzaUtenteMod2.IsChecked == true)
                    {
                        turno = 1;
                        txtTurni.Text = $"Il primo turno è il tuo!";
                    }
                    else if (rtbnSceltaPartenzaCPUMod2.IsChecked == true)
                    {
                        numeroSceltoBot = rnd.Next(9);
                        if (rtbnSceltaXMod2.IsChecked == true)
                            bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Blue);
                        else if (rtbnSceltaOMod2.IsChecked == true)
                            bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Red);
                        bottoniGriglia[numeroSceltoBot].Content = "CPU";
                        bottoniGriglia[numeroSceltoBot].IsHitTestVisible = false;
                        txtTurni.Text = $"Il primo turno è della CPU";
                    }
                }
            }
            return turno;
        }
        private bool ControlloBottoniCPU(int n)
        {
            if (bottoniGriglia[n].Content == null)
                return true;
            else
                return false;
        }
        private bool TrovaTris(string segno)
        {
            if (((string)btn1mat.Content == segno && (string)btn2mat.Content == segno && (string)btn3mat.Content == segno) || ((string)btn4mat.Content == segno && (string)btn5mat.Content == segno && (string)btn6mat.Content == segno) || ((string)btn7mat.Content == segno && (string)btn8mat.Content == segno && (string)btn9mat.Content == segno))
            {
                if (modalita != 2 && modalita != 3)
                    DisabilitaGriglia();
                return true;
            }
            if (((string)btn1mat.Content == segno && (string)btn4mat.Content == segno && (string)btn7mat.Content == segno) || ((string)btn2mat.Content == segno && (string)btn5mat.Content == segno && (string)btn8mat.Content == segno) || ((string)btn3mat.Content == segno && (string)btn6mat.Content == segno && (string)btn9mat.Content == segno))
            {
                if (modalita != 2 && modalita != 3)
                    DisabilitaGriglia();
                return true;
            }
            if (((string)btn1mat.Content == segno && (string)btn5mat.Content == segno && (string)btn9mat.Content == segno) || ((string)btn3mat.Content == segno && (string)btn5mat.Content == segno && (string)btn7mat.Content == segno))
            {
                if (modalita != 2 && modalita != 3)
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
        private bool Controllo_e_TrovaTrisCPU(Button[] bottoniGriglia, Random CPURandom)
        {
            cont++;
            if (cont != 4)
            {
                if (!TrovaDoppie("CPU") && !TrovaDoppie(segnoUtenteMod2))
                {
                    do
                    {
                        numeroSceltoBot = CPURandom.Next(9);
                    } while (!ControlloBottoniCPU(numeroSceltoBot));
                    if (segnoUtenteMod2 == "X")
                        bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Blue);
                    else if (segnoUtenteMod2 == "O")
                        bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Red);
                    bottoniGriglia[numeroSceltoBot].Content = "CPU";
                    bottoniGriglia[numeroSceltoBot].IsHitTestVisible = false;
                }
                else
                {
                    if (TrovaTris("CPU"))
                    {
                        DisabilitaGriglia();
                        txtTurni.Text = $"LA CPU HA VINTOOO";
                        return true;
                    }
                }
            }
            return false;
        }
        private bool Controllo_e_TrovaTrisCPUSpettatore(Button[] bottoniGriglia, Random CPURandom, string cpu1, string cpu2)
        {
            txtTurni.Text = "";
            if (!TrovaDoppie(cpu1) && !TrovaDoppie(cpu2))
            {
                do
                {
                    numeroSceltoBot = CPURandom.Next(9);
                } while (!ControlloBottoniCPU(numeroSceltoBot));
                if (cpu1 == "X")
                    bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Red);
                else if (cpu1 == "O")
                    bottoniGriglia[numeroSceltoBot].Foreground = new SolidColorBrush(Colors.Blue);
                bottoniGriglia[numeroSceltoBot].Content = cpu1;
                bottoniGriglia[numeroSceltoBot].IsHitTestVisible = false;
            }
            else
            {
                if (TrovaTris(cpu1))
                {
                    if (cpu1 == "X")
                        txtTurni.Text = $"{txtInserimentoUtente1.Text} HA VINTO";
                    else
                        txtTurni.Text = $"{txtInserimentoUtente2.Text} HA VINTO";
                    DisabilitaGriglia();
                    return true;
                }
            }
            return false;
        }
        private void ModificaBottoneCPU(Button btngriglia)
        {
            if (modalita == 2)
            {
                if (segnoUtenteMod2 == "X")
                    btngriglia.Foreground = new SolidColorBrush(Colors.Blue);
                else if (segnoUtenteMod2 == "O")
                    btngriglia.Foreground = new SolidColorBrush(Colors.Red);
                btngriglia.IsHitTestVisible = false;
                btngriglia.Content = "CPU";
            }
            else if(modalita == 3)
            {
                if (turno == 1)
                {
                    btngriglia.Foreground = new SolidColorBrush(Colors.Red);
                    btngriglia.Content = "X";
                }
                else
                {
                    btngriglia.Foreground = new SolidColorBrush(Colors.Blue);
                    btngriglia.Content = "O";
                }
                btngriglia.IsHitTestVisible = false;
            }
        }
        private bool TrovaDoppie(string segno)
        {
            for (int i = 0; i < bottoniGriglia.Length; i++)
            {
                if (bottoniGriglia[i].IsHitTestVisible == true)
                {
                    bottoniGriglia[i].Content = segno;
                    if (TrovaTris(segno))
                    {
                        ModificaBottoneCPU(bottoniGriglia[i]);
                        return true;
                    }
                    else
                    {
                        bottoniGriglia[i].Content = null;
                    }
                }
            }
            return false;
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
        private void GiocateMod2(object sender)
        {
            if (segnoUtenteMod2 == "X")
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Red);
            else if (segnoUtenteMod2 == "O")
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Blue);
            ((Button)sender).Content = segnoUtenteMod2;
            ((Button)sender).IsHitTestVisible = false;
            if (TrovaTris((string)((Button)sender).Content))
            {
                DisabilitaGriglia();
                txtTurni.Text = $"HAI VINTOOO";
            }
            else
            {
                if (!Controllo_e_TrovaTrisCPU(bottoniGriglia, rnd))
                    ControlloPareggio();
            }
        }
        private async void GiocateMod3()
         {
            assegnaBottoniGriglia();
            while (!TrovaTris("X")&&!TrovaTris("O")||!pareggio)
            {
                ControlloPareggio();
                if (turno == 1)
                {
                    if (pareggio == true)
                    {
                        btnReset.IsEnabled = true;
                        btnIndietro.IsEnabled = true;
                        break;
                    }
                    await Task.Delay(1000);
                    Controllo_e_TrovaTrisCPUSpettatore(bottoniGriglia, rnd, "X", "O");
                    if (TrovaTris("X"))
                    {
                        btnReset.IsEnabled = true;
                        btnIndietro.IsEnabled = true;
                        break;
                    }
                    turno = 2;
                }
                else
                {
                    if (pareggio == true)
                    {
                        btnReset.IsEnabled = true;
                        btnIndietro.IsEnabled = true;
                        break;
                    }
                    await Task.Delay(1000);
                    Controllo_e_TrovaTrisCPUSpettatore(bottoniGriglia, rnd, "O", "X");
                    if (TrovaTris("O"))
                    {
                        btnReset.IsEnabled = true;
                        btnIndietro.IsEnabled = true;
                        break;
                    }
                    turno = 1;
                }
            }
        }
    }
}
