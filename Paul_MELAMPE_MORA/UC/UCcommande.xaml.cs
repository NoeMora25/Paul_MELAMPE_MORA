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

        public Commande CommandeAModifier { get; set; } = null;

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
            // 1. VÉRIFICATION DU CLIENT
            Client clientFinal = CommandeAModifier != null ? CommandeAModifier.Client : LeClientAssocie;
            if (LeClientAssocie != null) clientFinal = LeClientAssocie;

            if (clientFinal == null)
            {
                MessageBox.Show("Impossible de valider : Vous devez sélectionner un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2. VÉRIFICATION DU PANIER ET DE LA DATE
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

            // 3. CALCULS FINANCIERS
            decimal totalCommande = 0;
            foreach (var ligne in LePanier)
            {
                totalCommande += ligne.LeProduit.Prix * ligne.Quantite;
            }
            decimal acompteCommande = totalCommande * 0.25m;

            // 4. GESTION DES VALEURS OPTIONNELLES
            int? nbPersonnes = null;
            if (!string.IsNullOrWhiteSpace(txtNbPersonnes.Text))
            {
                if (int.TryParse(txtNbPersonnes.Text, out int parsedNb) && parsedNb > 0)
                {
                    nbPersonnes = parsedNb;
                }
                else
                {
                    MessageBox.Show("Le nombre de personnes doit être supérieur à 0.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            int categorieEvenement = 0;
            if (comboCategorieEvenement.SelectedIndex == 1)
            {
                categorieEvenement = 1; // Familial
            }
            else if (comboCategorieEvenement.SelectedIndex == 2)
            {
                categorieEvenement = 2; // Professionnel
            }

            if (dateEvenement.SelectedDate.HasValue && dateEvenement.SelectedDate.Value < DateTime.Now)
            {
                MessageBox.Show("La date de l'événement ne peut pas être dans le passé. Veuillez corriger cette information.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dateEvenement.SelectedDate.HasValue && dateEvenement.SelectedDate.Value < dateRetrait.SelectedDate.Value)
            {
                MessageBox.Show("La date de l'événement doit être postérieure à la date de retrait. Veuillez corriger cette information.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // DÉBUT DU BLOC TRY (qui manquait)
            try
            {
                // DÉBUT DU IF POUR LA CRÉATION (qui manquait)
                if (CommandeAModifier == null)
                {
                    Commande nouvelleCommande = new Commande();
                    nouvelleCommande.Date_creation = DateOnly.FromDateTime(DateTime.Now);
                    nouvelleCommande.Date_retrait = DateOnly.FromDateTime(dateRetrait.SelectedDate.Value);
                    nouvelleCommande.Total = totalCommande;
                    nouvelleCommande.Acompte = acompteCommande;
                    nouvelleCommande.Est_prete = false;
                    nouvelleCommande.Est_recuperee = false;
                    nouvelleCommande.Date_evenement = dateEvenement.SelectedDate.HasValue ? DateOnly.FromDateTime(dateEvenement.SelectedDate.Value) : null;
                    nouvelleCommande.Nb_personne = nbPersonnes;
                    nouvelleCommande.Client = clientFinal;
                    nouvelleCommande.Categorie_evenement = categorieEvenement > 0 ? new Categorie_evenement { Categorie_evenement_id = categorieEvenement } : null;

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

                    MessageBox.Show("Commande enregistrée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    // SÉCURITÉ DE NAVIGATION
                    Window parentWindow = Window.GetWindow(this);
                    if (parentWindow is MainWindow mainWindow)
                    {
                        mainWindow.MainContent.Content = new UCproduit();
                    }
                }
                else
                {
                    // MODE MODIFICATION
                    CommandeAModifier.Client = clientFinal;
                    CommandeAModifier.Date_retrait = DateOnly.FromDateTime(dateRetrait.SelectedDate.Value);
                    CommandeAModifier.Total = totalCommande;
                    CommandeAModifier.Acompte = acompteCommande;
                    CommandeAModifier.Date_evenement = dateEvenement.SelectedDate.HasValue ? DateOnly.FromDateTime(dateEvenement.SelectedDate.Value) : null;
                    CommandeAModifier.Nb_personne = nbPersonnes;
                    CommandeAModifier.Categorie_evenement = categorieEvenement > 0 ? new Categorie_evenement { Categorie_evenement_id = categorieEvenement } : null;

                    CommandeAModifier.Update();

                    MessageBox.Show("La commande a été mise à jour avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    // On ferme le pop-up
                    Window popup = Window.GetWindow(this);
                    if (popup != null)
                    {
                        popup.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur critique avec la base de données : " + ex.Message, "Erreur SQL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRetourCatalogue_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            // Si on est dans l'écran principal, on retourne au catalogue
            if (parentWindow is MainWindow mainWindow)
            {
                mainWindow.MainContent.Content = new UCproduit();
            }
            // Si on est dans la Pop-up de modification, ce bouton ferme la fenêtre !
            else if (parentWindow != null)
            {
                parentWindow.Close();
            }
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

        public void ChargerPourModification(Commande laCommande)
        {
            this.CommandeAModifier = laCommande;

            // CORRECTION DU TEXTE DU BOUTON ICI (utilise le x:Name que l'on vient de rajouter)
            txtBtnValider.Text = "METTRE À JOUR LA COMMANDE";

            txtClientStatus.Visibility = Visibility.Collapsed;

            txtClientNomPrenom.Text = laCommande.Client.Nom + " " + laCommande.Client.Prenom;
            txtClientTel.Text = string.IsNullOrEmpty(laCommande.Client.Telephone) ? "Non renseigné" : laCommande.Client.Telephone;
            txtClientMail.Text = string.IsNullOrEmpty(laCommande.Client.Mail) ? "Non renseigné" : laCommande.Client.Mail;

            dateRetrait.SelectedDate = laCommande.Date_retrait.ToDateTime(TimeOnly.MinValue);

            if (laCommande.Date_evenement != null)
            {
                dateEvenement.SelectedDate = laCommande.Date_evenement.Value.ToDateTime(TimeOnly.MinValue);
            }

            txtNbPersonnes.Text = laCommande.Nb_personne?.ToString() ?? "";

            if (laCommande.Categorie_evenement != null)
            {
                if (laCommande.Categorie_evenement.Categorie_evenement_nom == "Familial")
                    comboCategorieEvenement.SelectedIndex = 1;
                else if (laCommande.Categorie_evenement.Categorie_evenement_nom == "Professionnel")
                    comboCategorieEvenement.SelectedIndex = 2;
            }

            lblTotal.Text = laCommande.Total.ToString("0.00") + " €";
            lblAcompte.Text = laCommande.Acompte.ToString("0.00") + " €";
            lblResteAPayer.Text = (laCommande.Total - laCommande.Acompte).ToString("0.00") + " €";

            LePanier.Clear();

            LigneCommande outilRecherche = new LigneCommande();
            var lignesDeCetteCommande = outilRecherche.FindBySelection(laCommande.Id);

            foreach (var ligne in lignesDeCetteCommande)
            {
                LePanier.Add(ligne);
            }

            // Rafraîchir l'interface graphique et recalculer les totaux (25% acompte, etc.)
            RafraichirAffichage();
        }
    }
}