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
            // Si on modifie, le client est celui de la commande. Si on crée, c'est LeClientAssocie.
            Client clientFinal = CommandeAModifier != null ? CommandeAModifier.Client : LeClientAssocie;

            // Si l'utilisateur a recherché un nouveau client pendant la modification, on le prend
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

            // 4. GESTION DES VALEURS OPTIONNELLES (Nullables)
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
                    categorieEvenement = 2; // Professionel
            }


            Commande nouvelleCommande = new Commande();
            nouvelleCommande.Date_creation = DateOnly.FromDateTime(DateTime.Now);
            nouvelleCommande.Date_retrait = DateOnly.FromDateTime(dateRetrait.SelectedDate.Value);
            nouvelleCommande.Total = totalCommande;
            nouvelleCommande.Acompte = acompteCommande;
            nouvelleCommande.Est_prete = false;
            nouvelleCommande.Est_recuperee = false;
            nouvelleCommande.Date_evenement = dateEvenement.SelectedDate.HasValue ? DateOnly.FromDateTime(dateEvenement.SelectedDate.Value) : null;
            nouvelleCommande.Nb_personne = nbPersonnes;
            nouvelleCommande.Client = LeClientAssocie;
            nouvelleCommande.Categorie_evenement = categorieEvenement > 0 ? new Categorie_evenement { Categorie_evenement_id = categorieEvenement } : null; 

                    int idCommandeGenere = nouvelleCommande.Create();

                    if (idCommandeGenere <= 0)
                    {
                        MessageBox.Show("Erreur lors de la création de la commande en base.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Sauvegarde des produits (Lignes_Commande)
                    foreach (var ligne in LePanier)
                    {
                        ligne.Commande_id = idCommandeGenere;
                        ligne.Create();
                    }

                    MessageBox.Show("Commande enregistrée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    // On retourne à l'écran produit
                    MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                    mainWindow.MainContent.Content = new UCproduit();
                }
                else
                {
                    // ==================================================
                    // MODE : MODIFICATION D'UNE COMMANDE EXISTANTE
                    // ==================================================
                    CommandeAModifier.Client = clientFinal;
                    CommandeAModifier.Date_retrait = DateOnly.FromDateTime(dateRetrait.SelectedDate.Value);
                    CommandeAModifier.Total = totalCommande;
                    CommandeAModifier.Acompte = acompteCommande;
                    CommandeAModifier.Date_evenement = dateEvt;
                    CommandeAModifier.Nb_personne = nbPersonnes;
                    CommandeAModifier.Categorie_evenement = categorieFinal;

                    // Mise à jour de la table Commande
                    CommandeAModifier.Update();

                    // NOTE POUR LES PRODUITS : 
                    // Si vous autorisez la modification des produits du panier, il faudra coder une méthode
                    // dans LigneCommande pour supprimer les anciennes lignes de cette commande
                    // et faire un "ligne.Create()" pour insérer le nouveau panier.

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


        public void ChargerPourModification(Commande laCommande)
        {
            this.CommandeAModifier = laCommande;

            BtnValiderCommande.Content = "Mettre à jour la commande";
            txtClientStatus.Visibility = Visibility.Collapsed; 

            txtClientNomPrenom.Text = laCommande.Client.Nom + " " + laCommande.Client.Prenom;
            txtClientTel.Text = string.IsNullOrEmpty(laCommande.Client.Telephone) ? "Non renseigné" : laCommande.Client.Telephone;
            txtClientMail.Text = string.IsNullOrEmpty(laCommande.Client.Mail) ? "Non renseigné" : laCommande.Client.Mail;

            // (WPF utilise DateTime, donc on convertit les DateOnly en DateTime)
            dateRetrait.SelectedDate = laCommande.Date_retrait.ToDateTime(TimeOnly.MinValue);

            if (laCommande.Date_evenement != null)
            {
                dateEvenement.SelectedDate = laCommande.Date_evenement.Value.ToDateTime(TimeOnly.MinValue);
            }

            // --- On remplit les informations de l'évènement ---
            txtNbPersonnes.Text = laCommande.Nb_personne?.ToString() ?? "";

            if (laCommande.Categorie_evenement != null)
            {
                if (laCommande.Categorie_evenement.Categorie_evenement_nom == "Familial")
                    comboCategorieEvenement.SelectedIndex = 1;
                else if (laCommande.Categorie_evenement.Categorie_evenement_nom == "Professionnel")
                    comboCategorieEvenement.SelectedIndex = 2;
            }

            // --- On remplit les totaux financiers ---
            lblTotal.Text = laCommande.Total.ToString("0.00") + " €";
            lblAcompte.Text = laCommande.Acompte.ToString("0.00") + " €";
            lblResteAPayer.Text = (laCommande.Total - laCommande.Acompte).ToString("0.00") + " €";


            // IMPORTANT : Ici, vous devrez également coder la logique pour remplir 
            // votre variable 'LePanier' avec les Lignes_Commande de cette commande 
            // pour que les produits s'affichent à gauche !
        }
    }
}