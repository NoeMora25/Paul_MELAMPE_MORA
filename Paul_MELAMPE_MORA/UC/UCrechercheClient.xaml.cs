using Paul_MELAMPE_MORA.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCrechercheClient : UserControl
    {
        public ObservableCollection<Client> LesClients { get; set; }
        public Client ClientChoisi { get; private set; }

        public Visibility VisibiliteBouton { get; set; }
        public Visibility VisibiliteBoutonCommande { get; set; }

        public UCrechercheClient(bool estModeSelection = true)
        {
            InitializeComponent();

            if (estModeSelection == false)
            {
                VisibiliteBouton = Visibility.Collapsed;
                VisibiliteBoutonCommande = Visibility.Visible;
            }
            else
            {
                VisibiliteBouton = Visibility.Visible;
                VisibiliteBoutonCommande = Visibility.Collapsed;
            }

            this.DataContext = this;
            LesClients = new ObservableCollection<Client>();
            ChargerLesClients();
        }

        private void ChargerLesClients()
        {
            LesClients.Clear();
            Client monClient = new Client();
            var listeBDD = monClient.FindAll();
            foreach (var c in listeBDD)
            {
                LesClients.Add(c);
            }
        }

        private void Filtre_Modifie(object sender, TextChangedEventArgs e)
        {
            if (icClients != null && icClients.ItemsSource != null)
            {
                var vue = CollectionViewSource.GetDefaultView(icClients.ItemsSource);
                vue.Filter = FiltrerLesClients;
                vue.Refresh();
            }
        }

        private bool FiltrerLesClients(object obj)
        {
            Client leClient = obj as Client;
            if (leClient == null) return false;

            bool matchNom = string.IsNullOrEmpty(txtNom.Text) || leClient.Nom.Contains(txtNom.Text, StringComparison.OrdinalIgnoreCase);
            bool matchPrenom = string.IsNullOrEmpty(txtPrenom.Text) || leClient.Prenom.Contains(txtPrenom.Text, StringComparison.OrdinalIgnoreCase);
            bool matchTel = string.IsNullOrEmpty(txtTel.Text) || leClient.Telephone.Contains(txtTel.Text, StringComparison.OrdinalIgnoreCase);
            bool matchMail = string.IsNullOrEmpty(txtMail.Text) || leClient.Mail.Contains(txtMail.Text, StringComparison.OrdinalIgnoreCase);

            return matchNom && matchPrenom && matchTel && matchMail;
        }

        private void BtnSelectionner_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Client clientSelectionne = btn.DataContext as Client;

            if (clientSelectionne != null)
            {
                ClientChoisi = clientSelectionne;
                Window fenetrePopup = Window.GetWindow(this);

                if (fenetrePopup != null)
                {
                    fenetrePopup.Close();
                }
            }
        }

        private void BtnCreeClient_Click(object sender, RoutedEventArgs e)
        {
            FenetreCreeClient fenetre = new FenetreCreeClient();

            if (fenetre.ShowDialog() == true)
            {
                ChargerLesClients();
            }
        }

        private void BtnVoirCommandes_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Client clientChoisi = btn.DataContext as Client;

            if (clientChoisi != null)
            {
                Window nouvelletWindow = Window.GetWindow(this);
                if (nouvelletWindow is MainWindow mainWindow)
                {
                    mainWindow.MainContent.Content = new UChistorique(clientChoisi);
                }
            }
        }
    }
}