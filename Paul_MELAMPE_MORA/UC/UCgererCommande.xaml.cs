using Paul_MELAMPE_MORA.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Paul_MELAMPE_MORA.UC
{
    public partial class UCgererCommande : UserControl
    {
        public ObservableCollection<Commande> LesCommandes { get; set; }

        public UCgererCommande()
        {
            InitializeComponent();
            LesCommandes = new ObservableCollection<Commande>();

            // INDISPENSABLE pour que la liste et les filtres fonctionnent
            this.DataContext = this;

            ChargerLesCommandes();
        }

        private void ChargerLesCommandes()
        {
            LesCommandes.Clear();
            Commande uneCommande = new Commande();
            var listeCommandes = uneCommande.FindAll();

            LigneCommande outilLigne = new LigneCommande();

            foreach (var elt in listeCommandes)
            {
                if (!elt.Est_recuperee)
                {
                    // 1. On va chercher les produits de la commande en BDD
                    var produitsDeLaCommande = outilLigne.FindBySelection(elt.Id);

                    // 2. On les met dans l'ObservableCollection de LA commande
                    elt.LignesCommande = new ObservableCollection<LigneCommande>(produitsDeLaCommande);

                    // 3. On ajoute la commande finale à l'écran
                    LesCommandes.Add(elt);
                }
            }
        }

        private void BtnFinaliser_Click(object sender, RoutedEventArgs e)
        {
            Button boutonClique = sender as Button;

            if (boutonClique != null && boutonClique.DataContext is Commande commandeSelectionnee)
            {
                try
                {
                    // On met à jour la commande en BDD
                    commandeSelectionnee.Est_prete = true;
                    commandeSelectionnee.Update();

                    MessageBox.Show("La commande a été marquée comme prête !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    // On recharge simplement la liste depuis la base de données
                    ChargerLesCommandes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la mise à jour en base de données : " + ex.Message, "Erreur BDD", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Button boutonClique = sender as Button;

            if (boutonClique != null && boutonClique.DataContext is Commande commandeChoisie)
            {
                UCcommande uneCommandeUC = new UCcommande();

                // On lui dit de se pré-remplir avec la commande cliquée
                uneCommandeUC.ChargerPourModification(commandeChoisie);

                Window popupCommande = new Window
                {
                    Title = "Modifier la commande de " + commandeChoisie.Client.Nom,
                    Content = uneCommandeUC,
                    Width = 1000,
                    Height = 750,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStyle = WindowStyle.ToolWindow,
                };

                popupCommande.ShowDialog();

                // On recharge la liste une fois le popup fermé pour voir les modifications
                ChargerLesCommandes();
            }
        }



        private bool FiltrerLesCommandes(object obj)
        {
            Commande laCommande = obj as Commande;
            if (laCommande == null) return false;

            bool matchTexteNomClient = RechercheMotClefNomClient(laCommande);
            bool matchTexteNomProduit = RechercheLigneProduit(laCommande);

            return matchTexteNomClient && matchTexteNomProduit;
        }

        private bool RechercheMotClefNomClient(Commande uneCommande)
        {
            if (string.IsNullOrEmpty(txtBoxNomClient.Text))
                return true;

            return uneCommande.Client.Nom.Contains(txtBoxNomClient.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool RechercheLigneProduit(Commande uneCommande)
        {
            if (string.IsNullOrEmpty(txtBoxNomProduit.Text))
                return true;

            LigneCommande lc = new LigneCommande();
            List<LigneCommande> listeLc = lc.FindBySelection(uneCommande.Id);

            foreach (LigneCommande ligne in listeLc)
            {
                if (ligne.LeProduit != null && ligne.LeProduit.Recette.Recette_nom.Contains(txtBoxNomProduit.Text, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void Filtre_ModifieCommande(object sender, RoutedEventArgs e)
        {
            if (icCommandes != null && icCommandes.ItemsSource != null)
            {
                var vue = CollectionViewSource.GetDefaultView(icCommandes.ItemsSource);
                vue.Filter = FiltrerLesCommandes;
                vue.Refresh();
            }
        }
    }
}