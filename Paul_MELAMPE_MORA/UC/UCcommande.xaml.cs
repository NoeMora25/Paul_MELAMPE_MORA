using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCcommande : UserControl
    {
        public ObservableCollection<LigneCommande> LePanier { get; set; }
        public Client LeClientAssocie { get; set; }

        public Commande CommandeChoisie { get; private set; }

        public UCcommande()
        {
            InitializeComponent();
            this.DataContext = this;
            LePanier = new ObservableCollection<LigneCommande>();
        }

        private void BtnMoinsUn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LigneCommande ligne = btn.DataContext as LigneCommande;

            if (ligne != null)
            {
                if (ligne.Quantite > 1)
                {
                    ligne.Quantite--;
                    int index = LePanier.IndexOf(ligne);
                    LePanier[index] = ligne;
                }
                else
                {
                    LePanier.Remove(ligne);
                }
                RafraichirAffichage();
            }
        }

        private void BtnPlusUn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LigneCommande ligne = btn.DataContext as LigneCommande;

            if (ligne != null)
            {
                ligne.Quantite++;
                int index = LePanier.IndexOf(ligne);
                LePanier[index] = ligne;
                RafraichirAffichage();
            }
        }

        private void BtnSupprimerLigne_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LigneCommande ligne = btn.DataContext as LigneCommande;

            if (ligne != null)
            {
                LePanier.Remove(ligne);
                RafraichirAffichage();
            }
        }

        private void BtnViderTout_Click(object sender, RoutedEventArgs e)
        {
            LePanier.Clear();
            RafraichirAffichage();
        }

        public void RafraichirAffichage()
        {
            if (icPanierFinal != null && icPanierFinal.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(icPanierFinal.ItemsSource).Refresh();
            }
            CalculerTotaux();
        }

        private void BtnRechercherClient_Click(object sender, RoutedEventArgs e)
        {
            UCrechercheClient ucRecherche = new UCrechercheClient();

            Window popup = new Window
            {
                Title = "Rechercher un client",
                Content = ucRecherche,
                Width = 800,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow
            };
            popup.ShowDialog();

            if (ucRecherche.ClientChoisi != null)
            {
                SelectionnerClient(ucRecherche.ClientChoisi);
            }
        }

        public void SelectionnerClient(Client clientSelectionne)
        {
            if (clientSelectionne != null)
            {
                LeClientAssocie = clientSelectionne;
                txtClientNomPrenom.Text = clientSelectionne.Nom + " " + clientSelectionne.Prenom;
                txtClientTel.Text = string.IsNullOrEmpty(clientSelectionne.Telephone) ? "Non renseigné" : clientSelectionne.Telephone;
                txtClientMail.Text = string.IsNullOrEmpty(clientSelectionne.Mail) ? "Non renseigné" : clientSelectionne.Mail;

                txtClientStatus.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            if (LeClientAssocie == null)
            {
                MessageBox.Show("Impossible de valider : Vous devez sélectionner un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (LePanier.Count == 0)
            {
                MessageBox.Show("Impossible de valider : Le panier est vide.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dateRetrait.SelectedDate == null)
            {
                MessageBox.Show("Impossible de valider : Veuillez choisir une date de retrait.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal totalCommande = 0;
            foreach (var ligne in LePanier)
            {
                totalCommande += ligne.LeProduit.Prix * ligne.Quantite;
            }
            decimal acompteCommande = totalCommande * 0.25m;

            int nbPersonnes = 1;
            int.TryParse(txtNbPersonnes.Text, out nbPersonnes);

            if (nbPersonnes <= 0)
            {
                MessageBox.Show("Le nombre de personnes doit être supérieur à 0. Veuillez corriger cette information.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Commande nouvelleCommande = new Commande();
            nouvelleCommande.Date_creation = DateOnly.FromDateTime(DateTime.Now);
            nouvelleCommande.Date_retrait = DateOnly.FromDateTime(dateRetrait.SelectedDate.Value);
            nouvelleCommande.Total = totalCommande;
            nouvelleCommande.Acompte = acompteCommande;
            nouvelleCommande.Est_prete = false;
            nouvelleCommande.Est_recuperee = false;
            nouvelleCommande.Date_evenement = DateOnly.FromDateTime(dateEvenement.SelectedDate ?? DateTime.MinValue);
            nouvelleCommande.Nb_personne = nbPersonnes;
            nouvelleCommande.Client = LeClientAssocie;

            try
            {
                int idCommandeGenere = nouvelleCommande.Create();

                if (idCommandeGenere <= 0)
                {
                    MessageBox.Show("Erreur lors de la création de la commande en base.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var ligne in LePanier)
                {
                    ligne.Commande_id = idCommandeGenere;
                    ligne.Create();
                }
                MessageBox.Show("Commande enregistrée avec succès !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow.MainContent.Content = new UCproduit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur critique avec la base de données : " + ex.Message, "Erreur SQL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRetourCatalogue_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainContent.Content = new UCproduit();
        }

        private void CalculerTotaux()
        {
            decimal total = 0;
            foreach (var ligne in LePanier)
            {
                if (ligne.LeProduit != null)
                {
                    total += ligne.LeProduit.Prix * ligne.Quantite;
                }
            }
            decimal acompte = total * 0.25m;
            decimal reste = total - acompte;

            lblTotal.Text = string.Format("{0:N2} €", total);
            lblAcompte.Text = string.Format("{0:N2} €", acompte);
            lblResteAPayer.Text = string.Format("{0:N2} €", reste);
        }
    }
}